#region

using System.Collections.Specialized;
using System.Configuration;

#endregion

namespace ExceptionHandler.Settings
{
    public class ExceptionManagerConfiguration : IConfigurationManager
    {
        private readonly NameValueCollection _appSettings;

        public ExceptionManagerConfiguration()
        {
            _appSettings = ConfigurationManager.AppSettings;
        }

        #region IConfigurationManager Members

        public string Get(string property)
        {
            return _appSettings.Get(property);
        }

        #endregion
    }
}