using System.Linq;
using System.Threading.Tasks;
using TitanBot;
using TitanBot.Contexts;
using TitanBot.TypeReaders;
using TT2Bot.Models.TT2;
using TT2Bot.Services.ServiceAreas;

namespace TT2Bot.TypeReaders
{
    abstract class GameEntityTypeReader<TEntity, TId> : TypeReader
        where TEntity : GameEntity<TId>
    {
        protected abstract GameEntityService<TEntity, TId> Service { get; }
        protected abstract string UNABLE_DOWNLOAD { get; }
        protected abstract string MULTIPLE_MATCHES { get; }

        public override async ValueTask<TypeReaderResponse> Read(IMessageContext context, string value)
        {
            var entities = await Service.GetAll();

            if (entities == null || entities.Length == 0)
                return TypeReaderResponse.FromError(UNABLE_DOWNLOAD, value, typeof(TEntity));

            var matches = entities.Where(e => e.Matches(context.TextResource, value)).ToArray();
            if (matches.Length == 0)
                return TypeReaderResponse.FromError(TBLocalisation.Logic.TYPEREADER_UNABLETOREAD, value, typeof(TEntity));
            else if (matches.Length > 1)
                return TypeReaderResponse.FromError(MULTIPLE_MATCHES, value, typeof(TEntity));
            return TypeReaderResponse.FromSuccess(matches[0]);
        }
    }
}
