using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Drawing;
using System.Threading.Tasks;
using TitanBot.Formatting;
using TitanBot.Formatting.Interfaces;
using TT2Bot.Helpers;

namespace TT2Bot.GameEntity.Base
{
    public abstract class GameEntity : ILocalisable<string>
    {
        public object Id { get; protected set; }
        public abstract LocalisedString Name { get; }
        public virtual Bitmap Image => _image.Value;
        public virtual string ImageUrl { get; }
        public string FileVersion { get; protected set; }
        public int? MaxLevel { get; protected set; }

        protected Func<string, ValueTask<Bitmap>> ImageGetter { get; set; }

        private Lazy<Bitmap> _image { get; set; }

        public GameEntity()
        {
            _image = new Lazy<Bitmap>(() => string.IsNullOrWhiteSpace(ImageUrl) ? null : ImageGetter?.Invoke(ImageUrl).Result);
        }

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

        public override string ToString()
            => Id.ToString();

        public virtual string Localise(ITextResourceCollection textResource)
            => $"{Name.Localise(textResource)} ({Id})";
        object ILocalisable.Localise(ITextResourceCollection textResource)
            => Localise(textResource);
    }

    public abstract class GameEntity<TId> : GameEntity
    {
        new public TId Id { get => (TId)base.Id; set => base.Id = value; }

        public override string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;

        protected static IReadOnlyDictionary<TId, string> ImageUrls { get; set; }
            = new Dictionary<TId, string>().ToImmutableDictionary();

        protected static string Imgur(string imageId)
            => $"http://i.imgur.com/{imageId}.png";

        protected static string Cockleshell(string id)
            => $"http://www.cockleshell.org/static/TT2/img/{id}.png";
    }
}
