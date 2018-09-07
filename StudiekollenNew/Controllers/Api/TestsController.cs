using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;

namespace StudiekollenNew.Controllers.Api
{
    public class TestsController : ApiController
    {
        //GET/api/tests
        public IEnumerable<Test> GetAllTests()
        {
            var testService = new TestService(new RepositoryFactory());

            return testService.GetAllTests().ToList();
        }

        //Get /api/tests/1
        public Test GetTest(int id)
        {
            var testService = new TestService(new RepositoryFactory());

            return testService.GetSingleTestModelByTestId(id);

        }











    }
}
