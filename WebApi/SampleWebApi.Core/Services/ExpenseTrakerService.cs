using SampleWebApi.Core.Interfaces;
using SampleWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Services
{
    public class ExpenseTrakerService : IExpenseTrackerService
    {
        private IExpenseTrackerUnitOfWork _etUnitOfWork;
        public ExpenseTrakerService()
        {

        }
        public bool DeleteExpense(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteExpenseGroup(int id)
        {
            throw new NotImplementedException();
        }

        public ExpenseDto GetExpense(int id, int? expenseGroupId = null)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupDto GetExpenseGroup(int id)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupDto GetExpenseGroup(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public IList<ExpenseGroupDto> GetExpenseGroups()
        {
            throw new NotImplementedException();
        }

        public IList<ExpenseGroupDto> GetExpenseGroups(string userId)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupStatusDto GetExpenseGroupStatus(int id)
        {
            throw new NotImplementedException();
        }

        public IList<ExpenseGroupStatusDto> GetExpenseGroupStatusses()
        {
            throw new NotImplementedException();
        }

        public IList<ExpenseGroupDto> GetExpenseGroupsWithExpenses()
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupDto GetExpenseGroupWithExpenses(int id)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupDto GetExpenseGroupWithExpenses(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public IList<ExpenseDto> GetExpenses()
        {
            throw new NotImplementedException();
        }

        public IList<ExpenseDto> GetExpenses(int expenseGroupId)
        {
            throw new NotImplementedException();
        }

        public ExpenseDto InsertExpense(ExpenseDto e)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupDto InsertExpenseGroup(ExpenseGroupDto eg)
        {
            throw new NotImplementedException();
        }

        public ExpenseDto UpdateExpense(ExpenseDto e)
        {
            throw new NotImplementedException();
        }

        public ExpenseGroupDto UpdateExpenseGroup(ExpenseGroupDto eg)
        {
            throw new NotImplementedException();
        }
    }
}
