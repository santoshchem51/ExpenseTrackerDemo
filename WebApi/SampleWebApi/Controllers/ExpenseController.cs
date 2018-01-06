using Marvin.JsonPatch;
using SampleWebApi.Core;
using SampleWebApi.Core.Interfaces;
using SampleWebApi.Core.Models;
using SampleWebApi.Core.UnitOfWork;
using SampleWebApi.Data.Helpers;
using SampleWebApi.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace SampleWebApi.Controllers
{
    [RoutePrefix("api")]
    public class ExpenseController : ApiController
    {
        IExpenseTrackerUnitOfWork _expenseUnitOfWork; // TODO instead of UOW import a service 
        ExpenseTrackerContext currentDataContext;
        const int maxPageSize = 5;
        public ExpenseController()
        {
            currentDataContext = new ExpenseTrackerContext();
            _expenseUnitOfWork = new ExpenseTrackerUnitOfWork(currentDataContext);
        }

        [Route("expensegroups/{expenseGroupId}/expenses", Name = "ExpensesForGroup")]
        public IHttpActionResult Get(int expenseGroupId, string fields = null, string sort = "date"
            , int page = 1, int pageSize = maxPageSize)
        { 
            try
            {

                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                var expenses = _expenseUnitOfWork.GetExpenses(expenseGroupId);

                if (expenses == null)
                {
                    // this means the expensegroup doesn't exist
                    return NotFound();
                }

                // ensure the page size isn't larger than the maximum.
                if (pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate data for metadata
                var totalCount = expenses.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var urlHelper = new UrlHelper(Request);

                var prevLink = page > 1 ? urlHelper.Link("ExpensesForGroup",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        expenseGroupId = expenseGroupId,
                        fields = fields,
                        sort = sort
                    }) : "";
                var nextLink = page < totalPages ? urlHelper.Link("ExpensesForGroup",
                    new
                    {
                        page = page + 1,
                        pageSize = pageSize,
                        expenseGroupId = expenseGroupId,
                        fields = fields,
                        sort = sort
                    }) : "";


                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));



                return Ok(expenses
                    .ApplySort(sort)
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList());

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [VersionedRoute("expensegroups/{expenseGroupId}/expenses/{id}", 1)]
        [VersionedRoute("expenses/{id}", 1)]
        public IHttpActionResult Get(int id, int? expenseGroupId = null, string fields = null)
        {
            try
            {
                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }
                ExpenseDto expense = null;

                if (expenseGroupId == null)
                {
                    expense = _expenseUnitOfWork.GetExpense(id);
                }
                else
                {
                    var expensesForGroup = _expenseUnitOfWork.GetExpenses((int)expenseGroupId);

                    // if the group doesn't exist, we shouldn't try to get the expenses
                    if (expensesForGroup != null)
                    {
                        expense = expensesForGroup.FirstOrDefault(eg => eg.Id == id);
                    }
                }

                if (expense != null)
                {
                    return Ok(DataShapingHelper<ExpenseDto>.CreateDataShapedObject(expense,lstOfFields));
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [VersionedRoute("expensegroups/{expenseGroupId}/expenses/{id}", 2)]
        [VersionedRoute("expenses/{id}", 2)]
        public IHttpActionResult GetV2(int id, int? expenseGroupId = null, string fields = null)
        {
            try
            {
                List<string> lstOfFields = new List<string>();

                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                ExpenseDto expense = null;

                if (expenseGroupId == null)
                {
                    expense = _expenseUnitOfWork.GetExpense(id);
                }
                else
                {
                    var expensesForGroup = _expenseUnitOfWork.GetExpenses((int)expenseGroupId);

                    // if the group doesn't exist, we shouldn't try to get the expenses
                    if (expensesForGroup != null)
                    {
                        expense = expensesForGroup.FirstOrDefault(eg => eg.Id == id);
                    }
                }

                if (expense != null)
                {
                    var returnValue = DataShapingHelper<ExpenseDto>.CreateDataShapedObject(expense, lstOfFields);
                    return Ok(returnValue);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



        [Route("expenses/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {

                var result = _expenseUnitOfWork.DeleteExpense(id);
                _expenseUnitOfWork.SaveChanges();

                if (result)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else 
                {
                    return NotFound();
                }                
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("expenses")]
        public IHttpActionResult Post([FromBody] ExpenseDto expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest();
                }

                // map
                var result = _expenseUnitOfWork.InsertExpense(expense);
                if (result != null)
                {
                    // map to dto
                    return Created<ExpenseDto>(Request.RequestUri + "/" + result.Id.ToString(), result);
                }

                return BadRequest();

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("expenses/{id}")]
        public IHttpActionResult Put(int id, [FromBody]ExpenseDto expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest();
                }

                            
                var result = _expenseUnitOfWork.UpdateExpense(expense);
                if (result != null)
                {
                    // map to dto
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }

              //  return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("expenses/{id}")]
        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody]JsonPatchDocument<ExpenseDto> expensePatchDocument)
        {
            try
            {
                // find 
                if (expensePatchDocument == null)
                {
                    return BadRequest();
                }

                var expense = _expenseUnitOfWork.GetExpense(id);
                if (expense == null)
                {
                    return NotFound();
                }

                // apply changes to the DTO
                expensePatchDocument.ApplyTo(expense);

                // map the DTO with applied changes to the entity, & update
                var result = _expenseUnitOfWork.UpdateExpense(expense);

                if (result != null)
                {
                    // map to dto                    
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
