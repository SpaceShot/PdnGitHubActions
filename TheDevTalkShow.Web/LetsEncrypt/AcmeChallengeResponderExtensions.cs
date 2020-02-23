using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace Innofactor.Acme
{
    public static class AcmeChallengeResponderExtensions
    {
        private const string WellKnownFolder = ".well-known";
        private const string WellKnownRequestPath = "/.well-known";
        private const string WellKnownContentType = "text/plain";

        public static IApplicationBuilder UseAcmeChallengeResponder(this IApplicationBuilder app, string rootFolderPath)
        {
            if (rootFolderPath == null) throw new ArgumentNullException(nameof(rootFolderPath));

            var root = new DirectoryInfo(rootFolderPath);
            if (!root.Exists)
            {
                throw new ArgumentException("The provided folder does not exist");
            }

            var wellKnownFolder = root.CreateSubdirectory(WellKnownFolder);

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = WellKnownRequestPath,
                FileProvider = new PhysicalFileProvider(wellKnownFolder.FullName),
                ServeUnknownFileTypes = true,
                DefaultContentType = WellKnownContentType
            });

            return app;
        }
    }
}