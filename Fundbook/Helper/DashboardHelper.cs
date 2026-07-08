using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Fundbook.Models;

namespace Fundbook.Helper
{
    public class DashboardHelper
    {
        private readonly string _ElectricPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Electric.json");
        private readonly string _WaterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Water.json");
        private readonly string _RentPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "Rent.json");
        private readonly string _TargetAmountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "TargetAmountMaster.json");
        private readonly string _MonthMasterPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Utility", "MonthMaster.json");

        public UtilityDashboardData GetUtilityDataForDashboard(bool showActiveOnly)
        {
            try
            {
                var monthsJson = File.ReadAllText(_MonthMasterPath);
                var allMonths = JsonConvert.DeserializeObject<List<MasterEntity>>(monthsJson) ?? new List<MasterEntity>();
                var currentMonth = allMonths.FirstOrDefault(item => item.IsCurrent && !item.IsActive);

                if (currentMonth == null)
                {
                    return new UtilityDashboardData
                    {
                        Water = new List<UtilityEntity>(),
                        Electric = new List<ElectricEntity>(),
                        Rent = new List<RentEntity>(),
                        TargetAmount = new List<TargetAmountEntity>()
                    };
                }

                int currentMonthID = currentMonth.ID;

                // Read Electric data
                var electricJson = File.ReadAllText(_ElectricPath);
                var allElectric = JsonConvert.DeserializeObject<List<ElectricEntity>>(electricJson) ?? new List<ElectricEntity>();
                var electric = allElectric.Where(e => e.MonthID == currentMonthID && (!showActiveOnly || e.IsActive)).ToList();
                var electricSummary = new ElectricEntity
                {
                    MonthID = currentMonthID,
                    Description= "",
                    Amount = electric.Sum(w => w.Amount),
                    Unit = electric.Sum(w => w.Unit),
                    IsActive = false
                };

                // Read Water data (Summarized)
                var waterJson = File.ReadAllText(_WaterPath);
                var allWater = JsonConvert.DeserializeObject<List<UtilityEntity>>(waterJson) ?? new List<UtilityEntity>();
                var filteredWater = allWater.Where(w => w.MonthID == currentMonthID && (!showActiveOnly || w.IsActive)).ToList();

                var waterSummary = new UtilityEntity
                {
                    MonthID = currentMonthID,
                    Amount = filteredWater.Sum(w => w.Amount),
                    Quantity = filteredWater.Sum(w => w.Quantity),
                    IsActive = false
                };

                // Read Rent data
                var rentJson = File.ReadAllText(_RentPath);
                var allRent = JsonConvert.DeserializeObject<List<RentEntity>>(rentJson) ?? new List<RentEntity>();
                var rent = allRent.Where(r => r.MonthID == currentMonthID && (!showActiveOnly || r.IsActive)).ToList();

                var rentSummary = new RentEntity
                {
                    MonthID = currentMonthID,
                    Amount = rent.Sum(w => w.Amount),
                    IsActive = false
                };

                // Read Target Amount data
                var targetAmountJson = File.ReadAllText(_TargetAmountPath);
                var allTargetAmount = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(targetAmountJson) ?? new List<TargetAmountEntity>();
                var targetAmount = allTargetAmount.Where(t => t.MonthID == currentMonthID && (!showActiveOnly || t.IsActive)).ToList();

                return new UtilityDashboardData
                {
                    Water = new List<UtilityEntity> { waterSummary },
                    Electric = new List<ElectricEntity> { electricSummary },
                    Rent = new List<RentEntity> { rentSummary },
                    TargetAmount = targetAmount
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UtilityDashboardData GetUtilityDataForDashboardDetailView(bool showActiveOnly)
        {
            try
            {
                // Read Electric data
                var electricJson = File.ReadAllText(_ElectricPath);
                var allElectric = JsonConvert.DeserializeObject<List<ElectricEntity>>(electricJson) ?? new List<ElectricEntity>();
                var electric = allElectric.Where(e => !e.IsActive).ToList(); // Filter IsActive = false

                // Read Water data
                var waterJson = File.ReadAllText(_WaterPath);
                var allWater = JsonConvert.DeserializeObject<List<UtilityEntity>>(waterJson) ?? new List<UtilityEntity>();
                var water = allWater.Where(w => !w.IsActive).ToList(); // Filter IsActive = false

                // Read Rent data
                var rentJson = File.ReadAllText(_RentPath);
                var allRent = JsonConvert.DeserializeObject<List<RentEntity>>(rentJson) ?? new List<RentEntity>();
                var rent = allRent.Where(r => !r.IsActive).ToList(); // Filter IsActive = false

                // Read Target Amount data
                var targetAmountJson = File.ReadAllText(_TargetAmountPath);
                var allTargetAmount = JsonConvert.DeserializeObject<List<TargetAmountEntity>>(targetAmountJson) ?? new List<TargetAmountEntity>();
                var targetAmount = allTargetAmount.Where(t => !t.IsActive).ToList(); // Filter IsActive = false

                return new UtilityDashboardData
                {
                    Water = water,
                    Electric = electric,
                    Rent = rent,
                    TargetAmount = targetAmount
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
