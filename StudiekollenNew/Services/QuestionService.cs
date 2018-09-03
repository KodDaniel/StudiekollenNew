using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;

namespace StudiekollenNew.Services
{
    public class QuestionService
    {

        private QuestionRepository _questionRepository;

        public QuestionService(RepositoryFactory repoFactory)
        {
            _questionRepository = repoFactory.GetQuestionRepository();
        }

        public Question GetSingleQuestionModelByQuestionId(int id)
        {
            return _questionRepository.GetSingleQuestionModelByQuestionId(id);
        }
        public void AddQuestionsToTest(int testId,Question questionModel)
        {
            _questionRepository.AddQuestionsToTest(testId,questionModel);
        }

        public void RemoveQuestionFromTest(int id)
        {
            _questionRepository.RemoveQuestionFromTest(id);
        }


        public List<Question> AllQuestionsModelsByTestId (int id)
        {
            return _questionRepository.AllQuestionModelsByTestId(id);
        }

        //public int GetNumberOfQuestionsOfaTestByTestId(int id)
        //{
        //   return _questionRepository.GetNumberOfQuestionsOfaTestByTestId(id);
        //}





    }
}