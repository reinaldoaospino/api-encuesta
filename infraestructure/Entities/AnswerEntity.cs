using System.Collections.Generic;

namespace Infraestructure.Entities
{
    public  class AnswerEntity
    {
        public string Id { get; set; }
        public string User { get; set; }
        public IEnumerable<AnswerSelectedEntity> AnswerSelected { get; set; }
    }
}
