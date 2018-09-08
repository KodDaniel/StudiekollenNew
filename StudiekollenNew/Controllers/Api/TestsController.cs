using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using StudiekollenNew.Dtos;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;

namespace StudiekollenNew.Controllers.Api
{
    public class TestsController : ApiController
    {
        //GET /api/tests
        public IEnumerable<TestDto> GetAllTests()
        {
            var testService = new TestService(new RepositoryFactory());

            return testService.GetAllTests().ToList().Select(Mapper.Map<Test, TestDto>);
        }


        //GET /api/tests/1
        public IHttpActionResult GetTest(int id)
        {
            var testService = new TestService(new RepositoryFactory());

            var test = testService.GetTest(id);

            if (test is null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Test, TestDto>(test));
        }

        //POST /api/tests
        [HttpPost]
        public IHttpActionResult CreateTest(TestDto testDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var testService = new TestService(new RepositoryFactory());

            var userId = User.Identity.GetUserId();

            var test = Mapper.Map<TestDto, Test>(testDto);

            testService.AddTest(test,userId);

            testDto.Id = test.Id;

            return Created(new Uri(Request.RequestUri + "/" + test.Id),testDto);
        }

        //PUT /api/tests/1
        [HttpPut]
        public void UpdateTest(TestDto testDto, int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var testService = new TestService(new RepositoryFactory());

            var testInDb = testService.GetTest(id);

            if (testInDb is null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map(testDto,testInDb);

            // rad nedan osäker
            testService.UpdateTest(testInDb, id);
        }

        //DELETE /api/tests/1
        [HttpDelete]
        public void DeleteTest(int id)
        {
            var testService = new TestService(new RepositoryFactory());

            var testInDb = testService.GetTest(id);

            if (testInDb is null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            testService.DeleteTest(id);
        }









    }
}
