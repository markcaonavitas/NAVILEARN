using System;

namespace NavitasBeta
{
    public class MasterPageItem : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if (value != _title)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }

        public string FileName { get; set; }
    }
}
