using Windows.UI.Xaml;
using Findier.Core.Windows.Helpers;

namespace Findier.Windows.CustomTriggers
{
    public class DeviceFamilyTrigger : StateTriggerBase
    {
        //private variables
        private DeviceFamily _queriedDeviceFamily;
        //Public property
        public DeviceFamily DeviceFamily
        {
            get { return _queriedDeviceFamily; }
            set
            {
                _queriedDeviceFamily = value;
                SetActive(DeviceHelper.IsType(_queriedDeviceFamily));
            }
        }
    }
}