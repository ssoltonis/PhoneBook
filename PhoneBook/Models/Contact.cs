using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhoneBook.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [StringLength(40, ErrorMessage = "Can't be longer than 40 characters")]
        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        [StringLength(40, ErrorMessage = "Can't be longer than 40 characters")]
        public string LastName { get; set; }
        
        public string PhonesInLine
        {
            get
            {
                return PhoneNumbers != null ? PhoneNumbers.Aggregate(string.Empty, (current, number) => current + string.Format("{0} ", number.PhoneNumber)) : string.Empty;
            }
        }

        public string EmailsInLine
        {
            get
            {
                return EmailAddresses != null ? EmailAddresses.Aggregate(string.Empty, (current, address) => current + string.Format("{0} ", address.EmailAddress)) : string.Empty;
            }
        }

        [Display(Name = "Phone numbers")]
        public virtual IList<Phone> PhoneNumbers { get; set; }

        [Display(Name = "Email addresses")]
        public virtual IList<Email> EmailAddresses { get; set; }
    }
}