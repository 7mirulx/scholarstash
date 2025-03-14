namespace scholarstash;

public partial class KeyInSpending : ContentPage
{
    FirebaseHelper firebaseHelper = new FirebaseHelper();
    public KeyInSpending()
	{
		InitializeComponent();
        this.BackgroundColor = Colors.White;
    }
    void OnReset(object sender, EventArgs e)
    {
        amountEntry.Text = null;
        purposePicker.SelectedIndex = -1;
    }
    async void SaveButton(object sender, EventArgs e)
    {
        
        
        var amount = double.Parse(amountEntry.Text);
        var DateRecorded = datePicker.Date.ToString("dd/MM/yyyy");
        string? purpose = purposePicker.SelectedItem?.ToString();
        await firebaseHelper.AddRecord(DateRecorded, amount, purpose);
        await DisplayAlert("Total Spending Calculated", "Total spending has been updated", "OK");

    }
}