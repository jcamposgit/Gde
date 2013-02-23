using System.Linq;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using RelationSample.Models;
using RelationSample.Services;
using RelationSample.ViewModels;

namespace RelationSample.Drivers {
    [UsedImplicitly]
    public class SponsorPartDriver : ContentPartDriver<SponsorPart> {
        private readonly ICustomerService _customerService;

        private const string TemplateName = "Parts/Sponsor";

        public SponsorPartDriver(ICustomerService customerService) {
            _customerService = customerService;
        }

        protected override string Prefix {
            get { return "Sponsor"; }
        }

        protected override DriverResult Display(SponsorPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Sponsor",
                            () => shapeHelper.Parts_Sponsor(
                                ContentPart: part,
                                Sponsor: part.Sponsor,
                                SponsorName: _customerService.GetCustomerName(part.Sponsor)
                                ));
        }

        protected override DriverResult Editor(SponsorPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Sponsor_Edit",
                    () => shapeHelper.EditorTemplate(
                        TemplateName: TemplateName,
                        Model: BuildEditorViewModel(part),
                        Prefix: Prefix));
        }

        protected override DriverResult Editor(SponsorPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new EditSponsorViewModel();
            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0) {
                _customerService.UpdateSponsorForContentItem(part.ContentItem, model);
            }

            return Editor(part, shapeHelper);
        }

        private EditSponsorViewModel BuildEditorViewModel(SponsorPart part) {
            var itemSponsor = new EditSponsorViewModel {
                CustomerId = part.ContentItem.Id,
                Customers = _customerService.GetCustomers()
            };
            if (part.Sponsor != null) {
                itemSponsor.SponsorId = part.Sponsor.Id;
            }
            return itemSponsor;
        }
    }
}