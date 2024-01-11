using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace NavitasBeta
{
    public class PageParameterList
    {
        public NavitasGeneralPage pagePointer;
        public bool Active = true;
        public long uniqueID = 0;
        public int PacketsEvaluatedForSending = 0;
        public string ParentTitle = "";
        public enum ParameterListType : int { TAC, TSX };
        public ParameterListType parameterListType;
        public List<GoiParameter> parameterList = new List<GoiParameter>();

        public PageParameterList(ParameterListType pageType, NavitasGeneralPage parentPage)
        {
            parameterListType = pageType;
            uniqueID = DateTime.Now.Ticks; //good enough for running a year instead of GUID

            ParentTitle = parentPage.Title;
            pagePointer = parentPage;
        }
        public PageParameterList() { } //somehow makes exposes it of typeof() so that I can serialize it?

    }
}
