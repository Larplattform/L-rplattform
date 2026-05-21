using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class CreateSubmissonsDTO
    {
      



        public string Content { get; set; } = string.Empty;

       

        public int UserId { get; set; }

      

        public int AssigmentId { get; set; }

       

       

       
    }
}
