using System.Windows;
using System.Windows.Data;
using Region_Syd.ViewModel;



namespace Region_Syd.View
{
    /// <summary>
    /// Interaction logic for AssignmentsView.xaml
    /// </summary>
    public partial class AssignmentsWindow : Window
    {
        public AssignmentsWindow()
        {

            var currentApp = Application.Current as App;
            string connectionString = currentApp.Configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            AssignmentsViewModel tvm = new AssignmentsViewModel(connectionString);
            DataContext = tvm;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(tvm.AllAssignments);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("StartRegion");
            view.GroupDescriptions.Add(groupDescription);

            InitializeComponent();

        }

        public void CantCombine()
        {
            MessageBox.Show("Denne kombination er ikke mulig.", "Kombinationsfejl", MessageBoxButton.OK, MessageBoxImage.Error);
        }

		private void Button_btnSavingsClick(object sender, RoutedEventArgs e)
		{
			SavingsWindow savingsWindow= new SavingsWindow();
			savingsWindow.Show();
			this.Close();
		}
	}
}
