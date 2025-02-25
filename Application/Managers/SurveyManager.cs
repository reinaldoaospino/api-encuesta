﻿using Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Interfaces.Application;
using Domain.Interfaces.Infraestructure;
using Domain.Exceptions;

namespace Application.Managers
{
    public class SurveyManager : ISurveyManager
    {
        private readonly ISurveyRepository _repository;

        public SurveyManager(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Survey>> Get()
        {
            return await _repository.Get();
        }

        public async Task Create(Survey survey)
        {
            survey.Id = survey.GenerateGuid();

            foreach (var option in survey.Options)
            {
                option.Id = survey.GenerateGuid();
            }
      
            await _repository.Create(survey);
        }

        public async Task Update(Survey survey)
        {
            var exixtingSurvey = await _repository.Get(survey.Id);

            var noExists = exixtingSurvey == null;

            if(noExists)
                throw new EntityNotFoundException<Survey>(survey.Id);

            exixtingSurvey.Update(survey);

            await _repository.Update(exixtingSurvey);
        }

        public async Task Delete(string id)
        {
            await _repository.Delete(id);
        }
    }
}