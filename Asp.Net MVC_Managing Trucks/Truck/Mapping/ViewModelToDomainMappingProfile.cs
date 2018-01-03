using AutoMapper;
using Truck.Core;
using Truck.Models;
using Truck.ViewModels;


namespace SR.Web.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName => "ViewModelToDomainMappings";

        protected override void Configure()
        {
            CreateMap<UserViewModel,ApplicationUser >().ForMember(dest=>dest.UserName,opt=>opt.MapFrom(item=>item.Email));
            CreateMap<CustomerViewModel, Customer>();
            CreateMap<InvoiceViewModel, Invoice>().ForMember(dest=>dest.Id,opt=>opt.Ignore());
            CreateMap<AssignmentViewModel,Assignment>().ForMember(dest=>dest.Invoices,opt=>opt.Ignore());
            CreateMap<AttachmentViewModel,Attachment>();
        }
    }
}