using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long")]
        [MaxLength(280, ErrorMessage = "Title must be at most 280 characters long")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Content must be at least 5 characters long")]
        [MaxLength(1000, ErrorMessage = "Content must be at most 1000 characters long")]
        public string Content { get; set; } = string.Empty;
    }
}