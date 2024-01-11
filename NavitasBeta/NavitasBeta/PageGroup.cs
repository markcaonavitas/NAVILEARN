using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class PageGroup
    {
        public string GroupViewType;
        public string GroupTitle;
        public string GroupDescription;
        public string DescriptionColor;
        public string visibilityUserGreaterThanOrEqualLevelBinding;
        public string visibilityBinding; //Just a parameter
        public string visibilityBindingConvertor;
        public string visibilityBindingValue;
        public List<PageItem> PageItems = new List<PageItem>();
        public List<PageButton> PageButtons = new List<PageButton>();
        public List<GroupDescriptionButton> GroupDescriptionButtons = new List<GroupDescriptionButton>();
    }

    public class PageButton
    {
        public string Text;
    }

    public class GroupDescriptionButton
    {
        public string Text;
    }
}
