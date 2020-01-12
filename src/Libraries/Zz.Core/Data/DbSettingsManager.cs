using Autofac.Engine;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace Zz.Core.Data
{
    public partial class DbSettingsManager
    {
        #region Fields
        private const string FILE_NAME = "dbSettings.json";
        #endregion

        #region Utilities
        private static string MapRootPath(string filename)
        {
            var env = EngineContext.Resolve<IHostingEnvironment>();
            var rootPath = env.ContentRootPath;

            return Path.Combine(rootPath ?? string.Empty, filename);
        }
        #endregion

        public static DbSettings Load()
        {
            var filePath = MapRootPath(FILE_NAME);
            if (!File.Exists(filePath))
                return new DbSettings();

            var contents = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(contents))
                return new DbSettings();

            return JsonConvert.DeserializeObject<DbSettings>(contents);
        }

        public static void Save(DbSettings settings)
        {
            if (settings == null)
                return;

            var filePath = MapRootPath(FILE_NAME);
            if (!File.Exists(filePath))
                using (File.Create(filePath)) { }

            var contents = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(filePath, contents, Encoding.UTF8);
        }
    }
}
