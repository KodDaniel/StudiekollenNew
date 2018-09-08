using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.ViewModels;

namespace StudiekollenNew.Services
{
    public class QuestionService
    {

        private QuestionRepository _questionRepository;

        public QuestionService(RepositoryFactory repoFactory)
        {
            _questionRepository = repoFactory.GetQuestionRepository();
        }

        public Question GetQuestion(int id)
        {
            return _questionRepository.GetQuestion(id);
        }

        public List<Question> GetAllQuestions(int id)
        {
            return _questionRepository.GetAllQuestions(id);
        }

        public void AddQuestion(int testId, Question questionModel)
        {
            _questionRepository.AddQuestion(testId, questionModel);
        }

        public void UpdateQuestion(Question questionModel, int questionId)
        {
            _questionRepository.UpdateQuestion(questionModel, questionId);
        }

        public void DeleteTest(int id)
        {
            _questionRepository.DeleteQuestion(id);
        }


        

        //public int NumberOfQuestionsForThisTest(int testId)
        //{
        //    return _questionRepository.NumberOfQuestionsForThisTest(testId);
        //}




    }
}