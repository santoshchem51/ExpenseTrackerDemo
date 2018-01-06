using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWebApi.Core.Helpers;
using SampleWebApi.Core.Interfaces;
using SampleWebApi.Core.Models;
using SampleWebApi.Core.Repositories;

namespace SampleWebApi.Core.UnitOfWork
{
    public class ExpenseTrackerUnitOfWork : UnitOfWorkBase, IExpenseTrackerUnitOfWork
    {
        private IExpenseTrackerRespository _expenseTrackerRespository;
        
        public ExpenseTrackerUnitOfWork(ExpenseTrackerContext dataContext) : base(dataContext)
        {
            _expenseTrackerRespository = new ExpenseTrackerRepository(dataContext);   
        }

        public bool DeleteExpense(int id)
        {
           var repResult =
                _expenseTrackerRespository.DeleteExpense(id);
            return repResult.ActionStatus == Enums.ExpenseTrackerEnums.eRepositoryActionStatus.Removed;
        }

        public bool DeleteExpenseGroup(int id)
        {
            var repResult =
                _expenseTrackerRespository.DeleteExpenseGroup(id);
            return repResult.ActionStatus == Enums.ExpenseTrackerEnums.eRepositoryActionStatus.Removed;
        }

        public ExpenseDto GetExpense(int id, int? expenseGroupId = null)
        {
            var repResult = _expenseTrackerRespository.GetExpense(id, expenseGroupId);
            return ExpenseTrackerHelper.CreateExpenseFromEntity(repResult);
        }

        public ExpenseGroupDto GetExpenseGroup(int id)
        {
           var repResult = _expenseTrackerRespository.GetExpenseGroup(id);
            return ExpenseTrackerHelper.CreateExpenseGroupFromEntity(repResult);
        }

        public ExpenseGroupDto GetExpenseGroup(int id, string userId)
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroup(id, userId);
            return ExpenseTrackerHelper.CreateExpenseGroupFromEntity(repResult);
        }

        public IList<ExpenseGroupDto> GetExpenseGroups()
        {
            return _expenseTrackerRespository.GetExpenseGroups().Select(ExpenseTrackerHelper.CreateExpenseGroupFromEntity).ToList();
        }

        public IList<ExpenseGroupDto> GetExpenseGroups(string userId)
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroups(userId);
            if(repResult!= null && repResult.Any())
            {
                return repResult.Select(ExpenseTrackerHelper.CreateExpenseGroupFromEntity).ToList();
            }
            return null;
        }

        public ExpenseGroupStatusDto GetExpenseGroupStatus(int id)
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroupStatus(id);
            return ExpenseTrackerHelper.CreateExpenseGroupStatusFromEntity(repResult);
            
        }

        public IList<ExpenseGroupStatusDto> GetExpenseGroupStatusses()
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroupStatusses();
            if (repResult != null && repResult.Any())
            {
                return repResult.Select(ExpenseTrackerHelper.CreateExpenseGroupStatusFromEntity).ToList();
            }
            return null;
        }

        public IList<ExpenseGroupDto> GetExpenseGroupsWithExpenses()
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroupsWithExpenses();
            if (repResult != null && repResult.Any())
            {
                return repResult.Select(ExpenseTrackerHelper.CreateExpenseGroupFromEntity).ToList();
            }
            return null;
        }

        public ExpenseGroupDto GetExpenseGroupWithExpenses(int id)
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroupWithExpenses(id);
            return ExpenseTrackerHelper.CreateExpenseGroupFromEntity(repResult);
        }

        public ExpenseGroupDto GetExpenseGroupWithExpenses(int id, string userId)
        {
            var repResult = _expenseTrackerRespository.GetExpenseGroupWithExpenses(id,userId);
            return ExpenseTrackerHelper.CreateExpenseGroupFromEntity(repResult);
        }

        public IList<ExpenseDto> GetExpenses()
        {
            var repResult = _expenseTrackerRespository.GetExpenses();
            if (repResult != null && repResult.Any())
            {
                return repResult.Select(ExpenseTrackerHelper.CreateExpenseFromEntity).ToList();
            }
            return null;
        }

        public IList<ExpenseDto> GetExpenses(int expenseGroupId)
        {
            var repResult = _expenseTrackerRespository.GetExpenses(expenseGroupId);
            if (repResult != null && repResult.Any())
            {
                return repResult.Select(ExpenseTrackerHelper.CreateExpenseFromEntity).ToList();
            }
            return null;
        }

        public ExpenseDto InsertExpense(ExpenseDto e)
        {
            var expenseEntity = ExpenseTrackerHelper.CreateExpenseFromDto(e);
            var repResult = _expenseTrackerRespository.InsertExpense(expenseEntity);

            return ExpenseTrackerHelper.CreateExpenseFromEntity(repResult.ModelDto);
        }

        public ExpenseGroupDto InsertExpenseGroup(ExpenseGroupDto eg)
        {
            var expenseEntity = ExpenseTrackerHelper.CreateExpenseGroupFromDto(eg);
            var repResult = _expenseTrackerRespository.InsertExpenseGroup(expenseEntity);

            return ExpenseTrackerHelper.CreateExpenseGroupFromEntity(repResult.ModelDto);
        }

        public bool SaveChanges()
        {
            expenseTrackerContext.SaveChanges();
            return true;
        }

        public ExpenseDto UpdateExpense(ExpenseDto e)
        {
            var expenseEntity = ExpenseTrackerHelper.CreateExpenseFromDto(e);
            var repResult = _expenseTrackerRespository.UpdateExpense(expenseEntity);

            return ExpenseTrackerHelper.CreateExpenseFromEntity(repResult.ModelDto);
        }

        public ExpenseGroupDto UpdateExpenseGroup(ExpenseGroupDto eg)
        {
            var expenseEntity = ExpenseTrackerHelper.CreateExpenseGroupFromDto(eg);
            var repResult = _expenseTrackerRespository.UpdateExpenseGroup(expenseEntity);

            return ExpenseTrackerHelper.CreateExpenseGroupFromEntity(repResult.ModelDto);
        }
    }
}
