using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;

namespace StudiekollenNew.Services
{
    public class TestService
    {
        private TestRepository _testRepository;

        public TestService(RepositoryFactory repoFactory)
        {
            _testRepository = repoFactory.GetTestRepository();
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _testRepository.GetAllTests();
        }

        public void AddTest(Test testModel)
        {
            _testRepository.AddTest(testModel);
        }

        public int GetMostRecentTestId(string currentUserId)
        {
            return _testRepository.GetMostRecentTestIdFromThisUser(currentUserId);
        }

        public string GetMostRecentTestName(string currentUserId)
        {
            return _testRepository.GetMostRecentTestNameFromThisUser(currentUserId);
        }

        public IEnumerable<Test> GetAllTestsForThisUserName(string userName)
        {
            return _testRepository.GetAllTestsForThisUserName(userName);
        }

        public Test GetSingleTestModelByTestId(int id)
        {
           return _testRepository.GetSingleTestModelByTestId(id);
        }

        public void RemoveTest(Test testModel)
        {
            _testRepository.RemoveTest(testModel);
        }



    }
}