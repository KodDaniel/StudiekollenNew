using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.DomainModels;
using StudiekollenNew.Migrations;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.ViewModels;
namespace StudiekollenNew.Services
{
    public class ExamService
    {
        private readonly ExamRepository _examRepository;

        public ExamService(RepositoryFactory repoFactory)
        {
            _examRepository = repoFactory.GetExamRepository();
        }

        public IEnumerable<Exam> GetAllExams()
        {
            return _examRepository.GetAllExams();
        }



        //public IEnumerable<Exam> OrderExams(IOrderedEnumerable<Exam> lambda)
        //{
        //   return _examRepository.OrderExams(lambda);
        //}


        public Exam GetMostRecentExam(string currentUserId)
        {
            return _examRepository.GetMostRecentExam(currentUserId);
        }


        public IEnumerable<Exam> GetAllExamsForThisExamId(int examId)
        {
            return _examRepository.GetAllExamsForThisExamId(examId);
        }

        public IEnumerable<Exam> GetAllExamsForThisUserId(string userId)
        {
            return _examRepository.GetAllExamsForThisUserId(userId);
        }

        public Exam GetSingleExam(int id)
        {
            return _examRepository.GetSingleExam(id);
        }

        public void AddExam(Exam examModel, string userId)
        {
            _examRepository.AddExam(examModel,userId);
        }

    
        public void DeleteExam(int id)
        {
            _examRepository.DeleteExam(id);
        }

        public void UpdateExam(Exam exam, int examId)
        {
            _examRepository.UpdateExam(exam,examId);
        }



    }
}