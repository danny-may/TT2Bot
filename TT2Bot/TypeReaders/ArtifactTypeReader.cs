using System;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.Contexts;
using TitanBot.TypeReaders;
using TT2Bot.Helpers;
using TT2Bot.Models;
using TT2Bot.Services;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.TypeReaders
{
    class ArtifactTypeReader : GameEntityTypeReader<Artifact, int>
    {
        private TT2DataService DataService { get; }

        protected override GameEntityService<Artifact, int> Service => DataService.Artifacts;
        protected override string UNABLE_DOWNLOAD => TT2Localisation.Game.Artifact.UNABLE_DOWNLOAD;
        protected override string MULTIPLE_MATCHES => TT2Localisation.Game.Artifact.MULTIPLE_MATCHES;

        public ArtifactTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }
    }
}
