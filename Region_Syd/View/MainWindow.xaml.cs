using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Region_Syd.View;
using Region_Syd.ViewModel;

namespace Region_Syd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// Test Commit Fra Daniel
    public partial class MainWindow : Window
    {

        MainViewModel vm = new MainViewModel();

        public MainWindow()
        {
            
            DataContext = vm;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Login())
            {
                AssignmentsWindow assignmentsWindow = new AssignmentsWindow();
                assignmentsWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Du er ikke oprettet som bruger!", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}