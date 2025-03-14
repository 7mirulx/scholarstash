
namespace scholarstash
{
    public partial class MainPage : ContentPage
    {



        public MainPage()
        {
            InitializeComponent();
            this.BackgroundColor = Colors.White;
            FirebaseHelper firebaseHelper = new FirebaseHelper();
            UpdateHighestCategory();
            UpdateHighestDate();
            TotalExpenses();
        }

        private async void KeyInSpendingPageButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//KeyInSpending");
        }

        private async void RecordedSpendingPageButton(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SpendingRecord");
        }
        private async Task UpdateHighestCategory()
        {
            try
            {
                var firebaseHelper = new FirebaseHelper();
                var highestCategorySpending = await firebaseHelper.GetHighestExpenseCategory();

                if (highestCategorySpending == null)
                {
                    highestCategoryLabel.Text = "No expense records found.";
                }
                else
                {
                    highestCategoryLabel.Text = $"You spent the most on {highestCategorySpending.Category}: ${highestCategorySpending.TotalAmount:0.00}";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching highest category: " + ex.Message);
            }
        }
        private async void RefreshCategoryImageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var firebaseHelper = new FirebaseHelper();
                var highestCategorySpending = await firebaseHelper.RefreshHighestExpenseCategory();

                if (highestCategorySpending == null)
                {
                    highestCategoryLabel.Text = "No expense records found.";
                }
                else
                {
                    highestCategoryLabel.Text = $"You spent the most on {highestCategorySpending.Category}: ${highestCategorySpending.TotalAmount:0.00}";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching highest category: " + ex.Message);
            }
        }

        /*private async void UpdateHighestCategory()
        {
            try
            {
                 // Create an instance of FirebaseHelper
                var highestSpendingCategory = await firebaseHelper.GetHighestTotalSpendingByCategory();
                highestCategoryLabel.Text = "You spent so much on " + highestSpendingCategory.Category + ": $" + highestSpendingCategory.TotalSpendingonCat.ToString("0.00");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching highest category: " + ex.Message);
            }
        }*/
        private async Task UpdateHighestDate()
        {
            try
            {
                var firebaseHelper = new FirebaseHelper();
                var highestDateSpending = await firebaseHelper.GetHighestTotalSpendingByDate();

                if (highestDateSpending == null)
                {
                    highestDateLabel.Text = "No expense records found.";
                }
                else
                {
                    highestDateLabel.Text = $"You spent the most on {highestDateSpending.Date}: ${highestDateSpending.TotalSpending:0.00}";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching highest spending date: " + ex.Message);
            }
        }
        private async void RefreshDateImageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var firebaseHelper = new FirebaseHelper();
                var highestDateSpending = await firebaseHelper.RefreshHighestTotalSpendingByDate();

                if (highestDateSpending == null)
                {
                    highestDateLabel.Text = "No expense records found.";
                }
                else
                {
                    highestDateLabel.Text = $"You spent the most on {highestDateSpending.Date}: ${highestDateSpending.TotalSpending:0.00}";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching highest spending date: " + ex.Message);
            }
        }



        private async void TotalExpenses()
        {
            try
            {
                var firebaseHelper = new FirebaseHelper(); // Create an instance of FirebaseHelper
                var totalExpenses = await firebaseHelper.GetTotalExpenses();
                // Assuming you have a Label in your XAML named totalExpensesLabel to display the total expenses
                totalExpensesLabel.Text = $" ${totalExpenses.ToString("0.00")}";
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching total expenses: " + ex.Message);
            }
        }
        private async void RefreshTotalExpensesImageButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var firebaseHelper = new FirebaseHelper();
                var totalExpenses = await firebaseHelper.GetTotalExpenses();

                // Assuming you have a Label in your XAML named totalExpensesLabel to display the total expenses
                totalExpensesLabel.Text = $" ${totalExpenses.ToString("0.00")}";
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error fetching total expenses: " + ex.Message);
            }
        }


    }
}


