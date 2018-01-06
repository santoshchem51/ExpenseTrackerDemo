using SampleWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Interfaces
{
    public  interface  IExpenseTrackerUnitOfWork
    {
        Boolean DeleteExpense(int id);
        Boolean DeleteExpenseGroup(int id);
        ExpenseDto GetExpense(int id, int? expenseGroupId = null);
        ExpenseGroupDto GetExpenseGroup(int id);
        ExpenseGroupDto GetExpenseGroup(int id, string userId);
        IList<ExpenseGroupDto> GetExpenseGroups();
        IList<ExpenseGroupDto> GetExpenseGroups(string userId);
        ExpenseGroupStatusDto GetExpenseGroupStatus(int id);
        IList<ExpenseGroupStatusDto> GetExpenseGroupStatusses();
        IList<ExpenseGroupDto> GetExpenseGroupsWithExpenses();
        ExpenseGroupDto GetExpenseGroupWithExpenses(int id);
        ExpenseGroupDto GetExpenseGroupWithExpenses(int id, string userId);
        IList<ExpenseDto> GetExpenses();
        IList<ExpenseDto> GetExpenses(int expenseGroupId);

        ExpenseDto InsertExpense(ExpenseDto e);
        ExpenseGroupDto InsertExpenseGroup(ExpenseGroupDto eg);
        ExpenseDto UpdateExpense(ExpenseDto e);
        ExpenseGroupDto UpdateExpenseGroup(ExpenseGroupDto eg);

        bool SaveChanges();

    }
}
