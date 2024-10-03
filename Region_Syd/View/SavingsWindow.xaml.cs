using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Region_Syd.ViewModel;

namespace Region_Syd.View
{
	/// <summary>
	/// Interaction logic for SavingsWindow.xaml
	/// </summary>
	public partial class SavingsWindow : Window
	{
		public SavingsWindow()
		{
			var currentApp = Application.Current as App;
			string connectionString = currentApp.Configuration.GetSection("ConnectionStrings")["TestConnection2"];

			SavingsViewModel Svm = new SavingsViewModel(connectionString);
			DataContext = Svm;

			InitializeComponent();

		}

		private void btnAssignments_Click(object sender, RoutedEventArgs e)
		{
			AssignmentsWindow assignmentsWindow = new AssignmentsWindow();
			assignmentsWindow.Show();
			this.Close();
		}
    }
}
