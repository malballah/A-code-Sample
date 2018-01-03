using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Truck.Core;
using Truck.Data.Repositories;


namespace Truck.Services
{
    //implementation of the IDbService
    public interface IAssignmentService : IDbService<Assignment>
    {
        void Recaculate(Assignment assignment);
    }
}