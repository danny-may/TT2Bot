using System.Linq;
using System.Threading.Tasks;
using TitanBot;
using TitanBot.Contexts;
using TitanBot.Dependencies;
using TitanBot.TypeReaders;
using TT2Bot.GameEntity.Entities;
using TT2Bot.GameEntity.TypeReaders;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.Base
{
    public static class GameEntityTypeReader
    {
        public static void RegisterReaders(ITypeReaderCollection typeReaders, IDependencyFactory factory)
        {
            var dataService = factory.GetOrStore<TT2DataService>();
            typeReaders.AddTypeReader<Artifact>(new ArtifactTypeReader(dataService));
            typeReaders.AddTypeReader<Pet>(new PetTypeReader(dataService));
            typeReaders.AddTypeReader<Equipment>(new EquipmentTypeReader(dataService));
            typeReaders.AddTypeReader<Helper>(new HelperTypeReader(dataService));
            typeReaders.AddTypeReader<Skill>(new SkillTreeTypeReader(dataService));
        }
    }

    abstract class GameEntityTypeReader<TEntity> : TypeReader
        where TEntity : GameEntity
    {
        protected abstract GameEntityService<TEntity> Service { get; }
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
