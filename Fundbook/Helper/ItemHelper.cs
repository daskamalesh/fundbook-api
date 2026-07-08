using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Fundbook.Models;

namespace Fundbook.Helper
{
    public class ItemHelper
    {
        private readonly string _addItemPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Item.json");
        private readonly string _getMonthPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "MonthMaster.json");

        public int AddItem(ItemEntity newItem)
        {
            int ReturnResult = 0;
            try
            {
                List<ItemEntity> items = new List<ItemEntity>();

                if (File.Exists(_addItemPath))
                {
                    var itemsJson = File.ReadAllText(_addItemPath);
                    items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();
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
                File.WriteAllText(_addItemPath, updatedJson);
                ReturnResult = newItem.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public List<ItemEntity> GetAllItems(bool showActiveOnly)
        {
            List<ItemEntity> ReturnResult = new List<ItemEntity>();
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return new List<ItemEntity>();
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

                var IsActive = showActiveOnly == false ? false : true;
                ReturnResult = items.Where(item => item.IsActive == IsActive).ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<ItemEntity>();
                throw ex;
            }
        }

        public List<ItemEntity> GetItemsMonthWise(int MonthID, int TransactionTypeID)
        {
            List<ItemEntity> ReturnResult = new List<ItemEntity>();
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return new List<ItemEntity>();
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

                if (MonthID > 0)
                {
                    ReturnResult = items.Where(item =>
                    item.MonthID == MonthID &&
                    item.IsActive == false &&
                    (TransactionTypeID == 0 || item.TransactionTypeID == TransactionTypeID ||
                    (TransactionTypeID == 0 && (item.TransactionTypeID == 1 || item.TransactionTypeID == 2)))
                    ).ToList();
                }

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<ItemEntity>();
                throw ex;
            }
        }

        public List<ItemEntity> GetItemsMonthUserWise(int MonthID, int UserID, int TransactionTypeID)
        {
            List<ItemEntity> ReturnResult = new List<ItemEntity>();
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return new List<ItemEntity>();
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

                if (UserID > 0 && MonthID > 0)
                {
                    ReturnResult = items.Where(item =>
                    item.MonthID == MonthID &&
                    item.UserID == UserID &&
                    item.IsActive == false &&
                    (TransactionTypeID == 0 || item.TransactionTypeID == TransactionTypeID ||
                    (TransactionTypeID == 0 && (item.TransactionTypeID == 1 || item.TransactionTypeID == 2)))
                    ).ToList();
                }

                return ReturnResult;
            }
            catch (Exception ex)
            {
                return new List<ItemEntity>();
                throw ex;
            }
        }

        //////public List<ItemSummaryEntity> GetItemsIndividualUserWise()
        //////{
        //////    var ReturnResult = new List<ItemSummaryEntity>();
        //////    try
        //////    {
        //////        if (!File.Exists(_addItemPath))
        //////        {
        //////            return new List<ItemSummaryEntity>();
        //////        }

        //////        var monthsJson = File.ReadAllText(_getMonthPath);
        //////        var allMonths = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
        //////        var currentMonth = allMonths.FirstOrDefault(item => item.IsCurrent && !item.IsActive);

        //////        if (currentMonth == null)
        //////        {
        //////            return new List<ItemSummaryEntity>();
        //////        }

        //////        var itemsJson = File.ReadAllText(_addItemPath);
        //////        var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

        //////        var filteredItems = items.Where(item => item.MonthID == currentMonth.ID && !item.IsActive).ToList();

        //////        var groupedItems = filteredItems.GroupBy(item => new { item.UserID, item.UserName, item.MonthID });

        //////        foreach (var group in groupedItems)
        //////        {
        //////            var userId = group.Key.UserID;

        //////            // Sum normal debits (excluding donations)
        //////            var totalDebited = group
        //////                .Where(item => item.TransactionTypeID == 1)
        //////                .Sum(item => item.Amount);

        //////            // Add accepted donations given by this user
        //////            var totalDonated = items
        //////                .Where(item => item.UserID == userId && item.TransactionTypeID == 2 && item.DonationUserID != 0 && item.IsAccepted)
        //////                .Sum(item => item.Amount);

        //////            // Sum normal credits (excluding received donations)
        //////            var totalCredited = group
        //////                .Where(item => item.TransactionTypeID == 2 && item.DonationUserID == 0)
        //////                .Sum(item => item.Amount);

        //////            // Add accepted donations received by this user
        //////            var totalReceived = items
        //////                .Where(item => item.DonationUserID == userId && item.TransactionTypeID == 2 && item.IsAccepted)
        //////                .Sum(item => item.Amount);

        //////            // Total expenditure (Debited + Donations given - Accepted donations received)
        //////            var totalExpenditure = (totalDebited + totalDonated) - totalReceived;

        //////            var summary = new ItemSummaryEntity
        //////            {
        //////                UserID = userId,
        //////                UserName = group.Key.UserName,
        //////                MonthID = group.Key.MonthID,
        //////                TotalAmountDebited = totalDebited + totalDonated,  // Include donations given
        //////                TotalAmountCredited = totalCredited + totalReceived,  // Include donations received
        //////                TotalExpenditure = totalExpenditure
        //////            };
        //////            ReturnResult.Add(summary);
        //////        }

        //////        return ReturnResult;
        //////    }
        //////    catch (Exception ex)
        //////    {
        //////        throw ex;
        //////    }
        //////}
        public List<ItemSummaryEntity> GetItemsIndividualUserWise()
        {
            var ReturnResult = new List<ItemSummaryEntity>();
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return new List<ItemSummaryEntity>();
                }

                var monthsJson = File.ReadAllText(_getMonthPath);
                var allMonths = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
                var currentMonth = allMonths.FirstOrDefault(item => item.IsCurrent && !item.IsActive);

                if (currentMonth == null)
                {
                    return new List<ItemSummaryEntity>();
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

                //  Ensure only current month data is used throughout
                var filteredItems = items.Where(item => item.MonthID == currentMonth.ID && !item.IsActive).ToList();

                var groupedItems = filteredItems.GroupBy(item => new { item.UserID, item.UserName, item.MonthID });

                foreach (var group in groupedItems)
                {
                    var userId = group.Key.UserID;

                    //  Sum normal debits (excluding donations) for the current month
                    var totalDebited = group
                        .Where(item => item.TransactionTypeID == 1)
                        .Sum(item => item.Amount);

                    //  Add accepted donations given by this user for the current month
                    var totalDonated = filteredItems
                        .Where(item => item.UserID == userId && item.TransactionTypeID == 2 && item.DonationUserID != 0 && item.IsAccepted)
                        .Sum(item => item.Amount);

                    //  Sum normal credits (excluding received donations) for the current month
                    var totalCredited = group
                        .Where(item => item.TransactionTypeID == 2 && item.DonationUserID == 0)
                        .Sum(item => item.Amount);

                    //  Add accepted donations received by this user for the current month
                    var totalReceived = filteredItems
                        .Where(item => item.DonationUserID == userId && item.TransactionTypeID == 2 && item.IsAccepted)
                        .Sum(item => item.Amount);

                    //  Total expenditure (Debited + Donations given - Accepted donations received) for the current month
                    var totalExpenditure = (totalDebited + totalDonated) - totalReceived;

                    var summary = new ItemSummaryEntity
                    {
                        UserID = userId,
                        UserName = group.Key.UserName,
                        MonthID = group.Key.MonthID,
                        TotalAmountDebited = totalDebited + totalDonated,  // Include donations given
                        TotalAmountCredited = totalCredited + totalReceived,  // Include donations received
                        TotalExpenditure = totalExpenditure
                    };

                    ReturnResult.Add(summary);
                }

                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ItemSettlementEntity> GetSettlementData()
        {
            var ReturnResult = new List<ItemSettlementEntity>();
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return new List<ItemSettlementEntity>();
                }

                var monthsJson = File.ReadAllText(_getMonthPath);
                var allMonths = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
                var currentMonth = allMonths.FirstOrDefault(item => item.IsCurrent && !item.IsActive);

                if (currentMonth == null)
                {
                    return new List<ItemSettlementEntity>();
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

                // Filter data for the current month and inactive records
                var filteredItems = items.Where(item => item.MonthID == currentMonth.ID && !item.IsActive).ToList();

                var groupedItems = filteredItems.GroupBy(item => new { item.UserID, item.UserName, item.MonthID });

                decimal grandTotalExpenditure = 0;
                int uniqueUserCount = groupedItems.Select(group => group.Key.UserID).Distinct().Count();

                // Dictionary to track each user's final expenditure
                Dictionary<int, decimal> userExpenditures = new Dictionary<int, decimal>();

                foreach (var group in groupedItems)
                {
                    var userId = group.Key.UserID;

                    // Total debited amount (Normal spending + Donations given)
                    var totalDebited = group
                        .Where(item => item.TransactionTypeID == 1)
                        .Sum(item => item.Amount);

                    var totalDonated = filteredItems
                        .Where(item => item.UserID == userId && item.TransactionTypeID == 2 && item.DonationUserID != 0 && item.IsAccepted)
                        .Sum(item => item.Amount);

                    // Total credited amount (Normal credit + Donations received)
                    var totalCredited = group
                        .Where(item => item.TransactionTypeID == 2 && item.DonationUserID == 0)
                        .Sum(item => item.Amount);

                    var totalReceived = filteredItems
                        .Where(item => item.DonationUserID == userId && item.TransactionTypeID == 2 && item.IsAccepted)
                        .Sum(item => item.Amount);

                    // Calculate net expenditure: (Debited + Donations given) - (Donations received)
                    decimal totalExpenditure = (totalDebited + totalDonated) - totalReceived;

                    // Store user-wise expenditure (ensuring no duplicate calculations)
                    if (!userExpenditures.ContainsKey(userId))
                    {
                        userExpenditures[userId] = totalExpenditure;
                    }
                    else
                    {
                        userExpenditures[userId] += totalExpenditure;
                    }
                }

                // Calculate Grand Total Expenditure
                grandTotalExpenditure = userExpenditures.Values.Sum();

                var summary = new ItemSettlementEntity
                {
                    GrandTotalExpenditureAmount = grandTotalExpenditure,
                    AmountToDevidedBy = uniqueUserCount > 0 ? uniqueUserCount : 0,
                    AmountPerHead = uniqueUserCount > 0 ? Math.Round(grandTotalExpenditure / uniqueUserCount, 3) : 0
                };

                ReturnResult.Add(summary);

                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ////////public List<UserSettlementEntity> GetSettlementDataUserWise()
        ////////{
        ////////    var ReturnResult = new List<UserSettlementEntity>();
        ////////    try
        ////////    {
        ////////        if (!File.Exists(_addItemPath))
        ////////        {
        ////////            return new List<UserSettlementEntity>();
        ////////        }

        ////////        var monthsJson = File.ReadAllText(_getMonthPath);
        ////////        var allMonths = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
        ////////        var currentMonth = allMonths.FirstOrDefault(item => item.IsCurrent && !item.IsActive);

        ////////        if (currentMonth == null)
        ////////        {
        ////////            return new List<UserSettlementEntity>();
        ////////        }

        ////////        var itemsJson = File.ReadAllText(_addItemPath);
        ////////        var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

        ////////        var filteredItems = items.Where(item => item.MonthID == currentMonth.ID && !item.IsActive).ToList();

        ////////        var groupedItems = filteredItems.GroupBy(item => new { item.UserID, item.UserName });

        ////////        // Dictionary to store each user's net expenditure
        ////////        Dictionary<int, decimal> userExpenditures = new Dictionary<int, decimal>();

        ////////        foreach (var group in groupedItems)
        ////////        {
        ////////            var userId = group.Key.UserID;

        ////////            // Self expenses (excluding donations)
        ////////            var selfDebitedAmount = group
        ////////                .Where(item => item.TransactionTypeID == 1)
        ////////                .Sum(item => item.Amount);

        ////////            // Donations given to other users (only accepted ones)
        ////////            var totalDonated = filteredItems
        ////////                .Where(item => item.UserID == userId && item.TransactionTypeID == 2 && item.DonationUserID != 0 && item.IsAccepted)
        ////////                .Sum(item => item.Amount);

        ////////            // Donations received from other users (only accepted ones)
        ////////            var totalReceived = filteredItems
        ////////                .Where(item => item.DonationUserID == userId && item.TransactionTypeID == 2 && item.IsAccepted)
        ////////                .Sum(item => item.Amount);

        ////////            // Net expenditure = (Self expenses + Donations given) - Donations received
        ////////            decimal totalExpenditure = (selfDebitedAmount + totalDonated) - totalReceived;

        ////////            // Store user-wise expenditure
        ////////            userExpenditures[userId] = totalExpenditure;
        ////////        }

        ////////        // Calculate the **Grand Total Expenditure**
        ////////        decimal grandTotalExpenditure = userExpenditures.Values.Sum();
        ////////        int uniqueUserCount = userExpenditures.Count;

        ////////        // Calculate the **Equal Share Per User**
        ////////        decimal amountPerHead = uniqueUserCount > 0 ? grandTotalExpenditure / uniqueUserCount : 0;

        ////////        // Generate user settlement details
        ////////        foreach (var group in groupedItems)
        ////////        {
        ////////            var userId = group.Key.UserID;
        ////////            decimal selfExpenditure = userExpenditures[userId];

        ////////            // Calculate how much this user **owes** or **receives**
        ////////            decimal balance = Math.Round(amountPerHead,3) - selfExpenditure;

        ////////            var settlement = new UserSettlementEntity
        ////////            {
        ////////                UserID = userId,
        ////////                UserName = group.Key.UserName,
        ////////                SelfDebitedAmount = selfExpenditure,
        ////////                AmountToGive = balance > 0 ? balance : 0,  // User needs to pay this amount
        ////////                AmountToTake = balance < 0 ? Math.Abs(balance) : 0  // User should receive this amount
        ////////            };

        ////////            ReturnResult.Add(settlement);
        ////////        }

        ////////        return ReturnResult;
        ////////    }
        ////////    catch (Exception ex)
        ////////    {
        ////////        throw ex;
        ////////    }
        ////////}
        public List<UserSettlementEntity> GetSettlementDataUserWise()
        {
            var ReturnResult = new List<UserSettlementEntity>();

            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return new List<UserSettlementEntity>();
                }

                var monthsJson = File.ReadAllText(_getMonthPath);
                var allMonths = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
                var currentMonth = allMonths.FirstOrDefault(item => item.IsCurrent && !item.IsActive);

                if (currentMonth == null)
                {
                    return new List<UserSettlementEntity>();
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();

                var filteredItems = items.Where(item => item.MonthID == currentMonth.ID && !item.IsActive).ToList();
                var groupedItems = filteredItems.GroupBy(item => new { item.UserID, item.UserName });

                Dictionary<int, decimal> userExpenditures = new Dictionary<int, decimal>();

                foreach (var group in groupedItems)
                {
                    var userId = group.Key.UserID;

                    // Self debited amount (excluding donations)
                    var selfDebitedAmount = group
                        .Where(item => item.TransactionTypeID == 1)
                        .Sum(item => item.Amount);

                    // Donations given (only accepted ones)
                    var totalDonated = group
                        .Where(item => item.TransactionTypeID == 2 && item.DonationUserID != 0 && item.IsAccepted)
                        .Sum(item => item.Amount);

                    // Donations received (only accepted ones)
                    var totalReceived = filteredItems
                        .Where(item => item.DonationUserID == userId && item.TransactionTypeID == 2 && item.IsAccepted)
                        .Sum(item => item.Amount);

                    // Correct adjustment:
                    // - Donor's donation is **added** to their expense.
                    // - Receiver's accepted donation is **deducted** from their expense.
                    decimal totalExpenditure = selfDebitedAmount + totalDonated - totalReceived;

                    userExpenditures[userId] = totalExpenditure;
                }

                decimal grandTotalExpenditure = userExpenditures.Values.Sum();
                int uniqueUserCount = userExpenditures.Count;
                decimal amountPerHead = uniqueUserCount > 0 ? grandTotalExpenditure / uniqueUserCount : 0;

                foreach (var group in groupedItems)
                {
                    var userId = group.Key.UserID;
                    decimal selfExpenditure = userExpenditures[userId];

                    decimal balance = Math.Round(amountPerHead, 3) - selfExpenditure;

                    var settlement = new UserSettlementEntity
                    {
                        UserID = userId,
                        UserName = group.Key.UserName,
                        SelfDebitedAmount = selfExpenditure,
                        AmountToGive = balance > 0 ? balance : 0,
                        AmountToTake = balance < 0 ? Math.Abs(balance) : 0
                    };

                    ReturnResult.Add(settlement);
                }

                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int AddSettlementData(ItemEntity newSettlement)
        {
            int ReturnResult = 0;
            try
            {
                List<ItemEntity> settlements = new List<ItemEntity>();

                if (File.Exists(_addItemPath))
                {
                    var settlementsJson = File.ReadAllText(_addItemPath);
                    settlements = JsonConvert.DeserializeObject<List<ItemEntity>>(settlementsJson) ?? new List<ItemEntity>();
                }

                if (newSettlement.ID == 0)
                {
                    newSettlement.ID = settlements.Any() ? settlements.Max(s => s.ID) + 1 : 1;
                }
                else if (settlements.Any(s => s.ID == newSettlement.ID))
                {
                    ReturnResult = -1;
                    return ReturnResult;
                }

                settlements.Add(newSettlement);

                var updatedJson = JsonConvert.SerializeObject(settlements, Formatting.Indented);
                File.WriteAllText(_addItemPath, updatedJson);
                ReturnResult = newSettlement.ID;
                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemEntity> GetSettlementDataByUserID(int userID)
        {
            var ReturnResult = new List<ItemEntity>();
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return ReturnResult;
                }

                var settlementsJson = File.ReadAllText(_addItemPath);
                var settlements = JsonConvert.DeserializeObject<List<ItemEntity>>(settlementsJson) ?? new List<ItemEntity>();

                ReturnResult = settlements
                    .Where(settlement =>
                        settlement.DonationUserID == userID && 
                        settlement.TransactionTypeID == 2 && 
                        settlement.IsAccepted == false
                    )
                    .ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ItemEntity> GetSettlementProcessRequestData()
        {
            try
            {
                var ReturnResult = new List<ItemEntity>();

                if (!File.Exists(_addItemPath))
                {
                    return ReturnResult; // Return an empty list if the file doesn't exist
                }

                var itemsJson = File.ReadAllText(_addItemPath);
                var items = JsonConvert.DeserializeObject<List<ItemEntity>>(itemsJson) ?? new List<ItemEntity>();
                
                ReturnResult = items
                    .Where(item => item.IsAccepted == false && item.TransactionTypeID == 2)
                    .ToList();

                return ReturnResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditSettlementData(int itemID)
        {
            try
            {
                if (!File.Exists(_addItemPath))
                {
                    return 0; // File does not exist
                }

                var settlementsJson = File.ReadAllText(_addItemPath);
                var settlements = JsonConvert.DeserializeObject<List<ItemEntity>>(settlementsJson) ?? new List<ItemEntity>();

                var settlementToEdit = settlements.FirstOrDefault(settlement => settlement.ID == itemID);

                if (settlementToEdit == null)
                {
                    return 0; // Item not found
                }

                settlementToEdit.IsAccepted = true;

                var updatedJson = JsonConvert.SerializeObject(settlements, Formatting.Indented);
                File.WriteAllText(_addItemPath, updatedJson);

                return itemID; // Return the edited item ID
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
