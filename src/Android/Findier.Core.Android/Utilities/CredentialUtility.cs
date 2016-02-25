using Findier.Core.Android.Helpers;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Core.Android.Utilities
{
    public class CredentialUtility : ICredentialUtility
    {
        private const string BasePrefix = "APP_CREDENTIAL_";
        private const string PasswordPrefix = BasePrefix + "PASSWORD_";
        private const string UsernamePrefix = BasePrefix + "USERNAME_";
        private readonly string _encryptionKey = "NOTsoSECR3T_hunter2";
        private readonly ISettingsUtility _settingsUtility;

        public CredentialUtility(ISettingsUtility settingsUtility)
        {
            _settingsUtility = settingsUtility;
        }

        public CredentialUtility(ISettingsUtility settingsUtility, string ecryptionKey)
        {
            _settingsUtility = settingsUtility;
            _encryptionKey = ecryptionKey;
        }

        public void DeleteCredentials(string resource)
        {
            _settingsUtility.Remove(UsernamePrefix + resource);
            _settingsUtility.Remove(PasswordPrefix + resource);
        }

        public AppCredential GetCredentials(string resource)
        {
            var username = _settingsUtility.Read(UsernamePrefix + resource, default(string));
            var password = _settingsUtility.Read(PasswordPrefix + resource, default(string));

            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                return null;
            }

            if (!string.IsNullOrEmpty(password))
            {
                password = CryptoHelper.Decrypt(password, _encryptionKey);
            }

            return new AppCredential(username, password);
        }

        public void SaveCredentials(string resource, string username, string password)
        {
            password = CryptoHelper.Encrypt(password, _encryptionKey);
            _settingsUtility.Write(UsernamePrefix + resource, username);
            _settingsUtility.Write(PasswordPrefix + resource, password);
        }
    }
}