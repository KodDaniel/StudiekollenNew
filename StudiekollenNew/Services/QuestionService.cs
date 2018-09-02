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

        public void AddQuestionsToTest(int testId,Question questionModel)
        {
            _questionRepository.AddQuestionsToTest(testId,questionModel);
        }

        public void RemoveQuestionFromTest(Dictionary<string, string> dictionary, string key)
        {
            _questionRepository.RemoveQuestionFromTest(dictionary,key);
        }


        public List<Question> AllQuestionsModelsByTestId (int id)
        {
            return _questionRepository.AllQuestionModelsByTestId(id);
        }
       


    }
}