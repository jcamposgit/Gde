using Orchard.ContentManagement;
using Orchard.Data;
using Orchard.ContentManagement.Handlers;
using RelationSample.Models;

namespace RelationSample.Handlers {
    public class SponsorPartHandler : ContentHandler {
        private readonly IContentManager _contentManager;

        public SponsorPartHandler(
            IRepository<SponsorPartRecord> repository,
            IContentManager contentManager) {

            Filters.Add(StorageFilter.For(repository));
            _contentManager = contentManager;

            OnInitializing<SponsorPart>(PropertySetHandlers);
            OnLoaded<SponsorPart>(LazyLoadHandlers);
        }

        void LazyLoadHandlers(LoadContentContext context, SponsorPart part) {
            // add handlers that will load content just-in-time
            part.SponsorField.Loader(() =>
                part.Record.Sponsor == null ?
                null : _contentManager.Get(part.Record.Sponsor.Id));
        }

        static void PropertySetHandlers(InitializingContentContext context, SponsorPart part) {
            // add handlers that will update records when part properties are set
            part.SponsorField.Setter(sponsor => {
                part.Record.Sponsor = sponsor == null
                    ? null
                    : sponsor.ContentItem.Record;
                return sponsor;
            });

            // Force call to setter if we had already set a value
            if (part.SponsorField.Value != null)
                part.SponsorField.Value = part.SponsorField.Value;
        }
    }
}