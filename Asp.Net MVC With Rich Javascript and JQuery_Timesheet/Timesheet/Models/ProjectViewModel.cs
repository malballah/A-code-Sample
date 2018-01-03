
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Timesheet.Infrastructure;

namespace Timesheet.Models
{
    public class ProjectViewModel
    {
        private Project _project;
        #region Constructor

        public ProjectViewModel() : this(new Project())
        {
            
        }
        public ProjectViewModel(Project project)
        {
            _project = project;
        }
        #endregion Constructor

        #region Properties

        public string Code => _project.Code;
        public string Description => _project.Description;

        public bool CanRemove { get; set; }

        #endregion Properties


    }
}