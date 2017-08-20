using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Formatting;
using static TT2Bot.TT2Localisation;

namespace TT2Bot.Models.TT2
{
    class Skill : GameEntity<string>
    {
        public static IReadOnlyDictionary<int, ArtifactTier> Tiers { get; }

        public string Note { get; }
        public string RequirementKey { get; }
        public Skill Requirement => AllSkills.FirstOrDefault(s => s.Id == RequirementKey);
        private List<Skill> AllSkills { get; }
        public int StageRequirement { get; }
        public (int Cost, double Bonus)[] Levels { get; }
        public int TotalCost => Levels.Sum(l => l.Cost);
        public double MaxBonus => Levels.Last().Bonus;
        public string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        public Bitmap Image => _image.Value;
        private Lazy<Bitmap> _image { get; }

        public Skill(string id,
                     string note,
                     string requirement,
                     List<Skill> allSkills,
                     int stageRequirement,
                     List<(int Cost, double Bonus)> levels,
                     string fileVersion,
                     Func<string, ValueTask<Bitmap>> imageGetter = null)
        {
            Id = id;
            RequirementKey = requirement;
            StageRequirement = StageRequirement;
            AllSkills = allSkills;
            Levels = levels?.ToArray();
            FileVersion = fileVersion;
            _image = new Lazy<Bitmap>(() => imageGetter?.Invoke(ImageUrl).Result);
        }

        public override string ToString()
        {
            return Id;
        }

        protected override LocalisedString GetName(string id)
            => Game.SkillTree.GetName(id);
    }
}
