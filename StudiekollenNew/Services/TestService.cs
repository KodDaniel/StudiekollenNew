﻿using System;
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

        public IEnumerable<Test> All()
        {
            return _testRepository.All();
        }

        public void Add(Test testModel)
        {
            _testRepository.Add(testModel);
        }

        public int GetMostRecentTestId(string currentUserId)
        {
            return _testRepository.GetMostRecentTestId(currentUserId);
        }

        public string GetMostRecentTestName(string currentUserId)
        {
            return _testRepository.GetMostRecentTestName(currentUserId);
        }



    }
}