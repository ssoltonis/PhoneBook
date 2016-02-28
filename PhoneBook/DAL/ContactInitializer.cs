using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhoneBook.DAL
{
    public class ContactInitializer : DropCreateDatabaseIfModelChanges<ContactContext>
    {
        protected override void Seed(ContactContext context)
        {
            var contacts = new List<Contact>
            {
                new Contact{FirstName="Mantas",LastName="Kalnietis"},
                new Contact{FirstName="Šarūnas",LastName="Jasikevičius"},
                new Contact{FirstName="Artūras",LastName="Jomantas"}
            };

            contacts.ForEach(s => context.Contacts.Add(s));
            context.SaveChanges();

            var phones = new List<Phone>
            {
                new Phone{ContactId=1, PhoneNumber="8646457891"},
                new Phone{ContactId=1, PhoneNumber="8646235689"},
                new Phone{ContactId=2, PhoneNumber="8646289542"},
                new Phone{ContactId=3, PhoneNumber="8646456891"},
            };
            phones.ForEach(s => context.Phones.Add(s));
            context.SaveChanges();

            var emails = new List<Email>
            {
                new Email{ContactId=1, EmailAddress="mantas.k@mail.com"},
                new Email{ContactId=2, EmailAddress="saras@mail.com"},
                new Email{ContactId=2, EmailAddress="sarunas.j@mail.com"},
                new Email{ContactId=3, EmailAddress="arturas@mail.com"},
            };
            emails.ForEach(s => context.Emails.Add(s));
            context.SaveChanges();
        }
    }
}