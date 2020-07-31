using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Survey
    {   [BsonId]
        public Guid Id { get; set; }
        public string Question { get; set; }
        public IEnumerable<SurveyOption> Options { get; set; }
    }
}