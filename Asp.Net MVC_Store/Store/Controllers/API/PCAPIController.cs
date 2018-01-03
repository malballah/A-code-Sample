using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Store.Core;
using Store.Data;
using Store.Data.Infrastructure;
using Store.Services;
using Store.ViewModels;
using Store.Web.ViewModels;
using System.Linq.Dynamic;

namespace Store.Controllers
{
    [Authorize]
    [RoutePrefix("api/PCAPI")]
    public class PCAPIController : ApiController
    {
        #region fields
        private readonly IEntityService<CPU> _cpuService;
        private readonly IEntityService<Memory> _memoryService;
        private readonly IEntityService<Motherboard> _motherboardService;
        private readonly IEntityService<PowerSupply> _powerSupplyService;
        private readonly IEntityService<CPUSocket> _cpuSocketService;
        private readonly IEntityService<PC> _pcService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion
        #region contructor
        public PCAPIController(IUnitOfWork unitOfWork,IEntityService<PC> pcService, IEntityService<CPU> cpuService,
            IEntityService<Memory> memoryService,
            IEntityService<Motherboard> motherboardService
            , IEntityService<PowerSupply> powerSupplyService, IEntityService<CPUSocket> cpuSocketService, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._pcService = pcService;
            this._cpuService = cpuService;
            this._cpuSocketService = cpuSocketService;
            this._memoryService = memoryService;
            this._motherboardService = motherboardService;
            this._powerSupplyService = powerSupplyService;
            this._mapper = mapper;
        }
        #endregion
        #region Methods

        // GET: api/PcData
        [HttpPost]
        [Route("AllPCs")]
        public async Task<IHttpActionResult> AllPCs(BootGridRequest bootRequest)
        {
            //get all pcs with it's properties
            var pcs = _pcService.AllIncluding(item => item.CPU, item => item.Motherboard, item => item.PCMemories,
                item => item.PowerSupply, item => item.PCMemories.Select(e => e.Memory))
                .Select(item => new PCBootGridModel
                {
                    Id = item.Id,
                    CustomerName = item.CustomerName,
                    Motherboard = item.Motherboard.Name,
                    MemoryAmount = item.PCMemories.Sum(pcm => pcm.Memory.Size*pcm.Count), //get sum of memory size
                    PowerSupply = item.PowerSupply.Name,
                    AssemblyNeeded = item.AssemblyNeeded,
                    TotalPrice = item.TotalPrice
                });
               
            //setup bootgrid model
            var model = new BootGridResponse<PCBootGridModel>(bootRequest,pcs);
          
            return Ok(model);
        }

        // GET: api/PcData
        [Route("GetAllowedCPUs/{motherboardId}")]
        public async Task<IHttpActionResult> GetAllowedCPUs(int motherboardId)
        {
            var motherboard = _motherboardService.Find(motherboardId);
            var cpus =
                _cpuService.FindBy(item => item.CPUSocketId == motherboard.CPUSocketId)
                    .ToList()
                    .Select(item => _mapper.Map<MotherboardViewModel>(item));
            return Ok(cpus);
        }

        // GET: api/PcData
        [Route("GetAllowedPowerSuppliers/{motherboardId}/{cpuId}/{memories}")]
        public async Task<IHttpActionResult> GetAllowedPowerSuppliers(int motherboardId, int cpuId, string memories)
        {
            var motherboard = _motherboardService.Find(motherboardId);
            var cpu = _cpuService.Find(cpuId);
            var memoryIds = memories.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            var selectedMemories = _memoryService.FindBy(item => memoryIds.Contains(item.Id.ToString()));
            int totalPCPowerCons = cpu.PowerConsumption + motherboard.PowerConsumption +
                                   selectedMemories.Sum(item => item.PowerConsumption);
            var allowedPowerSuppliers =
                _powerSupplyService.FindBy(item => item.MaxPowerOutput >= totalPCPowerCons + (.1*totalPCPowerCons))
                    .ToList()
                    .Select(item => _mapper.Map<PowerSupplyViewModel>(item));
            return Ok(allowedPowerSuppliers);
        }
        [HttpPost]
        [Route("DeletePC/{pcId}")]
        public async Task<IHttpActionResult> DeletePC(int pcId)
        {
            try
            {
                _pcService.Delete(pcId);
                _unitOfWork.SaveChanges();
                return Ok();
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        #endregion
    }
}