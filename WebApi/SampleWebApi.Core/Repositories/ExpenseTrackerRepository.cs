using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleWebApi.Core.Entities;
using SampleWebApi.Core.Models;
using SampleWebApi.Core.Interfaces;
using SampleWebApi.Core.Enums;
using static SampleWebApi.Core.Enums.ExpenseTrackerEnums;
using System.Data.Entity;

namespace SampleWebApi.Core.Repositories
{    
    public class ExpenseTrackerRepository : Interfaces.IExpenseTrackerRespository
    {
        private ExpenseTrackerContext _dbContext; // TODO wrap it with interface
        private IExpenseTrackerRespository _expenseTrackerRespository;
        public ExpenseTrackerRepository(ExpenseTrackerContext expenseTrackerContext)
        {
            _dbContext = expenseTrackerContext ?? throw new ArgumentNullException("ExpenseTracketContext Cannot be null");
            
        }
        public IRepositoryActionResult<Expense> DeleteExpense(int id)
        {
            try
            {
                var exp = _dbContext.Expenses.Where(e => e.Id == id).FirstOrDefault();
                if (exp != null)
                {
                    _dbContext.Expenses.Remove(exp);                    
                }
                return new RepositoryActionResult<Expense>(null, ExpenseTrackerEnums.eRepositoryActionStatus.Removed);
            }
            catch (Exception ex)
            {
                // todo: log exception to database.

                return new RepositoryActionResult<Expense>(null, eRepositoryActionStatus.Error, ex);

            }            
        }

        public IRepositoryActionResult<ExpenseGroup> DeleteExpenseGroup(int id)
        {
            try
            {

                var eg = _dbContext.ExpenseGroups.Where(e => e.Id == id).FirstOrDefault();
                if (eg != null)
                {
                    // also remove all expenses linked to this expensegroup

                    _dbContext.ExpenseGroups.Remove(eg);

                    return new RepositoryActionResult<ExpenseGroup>(null, eRepositoryActionStatus.Removed);
                }
                return new RepositoryActionResult<ExpenseGroup>(null, eRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ExpenseGroup>(null, eRepositoryActionStatus.Error, ex);
            }
        }

        public Expense GetExpense(int id, int? expenseGroupId = null)
        {
            return _dbContext.Expenses.FirstOrDefault(e => e.Id == id &&
               (expenseGroupId == null || expenseGroupId == e.ExpenseGroupId));
            
        }

        public ExpenseGroup GetExpenseGroup(int id)
        {
            return _dbContext.ExpenseGroups.FirstOrDefault(eg => eg.Id == id);
        }


        public ExpenseGroup GetExpenseGroup(int id, string userId)
        {
            return _dbContext.ExpenseGroups.FirstOrDefault(eg => eg.Id == id && eg.UserId == userId);
        }

        public IList<ExpenseGroup> GetExpenseGroups()
        {
            return _dbContext.ExpenseGroups.ToList();
        }

        public IList<ExpenseGroup> GetExpenseGroups(string userId)
        {
            return _dbContext.ExpenseGroups.Where(eg => eg.UserId == userId).ToList();
        }

        public ExpenseGroupStatus GetExpenseGroupStatus(int id)
        {
            return _dbContext.ExpenseGroupStatus.FirstOrDefault(egs => egs.Id == id);
        }

        public IList<ExpenseGroupStatus> GetExpenseGroupStatusses()
        {
            return _dbContext.ExpenseGroupStatus.ToList();
        }

        public IList<ExpenseGroup> GetExpenseGroupsWithExpenses()
        {
            return _dbContext.ExpenseGroups.Include("Expenses").ToList();
        }

        public ExpenseGroup GetExpenseGroupWithExpenses(int id)
        {
            return _dbContext.ExpenseGroups.Include("Expenses").FirstOrDefault(eg => eg.Id == id);
        }

        public ExpenseGroup GetExpenseGroupWithExpenses(int id, string userId)
        {
            return _dbContext.ExpenseGroups.Include("Expenses").FirstOrDefault(eg => eg.Id == id && eg.UserId == userId);
        }

        public IList<Expense> GetExpenses()
        {
            return _dbContext.Expenses.ToList();
        }

        public IList<Expense> GetExpenses(int expenseGroupId)
        {
            return _dbContext.Expenses.Where(e => e.ExpenseGroupId == expenseGroupId).ToList();
        }

        public IRepositoryActionResult<Expense> InsertExpense(Expense e)
        {
            try
            {
                _dbContext.Expenses.Add(e);
                return new RepositoryActionResult<Expense>(null, eRepositoryActionStatus.Added);
            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<Expense>(null, eRepositoryActionStatus.Error, ex);
            }

        }

        public IRepositoryActionResult<ExpenseGroup> InsertExpenseGroup(ExpenseGroup eg)
        {
            try
            {
                _dbContext.ExpenseGroups.Add(eg);
                return new RepositoryActionResult<ExpenseGroup>(eg, eRepositoryActionStatus.Added, null);
                            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ExpenseGroup>(null, eRepositoryActionStatus.Error, ex);
            }
        }

        public IRepositoryActionResult<Expense> UpdateExpense(Expense e)
        {
            throw new NotImplementedException();
        }

       IRepositoryActionResult<ExpenseGroup> IExpenseTrackerRespository.UpdateExpenseGroup(ExpenseGroup eg)
        {
            try
            {

                // you can only update when an expensegroup already exists for this id

                var existingEG = _dbContext.ExpenseGroups.FirstOrDefault(exg => exg.Id == eg.Id);

                if (existingEG == null)
                {
                    return new RepositoryActionResult<ExpenseGroup>(eg, eRepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _dbContext.Entry(existingEG).State = EntityState.Detached;

                // attach & save
                _dbContext.ExpenseGroups.Attach(eg);

                // set the updated entity state to modified, so it gets updated.
                _dbContext.Entry(eg).State = EntityState.Modified;


                return new RepositoryActionResult<ExpenseGroup>(eg, eRepositoryActionStatus.Updated, null);
                

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<ExpenseGroup>(null, eRepositoryActionStatus.Error, ex);
            }

        }
    }
}
