﻿using System;
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

        public void UpdateQuestion(Question questionModel, int questionId)
        {
            _questionRepository.UpdateQuestion(questionModel,questionId);
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

        //public int NumberOfQuestionsForThisTest(int testId)
        //{
        //    return _questionRepository.NumberOfQuestionsForThisTest(testId);
        //}




    }
}