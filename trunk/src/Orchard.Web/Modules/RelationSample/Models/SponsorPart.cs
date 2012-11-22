using Orchard.ContentManagement;
using Orchard.Core.Common.Utilities;

namespace RelationSample.Models {
    public class SponsorPart : ContentPart<SponsorPartRecord> {
        private readonly LazyField<IContent> _sponsor = new LazyField<IContent>();

        public LazyField<IContent> SponsorField { get { return _sponsor; } }

        public IContent Sponsor {
            get { return _sponsor.Value; }
            set { _sponsor.Value = value; }
        }
    }
}