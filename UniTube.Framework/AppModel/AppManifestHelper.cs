using System.Xml.Linq;

namespace UniTube.Framework.AppModel
{
    public static class AppManifestHelper
    {
        private static readonly XDocument manifest = XDocument.Load("AppxManifest.xml", LoadOptions.None);
        private static readonly XNamespace xNamespace = XNamespace.Get("http://schemas.microsoft.com/appx/manifest/foundation/windows10");

        /// <summary>
        /// Retrieves the Application Id from the AppxManifest.
        /// </summary>
        /// <returns>The Application Id.</returns>
        public static string GetApplicationId()
        {
            var applications = manifest.Descendants(xNamespace + "Application");
            foreach (var application in applications)
            {
                if (application.Attribute("Id") != null)
                    return application.Attribute("Id").Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Checks if the Search declaration was activated in the Package.appxmanifest.
        /// </summary>
        /// <returns><c>true</c> if Search is declared.</returns>
        public static bool IsSearchDeclared()
        {
            var extensions = manifest.Descendants(xNamespace + "Extension");
            foreach (var extension in extensions)
            {
                if (extension.Attribute("Category") != null && extension.Attribute("Category").Value == "Windows.search")
                    return true;
            }

            return false;
        }
    }
}
