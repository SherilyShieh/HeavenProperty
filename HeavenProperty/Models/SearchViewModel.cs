using System;
using System.ComponentModel.DataAnnotations;

namespace HeavenProperty.Models
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
        }
        [Required]
        public String Location { set; get; }
        public String Type { set; get; }
    }
}
