using System.Collections.Generic;
using RelationSample.Models;

namespace RelationSample.ViewModels {
    public class EditRewardsViewModel {
        public IList<RewardProgramEntry> Rewards { get; set; }
    }

    public class RewardProgramEntry {
        public RewardProgramRecord RewardProgram { get; set; }
        public bool IsChecked { get; set; }
    }
}