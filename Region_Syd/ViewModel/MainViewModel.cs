using Region_Syd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Region_Syd.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        List<string> usernames = new List<string>();

        private string _enteredUsername;

        public string EnteredUsername
        {
            get { return _enteredUsername; }
            set 
            { 
                _enteredUsername = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel() 
        { 
            usernames = new List<string>();
            usernames.Add("Sander");
            usernames.Add("Christian");
            usernames.Add("Daniel");
            usernames.Add("René");
            usernames.Add("Christina");
            usernames.Add("Troll");
        }

        public bool CanLogin()
        {
            if (string.IsNullOrEmpty(_enteredUsername)) return false;

            return true;
        }

        public RelayCommand LoginCommand => new RelayCommand(
                execute => usernames.Exists(u => u == EnteredUsername),
                canExecute => CanLogin()
                );

        public bool Login()
        {

            if (usernames.Exists(u => u == EnteredUsername))
            { return true; }

            return false;

        }
    }
}
