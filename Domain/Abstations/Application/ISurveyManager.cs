using Domain.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstations.Application
{
   public interface ISurveyManager
    {
        Task Create(Survey obj);
    }
}
