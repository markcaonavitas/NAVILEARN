using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Xml;

namespace NavitasBeta
{
    public class countries_states : ViewModelBase
    {
        public countries_states(List<country_state> country_states)
        {
            this.country_states = country_states;
        }

        private static string _stateLabel = "State";
        public string StateLabel
        {
            get { return _stateLabel; }
            set
            {
                _stateLabel = value;
                OnPropertyChanged();
            }
        }

        private static string _stateTitle = "Select a state";
        public string StateTitle
        {
            get { return _stateTitle; }
            set
            {
                if (_stateTitle != value)
                {
                    _stateTitle = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isPostalCodeEnable;
        public bool isPostalCodeEnable
        {
            get { return _isPostalCodeEnable; }
            set
            {
                _isPostalCodeEnable = value;
                OnPropertyChanged();
            }
        }

        private static string _lblPostalCode = "Zip Code";
        public string lblPostalCode
        {
            get { return _lblPostalCode; }
            set
            {
                if (_lblPostalCode != value)
                {
                    _lblPostalCode = value;
                    OnPropertyChanged();
                }
            }
        }

        private static string _phdPostalCode = "Enter your zip code";
        public string phdPostalCode
        {
            get { return _phdPostalCode; }
            set
            {
                if (_phdPostalCode != value)
                {
                    _phdPostalCode = value;
                    OnPropertyChanged();
                }
            }
        }

        List<country_state> _CountryStates;
        public List<country_state> country_states
        {
            get { return _CountryStates; }
            set
            {
                if (_CountryStates != value)
                {
                    _CountryStates = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDropDownProvinceAvailable = true;
        public bool IsDropDownProvinceAvailable
        {
            get { return _isDropDownProvinceAvailable; }
            set
            {
                if (_isDropDownProvinceAvailable != value)
                {
                    _isDropDownProvinceAvailable = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _haveToInputProvinceManually = false;
        public bool HaveToInputProvinceManually
        {
            get { return _haveToInputProvinceManually; }
            set
            {
                if(_haveToInputProvinceManually != value)
                {
                    _haveToInputProvinceManually = value;
                    IsDropDownProvinceAvailable = !_haveToInputProvinceManually;
                    OnPropertyChanged();
                }
            }
        }


        List<string> restTopCountryControllerConsumption = new List<string>() { "China", "Australia" };

        country_state _selectedCountry;
        public country_state SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    if(_selectedCountry.name == "Canada")
                    {
                        StateLabel = "Province";
                        StateTitle = "Select a Province";
                        lblPostalCode = "Postal Code";
                        phdPostalCode = "Enter your postal code";
                        isPostalCodeEnable = true;
                        HaveToInputProvinceManually = false;
                    }
                    else if (_selectedCountry.name == "United States")
                    {
                        StateLabel = "State";
                        StateTitle = "Select a State";
                        lblPostalCode = "Zip Code";
                        phdPostalCode = "Enter your zip code";
                        isPostalCodeEnable = true;
                        HaveToInputProvinceManually = false;
                    }
                    else
                    {
                        if (restTopCountryControllerConsumption.Contains(_selectedCountry.name))
                            HaveToInputProvinceManually = false;
                        else
                            HaveToInputProvinceManually = true;
                        StateLabel = "Province";
                        StateTitle = "Select a province";
                        phdPostalCode = "Not Available";
                        isPostalCodeEnable = false;
                    }
                    OnPropertyChanged();
                }
            }
        }

        state _selectedState;
        public state SelectedProvince
        {
            get { return _selectedState; }
            set
            {
                if (_selectedState != value)
                {
                    _selectedState = value;
                    OnPropertyChanged();
                }
            }
        }
        public countries_states()
        {
            //System.Diagnostics.Debug.WriteLine("ParametersViewModel TAC");
        }
    }
    public class country_state
    {
        public string name { get; set; }
        public List<state> states { get; set; }
    }
    public class state
    {
        public string name { get; set; }
    }
}
