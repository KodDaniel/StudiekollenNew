﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.ViewModels;
using StudiekollenNew.ViewModels.TestViewModels;
using NetPipeStyleUriParser = System.NetPipeStyleUriParser;

namespace StudiekollenNew.Repositories
{
    public class TestRepository
    {
        private ApplicationDbContext _context;

        public TestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Test GetTest(int id)
        {
            return _context.Test.Find(id);
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _context.Test.ToList();
        }

        public Test GetMostRecentTest(string currentUserId)
        {
            // Substitut för Last-operator. Tänk på att du ej behöver EagerLoda med "Include" eftersom som du ju här rör dig i en och samma tabell.
            return _context.Test
                .OrderByDescending(c => c.Id)
                .First(c => c.UserId == currentUserId);
        }


        public IEnumerable<Test> GetAllTestsForThisUserName(string userName)
        {
            return _context.Test.
                Include(a => a.User).Include(a => a.Questions)
                .Where(a => a.User.UserName == userName)
                .OrderByDescending(c => c.CreateDate)
                .ThenByDescending(c => c.ChangeDate)
                .ToList();
        }

        public IEnumerable<Test> GetAllTestsForThisUserId(string userId)
        {
            return _context.Test.
                Include(a => a.User).Where(a => a.UserId == userId).Include(a => a.Questions)
                .OrderByDescending(c => c.CreateDate)
                .ThenByDescending(c => c.ChangeDate)
                .ToList();
        }


        public void AddTest(Test testModel,string userId)
        {
            testModel.UserId = userId;

            _context.Test.Add(testModel);

            _context.SaveChanges();
        }

        public void RemoveTest(Test testModel)
        {
            _context.Test.Remove(testModel);

            _context.SaveChanges();
        }

      
      

        

       


    }
}

