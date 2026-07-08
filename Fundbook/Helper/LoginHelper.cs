using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Fundbook.Models;

namespace Fundbook.Helper
{
    public class LoginHelper
    {
        private readonly string _loginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Authentication.json");
        private readonly string _authDetailPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "AuthenticationDetail.json");
        private readonly string _electricPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Electric.json");
        private readonly string _itemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Item.json");
        private readonly string _rentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Rent.json");
        private readonly string _waterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Water.json");
        private readonly string _groceryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Grocery.json");

        public int AuthenticateUser(string username, string password)
        {
            int ReturnResult = 0;
            try
            {
                var usersJson = File.ReadAllText(_loginPath);
                var users = JsonConvert.DeserializeObject<List<MembershipFilterEntity>>(usersJson);

                if (users == null || users.Count == 0)
                {
                    ReturnResult = 0;
                }

                foreach (var user in users)
                {
                    if (user.UserName == username && user.Password == password)
                    {
                        ReturnResult = user.UserID;
                    }
                }
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object LoginAuthenticateDetail(Int32 loginuserid)
        {
            object authenticatedUserDetail = new object();
            try
            {
                var menusJson = File.ReadAllText(_authDetailPath);
                var menus = JsonConvert.DeserializeObject<List<AuthenticationDetailEntity>>(menusJson);

                if (menus == null || menus.Count == 0)
                {
                    return null;
                }
                foreach (var menu in menus)
                {
                    if (menu.UserID == loginuserid)
                    {
                        authenticatedUserDetail = menu;
                    }
                }

                //authenticatedUserDetail = users.FirstOrDefault(u => u.UserLoginID == loginuserid);
                return authenticatedUserDetail;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        private readonly string _menuDetailPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "AuthenticationMenuDetail.json");

        public List<MenuEntity> FetchLoginMenuRole(int LoginRoleID)
        {
            List<MenuEntity> MenuDetail = new List<MenuEntity>();
            try
            {
                var menusJson = File.ReadAllText(_menuDetailPath);
                var menus = JsonConvert.DeserializeObject<List<MenuEntity>>(menusJson);

                if (menus == null || menus.Count == 0)
                {
                    return MenuDetail;
                }

                MenuDetail = menus.Where(menu => menu.LoginRoleID == LoginRoleID).ToList();

                return MenuDetail;
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public int AddUser(MembershipFilterEntity newUser)
        {
            int ReturnResult = 0;
            try
            {
                List<MembershipFilterEntity> users = new List<MembershipFilterEntity>();

                if (File.Exists(_loginPath))  // Check if file exists
                {
                    var usersJson = File.ReadAllText(_loginPath);
                    users = JsonConvert.DeserializeObject<List<MembershipFilterEntity>>(usersJson) ?? new List<MembershipFilterEntity>();
                }

                if (newUser.UserID == 0)  // Assign unique UserID
                {
                    newUser.UserID = users.Any() ? users.Max(u => u.UserID) + 1 : 1;
                }
                else if (users.Any(u => u.UserID == newUser.UserID))  // Prevent duplicate IDs
                {
                    ReturnResult = -1;
                    return ReturnResult;
                }

                users.Add(newUser);

                var updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                File.WriteAllText(_loginPath, updatedJson);
                ReturnResult = newUser.UserID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateUserName(int userID, string newUserName)
        {
            try
            {
                if (!File.Exists(_loginPath) || !File.Exists(_authDetailPath) || !File.Exists(_groceryPath) || !File.Exists(_itemPath) || !File.Exists(_rentPath) || !File.Exists(_waterPath) || !File.Exists(_electricPath))
                {
                    return 0;
                }

                var userJson = File.ReadAllText(_loginPath);
                var detailJson = File.ReadAllText(_authDetailPath);
                var groceryJson = File.ReadAllText(_groceryPath);
                var itemJson = File.ReadAllText(_itemPath);
                var waterJson = File.ReadAllText(_waterPath);
                var electricJson = File.ReadAllText(_electricPath);
                var rentJson = File.ReadAllText(_rentPath);

                var users = JsonConvert.DeserializeObject<List<MembershipFilterEntity>>(userJson) ?? new List<MembershipFilterEntity>();
                var userDetails = JsonConvert.DeserializeObject<List<AuthenticationDetailEntity>>(detailJson) ?? new List<AuthenticationDetailEntity>();
                var waterDetails = JsonConvert.DeserializeObject<List<UtilityEntity>>(waterJson) ?? new List<UtilityEntity>();
                var groceryDetails = JsonConvert.DeserializeObject<List<GroceryEntity>>(groceryJson) ?? new List<GroceryEntity>();
                var itemDetails = JsonConvert.DeserializeObject<List<ItemEntity>>(itemJson) ?? new List<ItemEntity>();
                var rentDetails = JsonConvert.DeserializeObject<List<RentEntity>>(rentJson) ?? new List<RentEntity>();
                var electricDetails = JsonConvert.DeserializeObject<List<ElectricEntity>>(electricJson) ?? new List<ElectricEntity>();

                users.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });
                userDetails.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });
                waterDetails.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });
                groceryDetails.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });
                itemDetails.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });
                rentDetails.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });
                electricDetails.ForEach(user => { if (user.UserID == userID) user.UserName = newUserName; });

                File.WriteAllText(_loginPath, JsonConvert.SerializeObject(users, Formatting.Indented));
                File.WriteAllText(_authDetailPath, JsonConvert.SerializeObject(userDetails, Formatting.Indented));
                File.WriteAllText(_groceryPath, JsonConvert.SerializeObject(groceryDetails, Formatting.Indented));
                File.WriteAllText(_itemPath, JsonConvert.SerializeObject(itemDetails, Formatting.Indented));
                File.WriteAllText(_waterPath, JsonConvert.SerializeObject(waterDetails, Formatting.Indented));
                File.WriteAllText(_electricPath, JsonConvert.SerializeObject(electricDetails, Formatting.Indented));
                File.WriteAllText(_rentPath, JsonConvert.SerializeObject(rentDetails, Formatting.Indented));

                return userID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdatePassword(int userID, string newPassword)
        {
            try
            {
                if (!File.Exists(_loginPath))
                {
                    return 0; // File does not exist
                }

                var userJson = File.ReadAllText(_loginPath);
                var detailJson = File.ReadAllText(_authDetailPath);

                var users = JsonConvert.DeserializeObject<List<MembershipFilterEntity>>(userJson) ?? new List<MembershipFilterEntity>();
                var userDetails = JsonConvert.DeserializeObject<List<AuthenticationDetailEntity>>(detailJson) ?? new List<AuthenticationDetailEntity>();

                var userToEdit = users.FirstOrDefault(user => user.UserID == userID);
                var detailToEdit = userDetails.FirstOrDefault(user => user.UserID == userID);

                if (userToEdit == null || detailToEdit == null)
                {
                    return 0;
                }

                userToEdit.Password = newPassword;
                detailToEdit.Password = newPassword;

                var updatedUserJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                var updatedDetailJson = JsonConvert.SerializeObject(userDetails, Formatting.Indented);

                File.WriteAllText(_loginPath, updatedUserJson);
                File.WriteAllText(_authDetailPath, updatedDetailJson);

                return userID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AuthenticationDetailEntity> GetAllUser()
        {
            try
            {
                if (!File.Exists(_authDetailPath))
                {
                    return new List<AuthenticationDetailEntity>();
                }

                var UsersJson = File.ReadAllText(_authDetailPath);
                var users = JsonConvert.DeserializeObject<List<AuthenticationDetailEntity>>(UsersJson) ?? new List<AuthenticationDetailEntity>();

                if (users.Count > 0)
                {
                    users = users.Where(item => item.LoginRoleID == 1).ToList();
                }

                return users;
            }
            catch (Exception ex)
            {
                return new List<AuthenticationDetailEntity>();
                throw ex;
            }
        }
    }
}
