using AutoMapper;
using Store.Core;
using Store.Web.ViewModels;


namespace Store.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            CreateMap<CPUViewModel,CPU>();
            CreateMap<MotherboardViewModel,Motherboard>();
            CreateMap<CPUSocketViewModel,CPUSocket>();
            CreateMap<MemoryViewModel,Memory>();
            CreateMap<PowerSupplyViewModel,PowerSupply>();
            CreateMap<PCMemoryViewModel,PC>();
            CreateMap<PCMemoryViewModel,PCMemory>();
            CreateMap<PCViewModel, PC>().ForMember(item=>item.CPU,option=>option.Ignore())
                .ForMember(item => item.Motherboard, option => option.Ignore())
                .ForMember(item => item.PCMemories, option => option.Ignore())
                .ForMember(item => item.PowerSupply, option => option.Ignore())
                .ForMember(item => item.TotalPrice, option => option.Ignore());
        }
    }
}