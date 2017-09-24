using Windows.ApplicationModel.Resources;

namespace UniTube.Services
{
    public class LocalizedStrings
    {
        public string this[string key] => ResourceLoader.GetForViewIndependentUse().GetString(key);
    }
}
