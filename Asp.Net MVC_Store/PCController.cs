using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Store.Core;
using Store.Data;
using Store.Data.Infrastructure;
using Store.Services;
using Store.Web.ViewModels;

namespace Store.Controllers.MVC
{
    [Authorize]
    public class PCController : Controller
    {
        #region fields
        private readonly IEntityService<CPU> _cpuService;
        private readonly IEntityService<Memory> _memoryService;
        private readonly IEntityService<Motherboard> _motherboardService;
        private readonly IEntityService<PowerSupply> _powerSupplyService;
        private readonly IEntityService<CPUSocket> _cpuSocketService;
        private readonly IEntityService<PCMemory> _pcMemoryService;
        private readonly IEntityService<PC> _pcService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        #endregion
        #region contructor
        public PCController(IUnitOfWork unitOfWork,IEntityService<PC> pcService, IEntityService<CPU> cpuService, IEntityService<Memory> memoryService,
            IEntityService<Motherboard> motherboardService
            , IEntityService<PowerSupply> powerSupplyService, IEntityService<CPUSocket> cpuSocketService,IEntityService<PCMemory> pcMemoryService, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._pcService = pcService;
            this._cpuService = cpuService;
            this._cpuSocketService = cpuSocketService;
            this._memoryService = memoryService;
            this._motherboardService = motherboardService;
            this._powerSupplyService = powerSupplyService;
            this._pcMemoryService = pcMemoryService;
            this._mapper = mapper;
        }
        #endregion
        #region Methods
        // GET: PC
        public async Task<ActionResult> Index()
        {
            return View();
        }
        
        // GET: PC/Create
        public ActionResult Create()
        {
            //setup new model
            var model = new PCViewModel
            {
                SelectedMemories = _memoryService.All.ToList().Select(item => new MemorySelectViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    PowerConsumption=item.PowerConsumption,
                    Price=item.Price
                })
            };
            //load memories,motherboards and cpus
            LoadLists();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerName,CPUId,MotherboardId,PowerSupplyId,AssemblyNeeded,SelectedMemories")] PCViewModel model)
        {
            //get selected parts
            var motherboard = _motherboardService.Find(model.MotherboardId);
            var cpu = _cpuService.Find(model.CPUId);
            var powerSupply = _powerSupplyService.Find(model.PowerSupplyId);
            var selectedMemories = model.SelectedMemories.Where(item => item.IsSelected);

            if (ModelState.IsValid)
            {
                //validate pc configuration
                var message = ValidatePCConfiguration(motherboard, cpu, powerSupply, selectedMemories);
                if (!string.IsNullOrEmpty(message))
                {
                    //not cofigured well
                    ViewBag.ErrorMessage =message;
                    LoadLists();
                    return View(model);
                }
                //setup a new pc
                var newPC = _mapper.Map<PC>(model);
                //compute the price
                newPC.TotalPrice = cpu.Price + motherboard.Price + powerSupply.Price + selectedMemories.Sum(item => item.Price * item.Count) + (model.AssemblyNeeded ? 50 : 0);
                _pcService.Insert(newPC);

                //get selected memories
                foreach (var item in model.SelectedMemories.Where(item=>item.IsSelected))
                {
                    var pcMemory = new PCMemory
                    {
                        MemoryId = item.Id,
                        PC = newPC,
                        Count=item.Count
                    };
                    _pcMemoryService.Insert(pcMemory);
                }
                //save the changes
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            //if error load lists again
            LoadLists();
            return View(model);
        }
        //validate the pc configuration
        private string ValidatePCConfiguration(Motherboard motherboard, CPU cpu, PowerSupply powerSupply, IEnumerable<MemorySelectViewModel> memories)
        {
            if (memories.Sum(item => item.Count) == 0)
                return "You have to select at least one memory type.";
           if (cpu.CPUSocketId != motherboard.CPUSocketId)
                return "The CPU must has the same socket as the motherboard.";
           if (memories.Sum(item=>item.Count) > motherboard.MemorySlots)
                return "Motherboard contains memory slots less than the number of memories you have selected";
           //compute total power consumption
            var totalPowerCons = cpu.PowerConsumption + motherboard.PowerConsumption +
                                 memories.Sum(item => item.PowerConsumption*item.Count);
            //the max output power of the power supply should exceed the total consumprion power by 10%
            if (powerSupply.MaxPowerOutput < (totalPowerCons + (.1 * totalPowerCons)))
            {
                var message = "The power output of the power supply you have selected is not enough for this PC";
                //get lowest power supply enough for this configuation
                var suggestedPowerSupply= _powerSupplyService.All.OrderBy(item => item.MaxPowerOutput).FirstOrDefault(item => item.MaxPowerOutput >= (totalPowerCons + (.1 * totalPowerCons)));
                if (suggestedPowerSupply != null)
                {
                    message += Environment.NewLine + "\nSuggested power supply is \"" + suggestedPowerSupply.Name + "\"";
                }
                return message;
            }
            return string.Empty;
        }
        //load lists
        private void LoadLists(PCViewModel model=null)
        {
            ViewBag.MotherboardId = new SelectList(_motherboardService.All, "Id", "Name",model?.MotherboardId);
            ViewBag.CPUId = new SelectList(_cpuService.All, "Id", "Name",model?.CPUId);
            ViewBag.PowerSupplyId = new SelectList(_powerSupplyService.All, "Id", "Name",model?.PowerSupplyId);
        }
        
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = _pcService.Find(id.Value);
            if (entity == null)
            {
                return HttpNotFound();
            }
            var model = _mapper.Map<PCViewModel>(entity);
            //build selected memory view model
            model.SelectedMemories = _memoryService.All.ToList().Select(item =>
            {
                var memoryItem = new MemorySelectViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        PowerConsumption = item.PowerConsumption,
                        IsSelected = entity.PCMemories.Any(e => e.MemoryId == item.Id)
                    };
                //if item selected before get it's count
                var existMemoryItem = entity.PCMemories.FirstOrDefault(e => e.PCId == entity.Id && e.MemoryId == item.Id);
                if (existMemoryItem != null)
                    memoryItem.Count = existMemoryItem.Count;
                return memoryItem;
            });
            //load lists if error
            LoadLists(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerName,CPUId,MotherboardId,PowerSupplyId,AssemblyNeeded,SelectedMemories")] PCViewModel model)
        {
            var motherboard = _motherboardService.Find(model.MotherboardId);
            var cpu = _cpuService.Find(model.CPUId);
            var powerSupply = _powerSupplyService.Find(model.PowerSupplyId);
            var selectedMemories = model.SelectedMemories.Where(item => item.IsSelected);

            if (ModelState.IsValid)
            {
                var message = ValidatePCConfiguration(motherboard, cpu, powerSupply, selectedMemories);
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.ErrorMessage = message;
                    LoadLists();
                    return View(model);
                }
                //begin a transaction
                _unitOfWork.BeginTransaction();
                //setup the pc
                var oldPC = _pcService.Find(model.Id);
                oldPC.CustomerName = model.CustomerName;
                oldPC.CPUId = model.CPUId;
                oldPC.MotherboardId = model.MotherboardId;
                oldPC.PowerSupplyId = model.PowerSupplyId;
                oldPC.AssemblyNeeded = model.AssemblyNeeded;
                oldPC.TotalPrice = cpu.Price + motherboard.Price + powerSupply.Price + selectedMemories.Sum(item => item.Price*item.Count) + (model.AssemblyNeeded ? 50 : 0);
                //delete old selected memories
                for (int i = 0; i < oldPC.PCMemories.Count; i++)
                {
                    _pcMemoryService.Delete(oldPC.PCMemories.ElementAt(i));
                    i--;
                }
                _unitOfWork.SaveChanges();
                //add new selected memories
                foreach (var item in selectedMemories)
                {
                    var pcMemory = new PCMemory
                    {
                        MemoryId = item.Id,
                        PC = oldPC,
                        Count = item.Count
                    };
                    _pcMemoryService.Insert(pcMemory);
                }
                //save and commit all changes
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            LoadLists();
            return View(model);
        }
        #endregion

    }
}
