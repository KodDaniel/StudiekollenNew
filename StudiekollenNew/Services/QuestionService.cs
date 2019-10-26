using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudiekollenNew.Models;
using StudiekollenNew.Repositories;
using StudiekollenNew.ViewModels;
using StudiekollenNew.ViewModels.QuestionsViewModels;

namespace StudiekollenNew.Services
{
    public class QuestionService
    {

        private readonly QuestionRepository _questionRepository;

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

        public void AddQuestion(int examId, Question questionModel)
        {
            _questionRepository.AddQuestion(examId, questionModel);
        }

        public void UpdateQuestion(Question questionModel, int questionId)
        {
            _questionRepository.UpdateQuestion(questionModel, questionId);
        }

        public void DeleteQuestion(int id)
        {
            _questionRepository.DeleteQuestion(id);
        }



    }
}