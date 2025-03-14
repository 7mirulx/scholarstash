using System;
using System.Net.NetworkInformation;
using System.Text;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace scholarstash
{
    internal class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://scholarstash-default-rtdb.asia-southeast1.firebasedatabase.app/");
        //_______________________________________________________________________________________PUSHING DATA TO FIREBASE______________________________________________________________________________________________//
        
        //to ExpenseRecord
        public async Task AddRecord(string dt, double am, string pu)
        {
            await firebase
            .Child("ExpenseRecords")
            .PostAsync(new ExpenseRecord() { amount = am, DateRecorded = dt, purpose = pu });
        }

        //_______________________________________________________________________________________DATA VISUALIZATION______________________________________________________________________________________________//

        //fetch data from ExpenseRecord and assign to object
        public async Task<List<ExpenseRecord>> GetExpenseRecord()
        {
            return (await firebase
                .Child("ExpenseRecords")
                .OnceAsync<ExpenseRecord>()).Select(item => new ExpenseRecord
                {
                    DateRecorded = item.Object.DateRecorded,
                    amount = item.Object.amount,
                    purpose = item.Object.purpose,
                }).ToList();
        }
        /*public async Task<TotalSpendingByCategory> GetHighestTotalSpendingByCategory()
        {
            var totalSpendingByCategoryList = await firebase
                .Child("TotalSpendingByCategory")
                .OnceAsync<TotalSpendingByCategory>();

            var highestTotalSpendingByCategory = totalSpendingByCategoryList.OrderByDescending(x => x.Object.TotalSpendingonCat)
                .FirstOrDefault();

            return highestTotalSpendingByCategory?.Object ?? new TotalSpendingByCategory();
        }*/
        public class CategorySpending
        {
            public string Category { get; set; }
            public double TotalAmount { get; set; }
        }

        public async Task<CategorySpending> GetHighestExpenseCategory()
        {
            var expenseRecords = await firebase
                .Child("ExpenseRecords")
                .OnceAsync<ExpenseRecord>();

            var categoryTotals = expenseRecords
                .GroupBy(item => item.Object.purpose)
                .Select(group => new
                {
                    Category = group.Key,
                    TotalAmount = group.Sum(item => item.Object.amount)
                })
                .OrderByDescending(x => x.TotalAmount)
                .FirstOrDefault();

            if (categoryTotals == null)
                return null;

            return new CategorySpending
            {
                Category = categoryTotals.Category,
                TotalAmount = categoryTotals.TotalAmount
            };
        }
        public async Task<CategorySpending> RefreshHighestExpenseCategory()
        {
            return await GetHighestExpenseCategory();
        }
        public class DateSpending
        {
            public string Date { get; set; }
            public double TotalSpending { get; set; }
        }

        public async Task<DateSpending> GetHighestTotalSpendingByDate()
        {
            var expenseRecords = await firebase
                .Child("ExpenseRecords")
                .OnceAsync<ExpenseRecord>();

            var dateTotals = expenseRecords
                .GroupBy(item => item.Object.DateRecorded)
                .Select(group => new
                {
                    Date = group.Key,
                    TotalSpending = group.Sum(item => item.Object.amount)
                })
                .OrderByDescending(x => x.TotalSpending)
                .FirstOrDefault();

            if (dateTotals == null)
                return null;

            return new DateSpending
            {
                Date = dateTotals.Date,
                TotalSpending = dateTotals.TotalSpending
            };
        }
        public async Task<DateSpending> RefreshHighestTotalSpendingByDate()
        {
            return await GetHighestTotalSpendingByDate();
        }


        //sum of total expenses
        public async Task<double> GetTotalExpenses()
        {
            var expenseRecords = await firebase
                .Child("ExpenseRecords")
                .OnceAsync<ExpenseRecord>();

            var totalExpenses = expenseRecords.Sum(item => item.Object.amount);

            return totalExpenses;
        }

        

        /*public async Task<TotalSpendingByDate> GetHighestTotalSpendingByDate()
        {
            var totalSpendingByDateList = await firebase
                .Child("TotalSpendingbyDate")
                .OnceAsync<TotalSpendingByDate>();

            var highestTotalSpendingByDate = totalSpendingByDateList.OrderByDescending(x => x.Object.TotalSpending)
                .FirstOrDefault();

            return highestTotalSpendingByDate?.Object ?? new TotalSpendingByDate();
        }*/



    }
}
