using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.Helpers;

namespace TT2Bot.Models
{
    public abstract class GameEntity<TId> : ILocalisable<string>
    {
        public TId Id { get; protected set; }
        public LocalisedString Name => GetName(Id);

        protected abstract LocalisedString GetName(TId id);

        public virtual bool Matches(ITextResourceCollection textResource, string text)
        {
            var name = Name.Localise(textResource).ToLower();
            text = text.ToLower();
            return text == Id.ToString() ||
                   name == text ||
                   name.Without(" ") == text.Without(" ") ||
                   name.StartsWith(text) ||
                   name.Without(" ").StartsWith(text.Without(" "));
        }


        object ILocalisable.Localise(ITextResourceCollection textResource)
            => Localise(textResource);
        public virtual string Localise(ITextResourceCollection textResource)
            => $"{Name.Localise(textResource)} ({Id})";
    }
}
