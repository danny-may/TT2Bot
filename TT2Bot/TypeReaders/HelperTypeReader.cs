using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.TypeReaders;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.TypeReaders
{
    class HelperTypeReader : TypeReader
    {
        private TT2DataService DataService { get; }

        public HelperTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }

        public override async ValueTask<TypeReaderResponse> Read(ICommandContext context, string value)
        {
            var helper = Helper.Find(value);
            if (helper == null)
                return TypeReaderResponse.FromError("TYPEREADER_UNABLETOREAD", value, typeof(Helper));

            var hero = await DataService.GetHelper(helper);
            if (hero == null)
                return TypeReaderResponse.FromError("Could not download data for helper `{2}`", "#" + helper.Id.ToString(), typeof(Helper));
            return TypeReaderResponse.FromSuccess(hero);
        }
    }
}
