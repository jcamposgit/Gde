using System;
using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using RelationSample.Models;
using RelationSample.ViewModels;

namespace RelationSample.Services {
    public interface ICustomerService : IDependency {
        void UpdateSponsorForContentItem(ContentItem item, EditSponsorViewModel model);
        string GetCustomerName(IContent customer);
        IEnumerable<CustomerViewModel> GetCustomers();
    }

    public class CustomerService : ICustomerService {
        private readonly IContentManager _contentManager;

        public CustomerService(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public void UpdateSponsorForContentItem(ContentItem item, EditSponsorViewModel model) {
            var sponsorPart = item.As<SponsorPart>();
            sponsorPart.Sponsor = _contentManager.Get(model.SponsorId);
        }

        public string GetCustomerName(IContent customer) {
            return customer.ContentItem.Parts
                .SelectMany(p => p.Fields)
                .Where(f => f.Name == "Name")
                .First()
                .Storage.Get<string>(null);
        }

        public IEnumerable<CustomerViewModel> GetCustomers() {
            return _contentManager
                .Query("Customer")
                .List()
                .Select(ci => new CustomerViewModel {
                    Id = ci.Id,
                    Name = GetCustomerName(ci)
                });
        }
    }
}