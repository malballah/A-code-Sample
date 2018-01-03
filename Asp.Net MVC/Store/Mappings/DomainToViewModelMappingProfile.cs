using AutoMapper;
using Store.Core;
using Store.Web.ViewModels;

namespace TourGuide.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        protected override void Configure()
        {
            CreateMap<CPU,CPUViewModel>();
            CreateMap<Motherboard, MotherboardViewModel>();
            CreateMap<CPUSocket, CPUSocketViewModel>();
            CreateMap<Memory, MemoryViewModel>();
            CreateMap<PowerSupply, PowerSupplyViewModel>();
            CreateMap<PC, PCViewModel>();
            CreateMap<PCMemory, PCMemoryViewModel>();
        }
    }
}