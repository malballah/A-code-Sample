using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck.Core;


namespace Truck.Data
{
    public class TruckDbContext : DbContext
    {
        public TruckDbContext() : base("TruckDbContext")
        {
            Database.SetInitializer<TruckDbContext>(null);
        }
        
        #region Entity Sets
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Adjustment> Adjustments { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AssignmentStatus> AssignmentStatuses { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatuses { get; set; }
        public DbSet<InvoiceAttachment> InvoiceAttachments { get; set; }
        public DbSet<AdjustmentStatus> AdjustmentStatuses { get; set; }
        #endregion
        public async Task<List<int>> SearchInvoices(int? invoiceNumber=null, int? woNumber=null,int? assignmentNumber=null,int? customerId=null , string invoiceCustomerName="", int? statusId=null,int? period1=null,int? period2=null,DateTime? dateFrom=null, DateTime? dateTo=null)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(invoiceNumber.HasValue
                ? new SqlParameter("Number", invoiceNumber)
                : new SqlParameter("Number", DBNull.Value));
            parameters.Add(woNumber.HasValue
               ? new SqlParameter("WONumber", woNumber)
               : new SqlParameter("WONumber", DBNull.Value));
            parameters.Add(assignmentNumber.HasValue
              ? new SqlParameter("AssignmentNumber", assignmentNumber)
              : new SqlParameter("AssignmentNumber", DBNull.Value));
            parameters.Add(!string.IsNullOrEmpty(invoiceCustomerName)
                ? new SqlParameter("InvoiceCustomerName", invoiceCustomerName)
                : new SqlParameter("InvoiceCustomerName", DBNull.Value));
            parameters.Add(statusId.HasValue
                  ? new SqlParameter("StatusId", statusId)
                  : new SqlParameter("StatusId", DBNull.Value));
            parameters.Add(customerId.HasValue
                 ? new SqlParameter("CustomerId", customerId)
                 : new SqlParameter("CustomerId", DBNull.Value));
            parameters.Add(period1.HasValue
                     ? new SqlParameter("Period1", period1)
                     : new SqlParameter("Period1", DBNull.Value));
            parameters.Add(period2.HasValue
                   ? new SqlParameter("Period2", period2)
                   : new SqlParameter("Period2", DBNull.Value));
            parameters.Add(dateFrom.HasValue
                    ? new SqlParameter("DateFrom", dateFrom)
                    : new SqlParameter("DateFrom", DBNull.Value));
            parameters.Add(dateTo.HasValue
                   ? new SqlParameter("DateTo", dateTo)
                   : new SqlParameter("DateTo", DBNull.Value));
            return await Database.SqlQuery<int>("exec [SP_SearchInvoices] @Number,@WONumber,@AssignmentNumber,@CustomerId,@InvoiceCustomerName,@StatusId,@Period1,@Period2,@DateFrom,@DateTo", parameters.ToArray()).ToListAsync();

        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjustment>()
     .HasRequired(c => c.AppliedAssignment)
     .WithMany()
     .WillCascadeOnDelete(false);

            modelBuilder.Entity<Adjustment>()
     .HasRequired(c => c.OriginInvoice)
     .WithMany()
     .WillCascadeOnDelete(false);
            modelBuilder.Entity<Assignment>().Property(x => x.Fee).HasPrecision(18, 2);
            modelBuilder.Entity<Assignment>().Property(x => x.Fuel).HasPrecision(18, 2);
            modelBuilder.Entity<Assignment>().Property(x => x.Funded).HasPrecision(18, 2);
            modelBuilder.Entity<Assignment>().Property(x => x.Total).HasPrecision(18, 2);
            modelBuilder.Entity<Assignment>().Property(x => x.TotalPayable).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(x => x.Amount).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(x => x.CheckAmount).HasPrecision(18, 2);
            modelBuilder.Entity<Invoice>().Property(x => x.PaidAmount).HasPrecision(18, 2);
        }

    }
}
