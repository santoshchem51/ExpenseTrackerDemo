using SampleWebApi.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Interfaces
{
    public interface IRepositoryActionResult <T> where T: class
    {
       T ModelDto { get; set; }
       ExpenseTrackerEnums.eRepositoryActionStatus  ActionStatus { get; set; }
       Exception Exception { get; set; }

    }
}
