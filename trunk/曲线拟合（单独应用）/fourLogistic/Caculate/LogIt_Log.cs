using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fourLogistic.Caculate
{
    public class LogIt_Log : PolynomialFit
    {
        private List<Data_Value> OriginalData = new List<Data_Value>();
        private List<Data_Value> pridata = new List<Data_Value>();
        public LogIt_Log()
            : base(1)
        {
            _pars = new double[3];
        }
        public LogIt_Log(double a, double b)
            : base(1)
        {
            _pars = new double[3];
            _pars[0] = b;
            _pars[1] = a;
        }
        public LogIt_Log(double[] pars)
            : base(1)
        {
            _pars = new double[3];
            _pars[0] = pars[1];
            _pars[1] = pars[0];
            _pars[2] = pars[2];
        }
        public LogIt_Log(List<double> pars):base(1)
        {
            _pars = new double[3];
            _pars[0] = pars[1];
            _pars[1] = pars[0];
            _pars[2] = pars[2];
        }
        public override void AddData(List<Data_Value> datapar)
        {
            List<Data_Value> data = datapar.OrderByDescending(ty => ty.DataValue).ToList();
            _pars[2] = data[0].DataValue;
            OriginalData.Clear();
            foreach (Data_Value dv in data)
                OriginalData.Add(new Data_Value() { Data = dv.Data, DataValue = dv.DataValue });
            pridata.Clear();
            for (int i = 1; i < data.Count; i++)
            {
                pridata.Add(new Data_Value() { Data = Math.Log10(data[i].Data), DataValue = Math.Log((data[i].DataValue / data[0].DataValue) / (1 - data[i].DataValue / data[0].DataValue), Math.E) });
            }
            //data.RemoveAt(0);
            base.AddData(pridata);
        }
        //public  double GetResult1(double xValue)
        //{
        //    double dl= _pars[1] * Math.Log10(xValue) + _pars[0];
        //    dl = Math.Pow(Math.E, dl);

        //    return dl / (1 + dl) * _pars[2];
        //}

        public override double GetResult(double xValue)
        {
            double dl = _pars[1] *xValue + _pars[0];
            

            return dl ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="yValue">吸光度比</param>
        /// <returns></returns>
        public override double GetResultInverse(double yValue)
        {
            double t1 = yValue / OriginalData[0].DataValue;
            t1 = (Math.Log(t1 / (1 - t1)) - _pars[0]) / _pars[1];
            return Math.Pow(10, t1);
        }
        public override int LeastNum
        {
            get { return 3; }
        }
        public override string StrPars
        {
            get
            {
                return base.StrPars + "|" + _pars[2];
            }
        }
        //public override double R
        //{
        //    get
        //    {
        //        if (OriginalData == null || OriginalData.Count < 1)
        //            return 0;
        //        double d1 = 0, d2 = 0;
        //        double yavg = OriginalData.Skip(1).Average(ty => ty.DataValue);
        //        foreach (Data_Value dv in OriginalData.Skip(1))
        //        {
        //            d1 += Math.Pow(dv.DataValue - GetResult1(dv.Data), 2);
        //            d2 += Math.Pow(dv.DataValue - yavg, 2);
        //        }
        //        return 1 - d1 / d2;
        //    }
        //}
    }
}
