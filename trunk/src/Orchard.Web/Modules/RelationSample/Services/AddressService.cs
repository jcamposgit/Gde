using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using RelationSample.Models;
using RelationSample.ViewModels;

namespace RelationSample.Services {
    public interface IAddressService : IDependency {
        void UpdateAddressForContentItem(ContentItem item, EditAddressViewModel model);
        IEnumerable<StateRecord> GetStates();
    }

    public class AddressService : IAddressService {
        private readonly IRepository<StateRecord> _stateRepository;

        public AddressService(IRepository<StateRecord> stateRepository) {
            _stateRepository = stateRepository;
        }

        public void UpdateAddressForContentItem(ContentItem item, EditAddressViewModel model) {
            var addressPart = item.As<AddressPart>();
            addressPart.Address = model.Address;
            addressPart.City = model.City;
            addressPart.Zip = model.Zip;
            addressPart.State = _stateRepository.Get(s => s.Code == model.StateCode);
        }

        public IEnumerable<StateRecord> GetStates() {
            return _stateRepository.Table.ToList();
        }
    }
}