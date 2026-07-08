using System;
using System.Collections.Generic;
using System.ComponentModel; // Required for INotifyPropertyChanged

namespace Fundbook.Models
{
    public class ItemEntity : INotifyPropertyChanged
    {
        #region "Define Default Values"
        private string _Description;
        private string _Date;
        private string _ImportDate;
        private string _UserName;
        private string _DonationUserName;
        private string _ShowActiveOnly;
        private string _TransactionType;
        #endregion

        #region "Constructor"
        public ItemEntity()
        {
            this._Description = string.Empty;
            this._Date = string.Empty;
            this._ImportDate = string.Empty;
            this._UserName = string.Empty;
            this._DonationUserName = string.Empty;
            this._ShowActiveOnly = string.Empty;
            this._TransactionType = String.Empty;
        }
        #endregion
        public int UserID { get; set; }
        public int DonationUserID { get; set; }
        public int ID { get; set; }
        //public int GroupID { get; set; }
        public int MonthID { get; set; }
        public int TransactionTypeID { get; set; }
        public decimal Amount { get; set; }
        public bool IsActive { get; set; }
        public bool IsAccepted { get; set; }
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

        public string Date
        {
            get { return this._Date; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._Date, value);
            }
        }
        public string ImportDate
        {
            get { return this._ImportDate; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._ImportDate, value);
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
        public string DonationUserName
        {
            get { return this._DonationUserName; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._DonationUserName, value);
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
        public string TransactionType
        {
            get { return this._TransactionType; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._TransactionType, value);
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

    public class ItemSummaryEntity
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int MonthID { get; set; }
        public decimal TotalAmountDebited { get; set; }
        public decimal TotalAmountCredited { get; set; }
        public decimal TotalExpenditure { get; set; }
    }

    public class ItemSettlementEntity
    {
        public int AmountToDevidedBy { get; set; }
        public decimal GrandTotalExpenditureAmount { get; set; }
        public decimal AmountPerHead { get; set; }
    }

    public class UserSettlementEntity
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public decimal SelfDebitedAmount { get; set; }
        public decimal AmountToTake { get; set; }
        public decimal AmountToGive { get; set; }
        public Boolean IsAcceptAmount { get; set; }
    }
}
