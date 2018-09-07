using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;

namespace StudiekollenNew.Controllers.Api
{
    public class TestsController : ApiController
    {
        //GET /api/tests
        public IEnumerable<Test> GetAllTests()
        {
            var testService = new TestService(new RepositoryFactory());

            return testService.GetAllTests().ToList();
        }

        //GET /api/tests/1
        public Test GetTest(int id)
        {
            var testService = new TestService(new RepositoryFactory());

            var test = testService.GetTest(id);

            if (test is null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return test;
        }

        //POST /api/tests
        [HttpPost]
        public Test CreateTest(Test test, string userId)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var testService = new TestService(new RepositoryFactory());
            
            testService.AddTest(test,userId);

            return test; 
        }

        //PUT /api/tests/1
        public void UpdateTest(Test test, int id)
        {
             if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }


        }









    }
}
