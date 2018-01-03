using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Truck.Core;
using Truck.Data.Infrastructure;
using Truck.Data.Repositories;


namespace Truck.Services
{
    //implementation of the IDbService
    public class AssignmentService : DbService<Assignment>,IAssignmentService
    {
        private readonly IDbService<Adjustment> _adjustmentService;
        private readonly IUnitOfWork _uow;
        public AssignmentService(IRepository<Assignment> repository,IDbService<Adjustment> adjustmentService,IUnitOfWork uow) : base(repository)
        {
            _adjustmentService = adjustmentService;
            _uow=uow;
    }

        public void Recaculate(Assignment assignment)
        {
            assignment.Total = assignment.Invoices.Sum(item => item.Amount);
            assignment.TotalPayable = assignment.Invoices.Sum(item => item.PaidAmount);
            assignment.Funded = assignment.Total - ( assignment.Total  * assignment.Customer.Rate/100) - assignment.Fuel;
            if(assignment.Funded < 0)
                assignment.Funded = 0;
            assignment.Fee = assignment.Total*assignment.Customer.Rate/100;
            //remove applied adjustments
            var adjustments = _adjustmentService.FindBy(item=>item.AppliedAssignmentId==assignment.Id).ToList();
            var autoAdjustmentsIds = from adjustment in adjustments
                from autoAdjustment in _adjustmentService.All
                                     where autoAdjustment.OriginAdjustmentId == adjustment.Id
                                     select autoAdjustment.Id;
            _adjustmentService.Delete(autoAdjustmentsIds.ToArray());
            
            //reapply adjustments
            int i = 0;
            for (; i < adjustments.Count; i++)
            {
                assignment.Funded = assignment.Funded + adjustments[i].Amount;
                if (assignment.Funded < 0)
                {
                   _adjustmentService.Insert(new Adjustment
                    {
                        Amount = assignment.Funded,
                        AppliedAssignmentId = assignment.Id,
                        CustomerId=assignment.CustomerId,
                        OriginAdjustmentId= adjustments[i].Id,
                        OriginInvoiceId= adjustments[i].OriginInvoiceId,
                        StatusId=1
                    });
                    assignment.Funded = 0;
                }
                if(assignment.Funded==0)
                    break;
            }
            //remove the rest of adjustments
            for (int j = i; i < adjustments.Count; i++)
            {
                _adjustmentService.Delete(adjustments[j]);
            }
        }
        
    }
}