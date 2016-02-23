﻿using System;

namespace Findier.Windows.Engine
{
    public class ChangedEventArgs<TValue> : EventArgs
    {
        private readonly TValue oldValue;
        private readonly TValue newValue;

        public ChangedEventArgs(TValue oldValue, TValue newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public TValue OldValue => this.oldValue;

        public TValue NewValue => this.newValue;
    }
}