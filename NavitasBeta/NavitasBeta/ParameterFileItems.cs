namespace NavitasBeta
{
    public class ParameterFileItem : ViewModelBase
    {
        public enum ViewTypes : int { ReadOnly, Float, Switch };

        public ViewTypes ViewType;
        public int Address;
        public string PropertyName;
        public float ParameterValue;
        public ushort ParameterValueRaw;
    }
}
