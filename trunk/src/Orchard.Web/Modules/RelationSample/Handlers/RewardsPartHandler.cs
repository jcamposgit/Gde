using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using RelationSample.Models;

namespace RelationSample.Handlers {
    public class RewardsPartHandler : ContentHandler {
        public RewardsPartHandler(IRepository<RewardsPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}