using DataLogicLayer.Models;
using System.Collections.Generic;

namespace DejlienApp.Models
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public Contact Contact { get; set; } = new Contact();
    }
}