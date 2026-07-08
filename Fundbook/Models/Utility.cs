using System;
using System.Collections.Generic;
using System.ComponentModel; // Required for INotifyPropertyChanged

namespace Fundbook.Models
{
    public class UtilityEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _EntryDate;
        private string _UserName;
        private string _ShowActiveOnly;
        #endregion

        #region "Constructor"
        public UtilityEntity()
        {
            this._EntryDate = string.Empty;
            this._UserName = string.Empty;
            this._ShowActiveOnly = string.Empty;
        }
        #endregion
        public int UserID { get; set; }
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int MonthID { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        #region "Properties"

        public string EntryDate
        {
            get { return this._EntryDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._EntryDate, value);
            }
        }
        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._UserName, value);
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

    public class ElectricEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _EntryDate;
        private string _FromDate;
        private string _ToDate;
        private string _Description;
        private string _UserName;
        private string _ShowActiveOnly;
        #endregion

        #region "Constructor"
        public ElectricEntity()
        {
            this._EntryDate = string.Empty;
            this._FromDate = string.Empty;
            this._ToDate = string.Empty;
            this._UserName = string.Empty;
            this._ShowActiveOnly = string.Empty;
        }
        #endregion
        public int UserID { get; set; }
        public int ID { get; set; }
        public decimal Unit { get; set; }
        public decimal Amount { get; set; }
        public int MonthID { get; set; }
        public bool IsActive { get; set; }
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
        public string EntryDate
        {
            get { return this._EntryDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._EntryDate, value);
            }
        }
        public string FromDate
        {
            get { return this._FromDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._FromDate, value);
            }
        }
        public string ToDate
        {
            get { return this._ToDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._ToDate, value);
            }
        }
        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._UserName, value);
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

    public class RentEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _EntryDate;
        private string _UserName;
        private string _ShowActiveOnly;
        private string _Month;
        #endregion

        #region "Constructor"
        public RentEntity()
        {
            this._EntryDate = string.Empty;
            this._UserName = string.Empty;
            this._ShowActiveOnly = string.Empty;
            this._Month = string.Empty;
        }
        #endregion
        public int UserID { get; set; }
        public int ID { get; set; }
        public int MonthID { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        #region "Properties"

        public string EntryDate
        {
            get { return this._EntryDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._EntryDate, value);
            }
        }
        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._UserName, value);
            }
        }
        public string Month
        {
            get { return this._Month; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._Month, value);
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
    public class GroceryEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _Description;
        private string _ShowActiveOnly;
        private string _UserName;
        private string _QuantityType;
        #endregion

        #region "Constructor"
        public GroceryEntity()
        {
            this._Description = string.Empty;
            this._UserName = string.Empty;
            this._ShowActiveOnly = string.Empty;
            this._QuantityType = string.Empty;
        }
        #endregion
        public int UserID { get; set; }
        public int ID { get; set; }
        public decimal Quantity { get; set; }
        public int QuantityTypeID { get; set; }
        public bool IsActive { get; set; }

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

        public string UserName
        {
            get { return this._UserName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._UserName, value);
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

    public class UtilityDashboardData
    {
        public List<UtilityEntity> Water { get; set; }
        public List<ElectricEntity> Electric { get; set; }
        public List<RentEntity> Rent { get; set; }
        public List<TargetAmountEntity> TargetAmount { get; set; }
    }
}
