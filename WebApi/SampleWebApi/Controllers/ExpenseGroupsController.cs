using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Marvin.JsonPatch;
using SampleWebApi.Core;
using SampleWebApi.Core.Helpers;
using SampleWebApi.Core.Interfaces;
using SampleWebApi.Core.Models;
using SampleWebApi.Core.UnitOfWork;
using SampleWebApi.Data.Helpers;

namespace SampleWebApi.Controllers
{
    public class ExpenseGroupsController : ApiController
    {

        IExpenseTrackerUnitOfWork _expenseUnitOfWork; // TODO instead of UOW import a service 
        ExpenseTrackerContext currentDataContext;
        const int MaxPageSize = 10;
        public ExpenseGroupsController()
        {
            currentDataContext = new ExpenseTrackerContext();
            _expenseUnitOfWork = new ExpenseTrackerUnitOfWork(currentDataContext);
        }

        public object ExpenseTrackingHelper { get; private set; }

        [Route("api/expenseGroups",Name = "ExpenseGroupsList")]
        public IHttpActionResult Get(string sort = "id", string status = null, string userId = null, int page = 1, int pagesize = 5)
        {
            try
            {
                int statusId = -1;
                if (status != null) // TODO: convert status to enums for expensegroups status
                {
                    switch (status.ToLower())
                    {
                        case "open":
                            statusId = 1;
                            break;
                        case "confirmed":
                            statusId = 2;
                            break;
                        case "processed":
                            statusId = 3;
                            break;
                        default:
                            break;
                    }
                }
                var expenseGroups = _expenseUnitOfWork.GetExpenseGroups();

                var sortedExpenseGroups = expenseGroups
                     .ApplySort(sort)
                     .Where(eg => (statusId == -1 || eg.ExpenseGroupStatusId == statusId))
                     .Where(eg => (userId == null || eg.UserId == userId))
                     .ToList();

                if(pagesize > MaxPageSize)
                {
                    pagesize = MaxPageSize;
                }
                // pagination cals
                var totalCount = sortedExpenseGroups.Count();
                var totalPages = (int) Math.Ceiling((double) totalCount / pagesize);

                // next and preview pages
                var urlHelper = new UrlHelper(Request); // this needs route name to be set so that we can call it by name
                var prevPage = page > 1 ? urlHelper.Link("ExpenseGroupsList", new
                {
                    page = page - 1,
                    status,
                    sort,
                    userId,
                    pagesize                    
                }) : "";

                var nextPage = page < totalPages ? urlHelper.Link("ExpenseGroupsList", new
                {
                    page = page + 1,
                    status,
                    sort,
                    userId,
                    pagesize
                }) : "";

                // now created header object with pagination info
                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pagesize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    nextPageLink = nextPage,
                    prevPageLink = prevPage
                };

                System.Web.HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

                return Ok(sortedExpenseGroups.Skip(pagesize * (page-1)).Take(pagesize));
            }
            catch (Exception ex)
            {
                return InternalServerError();
                
            }
          
        }

        public IHttpActionResult Get(int id)
        {
            try
            {
                var expenseGroup = _expenseUnitOfWork.GetExpenseGroup(id);
                if(expenseGroup == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(expenseGroup);
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }
       

        [HttpPost]
        public IHttpActionResult Post([FromBody] ExpenseGroupDto expenseGroup)
        {
            try
            {
                if(expenseGroup == null)
                {
                    return BadRequest();
                }

                var result = _expenseUnitOfWork.InsertExpenseGroup(expenseGroup); // TODO create service

                if(result == null)
                {
                    _expenseUnitOfWork.SaveChanges();

                    return Created(Request.RequestUri + "/" + result.Id, result);
                }

                return BadRequest();


            }
            catch (Exception)
            {

                return InternalServerError();
            }          
            
        }

        public IHttpActionResult Put(int id, [FromBody] ExpenseGroupDto expenseGroup)
        {
            try
            {
                if(expenseGroup == null)
                {
                    return BadRequest();
                }

                var eg = _expenseUnitOfWork.UpdateExpenseGroup(expenseGroup);
                _expenseUnitOfWork.SaveChanges();

                if(eg != null)
                {
                    return Ok(eg);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // https://tools.ietf.org/html/rc6902
        // ContentType : application/json-patch+json
        // json patch documents

        [HttpPatch]
        public IHttpActionResult Patch(int id, [FromBody] JsonPatchDocument<ExpenseGroupDto> expenseGroupPatchDocument)
        {
            try
            {
                if(expenseGroupPatchDocument == null)
                {
                    return BadRequest();
                }
                var expenseGroup = _expenseUnitOfWork.GetExpenseGroup(id);

                if(expenseGroup == null)
                {
                    return NotFound();
                }

                // now apply patch 
                expenseGroupPatchDocument.ApplyTo(expenseGroup);

                // now update the
               var result = _expenseUnitOfWork.UpdateExpenseGroup(expenseGroup);
                _expenseUnitOfWork.SaveChanges();

                return Ok(result);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _expenseUnitOfWork.DeleteExpenseGroup(id);
                _expenseUnitOfWork.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }
    }
}
