using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.ViewModels;
using StudiekollenNew.ViewModels.TestViewModels;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using System.Security.Claims;
using System.Collections;
using System.Web.WebPages;
using StudiekollenNew.DataBase;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Migrations;
using StudiekollenNew.Repositories;
using StudiekollenNew.Services;


namespace StudiekollenNew.Repositories
{

    public class ExamRepository
    {
        private ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Exam GetSingleExam(int id)
        {
            return _context.Exam.Find(id);
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _context.Exam.ToList();
        }

        public Exam GetMostRecentExam(string currentUserId)
        {
            // Substitut för Last-operator. Tänk på att du ej behöver EagerLoda med "Include" eftersom som du ju här rör dig i en och samma tabell.
            return _context.Exam
                .OrderByDescending(e => e.ExamId)
                .First(c => c.UserId == currentUserId);
        }


        public IEnumerable<Exam> GetAllExamsForThisExamId(int id)
        {
            return _context.Exam.Where(a => a.ExamId == id)
                .OrderByDescending(c => c.CreateDate)
                .ThenByDescending(c => c.ChangeDate)
                .ToList();
        }

        public IEnumerable<Exam> GetAllExamsForThisUserId(string userId)
        {
            return _context.Exam.
                Include(a => a.User).Where(a => a.UserId == userId).Include(a => a.Questions)
                .OrderByDescending(c => c.CreateDate)
                .ThenByDescending(c => c.ChangeDate)
                .ToList();
        }


        public void AddExam(Exam examModel, string userId)
        {
            examModel.UserId = userId;

            _context.Exam.Add(examModel);
            
            _context.SaveChanges();
        }

        public void DeleteExam(int id)
        {
            var exam = _context.Exam
                .Single(a => a.ExamId== id);

            _context.Exam.Remove(exam);

            _context.SaveChanges();
        }

        public void UpdateExam(Exam exam, int eXamId)
        {
            var currentExam = GetSingleExam(eXamId);

            currentExam.ExamName = exam.ExamName;

            currentExam.ChangeDate = DateTime.Now;
           
            _context.SaveChanges();
        }









    }
}

