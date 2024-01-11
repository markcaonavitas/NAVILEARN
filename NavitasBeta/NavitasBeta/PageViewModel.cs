using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NavitasBeta
{
    public class PageViewModel : ViewModelBase
    {

        public string PageTitle = "des 1";
        public string PageType = "";
        public string PageControl = "";
        //for backwards compatibility (no groups)
        private ObservableCollection<PageItem> _pageItems = new ObservableCollection<PageItem>();
        public ObservableCollection<PageItem> PageItems
        {
            set { SetProperty(ref _pageItems, value); }
            get { return _pageItems; }
        }

        //private static ObservableCollection<PageSubItem> _pageSubItems = new ObservableCollection<PageSubItem>();
        //public ObservableCollection<PageSubItem> PageSubItems
        //{
        //    set { SetProperty(ref _pageSubItems, value); }
        //    get { return _pageSubItems; }
        //}

        private ObservableCollection<PageGroup> _pageGroups = new ObservableCollection<PageGroup>();
        public ObservableCollection<PageGroup> PageGroups
        {
            set { SetProperty(ref _pageGroups, value); }
            get { return _pageGroups; }
        }

        public PageViewModel()
        {
            //System.Diagnostics.Debug.WriteLine("ParametersViewModel TAC");
        }
    }
}
