using System.Collections.Generic;
using Orchard.ContentManagement.Records;

namespace RelationSample.Models {
    public class RewardsPartRecord : ContentPartRecord {
        public RewardsPartRecord() {
            Rewards = new List<ContentRewardProgramsRecord>();
        }
        public virtual IList<ContentRewardProgramsRecord> Rewards { get; set; }
    }
}