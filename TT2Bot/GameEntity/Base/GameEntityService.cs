using Csv;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TitanBot.Downloader;
using TT2Bot.Models;
using TT2Bot.Models.General;
using TT2Bot.Services;

namespace TT2Bot.GameEntity.Base
{
    internal abstract class GameEntityService<TEntity> where TEntity : GameEntity
    {
        protected abstract string FilePath { get; }
        protected abstract string FileVersion { get; }
        protected string CachedRaw { get; private set; }
        protected List<TEntity> CachedObjects { get; private set; }

        protected TT2GlobalSettings Settings => _settings();
        private Func<TT2GlobalSettings> _settings { get; }
        protected IDownloader WebClient { get; }

        internal GameEntityService(Func<TT2GlobalSettings> settings, IDownloader webClient)
        {
            _settings = settings;
            WebClient = webClient;
        }

        protected abstract TEntity Build(Iterable<string> row, string version);

        private TEntity Build(ICsvLine row, string version)
            => Build(new Iterable<string>(Enumerable.Range(0, row.ColumnCount).Select(i => row[i])), version);

        public virtual async ValueTask<TEntity> Get(object id)
        {
            await GetAll();
            return CachedObjects.FirstOrDefault(a => a.Id.Equals(id));
        }

        public virtual async ValueTask<TEntity[]> GetAll()
        {
            if (CachedObjects == null || CachedObjects.Count == 0)
                await DeferredUpdate();
            else
                DeferredUpdate().DontWait();

            return CachedObjects.ToArray();
        }

        protected virtual async Task DeferredUpdate()
        {
            var version = FileVersion;
            if (string.IsNullOrWhiteSpace(version))
                version = Settings.DefaultVersion;

            var data = await WebClient.GetString(new Uri(TT2DataService.GHStatic + version + FilePath));

            if (data == null || data == CachedRaw)
                return;

            CachedObjects = new List<TEntity>();

            var readerOptions = new CsvOptions
            {
                RowsToSkip = 1,
                HeaderMode = HeaderMode.HeaderAbsent
            };

            var rows = CsvReader.ReadFromText(data, readerOptions).ToArray();

            var objects = rows.Select(r => Build(new Iterable<string>(Enumerable.Range(0, r.ColumnCount).Select(i => r[i]).GetEnumerator()), version))
                              .Where(e => e != null)
                              .ToArray();
            CachedRaw = data;
            CachedObjects.AddRange(objects);
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