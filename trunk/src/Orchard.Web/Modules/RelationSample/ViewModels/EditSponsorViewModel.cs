using System.Collections.Generic;
using Orchard.ContentManagement;
using RelationSample.Models;

namespace RelationSample.ViewModels {
    public class EditSponsorViewModel {
        public int CustomerId { get; set; }
        public int SponsorId { get; set; }
        public IEnumerable<CustomerViewModel> Customers { get; set; }
    }

    public class CustomerViewModel {
        public int Id { get; set;}
        public string Name { get; set;}
    }
}