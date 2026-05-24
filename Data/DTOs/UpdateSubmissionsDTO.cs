using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class UpdateSubmissionsDTO
    {
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Feedback is needed")]
        [StringLength(5000, ErrorMessage = "Feedback is to long (5000 characters max)")]
        public string Feedback { get; set; } = string.Empty;

        public UpdateGradeEnumDTO Grade { get; set; }
        [Required(ErrorMessage = "UserId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be 1")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "AssignmentId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "AssignmentId must be 1")]
        public int AssigmentId { get; set; }

        [JsonIgnore]
        public bool Status { get; set; }
    }
}
