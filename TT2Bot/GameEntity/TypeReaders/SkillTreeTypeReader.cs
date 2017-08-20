using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.TypeReaders
{
    class SkillTreeTypeReader : GameEntityTypeReader<Skill>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Skill> Service => DataService.SkillTree;
        protected override string UNABLE_DOWNLOAD => Skill.Localisation.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => Skill.Localisation.MULTIPLE_MATCHES;

        public SkillTreeTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}
