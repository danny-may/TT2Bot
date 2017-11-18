using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
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
        public virtual LocalisedString Abbreviations { get; } = null;
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

        public virtual double MatchCertainty(ITextResourceCollection textResource, string text)
        {
            text = text.ToLower();
            var id = Id.ToString().ToLower();
            var name = Name.Localise(textResource).ToLower();
            var abbrev = Abbreviations?.Localise(textResource).ToLower().Split(',');

            return new List<double>
            {
                id == text ? 0.95 : 0,
                name == text ? 0.9 : 0,
                abbrev?.Any(a => a == text) ?? false ? 0.9 : 0,
                id.StartsWith(text) ? 0.75 : 0,
                name.StartsWith(text) ? 0.7 : 0,
                id.Contains(text) ? 0.55 : 0,
                name.Contains(text) ? 0.5 : 0,
                id.Without(" ") == text.Without(" ") ? 0.35 : 0,
                name.Without(" ") == text.Without(" ") ? 0.3 : 0,
                id.Without(" ").StartsWith(text.Without(" ")) ? 0.15 : 0,
                name.Without(" ").StartsWith(text.Without(" ")) ? 0.1 : 0,
            }.Max();
        }

        public override string ToString()
            => Id.ToString();

        public virtual string Localise(ITextResourceCollection textResource)
            => $"**{Name.Localise(textResource)}** ({Id})";
        object ILocalisable.Localise(ITextResourceCollection textResource)
            => Localise(textResource);
    }

    public abstract class GameEntity<TId> : GameEntity
    {
        new public TId Id { get => (TId)base.Id; set => base.Id = value; }

        private static HttpClient _client = new HttpClient();

        //public override string ImageUrl => ImageUrls.TryGetValue(Id, out var url) ? url : null;
        //
        //protected static IReadOnlyDictionary<TId, string> ImageUrls { get; set; }
        //    = new Dictionary<TId, string>().ToImmutableDictionary();

        protected static string Imgur(string imageId)
            => $"http://i.imgur.com/{imageId}.png";

        protected static string Cockleshell(string id)
            => $"http://www.cockleshell.org/static/TT2/img/{id}.png";

        //Forgive me, this is a quick and VERY DIRTY way to do the image urls
        //I wont do it like this in the rewrite
        protected static TResult DownloadImgurAlbum<TResult>(string albumId, string clientId, Func<JToken[], TResult> selector)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.imgur.com/3/album/{albumId}/images");
            request.Headers.Add("Authorization", $"Client-ID {clientId}");

            var response = _client.SendAsync(request).Result;

            var data = JObject.Parse(response.Content.ReadAsStringAsync().Result)["data"].ToArray();

            return selector(data);
        }
    }
}