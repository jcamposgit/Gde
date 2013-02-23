using System;
using System.Collections.Generic;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data;
using Orchard.Data.Migration;
using RelationSample.Models;

namespace RelationSample {
    public class RelationSampleDataMigration : DataMigrationImpl {
        private readonly IRepository<StateRecord> _stateRepository;
        private readonly IRepository<RewardProgramRecord> _rewardProgramRepository;

        private readonly IEnumerable<StateRecord> _states =
            new List<StateRecord> {
                new StateRecord {Code = "AL", Name = "Alabama"},
                new StateRecord {Code = "AK", Name = "Alaska"},
                new StateRecord {Code = "AZ", Name = "Arizona"},
                new StateRecord {Code = "AR", Name = "Arkansas"},
                new StateRecord {Code = "CA", Name = "California"},
                new StateRecord {Code = "CO", Name = "Colorado"},
                new StateRecord {Code = "CT", Name = "Connecticut"},
                new StateRecord {Code = "DE", Name = "Delaware"},
                new StateRecord {Code = "FL", Name = "Florida"},
                new StateRecord {Code = "GA", Name = "Georgia"},
                new StateRecord {Code = "HI", Name = "Hawaii"},
                new StateRecord {Code = "ID", Name = "Idaho"},
                new StateRecord {Code = "IL", Name = "Illinois"},
                new StateRecord {Code = "IN", Name = "Indiana"},
                new StateRecord {Code = "IA", Name = "Iowa"},
                new StateRecord {Code = "KS", Name = "Kansas"},
                new StateRecord {Code = "KY", Name = "Kentucky"},
                new StateRecord {Code = "LA", Name = "Louisiana"},
                new StateRecord {Code = "ME", Name = "Maine"},
                new StateRecord {Code = "MD", Name = "Maryland"},
                new StateRecord {Code = "MA", Name = "Massachusetts"},
                new StateRecord {Code = "MI", Name = "Michigan"},
                new StateRecord {Code = "MN", Name = "Minnesota"},
                new StateRecord {Code = "MS", Name = "Mississippi"},
                new StateRecord {Code = "MO", Name = "Missouri"},
                new StateRecord {Code = "MT", Name = "Montana"},
                new StateRecord {Code = "NE", Name = "Nebraska"},
                new StateRecord {Code = "NV", Name = "Nevada"},
                new StateRecord {Code = "NH", Name = "New Hampshire"},
                new StateRecord {Code = "NJ", Name = "New Jersey"},
                new StateRecord {Code = "NM", Name = "New Mexico"},
                new StateRecord {Code = "NY", Name = "New York"},
                new StateRecord {Code = "NC", Name = "North Carolina"},
                new StateRecord {Code = "ND", Name = "North Dakota"},
                new StateRecord {Code = "OH", Name = "Ohio"},
                new StateRecord {Code = "OK", Name = "Oklahoma"},
                new StateRecord {Code = "OR", Name = "Oregon"},
                new StateRecord {Code = "PA", Name = "Pennsylvania"},
                new StateRecord {Code = "RI", Name = "Rhode Island"},
                new StateRecord {Code = "SC", Name = "South Carolina"},
                new StateRecord {Code = "SD", Name = "South Dakota"},
                new StateRecord {Code = "TN", Name = "Tennessee"},
                new StateRecord {Code = "TX", Name = "Texas"},
                new StateRecord {Code = "UT", Name = "Utah"},
                new StateRecord {Code = "VT", Name = "Vermont"},
                new StateRecord {Code = "VA", Name = "Virginia"},
                new StateRecord {Code = "WA", Name = "Washington"},
                new StateRecord {Code = "WV", Name = "West Virginia"},
                new StateRecord {Code = "WI", Name = "Wisconsin"},
                new StateRecord {Code = "WY", Name = "Wyoming"},
                new StateRecord {Code = "AB", Name = "Alberta"},
                new StateRecord {Code = "BC", Name = "British Columbia"},
                new StateRecord {Code = "MB", Name = "Manitoba"},
                new StateRecord {Code = "NB", Name = "New Brunswick"},
                new StateRecord {Code = "NF", Name = "Newfoundland and Labrador"},
                new StateRecord {Code = "NT", Name = "Northwest Territories"},
                new StateRecord {Code = "NS", Name = "Nova Scotia"},
                new StateRecord {Code = "NU", Name = "Nunavut"},
                new StateRecord {Code = "ON", Name = "Ontario"},
                new StateRecord {Code = "PE", Name = "Prince Edward Island"},
                new StateRecord {Code = "PQ", Name = "Quebec"},
                new StateRecord {Code = "SK", Name = "Saskatchewan"},
                new StateRecord {Code = "YT", Name = "Yukon Territory"},
                new StateRecord {Code = "AC", Name = "Australian Capital Territory"},
                new StateRecord {Code = "NW", Name = "New South Wales"},
                new StateRecord {Code = "NO", Name = "Northern Territory"},
                new StateRecord {Code = "QL", Name = "Queensland"},
                new StateRecord {Code = "SA", Name = "South Australia"},
                new StateRecord {Code = "TS", Name = "Tasmania"},
                new StateRecord {Code = "VC", Name = "Victoria"},
                new StateRecord {Code = "WS", Name = "Western Australia"},
            };

        private readonly IEnumerable<RewardProgramRecord> _rewardPrograms =
            new List<RewardProgramRecord> {
                new RewardProgramRecord {Name = "Senior", Discount = 0.05},
                new RewardProgramRecord {Name = "Family", Discount = 0.10},
                new RewardProgramRecord {Name = "Member", Discount = 0.15},
            };

        public RelationSampleDataMigration(
            IRepository<StateRecord> stateRepository,
            IRepository<RewardProgramRecord> rewardProgramRepositoryRepository) {

            _stateRepository = stateRepository;
            _rewardProgramRepository = rewardProgramRepositoryRepository;
        }

        public int Create() {
            SchemaBuilder.CreateTable("AddressPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<string>("Address")
                    .Column<string>("City")
                    .Column<int>("StateRecord_Id")
                    .Column<string>("Zip")
                );

            SchemaBuilder.CreateTable("StateRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Code", column => column.WithLength(2))
                    .Column<string>("Name")
                );

            ContentDefinitionManager.AlterPartDefinition("AddressPart", builder => builder.Attachable());

            return 1;
        }

        public int UpdateFrom1() {
            if (_stateRepository == null) throw new InvalidOperationException("Couldn't find state repository.");
            foreach (var state in _states) {
                _stateRepository.Create(state);
            }
            return 2;
        }

        public int UpdateFrom2() {
            SchemaBuilder.CreateTable("RewardsPartRecord",
                table => table
                    .ContentPartRecord()
                );

            SchemaBuilder.CreateTable("RewardProgramRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<string>("Name")
                    .Column<double>("Discount")
                );

            SchemaBuilder.CreateTable("ContentRewardProgramsRecord",
                table => table
                    .Column<int>("Id", column => column.PrimaryKey().Identity())
                    .Column<int>("RewardsPartRecord_Id")
                    .Column<int>("RewardProgramRecord_Id")
                );

            ContentDefinitionManager.AlterPartDefinition("RewardsPart", builder => builder.Attachable());

            return 3;
        }

        public int UpdateFrom3() {
            if (_rewardProgramRepository == null) throw new InvalidOperationException("Couldn't find reward program repository.");
            foreach (var rewardProgram in _rewardPrograms) {
                _rewardProgramRepository.Create(rewardProgram);
            }
            return 4;
        }

        public int UpdateFrom4() {

            SchemaBuilder.CreateTable("SponsorPartRecord",
                table => table
                    .ContentPartRecord()
                    .Column<int>("Sponsor_Id")
                );

            ContentDefinitionManager.AlterPartDefinition("SponsorPart", builder => builder.Attachable());

            return 5;
        }
    }
}