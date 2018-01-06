using SampleWebApi.Core.Entities;
using SampleWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Interfaces
{
    public interface IExpenseTrackerRespository
    {
        IRepositoryActionResult<Expense> DeleteExpense(int id);
        IRepositoryActionResult<ExpenseGroup> DeleteExpenseGroup(int id);
        Expense GetExpense(int id, int? expenseGroupId = null);
        ExpenseGroup GetExpenseGroup(int id);
        ExpenseGroup GetExpenseGroup(int id, string userId);
        IList<ExpenseGroup> GetExpenseGroups();
        IList<ExpenseGroup> GetExpenseGroups(string userId);
        ExpenseGroupStatus GetExpenseGroupStatus(int id);
        IList<ExpenseGroupStatus> GetExpenseGroupStatusses();
        IList<ExpenseGroup> GetExpenseGroupsWithExpenses();
        ExpenseGroup GetExpenseGroupWithExpenses(int id);
        ExpenseGroup GetExpenseGroupWithExpenses(int id, string userId);
        IList<Expense> GetExpenses();
        IList<Expense> GetExpenses(int expenseGroupId);

        IRepositoryActionResult<Expense> InsertExpense(Entities.Expense e);
        IRepositoryActionResult<ExpenseGroup> InsertExpenseGroup(Entities.ExpenseGroup eg);
        IRepositoryActionResult<Expense> UpdateExpense(Entities.Expense e);
        IRepositoryActionResult<ExpenseGroup> UpdateExpenseGroup(Entities.ExpenseGroup eg);
    }
}
