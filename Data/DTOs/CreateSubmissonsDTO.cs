using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class CreateSubmissonsDTO
    {


        [Required(ErrorMessage = "Content is needed")]
        [StringLength(5000, ErrorMessage = "Content is to long (5000 characters max)")]

        public string Content { get; set; } = string.Empty;


        [Required(ErrorMessage = "UserId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be 1")]
        public int UserId { get; set; }


        
        public int AssigmentId { get; set; }

       

       

       
    }
}
