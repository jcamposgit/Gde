using System.Collections.Generic;
using RelationSample.Models;

namespace RelationSample.ViewModels {
    public class EditAddressViewModel {
        public string Address { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string Zip { get; set; }
        public IEnumerable<StateRecord> States { get; set; }
    }
}