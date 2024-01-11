using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavitasBeta
{
    public class ViewModelLocator
    {

        private ParametersViewModelTSX _parametersViewModelTSX = new ParametersViewModelTSX();
        public ParametersViewModelTSX MainViewModelTSX
        {
            get
            {
                return _parametersViewModelTSX;
            }
        }
        private ParametersViewModel _parametersViewModel = new ParametersViewModel();
        public ParametersViewModel MainViewModel
        {
            get
            {
                return _parametersViewModel;
            }
        }
        public GoiParameter GetParameter(string name)
        {
            GoiParameter parameter = null;
            try
            {
                parameter = _parametersViewModel.GoiParameterList.FirstOrDefault(x => (x.PropertyName == name));
                if (parameter == null)
                    throw new InvalidOperationException("GetParameter TAC can't find " + name);
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return parameter;
        }
        public GoiParameter GetParameter(float address)
        {
            GoiParameter parameter = null;
            try
            {
                parameter = _parametersViewModel.GoiParameterList.FirstOrDefault(x => (x.Address == address));
                if (parameter == null)
                    throw new InvalidOperationException("GetParameter TAC can't find address" + address.ToString());
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return parameter;
        }
        public GoiParameter GetParameterTSX(string name)
        {
            GoiParameter parameter = null;
            try
            {
                parameter = _parametersViewModelTSX.GoiParameterList.FirstOrDefault(x => (x.PropertyName == name));
                if (parameter == null)
                    throw new InvalidOperationException("GetParameter TSX can't find " + name);
            }
            catch (InvalidOperationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            return parameter;
        }
    }

}
