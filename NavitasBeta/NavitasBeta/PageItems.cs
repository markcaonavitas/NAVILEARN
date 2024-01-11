using System.Collections.Generic;

namespace NavitasBeta
{
    public class PageItem
    {
        public enum ViewTypes : int { Hidden, ReadOnly, Float, ReadOnlyTwoDecimalPlaces, ReadOnlyThreeDecimalPlaces, ReadOnlyString, FloatTwoDecimalPlaces, FloatThreeDecimalPlaces, FloatFourDecimalPlaces, Switch, Enum, Hex, DropDown, Vertical };

        public ViewTypes ViewType;
        public int Address;
        public string PropertyName;
        public string visibilityUserGreaterThanOrEqualLevelBinding;
        public string visibilityBinding;
        public string visibilityBindingValue;
        public string IsSelectedForDatalogging;
        public string TapGesutreRecognizer;
        public List<PageItem> PageSubItems = new List<PageItem>();
    }
}
