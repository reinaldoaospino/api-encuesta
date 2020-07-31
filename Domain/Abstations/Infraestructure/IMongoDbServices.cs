using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstations.Infraestructure
{
    public interface IMongoDbServices
    {
        Task Create<T>(string table, T record);
    }
}