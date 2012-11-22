namespace RelationSample.Models {
    public class ContentRewardProgramsRecord {
        public virtual int Id { get; set; }
        public virtual RewardsPartRecord RewardsPartRecord { get; set; }
        public virtual RewardProgramRecord RewardProgramRecord { get; set; }
    }
}