
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Timesheet.Models
{
   
        public class TimesheetDbContext : DbContext
        {
            #region Constructor 

            public TimesheetDbContext() : base("DbConnection")
            {
                Database.SetInitializer<TimesheetDbContext>(null);
            }

        #endregion Constructor 

        #region Methods 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

               // modelBuilder.Entity<ApplicationUser>().ToTable("User");

                modelBuilder.Entity<Employee>().ToTable("Employee");

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.EmployeeCurrentProjects);
                    //.WithOne(ec => ec.Employee)
                    //.OnDelete(DeleteBehavior.Restrict)
                    //.HasPrincipalKey(e => e.Code);


            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Assignments);
                    //.WithOne(a => a.Employee)
                   // .OnDelete(DeleteBehavior.Restrict)
                   // .HasPrincipalKey(e => e.Code);


                modelBuilder.Entity<Project>().ToTable("Project");

            modelBuilder.Entity<Project>()
                .HasMany(p => p.ProjectTrackers);
                    //.WithOne(pt => pt.Project)
                    //.OnDelete(DeleteBehavior.Restrict)
                    //.HasPrincipalKey(p => p.Code);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.EmployeeCurrentProjects);
                    //.WithOne(ec => ec.Project)
                    //.OnDelete(DeleteBehavior.Restrict)
                    //.HasPrincipalKey(p => p.Code);

                modelBuilder.Entity<Tracker>().ToTable("Tracker");

            modelBuilder.Entity<Tracker>()
                .HasMany(t => t.ProjectTrackers);
                    //.WithOne(pt => pt.Tracker)
                    //.OnDelete(DeleteBehavior.Restrict)
                    //.HasPrincipalKey(t => t.Code);

                modelBuilder.Entity<ProjectTracker>().ToTable("Project_Tracker");

                //modelBuilder.Entity<ProjectTracker>()
                //    .HasOne(p => p.Project)
                //    .WithMany(pt => pt.ProjectTrackers)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(pt => pt.Project_Code);

                //modelBuilder.Entity<ProjectTracker>()
                //    .HasOne(t => t.Tracker)
                //    .WithMany(pt => pt.ProjectTrackers)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(pt => pt.Tracker_Code);

                modelBuilder.Entity<ProjectTracker>()
                    .HasMany(a => a.Assignments);
                    //.WithOne(pt => pt.ProjectTracker)
                    //.OnDelete(DeleteBehavior.Restrict)
                    //.HasPrincipalKey(pt => pt.Id);

                modelBuilder.Entity<ProjectTracker>()
                    .HasKey(pt => new {pt.Id});

                //modelBuilder.Entity<ProjectTracker>()
                //    .HasAlternateKey(pt => new {pt.Project_Code, pt.Tracker_Code});

                //modelBuilder.Entity<ProjectTracker>().Property(pt => pt.Id).ValueGeneratedOnAdd();

                modelBuilder.Entity<EmployeeCurrentProject>().ToTable("Employee_Current_Project");

                //modelBuilder.Entity<EmployeeCurrentProject>()
                //    .HasOne(e => e.Employee)
                //    .WithMany(ec => ec.EmployeeCurrentProjects)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(ec => ec.Employee_Code);

                //modelBuilder.Entity<EmployeeCurrentProject>()
                //    .HasOne(p => p.Project)
                //    .WithMany(ec => ec.EmployeeCurrentProjects)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(ec => ec.Project_Code);

                modelBuilder.Entity<EmployeeCurrentProject>()
                    .HasKey(ec => new {ec.Employee_Code, ec.Project_Code});


                modelBuilder.Entity<YYYYMMLocked>().ToTable("YYYYMM_Locked");


                modelBuilder.Entity<YYYYMM>().ToTable("YYYYMM");

            modelBuilder.Entity<YYYYMM>()
                .HasMany(wd => wd.WorkDates);
                    //.WithOne(ym => ym.YYYYMM)
                    //.OnDelete(DeleteBehavior.Restrict)
                    //.HasPrincipalKey(ym => ym.Code);



                modelBuilder.Entity<Assignment>().ToTable("Assignment");

                //modelBuilder.Entity<Assignment>()
                //    .HasOne(e => e.Employee)
                //    .WithMany(a => a.Assignments)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(tc => tc.Employee_Code);

                //modelBuilder.Entity<Assignment>()
                //    .HasOne(pt => pt.ProjectTracker)
                //    .WithMany(a => a.Assignments)
                //    .OnDelete(DeleteBehavior.Restrict)
                 //   .HasForeignKey(a => a.Project_Tracker_Id);

                modelBuilder.Entity<Assignment>()
                    .HasKey(a => new {a.Id});

                //modelBuilder.Entity<Assignment>()
                //    .HasAlternateKey(a => new {a.Employee_Code, a.Project_Tracker_Id});

                //modelBuilder.Entity<Assignment>().Property(a => a.Id).ValueGeneratedOnAdd();

                modelBuilder.Entity<WorkDate>().ToTable("Work_Date");

                //modelBuilder.Entity<WorkDate>()
                //    .HasOne(ym => ym.YYYYMM)
                //    .WithMany(wd => wd.WorkDates)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(wd => wd.YYYYMM_Code);


                modelBuilder.Entity<WorkDate>()
                    .HasKey(wd => new {wd.Id_Date});

                //modelBuilder.Entity<WorkDate>()
                //    .HasAlternateKey(wd => new {wd.YYYYMM_Code, wd.Day_of_Month});


                modelBuilder.Entity<TimeCard>().ToTable("Time_Card");

                //modelBuilder.Entity<TimeCard>()
                //    .HasOne(a => a.Assignment)
                //    .WithMany(tc => tc.TimeCards)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(a => a.Assignment_Id);

                //modelBuilder.Entity<TimeCard>()
                //    .HasOne(wd => wd.WorkDate)
                //    .WithMany(tc => tc.TimeCards)
                //    .OnDelete(DeleteBehavior.Restrict)
                //    .HasForeignKey(wd => wd.Work_Date_Id);

                modelBuilder.Entity<TimeCard>()
                    .HasKey(tc => new {tc.Assignment_Id, tc.Work_Date_Id});

            }



            #endregion Methods

            #region Properties

            //public DbSet<ApplicationUser> Users { get; set; }

            public DbSet<Project> Projects { get; set; }

            public DbSet<Tracker> Trackers { get; set; }

            public DbSet<ProjectTracker> ProjectTrackers { get; set; }

            public DbSet<Employee> Employees { get; set; }

            public DbSet<EmployeeCurrentProject> EmployeeCurrentProjects { get; set; }

            public DbSet<YYYYMM> YYYYMMs { get; set; }

            public DbSet<YYYYMMLocked> YYYYMMLockeds { get; set; }


            public DbSet<WorkDate> WorkDates { get; set; }

            public DbSet<Assignment> Assignments { get; set; }

            public DbSet<TimeCard> TimeCards { get; set; }


            #endregion Properties



        }
   
}