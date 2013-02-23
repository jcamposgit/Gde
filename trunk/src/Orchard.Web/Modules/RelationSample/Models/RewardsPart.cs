using System.Collections.Generic;
using System.Linq;
using Orchard.ContentManagement;

namespace RelationSample.Models {
    public class RewardsPart : ContentPart<RewardsPartRecord> {
        public IEnumerable<RewardProgramRecord> Rewards {
            get {
                return Record.Rewards.Select(r => r.RewardProgramRecord);
            }
        }
    }
}