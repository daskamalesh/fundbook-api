using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel; // Required for INotifyPropertyChanged
//using Orbital.Utility.Common;

namespace Fundbook.Models
{
    public class RecordFilterEntity
    {
        #region "Define Default Value"   
        #endregion
        public RecordFilterEntity()
        {
        }
        #region "Properties"
        public int ID { get; set; }
        public Int16 ShowActiveOnly { get; set; }
        #endregion
    }
    public class CustomMessage
    {
        #region"Static Members"
        public static string messageUnabletoSaveData = "Unable to save data. Check log file for more info";
        public static int valueUnabletoSaveData = -1;
        public static string messageLoginAuthenticationFailed = "Login authentication failed";
        public static int valueLoginAuthenticationFailed = -99;
        public static string messageInvalid = "Internal Error";
        public static int valueInvalid = -9;
        #endregion
    }
    public class AuthenticationDetailEntity
    {
        private string _UserNameDefault = String.Empty;
        private string _FullNameDefault = String.Empty;
        private string _PasswordDefault = String.Empty;
        private string _MenuNameDefault = String.Empty;
        private string _RoutingPath = String.Empty;
        private string _IconCssClassDefault = String.Empty;
        private string _IconColorCssClassDefault = String.Empty;
        public int UserID { get; set; }
        public int UserLoginID { get; set; }
        public int LoginRoleID { get; set; }
        public int MenuID { get; set; }
        public AuthenticationDetailEntity()
        {
            this._UserNameDefault = string.Empty;
            this._FullNameDefault = string.Empty;
            this._PasswordDefault = string.Empty;
            this._MenuNameDefault = string.Empty;
            this._RoutingPath = string.Empty;
            this._IconCssClassDefault = string.Empty;
            this._IconColorCssClassDefault = string.Empty;
        }
        public string UserName
        {
            get { return this._UserNameDefault; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._UserNameDefault, value);
            }
        }
        public string FullName
        {
            get { return this._FullNameDefault; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._FullNameDefault, value);
            }
        }
        public string Password
        {
            get { return this._PasswordDefault; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._PasswordDefault, value);
            }
        }
        public string MenuName
        {
            get { return this._MenuNameDefault; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._MenuNameDefault, value);
            }
        }
        public string RoutingPath
        {
            get { return this._RoutingPath; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._RoutingPath, value);
            }
        }
        public string IconCssClass
        {
            get { return this._IconCssClassDefault; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._IconCssClassDefault, value);
            }
        }
        public string IconColorCssClass
        {
            get { return this._IconColorCssClassDefault; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._IconColorCssClassDefault, value);
            }
        }

        #region "INotifyPropertyChanged Implementation"
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false; // Value has not changed.
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
    public class MembershipFilterEntity : INotifyPropertyChanged
    {
        #region "Define Default Value"
        private string _UserName;
        private string _Password;
        private string _RemoteAddress;
        private string _UserAgent;
        #endregion

        #region "Constructor"
        public MembershipFilterEntity()
        {
            this._UserName = string.Empty;
            this._Password = string.Empty;
            this._RemoteAddress = string.Empty;
            this._UserAgent = string.Empty;
        }
        #endregion

        #region "Properties"
        public int LoginSessionID { get; set; }
        public int LoginRoleTypeID { get; set; }
        public int RunAsLive { get; set; }
        public int LoginHistoryID { get; set; }
        public int LoginUserID { get; set; }
        public int UserID { get; set; }

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

        public string Password
        {
            get { return this._Password; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._Password, value);
            }
        }

        public string RemoteAddress
        {
            get { return this._RemoteAddress; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._RemoteAddress, value);
            }
        }

        public string UserAgent
        {
            get { return this._UserAgent; }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                this.SetProperty(ref this._UserAgent, value);
            }
        }
        #endregion

            #region "INotifyPropertyChanged Implementation"
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false; // Value has not changed.
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
        

    }


    


}
