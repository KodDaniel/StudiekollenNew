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

        public void UpdateQuestion(EditQuestionViewModel viewModel, int questionId)
        {
            _questionRepository.UpdateQuestion(viewModel,questionId);
        }
        public Question GetSingleQuestionModelByQuestionId(int id)
        {
            return _questionRepository.GetSingleQuestionModelByQuestionId(id);
        }
        public void AddQuestionsToTest(CreateTestViewModel viewModel)
        {
            _questionRepository.AddQuestionsToTest(viewModel);
        }

        public void RemoveQuestionFromTest(int id)
        {
            _questionRepository.RemoveQuestionFromTest(id);
        }


        public List<Question> AllQuestionsModelsByTestId (int id)
        {
            return _questionRepository.AllQuestionModelsByTestId(id);
        }

        //public int NumberOfQuestionsForThisTest(int testId)
        //{
        //    return _questionRepository.NumberOfQuestionsForThisTest(testId);
        //}




    }
}