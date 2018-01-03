using AutoMapper;
using Truck.Core;
using Truck.Models;
using Truck.ViewModels;

namespace Truck.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName => "DomainToViewModelMappings";

        protected override void Configure()
        {
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(des=>des.CustomerName,opt=>opt.MapFrom(src=>src.Customer.CustomerName));
            CreateMap<Customer, CustomerViewModel>();

            CreateMap<Assignment, AssignmentViewModel>()
                .ForMember(item=>item.CustomerName,opt=>opt.MapFrom(src=>src.Customer.CustomerName))
                .ForMember(item => item.Status, opt => opt.MapFrom(src => src.AssignmentStatus.Status))
            .ForMember(item => item.IsFunded, opt => opt.MapFrom(src => src.StatusId>=4));
            CreateMap<Assignment, ReportAssignmentViewModel>()
               .ForMember(item => item.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
               .ForMember(item => item.Status, opt => opt.MapFrom(src => src.AssignmentStatus.Status));
            CreateMap<Adjustment, AdjustmentViewModel>() 
                .ForMember(item => item.AssignmentNumber, opt => opt.MapFrom(src => src.OriginInvoice.Assignment.Number));
            CreateMap<Invoice, InvoiceViewModel>()
                .ForMember(item => item.AssignmentStatusId,
                    opt => opt.MapFrom(src => src.Assignment.AssignmentStatus.Id))
                .ForMember(item => item.Status,
                    opt => opt.MapFrom(src => src.InvoiceStatus.Status))
                .ForMember(item => item.AssignmentNumber, opt => opt.MapFrom(src => src.Assignment.Number));
            CreateMap<Attachment, AttachmentViewModel>();
            CreateMap<Assignment, Assignment>()
                .ForMember(item => item.Id, opt => opt.Ignore())
                .ForMember(item => item.Customer, opt => opt.Ignore())
                .ForMember(item => item.Invoices, opt => opt.Ignore())
                .ForMember(item => item.AssignmentStatus, opt => opt.Ignore());
        }
    }
}