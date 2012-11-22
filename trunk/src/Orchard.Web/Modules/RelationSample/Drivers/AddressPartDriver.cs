using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using RelationSample.Models;
using RelationSample.Services;
using RelationSample.ViewModels;

namespace RelationSample.Drivers {
    [UsedImplicitly]
    public class AddressPartDriver : ContentPartDriver<AddressPart> {
        private readonly IAddressService _addressService;

        private const string TemplateName = "Parts/Address";

        public AddressPartDriver(IAddressService addressService) {
            _addressService = addressService;
        }

        protected override string Prefix {
            get { return "Address"; }
        }

        protected override DriverResult Display(AddressPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Address",
                            () => shapeHelper.Parts_Address(
                                ContentPart: part,
                                Address: part.Address,
                                City: part.City,
                                Zip: part.Zip,
                                StateCode: part.State.Code,
                                StateName: part.State.Name));
        }

        protected override DriverResult Editor(AddressPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Address_Edit",
                    () => shapeHelper.EditorTemplate(
                        TemplateName: TemplateName,
                        Model: BuildEditorViewModel(part),
                        Prefix: Prefix));
        }

        protected override DriverResult Editor(AddressPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new EditAddressViewModel();
            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0) {
                _addressService.UpdateAddressForContentItem(part.ContentItem, model);
            }

            return Editor(part, shapeHelper);
        }

        private EditAddressViewModel BuildEditorViewModel(AddressPart part) {
            var avm = new EditAddressViewModel {
                Address = part.Address,
                City = part.City,
                Zip = part.Zip,
                States = _addressService.GetStates()
            };
            if (part.State != null) {
                avm.StateCode = part.State.Code;
                avm.StateName = part.State.Name;
            }
            return avm;
        }
    }
}