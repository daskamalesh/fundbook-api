using System;
using System.Collections.Generic;
using System.ComponentModel; // Required for INotifyPropertyChanged

namespace Fundbook.Models
{
    public class MasterEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _Description;
        private string _StartDate;
        private string _EndDate;
        private string _ShowActiveOnly;
        #endregion

        #region "Constructor"
        public MasterEntity()
        {
            this._Description = string.Empty;
            this._StartDate = string.Empty;
            this._EndDate = string.Empty;
            this._ShowActiveOnly = string.Empty;
        }
        #endregion
        //public int UserID { get; set; }
        public int ID { get; set; }
        //public int GroupID { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }
        #region "Properties"
        public string Description
        {
            get { return this._Description; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._Description, value);
            }
        }

        public string StartDate
        {
            get { return this._StartDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._StartDate, value);
            }
        }
        public string EndDate
        {
            get { return this._EndDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._EndDate, value);
            }
        }
        public string ShowActiveOnly
        {
            get { return this._ShowActiveOnly; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._ShowActiveOnly, value);
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

    public class TargetAmountEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _MonthName;
        #endregion
        public decimal Amount { get; set; }
        public int ID { get; set; }
        public int MonthID { get; set; }
        public bool IsActive { get; set; }
        public bool IsCurrent { get; set; }
        public TargetAmountEntity()
        {
            this._MonthName = string.Empty;
        }

        #region "Properties"
        public string MonthName
        {
            get { return this._MonthName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._MonthName, value);
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

    public class MenuEntity 
    {
        public int MenuID { get; set; }
        public int ParentMenuID { get; set; }
        public int LoginRoleID { get; set; }
        public string MenuName { get; set; }
        public string IconCSSClass { get; set; }
        public string AlternateIconCSSClass { get; set; }
        public string PageName { get; set; }
        public string BackColorCssClass { get; set; }
        public string IconColorCssClass { get; set; }
        public string DisplayText { get; set; }
    }

    public class QuantityTypeEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _QuantityType;
        #endregion
        public int ID { get; set; }
        public bool IsActive { get; set; }
        public QuantityTypeEntity()
        {
            this._QuantityType = string.Empty;
        }

        #region "Properties"
        public string QuantityType
        {
            get { return this._QuantityType; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._QuantityType, value);
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

    public class BackupEntity
    {
        public bool IsActive { get; set; }
        // Other properties and methods for the entity
    }

}
