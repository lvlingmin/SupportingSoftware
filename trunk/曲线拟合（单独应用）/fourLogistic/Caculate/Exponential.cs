using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fourLogistic.Caculate
{
    public class Exponential : PolynomialFit
    {
        public Exponential()
            : base(1)
        {

        }
        public Exponential(double a, double b)
            : base(1)
        {
            _pars[0] = b;
            _pars[1] = a;
        }
        public Exponential(double[] pars)
            : base(1)
        {
            _pars[0] = pars[1];
            _pars[1] = pars[0];
        }
        public Exponential(List<double> pars)
            : base(1)
        {
            _pars[0] = pars[1];
            _pars[1] = pars[0];
        }
        public override void AddData(List<Data_Value> datapar)
        {
            base.AddData(datapar);
            foreach (Data_Value dv in _fitData)
            {
                dv.DataValue = Math.Log(dv.DataValue, Math.E);
            }            
        }
        public override void Fit()
        {
            base.Fit();
            _pars[0] = Math.Pow(Math.E, _pars[0]);
        }
        public override double GetResult(double xValue)
        {
            return _pars[0] * Math.Pow(Math.E, _pars[1] * xValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yValue">吸光度比</param>
        /// <returns></returns>
        public override double GetResultInverse(double yValue)
        {
            return (Math.Log(yValue, Math.E) - Math.Log(_pars[0], Math.E)) / _pars[1];
        }
        public override int LeastNum
        {
            get { return 2; }
        }
        public override string StrFunc
        {
            get
            {
                return _pars[0].ToString() + "*e^(" + _pars[1].ToString() + "*X)";
            }
        }
    }
}
