using Android.Content;
using Findier.Core.Utilities.Interfaces;
using Newtonsoft.Json;

namespace Findier.Core.Android.Utilities
{
    public class SettingsUtility : ISettingsUtility
    {
        public SettingsUtility(ISharedPreferences preferences)
        {
            LocalSettings = preferences;
            RoamingSettings = preferences;
        }

        public SettingsUtility(ISharedPreferences localPreferences, ISharedPreferences roamingPreferences)
        {
            LocalSettings = localPreferences;
            RoamingSettings = roamingPreferences;
        }

        public ISharedPreferences LocalSettings { get; set; }

        public ISharedPreferences RoamingSettings { get; set; }

        public bool Exists(string key, SettingsStrategy strategy = SettingsStrategy.Local)
        {
            return Container(strategy).Contains(key);
        }

        public T Read<T>(string key, T otherwise, SettingsStrategy strategy = SettingsStrategy.Local)
        {
            var container = Container(strategy);
            var type = typeof (T);
            var defaultObject = (object)otherwise;
            object returnValue;

            if (type == typeof (int))
            {
                returnValue = container.GetInt(key, (int)defaultObject);
            }
            else if (type == typeof (long))
            {
                returnValue = container.GetLong(key, (long)defaultObject);
            }
            else if (type == typeof (float))
            {
                returnValue = container.GetFloat(key, (float)defaultObject);
            }
            else if (type == typeof (bool))
            {
                returnValue = container.GetBoolean(key, (bool)defaultObject);
            }
            else if (type == typeof (string))
            {
                returnValue = container.GetString(key, (string)defaultObject);
            }
            else
            {
                var json = container.GetString(key, null);
                returnValue = JsonConvert.DeserializeObject<T>(json);
            }

            return (T)returnValue;
        }

        public void Remove(string key, SettingsStrategy strategy = SettingsStrategy.Local)
        {
            Container(strategy).Edit().Remove(key);
        }

        public void Write<T>(string key, T value, SettingsStrategy strategy = SettingsStrategy.Local)
        {
            var type = value.GetType();
            var editor = LocalSettings.Edit();
            var valueObj = (object)value;

            if (type == typeof (int))
            {
                editor.PutInt(key, (int)valueObj);
            }
            else if (type == typeof (long))
            {
                editor.PutLong(key, (long)valueObj);
            }
            else if (type == typeof (float))
            {
                editor.PutFloat(key, (float)valueObj);
            }
            else if (type == typeof (bool))
            {
                editor.PutBoolean(key, (bool)valueObj);
            }
            else if (type == typeof (string))
            {
                editor.PutString(key, (string)valueObj);
            }
            else
            {
                var json = JsonConvert.SerializeObject(value);
                editor.PutString(key, json);
            }

            editor.Commit();
        }

        private ISharedPreferences Container(SettingsStrategy strategy)
        {
            return strategy == SettingsStrategy.Local
                ? LocalSettings
                : RoamingSettings;
        }
    }
}