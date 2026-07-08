using System;
using System.Collections.Generic;
using System.ComponentModel; // Required for INotifyPropertyChanged

namespace Fundbook.Models
{
    public class RequestDataEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        //private string _ResultKey;
        //private string _ResultValue;
        #endregion

        #region "Constructor"
        public RequestDataEntity()
        {
            //this._ResultKey = string.Empty;
            //this._ResultValue = string.Empty;
        }
        public int LoginRoleID { get; set; }
        public Boolean showActiveOnly { get; set; }
        #endregion

        #region "Properties"
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

    public class DeleteDataEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        #endregion

        #region "Constructor"
        public DeleteDataEntity()
        {
        }
        public int ID { get; set; }
        #endregion

        #region "Properties"
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

    public class ItemPostDataEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        //private string _ResultKey;
        //private string _ResultValue;
        #endregion

        #region "Constructor"
        public ItemPostDataEntity()
        {
            //this._ResultKey = string.Empty;
            //this._ResultValue = string.Empty;
        }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        #endregion

        #region "Properties"
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

    public class UpdateCredDataEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _NewUserName;
        private string _NewPassword;
        #endregion

        #region "Constructor"
        public UpdateCredDataEntity()
        {
            this._NewUserName = string.Empty;
            this._NewPassword = string.Empty;
        }
        public int UserID { get; set; }
        #endregion

        #region "Properties"
        public string NewUserName
        {
            get { return this._NewUserName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._NewUserName, value);
            }
        }
        public string NewPassword
        {
            get { return this._NewPassword; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._NewPassword, value);
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

    public class ReqPostDataEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        #endregion

        #region "Constructor"
        public ReqPostDataEntity()
        {
        }
        public int ID { get; set; }
        #endregion

        #region "Properties"
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
