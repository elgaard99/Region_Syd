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
        RegionRepo _regionRepo;
        private ObservableCollection<Region> _regions;

        public ObservableCollection<Region> Regions
        {
            get { return _regions; }
            set { 
                _regions = value;
				OnPropertyChanged(nameof(Regions));

			}
		}

        public SavingsViewModel(string connectionString)
        {
			_regionRepo = new RegionRepo(connectionString);

			Regions = new ObservableCollection<Region> (_regionRepo.GetAll());
			Regions.Add(_regionRepo.CalculateTotalSavings());
		}



		//public double TimeSavedSouth => Savings.FirstOrDefault(s => s.Region == RegionEnum.RSy)?.SavedHours ?? 0;
		//public double DistanceSavedSouth => TimeSavedSouth * 90;
		//public double TimeSavedNorth => Savings.FirstOrDefault(s => s.Region == RegionEnum.RN)?.SavedHours ?? 0;
		//public double DistanceSavedNorth => TimeSavedNorth * 90;
		//public double TimeSavedZealand => Savings.FirstOrDefault(s => s.Region == RegionEnum.RSj)?.SavedHours ?? 0;
		//public double DistanceSavedZealand => TimeSavedZealand * 90;
		//public double TimeSavedMid => Savings.FirstOrDefault(s => s.Region == RegionEnum.RM)?.SavedHours ?? 0;
		//public double DistanceSavedMid => TimeSavedMid * 90;
		//public double TimeSavedCapital => Savings.FirstOrDefault(s => s.Region == RegionEnum.RH)?.SavedHours ?? 0;
		//public double DistanceSavedCapital => TimeSavedCapital * 90;
		//public double TimeSavedDenmark => TimeSavedSouth + TimeSavedNorth + TimeSavedZealand + TimeSavedMid + TimeSavedCapital;
		//public double DistanceSavedDenmark => TimeSavedDenmark * 90;
	}

}
