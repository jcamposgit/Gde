using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace RelationSample.Models {
    public class SponsorPartRecord : ContentPartRecord {
        public virtual ContentItemRecord Sponsor { get; set; }
    }
}