using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleWebApi.Core.UnitOfWork
{
    public abstract class UnitOfWorkBase
    {
        protected readonly ExpenseTrackerContext expenseTrackerContext;
        protected bool Disposed;
        protected UnitOfWorkBase(ExpenseTrackerContext dataContext)
        {
            expenseTrackerContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            Disposed = false;
        }

        /// <summary>
        /// Disposes of the <see cref="IDataContext"/> and prevents the finalizer from releasing 
        /// unmanaged resources that have already been freed by the IDisposable.Dispose.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the <see cref="IDataContext"/> if it has not already been disposed of.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                if (disposing)
                {
                    expenseTrackerContext.Dispose();
                }
            }
            this.Disposed = true;
        }
    }
}
