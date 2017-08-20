using TT2Bot.Models.TT2;
using TT2Bot.Services;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.TypeReaders
{
    class SkillTreeTypeReader : GameEntityTypeReader<Skill, string>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Skill, string> Service => DataService.SkillTree;
        protected override string UNABLE_DOWNLOAD => TT2Localisation.Game.SkillTree.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => TT2Localisation.Game.SkillTree.MULTIPLE_MATCHES;

        public SkillTreeTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}
