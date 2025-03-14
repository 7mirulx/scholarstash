namespace scholarstash;

public partial class SpendingRecord : ContentPage
{
    protected async override void OnAppearing()
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        base.OnAppearing();
        displayRecord.ItemsSource = await firebaseHelper.GetExpenseRecord();
        TotalExpenses();
    }
    
    public SpendingRecord()
	{
		InitializeComponent();
        this.BackgroundColor = Colors.White;
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
}