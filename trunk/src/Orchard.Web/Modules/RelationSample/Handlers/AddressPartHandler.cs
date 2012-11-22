using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using RelationSample.Models;

namespace RelationSample.Handlers {
    public class AddressPartHandler : ContentHandler {
        public AddressPartHandler(IRepository<AddressPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}