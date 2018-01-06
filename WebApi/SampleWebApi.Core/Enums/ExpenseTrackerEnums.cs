using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Enums
{
    public class ExpenseTrackerEnums
    {
        public enum eRepositoryActionStatus
        {
            Ok,
            Added,
            Updated,
            NotFound,
            Removed,
            NothingModified,
            Error
        }

        public enum eServiceActionResult
        {

        }
    }
}
