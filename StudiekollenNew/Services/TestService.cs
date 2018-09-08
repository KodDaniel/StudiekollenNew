using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.ViewModels;
using StudiekollenNew.ViewModels.TestViewModels;

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

        public Test GetMostRecentTest(string currentUserId)
        {
            return _testRepository.GetMostRecentTest(currentUserId);
        }


        public IEnumerable<Test> GetAllTestsForThisUserName(string userName)
        {
            return _testRepository.GetAllTestsForThisUserName(userName);
        }

        public IEnumerable<Test> GetAllTestsForThisUserId(string userId)
        {
            return _testRepository.GetAllTestsForThisUserId(userId);
        }

        public Test GetTest(int id)
        {
            return _testRepository.GetTest(id);
        }

        public void AddTest(Test testModel)
        {
            _testRepository.AddTest(testModel);
        }

    
        public void DeleteTest(int id)
        {
            _testRepository.DeleteTest(id);
        }

        public void UpdateTest(Test test, int testId)
        {
            _testRepository.UpdateTest(test,testId);
        }



    }
}