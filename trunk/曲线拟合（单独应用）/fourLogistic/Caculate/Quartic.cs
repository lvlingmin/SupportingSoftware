using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fourLogistic.Caculate
{
    public class Quartic : PolynomialFit
    {
        public Quartic()
            : base(4)
        {
        }
        public Quartic(double a, double b, double c,double d,double e)
            : base(4)
        {
            _pars[0] = e;
            _pars[1] = d;
            _pars[2] = c;
            _pars[3] = b;
            _pars[4] = a;
        }
        public Quartic(double[] pars)
            : base(4)
        {
            _pars[0] = pars[4];
            _pars[1] = pars[3];
            _pars[2] = pars[2];
            _pars[3] = pars[1];
            _pars[4] = pars[0];
        }
        public override double GetResultInverse(double yValue)
        {
            double error = 0.00001;
            double x = 0, d1, d2 = double.MaxValue, d3 = double.MaxValue;
            while (d3 > error)
            {
                d1 = x - (_pars[4] * Math.Pow(x, 4) + _pars[3] * Math.Pow(x, 3) + _pars[2] * Math.Pow(x, 2) + _pars[1] * x + _pars[0] - yValue) / (4 * _pars[4] * Math.Pow(x, 3) + 3 * _pars[3] * Math.Pow(x, 2) + 2 * _pars[2] * x + _pars[1]);
                if (x == d1) break;

                d2 = (_pars[4] * Math.Pow(d1, 4) + _pars[3] * Math.Pow(d1, 3) + _pars[2] * Math.Pow(d1, 2) + _pars[1] * d1 + _pars[0]) - yValue;
                if (d2 > d3)
                    break;
                x = d1;
                d3 = d2;
            }
            return x;
        }
    }
}
