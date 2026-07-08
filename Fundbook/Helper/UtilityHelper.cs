using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Fundbook.Models;

namespace Fundbook.Helper
{
    public class UtilityHelper
    {
        private readonly string _addWaterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Water.json");

        public int AddWater(UtilityEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<UtilityEntity> items = new List<UtilityEntity>();

                if (File.Exists(_addWaterPath))
                {
                    var itemsJson = File.ReadAllText(_addWaterPath);
                    items = JsonConvert.DeserializeObject<List<UtilityEntity>>(itemsJson) ?? new List<UtilityEntity>();
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
                File.WriteAllText(_addWaterPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<UtilityEntity> GetAllWater(bool showActiveOnly)
        {
            List<UtilityEntity> ReturnResult = new List<UtilityEntity>();
            try
            {
                if (!File.Exists(_addWaterPath))
                {
                    return new List<UtilityEntity>();
                }

                var itemsJson = File.ReadAllText(_addWaterPath);
                var items = JsonConvert.DeserializeObject<List<UtilityEntity>>(itemsJson) ?? new List<UtilityEntity>();

                var IsActive = showActiveOnly == false ? false : true;
                ReturnResult = items.Where(item => item.IsActive == IsActive).ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<UtilityEntity>();
                throw ex;
            }
        }

        private readonly string _addElectricPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Electric.json");

        public int AddElectricUnit(ElectricEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<ElectricEntity> items = new List<ElectricEntity>();

                if (File.Exists(_addElectricPath))
                {
                    var itemsJson = File.ReadAllText(_addElectricPath);
                    items = JsonConvert.DeserializeObject<List<ElectricEntity>>(itemsJson) ?? new List<ElectricEntity>();
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
                File.WriteAllText(_addElectricPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<ElectricEntity> GetAllElectricUnit(bool showActiveOnly)
        {
            List<ElectricEntity> ReturnResult = new List<ElectricEntity>();
            try
            {
                if (!File.Exists(_addElectricPath))
                {
                    return new List<ElectricEntity>();
                }

                var itemsJson = File.ReadAllText(_addElectricPath);
                var items = JsonConvert.DeserializeObject<List<ElectricEntity>>(itemsJson) ?? new List<ElectricEntity>();

                var IsActive = showActiveOnly == false ? false : true;
                ReturnResult = items.Where(item => item.IsActive == IsActive).ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<ElectricEntity>();
                throw ex;
            }
        }


        private readonly string _addRentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Rent.json");
        private readonly string _MonthPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "MonthMaster.json");

        public int AddRent(RentEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<RentEntity> rents = new List<RentEntity>();
                List<MasterEntity> Months = new List<MasterEntity>();

                if (File.Exists(_addRentPath))
                {
                    var itemsJson = File.ReadAllText(_addRentPath);
                    rents = JsonConvert.DeserializeObject<List<RentEntity>>(itemsJson) ?? new List<RentEntity>();
                }
                if (File.Exists(_MonthPath))
                {
                    var monthsJson = File.ReadAllText(_MonthPath);
                    Months = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
                }

                if (newItem.ID == 0)
                {
                    newItem.ID = rents.Any() ? rents.Max(i => i.ID) + 1 : 1;
                    
                    var monthEntity = Months.FirstOrDefault(i => i.ID == newItem.MonthID);
                    newItem.Month = monthEntity != null ? monthEntity.Description : string.Empty; // Set to empty string if not found
                }
                else if (rents.Any(i => i.ID == newItem.ID))
                {
                    ReturnResult = -1;
                }

                rents.Add(newItem);

                var updatedJson = JsonConvert.SerializeObject(rents, Formatting.Indented);
                File.WriteAllText(_addRentPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<RentEntity> GetAllRent(bool showActiveOnly)
        {
            List<RentEntity> ReturnResult = new List<RentEntity>();
            try
            {
                if (!File.Exists(_addRentPath))
                {
                    return new List<RentEntity>();
                }

                var rentJson = File.ReadAllText(_addRentPath);
                var rents = JsonConvert.DeserializeObject<List<RentEntity>>(rentJson) ?? new List<RentEntity>();

                var IsActive = showActiveOnly == false ? false : true;
                ReturnResult = rents.Where(item => item.IsActive == IsActive).ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<RentEntity>();
                throw ex;
            }
        }

        private readonly string _addGroceryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Grocery.json");

        public int AddGrocery(GroceryEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<GroceryEntity> items = new List<GroceryEntity>();

                if (File.Exists(_addGroceryPath))
                {
                    var itemsJson = File.ReadAllText(_addGroceryPath);
                    items = JsonConvert.DeserializeObject<List<GroceryEntity>>(itemsJson) ?? new List<GroceryEntity>();
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
                File.WriteAllText(_addGroceryPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<GroceryEntity> GetAllGrocery(bool showActiveOnly)
        {
            List<GroceryEntity> ReturnResult = new List<GroceryEntity>();
            try
            {
                if (!File.Exists(_addGroceryPath))
                {
                    return new List<GroceryEntity>();
                }

                var itemsJson = File.ReadAllText(_addGroceryPath);
                var items = JsonConvert.DeserializeObject<List<GroceryEntity>>(itemsJson) ?? new List<GroceryEntity>();

                var IsActive = showActiveOnly == false ? false : true;
                ReturnResult = items.Where(item => item.IsActive == IsActive).ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<GroceryEntity>();
                throw ex;
            }
        }
        public int DeleteGrocery(int id)
        {
            int ReturnResult = 0;
            try
            {
                if (!File.Exists(_addGroceryPath))
                {
                    return ReturnResult;
                }
                
                var itemsJson = File.ReadAllText(_addGroceryPath);
                var items = JsonConvert.DeserializeObject<List<GroceryEntity>>(itemsJson) ?? new List<GroceryEntity>();
                
                var itemToUpdate = items.FirstOrDefault(item => item.ID == id);

                if (itemToUpdate == null)
                {
                    return ReturnResult;
                }
                
                itemToUpdate.IsActive = true;
                
                var updatedJson = JsonConvert.SerializeObject(items, Formatting.Indented);
                File.WriteAllText(_addGroceryPath, updatedJson);
                ReturnResult = itemToUpdate.ID;
                return ReturnResult; 
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }

        public int EditWater(UtilityEntity updatedItem)
        {
            try
            {
                if (!File.Exists(_addWaterPath))
                {
                    return -1;
                }

                var waterJson = File.ReadAllText(_addWaterPath);
                var waters = JsonConvert.DeserializeObject<List<UtilityEntity>>(waterJson) ?? new List<UtilityEntity>();

                var index = waters.FindIndex(w => w.ID == updatedItem.ID);
                if (index == -1)
                {
                    return -1; // Item not found
                }

                waters[index] = updatedItem;
                var updatedJson = JsonConvert.SerializeObject(waters, Formatting.Indented);
                File.WriteAllText(_addWaterPath, updatedJson);

                return updatedItem.ID; // Successfully updated
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UtilityEntity GetWaterByID(int waterID)
        {
            try
            {
                if (!File.Exists(_addWaterPath))
                {
                    return null;
                }

                var waterJson = File.ReadAllText(_addWaterPath);
                var waters = JsonConvert.DeserializeObject<List<UtilityEntity>>(waterJson) ?? new List<UtilityEntity>();

                return waters.FirstOrDefault(w => w.ID == waterID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditRent(RentEntity updatedItem)
        {
            try
            {
                if (!File.Exists(_addRentPath))
                {
                    return -1;
                }

                var rentJson = File.ReadAllText(_addRentPath);
                var rents = JsonConvert.DeserializeObject<List<RentEntity>>(rentJson) ?? new List<RentEntity>();

                var index = rents.FindIndex(w => w.ID == updatedItem.ID);
                if (index == -1)
                {
                    return -1; // Item not found
                }

                rents[index] = updatedItem;
                var updatedJson = JsonConvert.SerializeObject(rents, Formatting.Indented);
                File.WriteAllText(_addRentPath, updatedJson);

                return updatedItem.ID; // Successfully updated
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public RentEntity GetRentByID(int rentID)
        {
            try
            {
                if (!File.Exists(_addRentPath))
                {
                    return null;
                }

                var rentJson = File.ReadAllText(_addRentPath);
                var rents = JsonConvert.DeserializeObject<List<RentEntity>>(rentJson) ?? new List<RentEntity>();

                return rents.FirstOrDefault(w => w.ID == rentID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditElectricUnit(ElectricEntity updatedItem)
        {
            try
            {
                if (!File.Exists(_addElectricPath))
                {
                    return -1;
                }

                var electricJson = File.ReadAllText(_addElectricPath);
                var electrics = JsonConvert.DeserializeObject<List<ElectricEntity>>(electricJson) ?? new List<ElectricEntity>();

                var index = electrics.FindIndex(w => w.ID == updatedItem.ID);
                if (index == -1)
                {
                    return -1; // Item not found
                }

                electrics[index] = updatedItem;
                var updatedJson = JsonConvert.SerializeObject(electrics, Formatting.Indented);
                File.WriteAllText(_addElectricPath, updatedJson);

                return updatedItem.ID; // Successfully updated
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ElectricEntity GetElectricByID(int electricID)
        {
            try
            {
                if (!File.Exists(_addElectricPath))
                {
                    return null;
                }

                var electricJson = File.ReadAllText(_addElectricPath);
                var electrics = JsonConvert.DeserializeObject<List<ElectricEntity>>(electricJson) ?? new List<ElectricEntity>();

                return electrics.FirstOrDefault(w => w.ID == electricID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
