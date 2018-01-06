using SampleWebApi.Core.Entities;
using SampleWebApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.Helpers
{
    public static class ExpenseTrackerHelper
    {
        public static ExpenseDto CreateExpenseFromEntity(Expense expense)
        {
            return new ExpenseDto()
            {
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                ExpenseGroupId = expense.ExpenseGroupId,
                Id = expense.Id
            };
        }



        public static Expense CreateExpenseFromDto(ExpenseDto expense)
        {
            return new Expense()
            {
                Amount = expense.Amount,
                Date = expense.Date,
                Description = expense.Description,
                ExpenseGroupId = expense.ExpenseGroupId,
                Id = expense.Id
            };
        }

        public static ExpenseGroup CreateExpenseGroupFromDto(ExpenseGroupDto expenseGroup)
        {
            return new ExpenseGroup()
            {
                Description = expenseGroup.Description,
                ExpenseGroupStatusId = expenseGroup.ExpenseGroupStatusId,
                Id = expenseGroup.Id,
                Title = expenseGroup.Title,
                UserId = expenseGroup.UserId,
                Expenses = expenseGroup.Expenses == null ? new List<Expense>() : expenseGroup.Expenses.Select(e => CreateExpenseFromDto(e)).ToList()
            };
        }


        public static ExpenseGroupDto CreateExpenseGroupFromEntity(ExpenseGroup expenseGroup)
        {
            return new ExpenseGroupDto()
            {
                Description = expenseGroup.Description,
                ExpenseGroupStatusId = expenseGroup.ExpenseGroupStatusId,
                Id = expenseGroup.Id,
                Title = expenseGroup.Title,
                UserId = expenseGroup.UserId,
                Expenses = expenseGroup.Expenses.Select(e => CreateExpenseFromEntity(e)).ToList()
            };
        }

        public static ExpenseGroupStatus CreateExpenseGroupStatusFromDto(ExpenseGroupStatusDto expenseGroupStatus)
        {
            return new ExpenseGroupStatus()
            {
                Description = expenseGroupStatus.Description,
                Id = expenseGroupStatus.Id
            };
        }


        public static ExpenseGroupStatusDto CreateExpenseGroupStatusFromEntity(ExpenseGroupStatus expenseGroupStatus)
        {
            return new ExpenseGroupStatusDto()
            {
                Description = expenseGroupStatus.Description,
                Id = expenseGroupStatus.Id
            };
        }
    }
}
