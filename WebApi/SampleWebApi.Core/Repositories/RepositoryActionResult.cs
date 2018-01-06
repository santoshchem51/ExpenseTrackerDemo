using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWebApi.Core.Enums;
using static SampleWebApi.Core.Enums.ExpenseTrackerEnums;

namespace SampleWebApi.Core.Repositories
{
    public class RepositoryActionResult<T> : Interfaces.IRepositoryActionResult<T> where T : class
    {
        public T ModelDto { get; set; }

        public ExpenseTrackerEnums.eRepositoryActionStatus ActionStatus { get; set; }

        public Exception Exception { get; set; }

        public RepositoryActionResult(T entity, eRepositoryActionStatus status)
        {
            ModelDto = entity;
            ActionStatus = status;
        }

        public RepositoryActionResult(T entity, eRepositoryActionStatus status, Exception exception) : this(entity, status)
        {
            Exception = exception;
        }
    }
}
