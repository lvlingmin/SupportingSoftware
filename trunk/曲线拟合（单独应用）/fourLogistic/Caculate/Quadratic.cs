using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fourLogistic.Caculate
{
    public class Quadratic:PolynomialFit
    {
        public Quadratic()
            : base(2)
        {

        }
        public Quadratic(double a, double b, double c):base(2)
        {
            _pars[0] = c;
            _pars[1] = b;
            _pars[2] = a;
        }
        public Quadratic(List<double> pars)
            : base(2)
        {
            _pars[0] = pars[2];
            _pars[1] = pars[1];
            _pars[2] = pars[0];
        }
        public override double GetResultInverse(double yValue)
        {
            //double error = 0.00001;
            //double x = 9,d1,d2,d3,d4,d5;
            //while ((_pars[2] * Math.Pow(x, 2) + _pars[1] * x + _pars[0]) > error)
            //{
            //    d1 = x - (_pars[2] * Math.Pow(x, 2) + _pars[1] * x + _pars[0]) / (2 * _pars[2] * x + _pars[1]);
            //    d2 = d1 - (_pars[2] * Math.Pow(d1, 2) + _pars[1] * d1 + _pars[0]) / (2 * _pars[2] * d1 + _pars[1]);
            //    d3 = d2 - (_pars[2] * Math.Pow(d2, 2) + _pars[1] * d2 + _pars[0]) / (2 * _pars[2] * d2 + _pars[1]);
            //    d4 = d3 - (_pars[2] * Math.Pow(d3, 2) + _pars[1] * d3 + _pars[0]) / (2 * _pars[2] * d3 + _pars[1]);
            //    d5 = d4 - (_pars[2] * Math.Pow(d4, 2) + _pars[1] * d4 + _pars[0]) / (2 * _pars[2] * d4 + _pars[1]);
            //    x = d5;
            //    if (d4 == d5) break;
            //}
            //return x;
            double error = 0.00001;
            double x = 0, d1, d2 = double.MaxValue, d3 = double.MaxValue;
            while (d3 > error)
            {
                d1 = x - (_pars[2] * Math.Pow(x, 2) + _pars[1] * x + _pars[0] - yValue) / (2 * _pars[2] * x + _pars[1]);
                if (x == d1) break;
                d2 = (_pars[2] * Math.Pow(d1, 2) + _pars[1] * d1 + _pars[0]) - yValue;
                if (d2 > d3)
                    break;
                x = d1;
                d3 = d2;
            }
            return x;
        }
    }
}
