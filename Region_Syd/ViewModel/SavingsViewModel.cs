using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Region_Syd.Model;

namespace Region_Syd.ViewModel
{
    
    public class SavingsViewModel : ViewModelBase
    {

		private ObservableCollection<Region> _regions;
		public ObservableCollection<Region> Regions {  get { return _regions; } set { _regions = value; OnPropertyChanged(); } }

        public SavingsViewModel(string connectionString)
        {
			RegionRepo regionRepo = new RegionRepo(connectionString);

			Regions = new ObservableCollection<Region>(regionRepo.GetAll());
			Regions.Add(regionRepo.CalculateTotalSavings());

		}
	}
}
