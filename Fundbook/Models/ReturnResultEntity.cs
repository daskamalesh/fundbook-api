using System;
using System.Collections.Generic;
using System.ComponentModel; // Required for INotifyPropertyChanged

namespace Fundbook.Models
{
    public class ReturnResultEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _ResultKey;
        private string _ResultValue;
        #endregion

        #region "Constructor"
        public ReturnResultEntity()
        {
            this._ResultKey = string.Empty;
            this._ResultValue = string.Empty;
        }
        #endregion

        #region "Properties"
        public string ResultKey
        {
            get { return this._ResultKey; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._ResultKey, value);
            }
        }

        public string ResultValue
        {
            get { return this._ResultValue; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._ResultValue, value);
            }
        }
        #endregion

        #region "Property Change Notification"
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false; // Value is the same, no need to notify.
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
