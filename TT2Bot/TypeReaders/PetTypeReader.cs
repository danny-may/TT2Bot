using System;
using System.Threading.Tasks;
using TitanBot.Commands;
using TitanBot.TypeReaders;
using TT2Bot.Helpers;
using TT2Bot.Models;

namespace TT2Bot.TypeReaders
{
    class PetTypeReader : TypeReader
    {
        private TT2DataService DataService { get; }

        public PetTypeReader(TT2DataService dataService)
        {
            DataService = dataService;
        }

        public override async Task<TypeReaderResponse> Read(ICommandContext context, string value)
        {
            var pet = Pet.Find(value);
            if (pet == null)
                return TypeReaderResponse.FromError("TYPEREADER_UNABLETOREAD", value, typeof(Pet));

            var petData = await DataService.GetPet(pet);
            if (petData == null)
                return TypeReaderResponse.FromError("Could not download data for pet `{2}`", "#" + pet.Id.ToString(), typeof(Pet));
            return TypeReaderResponse.FromSuccess(petData);
        }
    }
}
