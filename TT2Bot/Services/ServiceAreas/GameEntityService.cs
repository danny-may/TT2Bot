using Csv;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Downloader;
using TT2Bot.Models;

namespace TT2Bot.Services.ServiceAreas
{
    abstract class GameEntityService<TResource, TId> where TResource : GameEntity<TId>
    {
        protected abstract string FilePath { get; }
        protected abstract string FileVersion { get; }
        protected string CachedRaw { get; private set; }
        protected TResource[] CachedObjects { get; private set; }

        protected TT2GlobalSettings Settings => _settings();
        private Func<TT2GlobalSettings> _settings { get; }
        protected IDownloader WebClient { get; }

        internal GameEntityService(Func<TT2GlobalSettings> settings, IDownloader webClient)
        {
            _settings = settings;
            WebClient = webClient;
        }

        protected abstract TResource Build(ICsvLine row, string version);


        public virtual async ValueTask<TResource> Get(TId id)
        {
            await GetAll();
            return CachedObjects.FirstOrDefault(a => a.Id.Equals(id));
        }

        public virtual async ValueTask<TResource[]> GetAll()
        {
            if (CachedObjects == null || CachedObjects.Length == 0)
                await DeferredUpdate();
            else
                DeferredUpdate().DontWait();

            return CachedObjects;
        }

        protected virtual async Task DeferredUpdate()
        {
            var version = FileVersion;
            if (string.IsNullOrWhiteSpace(version))
                version = Settings.DefaultVersion;

            var data = await WebClient.GetString(new Uri(TT2DataService.GHStatic + version + FilePath));

            if (data == null || data == CachedRaw)
                return;

            var objects = CsvReader.ReadFromText(data).Select(r => Build(r, version));
            CachedRaw = data;
            CachedObjects = objects.ToArray();
        }

        protected ValueTask<Bitmap> GetImage(string url)
            => GetImage(url, 5);
        protected async ValueTask<Bitmap> GetImage(string url, int retries = 1)
        {
            if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
                return new Bitmap(1, 1);
            try
            {
                return await WebClient.GetImage(new Uri(url));
            }
            catch
            {
                if (retries > 0)
                {
                    WebClient.HardReset(new Uri(url));
                    return await GetImage(url, --retries);
                }

                return null;
            }
        }
    }
}