using TT2Bot.GameEntity.Base;
using TT2Bot.GameEntity.Entities;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.TypeReaders
{
    class ArtifactTypeReader : GameEntityTypeReader<Artifact>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Artifact> Service => DataService.Artifacts;
        protected override string UNABLE_DOWNLOAD => Artifact.Localisation.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => Artifact.Localisation.MULTIPLE_MATCHES;

        public ArtifactTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}
