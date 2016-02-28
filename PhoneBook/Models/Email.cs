using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PhoneBook.Models
{
    public class Email
    {
        public int Id { get; set; }

        public int ContactId { get; set; }
        
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(40, ErrorMessage = "Can't be longer than 40 characters")]
        public string EmailAddress { get; set; }
        
        public virtual Contact Contact { get; set; }
    }
}