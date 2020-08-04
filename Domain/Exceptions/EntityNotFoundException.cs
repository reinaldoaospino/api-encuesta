using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Exceptions
{
   public class EntityNotFoundException<TEntity> : ApplicationException
    {
        public EntityNotFoundException(string id)
            : base($"The id {id} not exist in {typeof(TEntity).Name}")
        {}
    }
}
