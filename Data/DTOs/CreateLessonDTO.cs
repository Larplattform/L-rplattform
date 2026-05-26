using Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DTOs
{
    public class CreateLessonDTO
    {

        [Required(ErrorMessage = "Title is needed")]
        [StringLength(100, ErrorMessage = "TItle is to long (100 characters max)")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is needed")]
        [StringLength(500, ErrorMessage = "Description is to long (500 characters max)")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Content is needed")]
        [StringLength(5000, ErrorMessage = "Content is to long (5000 characters max)")]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "CourseId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be 1")]
        public int CourseID { get; set; }

        public int TeacherID { get; set; }

      
    }
}
