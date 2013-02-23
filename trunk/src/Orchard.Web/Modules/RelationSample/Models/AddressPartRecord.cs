using Orchard.ContentManagement.Records;

namespace RelationSample.Models {
    public class AddressPartRecord : ContentPartRecord {
        public virtual string Address { get; set; }
        public virtual string City { get; set; }
        public virtual StateRecord StateRecord { get; set; }
        public virtual string Zip { get; set; }
    }
}