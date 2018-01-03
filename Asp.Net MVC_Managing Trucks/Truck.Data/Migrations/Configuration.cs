using Truck.Core;

namespace Truck.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Truck.Data.TruckDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Truck.Data.TruckDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //update AssignmentStatus set [Status] = 'Funded' where Id = 3
            //update AssignmentStatus set[Status] = 'Closed' where Id = 4

            context.AssignmentStatuses.AddOrUpdate(
              p => p.Id,
              new AssignmentStatus {Id=1, Status = "New" },
              new AssignmentStatus {Id=2, Status = "Returned" },
              new AssignmentStatus {Id = 3, Status = "Submitted" },
              new AssignmentStatus {Id=4, Status = "Funded" },
              new AssignmentStatus { Id = 5, Status = "Closed" }
            );
            context.InvoiceStatuses.AddOrUpdate(
              p => p.Id,
              new InvoiceStatus { Id = 1, Status = "Not Paid" },
              new InvoiceStatus { Id = 2, Status = "Paid" },
              new InvoiceStatus { Id = 3, Status = "On Hold" },
              new InvoiceStatus { Id = 4, Status = "Originals Required" },
              new InvoiceStatus { Id = 5, Status = "Chargeback" }
            );
            context.AdjustmentStatuses.AddOrUpdate(
             p => p.Id,
             new AdjustmentStatus { Id = 1, Status = "Outstanding" },
             new AdjustmentStatus { Id = 2, Status = "Closed" }
           );
        }
    }
}
