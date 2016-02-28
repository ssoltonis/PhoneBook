using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Models
{
    public class Phone
    {
        public int Id { get; set; }

        public int ContactId { get; set; }
        
        [Display(Name = "Phone number")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "Can't be longer than 12 characters.")]
        public string PhoneNumber { get; set; }

        public virtual Contact Contact { get; set; }
    }
}