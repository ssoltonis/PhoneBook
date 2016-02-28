using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneBook.DAL;
using PhoneBook.Models;
using System.Linq.Dynamic;

namespace PhoneBook.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactContext _dbContext = new ContactContext();

        // GET: Contact
        public ActionResult Index()
        {
            var contacts = _dbContext.Contacts.ToList();
            return View(contacts);
        }

        public PartialViewResult ContactSearch(string search)
        {
            var contacts = _dbContext.Contacts.ToList();

            if (!string.IsNullOrEmpty(search))
                contacts = _dbContext.Contacts.ToList().Where(x => x.FirstName.ToLower().Contains(search) || x.LastName.ToLower().Contains(search)).ToList();

            return PartialView("Partials/_ContactList", contacts);
        }
        
        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = _dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            var contact = new Contact
            {
                PhoneNumbers = new List<Phone> {new Phone()},
                EmailAddresses = new List<Email> {new Email()}
            };

            return View(contact);
        }

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,PhoneNumbers,EmailAddresses")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = _dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            if (contact.PhoneNumbers.Count == 0)
                contact.PhoneNumbers.Add(new Phone {ContactId = contact.Id});

            if (contact.EmailAddresses.Count == 0)
                contact.EmailAddresses.Add(new Email { ContactId = contact.Id });

            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,PhoneNumbers,EmailAddresses")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                var currentContact = _dbContext.Contacts
                .Where(p => p.Id == contact.Id)
                .Include(p => p.PhoneNumbers)
                .Include(p => p.EmailAddresses)
                .SingleOrDefault();

                if (currentContact == null) return View(contact);

                // Update parent
                _dbContext.Entry(currentContact).CurrentValues.SetValues(contact);

                // Update child items
                UpdateChildItems(currentContact, contact);
                
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        private void UpdateChildItems(Contact currentContact, Contact newContact)
        {
            
            // Delete phones
            foreach (var phone in currentContact.PhoneNumbers.ToList())
            {
                if (newContact.PhoneNumbers.All(c => c.Id != phone.Id))
                    _dbContext.Phones.Remove(phone);
            }

            // Delete emails
            foreach (var email in currentContact.EmailAddresses.ToList())
            {
                if (newContact.EmailAddresses.All(c => c.Id != email.Id))
                    _dbContext.Emails.Remove(email);
            }

            // Update and Insert phones
            foreach (var phoneModel in newContact.PhoneNumbers)
            {
                var currentPhone = currentContact.PhoneNumbers
                    .SingleOrDefault(c => c.Id == phoneModel.Id);

                if (currentPhone != null)
                {
                    // Update phone
                    _dbContext.Entry(currentPhone).CurrentValues.SetValues(phoneModel);
                }
                else
                {
                    // Insert phone
                    var newPhone = new Phone
                    {
                        Id = phoneModel.Id,
                        ContactId = phoneModel.ContactId,
                        PhoneNumber = phoneModel.PhoneNumber
                    };

                    currentContact.PhoneNumbers.Add(newPhone);
                }
            }

            // Update and Insert emails
            foreach (var emailModel in newContact.EmailAddresses)
            {
                var currentEmail = currentContact.EmailAddresses
                    .SingleOrDefault(c => c.Id == emailModel.Id);

                if (currentEmail != null)
                {
                    // Update email
                    _dbContext.Entry(currentEmail).CurrentValues.SetValues(emailModel);
                }
                else
                {
                    // Insert email
                    var newEmail = new Email
                    {
                        Id = emailModel.Id,
                        ContactId = emailModel.ContactId,
                        EmailAddress = emailModel.EmailAddress
                    };

                    currentContact.EmailAddresses.Add(newEmail);
                }
            }

            _dbContext.SaveChanges();
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var contact = _dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var contact = _dbContext.Contacts.Find(id);
            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult AddPhone(int contactId)
        {
            var phone = new Phone{ContactId = contactId};
            return PartialView("Partials/_PhoneEditor", phone);
        }
        
        public ActionResult AddEmail(int contactId)
        {
            var email = new Email { ContactId = contactId };
            return PartialView("Partials/_EmailEditor", email);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
        
    }
}
