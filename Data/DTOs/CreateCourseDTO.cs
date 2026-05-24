using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class CreateCourseDTO
    {

        [Required(ErrorMessage = "SubjectName is needed")]
        [StringLength(100, ErrorMessage = "SubjectName is to long (100 characters max)")]
        public string SubjectName { get; set; } = string.Empty;

        [Required]
        [Range(0 , 150 , ErrorMessage ="TotalMarks must be between 0 and 150")]
        public int TotalMarks { get; set; }

        
        [Url(ErrorMessage ="Please Enter a valid Url")]
        public string? Url { get; set; }
        [Required(ErrorMessage = "ClassName is needed")]
        [StringLength(100, ErrorMessage = "ClassName is to long (100 characters max)")]
        public string ClassName { get; set; } = string.Empty;

        [Required( ErrorMessage = "TeacherId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "TeacherId must be 1")]
        public int TeacherID { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
