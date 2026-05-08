using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class CreateSubmissonsDTO
    {
      



        public string Content { get; set; } = string.Empty;

        public string Feedback { get; set; } = string.Empty;

        public GradeEnumDTO Grade { get; set; }

        public int UserId { get; set; }

      

        public int AssigmentId { get; set; }

        [JsonIgnore]
       public bool Status { get; set; }

       

       
    }
}
