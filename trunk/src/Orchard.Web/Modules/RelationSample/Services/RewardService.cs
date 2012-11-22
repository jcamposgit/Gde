using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Data;
using RelationSample.Models;
using RelationSample.ViewModels;

namespace RelationSample.Services {
    public interface IRewardService : IDependency {
        void UpdateRewardsForContentItem(ContentItem item, IEnumerable<RewardProgramEntry> rewards);
        IEnumerable<RewardProgramRecord> GetRewards();
    }

    public class RewardService : IRewardService {
        private readonly IRepository<RewardProgramRecord> _rewardProgramRepository;
        private readonly IRepository<ContentRewardProgramsRecord> _contentRewardRepository;

        public RewardService(
            IRepository<RewardProgramRecord> rewardProgramRepository,
            IRepository<ContentRewardProgramsRecord> contentRewardRepository) {

            _rewardProgramRepository = rewardProgramRepository;
            _contentRewardRepository = contentRewardRepository;
        }

        public void UpdateRewardsForContentItem(ContentItem item, IEnumerable<RewardProgramEntry> rewards) {
            var record = item.As<RewardsPart>().Record;
            var oldRewards = _contentRewardRepository.Fetch(
                r => r.RewardsPartRecord == record);
            var lookupNew = rewards
                .Where(e => e.IsChecked)
                .Select(e => e.RewardProgram)
                .ToDictionary(r => r, r => false);
            // Delete the rewards that are no longer there and mark the ones that should stay
            foreach(var contentRewardProgramsRecord in oldRewards) {
                if (lookupNew.ContainsKey(contentRewardProgramsRecord.RewardProgramRecord)) {
                    lookupNew[contentRewardProgramsRecord.RewardProgramRecord] = true;
                }
                else {
                    _contentRewardRepository.Delete(contentRewardProgramsRecord);
                }
            }
            // Add the new rewards
            foreach(var reward in lookupNew.Where(kvp => !kvp.Value).Select(kvp => kvp.Key)) {
                _contentRewardRepository.Create(new ContentRewardProgramsRecord {
                    RewardsPartRecord = record,
                    RewardProgramRecord = reward
                });
            }
        }

        public IEnumerable<RewardProgramRecord> GetRewards() {
            return _rewardProgramRepository.Table.ToList();
        }
    }
}