using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Fundbook.Models;

namespace Fundbook.Helper
{
    public class MasterHelper
    {
        private readonly string _addMonthPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "MonthMaster.json");
        private readonly string _addTargetAmountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "TargetAmountMaster.json");
        private readonly string _addQuantityTypePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "QuantityTypeMaster.json");

        #region 'Month'
        public int AddMonth(MasterEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<MasterEntity> items = new List<MasterEntity>();

                if (File.Exists(_addMonthPath))
                {
                    var itemsJson = File.ReadAllText(_addMonthPath);
                    items = JsonConvert.DeserializeObject<List<MasterEntity>>(itemsJson) ?? new List<MasterEntity>();
                }

                if (newItem.ID == 0)
                {
                    newItem.ID = items.Any() ? items.Max(i => i.ID) + 1 : 1;
                }
                else if (items.Any(i => i.ID == newItem.ID))
                {
                    ReturnResult = -1;
                }

                items.Add(newItem);

                var updatedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(_addMonthPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public MasterEntity GetMonthConfigureByID(int MonthID)
        {
            try
            {
                if (!File.Exists(_addMonthPath))
                {
                    return null;
                }

                var monthJson = File.ReadAllText(_addMonthPath);
                var months = JsonConvert.DeserializeObject<List<MasterEntity>>(monthJson) ?? new List<MasterEntity>();

                return months.FirstOrDefault(w => w.ID == MonthID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ConfigureMonth(MasterEntity updatedItem)
        {
            try
            {
                if (!File.Exists(_addMonthPath))
                {
                    return -1;
                }

                var MonthJson = File.ReadAllText(_addMonthPath);
                var months = JsonConvert.DeserializeObject<List<MasterEntity>>(MonthJson) ?? new List<MasterEntity>();

                var index = months.FindIndex(w => w.ID == updatedItem.ID);
                if (index == -1)
                {
                    return -1; // Item not found
                }

                months[index] = updatedItem;
                var updatedJson = JsonConvert.SerializeObject(months, Formatting.Indented);
                File.WriteAllText(_addMonthPath, updatedJson);

                return updatedItem.ID; // Successfully updated
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterEntity> GetAllMonths(bool showActiveOnly)
        {
            try
            {
                if (!File.Exists(_addMonthPath))
                {
                    return new List<MasterEntity>();
                }

                var itemsJson = File.ReadAllText(_addMonthPath);
                var items = JsonConvert.DeserializeObject<List<MasterEntity>>(itemsJson) ?? new List<MasterEntity>();

                if (showActiveOnly)
                {
                    items = items.Where(item => item.IsActive).ToList();
                }

                return items;
            }
            catch (Exception ex)
            {
                return new List<MasterEntity>();
                throw ex;
            }
        }
        #endregion

        #region 'Target Amount'
        public int AddTargetAmount(TargetAmountEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<TargetAmountEntity> items = new List<TargetAmountEntity>();
                List<MasterEntity> Months = new List<MasterEntity>();

                if (File.Exists(_addTargetAmountPath))
                {
                    var itemsJson = File.ReadAllText(_addTargetAmountPath);
                    items = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(itemsJson) ?? new List<TargetAmountEntity>();
                }

                if (File.Exists(_addMonthPath))
                {
                    var monthsJson = File.ReadAllText(_addMonthPath);
                    Months = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
                }

                if (newItem.ID == 0)
                {
                    newItem.ID = items.Any() ? items.Max(i => i.ID) + 1 : 1;

                    var monthEntity = Months.FirstOrDefault(i => i.ID == newItem.MonthID);
                    newItem.MonthName = monthEntity != null ? monthEntity.Description : string.Empty; // Set to empty string if not found
                }
                else if (items.Any(i => i.ID == newItem.ID))
                {
                    ReturnResult = -1;
                }

                items.Add(newItem);

                var updatedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(_addTargetAmountPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<TargetAmountEntity> GetAllTargetAmount(bool showActiveOnly)
        {
            try
            {
                if (!File.Exists(_addTargetAmountPath))
                {
                    return new List<TargetAmountEntity>();
                }

                var itemsJson = File.ReadAllText(_addTargetAmountPath);
                var items = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(itemsJson) ?? new List<TargetAmountEntity>();

                if (showActiveOnly)
                {
                    items = items.Where(item => item.IsActive).ToList();
                }

                return items;
            }
            catch (Exception ex)
            {
                return new List<TargetAmountEntity>();
                throw ex;
            }
        }
        public int DeleteTargetAmount(int id)
        {
            int ReturnResult = 0;
            try
            {
                if (!File.Exists(_addTargetAmountPath))
                {
                    return ReturnResult;
                }

                var itemsJson = File.ReadAllText(_addTargetAmountPath);
                var items = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(itemsJson) ?? new List<TargetAmountEntity>();

                var itemToUpdate = items.FirstOrDefault(item => item.ID == id);

                if (itemToUpdate == null)
                {
                    return ReturnResult;
                }

                itemToUpdate.IsActive = true;

                var updatedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(_addTargetAmountPath, updatedJson);
                ReturnResult = itemToUpdate.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TargetAmountEntity GetTargetAmountByID(int TargetAmountID)
        {
            try
            {
                if (!File.Exists(_addTargetAmountPath))
                {
                    return null;
                }

                var waterJson = File.ReadAllText(_addTargetAmountPath);
                var waters = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(waterJson) ?? new List<TargetAmountEntity>();

                return waters.FirstOrDefault(w => w.ID == TargetAmountID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditTargetAmount(TargetAmountEntity updatedItem)
        {
            try
            {
                if (!File.Exists(_addTargetAmountPath))
                {
                    return -1;
                }

                var waterJson = File.ReadAllText(_addTargetAmountPath);
                var waters = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(waterJson) ?? new List<TargetAmountEntity>();

                var index = waters.FindIndex(w => w.ID == updatedItem.ID);
                if (index == -1)
                {
                    return -1; // Item not found
                }

                waters[index] = updatedItem;
                var updatedJson = JsonConvert.SerializeObject(waters, Formatting.Indented);
                File.WriteAllText(_addTargetAmountPath, updatedJson);

                return updatedItem.ID; // Successfully updated
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 'Quantity Type'
        public int AddQuantityType(QuantityTypeEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<QuantityTypeEntity> items = new List<QuantityTypeEntity>();

                if (File.Exists(_addQuantityTypePath))
                {
                    var itemsJson = File.ReadAllText(_addQuantityTypePath);
                    items = JsonConvert.DeserializeObject<List<QuantityTypeEntity>>(itemsJson) ?? new List<QuantityTypeEntity>();
                }

                if (newItem.ID == 0)
                {
                    newItem.ID = items.Any() ? items.Max(i => i.ID) + 1 : 1;
                }
                else if (items.Any(i => i.ID == newItem.ID))
                {
                    ReturnResult = -1;
                }

                items.Add(newItem);

                var updatedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(_addQuantityTypePath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<QuantityTypeEntity> GetAllQuantityType(bool showActiveOnly)
        {
            try
            {
                if (!File.Exists(_addQuantityTypePath))
                {
                    return new List<QuantityTypeEntity>();
                }

                var itemsJson = File.ReadAllText(_addQuantityTypePath);
                var items = JsonConvert.DeserializeObject<List<QuantityTypeEntity>>(itemsJson) ?? new List<QuantityTypeEntity>();

                if (showActiveOnly)
                {
                    items = items.Where(item => item.IsActive).ToList();
                }

                return items;
            }
            catch (Exception ex)
            {
                return new List<QuantityTypeEntity>();
                throw ex;
            }
        }
        #endregion
    }
}
