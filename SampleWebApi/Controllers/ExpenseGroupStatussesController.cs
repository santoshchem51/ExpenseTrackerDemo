using Marvin.JsonPatch;
using SampleWebApi.Core;
using SampleWebApi.Core.Interfaces;
using SampleWebApi.Core.Models;
using SampleWebApi.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SampleWebApi.Controllers
{
    public class ExpenseGroupStatussesController : ApiController
    {
        IExpenseTrackerUnitOfWork _expenseUnitOfWork; // TODO instead of UOW import a service 
        ExpenseTrackerContext currentDataContext;

        public ExpenseGroupStatussesController()
        {
            currentDataContext = new ExpenseTrackerContext();
            _expenseUnitOfWork = new ExpenseTrackerUnitOfWork(currentDataContext);
        }


        public IHttpActionResult Get()
        {

            try
            {
                // get expensegroupstatusses & map to DTO's
                var expenseGroupStatusses = _expenseUnitOfWork.GetExpenseGroupStatusses().ToList();

                return Ok(expenseGroupStatusses);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
