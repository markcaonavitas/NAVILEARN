class NavitasGeneralPageView
{
    constructor() {
        //super();
        //var _this = this; //_this allows this to be exposed to constructor functions
    }

    BuildCommunicationsList(parameterList, parameter) {
        var parentParameter = parameter;
        if (parameter.SubsetOfAddress) //change to parent parameter
            parentParameter = App1.App.NavitasMotorController.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == parameter.Address));

        if (!parameterList.Contains(parentParameter))
            parameterList.Add(parentParameter);

        if (parameter.GroupedParameters != null && parameter.GroupedParameters.Count != 0) {//if not in the xaml list then add to communications list so the binding responds
            foreach(group in parameter.GroupedParameters)
            {
                foreach(item in group.ParameterFileItemList)
                {
                    var subParameter = App.NavitasMotorController.GetParameter(item.PropertyName);

                    parentParameter = subParameter;
                    if (subParameter.SubsetOfAddress) //change to parent parameter
                        parentParameter = App.NavitasMotorController.MainViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == subParameter.Address));

                    if (!parameterList.Contains(parentParameter))
                        parameterList.Add(parentParameter);
                }
            }
        }
    }
}