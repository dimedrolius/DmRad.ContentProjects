using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace DmRad.ContentProjects.Common.Tools
{
    public static class Utils
    {
        public static T GetConfigValueByKey<T>(string key)
        {
            var result = default(T);
            if (!((IList) ConfigurationManager.AppSettings.AllKeys).Contains(key))
                return result;

            try
            {
                var sValue = ConfigurationManager.AppSettings[key];
                var converter = TypeDescriptor.GetConverter(typeof(T));
                result = (T)converter.ConvertFromString(sValue);
            }
            catch {}

            return result;
        }
    }
}
