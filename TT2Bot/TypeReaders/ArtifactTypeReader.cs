using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.TypeReaders;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.TypeReaders
{
    class ArtifactTypeReader : TypeReader
    {
        private TT2DataService DataService { get; }

        public ArtifactTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }

        public override async ValueTask<TypeReaderResponse> Read(ICommandContext context, string value)
        {
            var art = Artifact.Find(value);
            if (art == null)
                return TypeReaderResponse.FromError("TYPEREADER_UNABLETOREAD", value, typeof(Artifact));

            var artifact = await DataService.GetArtifact(art);
            if (artifact == null)
                return TypeReaderResponse.FromError("Could not download data for artifact `{2}`", "#" + art.Id.ToString(), typeof(Artifact));
            return TypeReaderResponse.FromSuccess(artifact);
        }
    }
}
