using System;
using System.ComponentModel.DataAnnotations;

namespace QuotingDojoWithEF.Models
{
    public partial class Quote
        {
            public int QuoteId { get; set; }
            [Required]
            public string Name { get; set; }
            [Required]
            public string QuoteText { get; set; }
            public DateTime CreatedAt { get; set; }
        }
}