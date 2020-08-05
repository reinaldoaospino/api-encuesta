using System;

namespace Domain.Entities
{
    public class Email
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreationTime { get; set; }
        public string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }

        public void GenerateCreationTime() 
        {
            CreationTime = DateTime.Now;
        }
    }
}