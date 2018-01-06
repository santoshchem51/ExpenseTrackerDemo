using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Models
{
    public class ExpenseGroupDto
    {

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int ExpenseGroupStatusId { get; set; }

        public ICollection<ExpenseDto> Expenses { get; set; }
    }
}
