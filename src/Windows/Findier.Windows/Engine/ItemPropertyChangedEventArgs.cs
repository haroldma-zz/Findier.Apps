﻿using System.ComponentModel;

namespace Findier.Client.Windows.Engine
{
    public class ItemPropertyChangedEventArgs
    {
        public ItemPropertyChangedEventArgs(object item, int changedIndex, PropertyChangedEventArgs e)
        {
            this.ChangedItem = item;
            this.ChangedItemIndex = changedIndex;
            this.PropertyChangedArgs = e;
        }
        public object ChangedItem { get; set; }

        public int ChangedItemIndex { get; set; }

        public PropertyChangedEventArgs PropertyChangedArgs { get; set; }
    }
}
