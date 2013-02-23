using System.Linq;
using JetBrains.Annotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using RelationSample.Models;
using RelationSample.Services;
using RelationSample.ViewModels;

namespace RelationSample.Drivers {
    [UsedImplicitly]
    public class RewardsPartDriver : ContentPartDriver<RewardsPart> {
        private readonly IRewardService _rewardService;

        private const string TemplateName = "Parts/Rewards";

        public RewardsPartDriver(IRewardService rewardService) {
            _rewardService = rewardService;
        }

        protected override string Prefix {
            get { return "Rewards"; }
        }

        protected override DriverResult Display(RewardsPart part, string displayType, dynamic shapeHelper) {
            return ContentShape("Parts_Rewards",
                            () => shapeHelper.Parts_Rewards(
                                ContentPart: part,
                                Rewards: part.Rewards));
        }

        protected override DriverResult Editor(RewardsPart part, dynamic shapeHelper) {
            return ContentShape("Parts_Rewards_Edit",
                    () => shapeHelper.EditorTemplate(
                        TemplateName: TemplateName,
                        Model: BuildEditorViewModel(part),
                        Prefix: Prefix));
        }

        protected override DriverResult Editor(RewardsPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new EditRewardsViewModel();
            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0) {
                _rewardService.UpdateRewardsForContentItem(part.ContentItem, model.Rewards);
            }

            return Editor(part, shapeHelper);
        }

        private EditRewardsViewModel BuildEditorViewModel(RewardsPart part) {
            var itemRewards = part.Rewards.ToLookup(r => r.Id);
            return new EditRewardsViewModel {
                Rewards = _rewardService.GetRewards().Select(r => new RewardProgramEntry {
                    RewardProgram = r,
                    IsChecked = itemRewards.Contains(r.Id)
                }).ToList()
            };
        }
    }
}