using System;

namespace AGI.CalcScalar.ExternalDataFOM
{
    public interface IExternalFOMCalcScalar
    {
        double Tolerance { get; set; }
        string FilePath { get; set; }
    }
}
