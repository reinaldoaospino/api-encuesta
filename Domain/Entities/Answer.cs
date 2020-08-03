using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Answer
    {
        public string Id { get; set; }
        public string User { get; set; }
        public IEnumerable<AnswerSelected> AnswerSelected { get; set; }

        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}