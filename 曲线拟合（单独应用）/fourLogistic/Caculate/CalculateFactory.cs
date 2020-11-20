using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fourLogistic.Caculate;

namespace fourLogistic.Calculate
{
    public class CalculateFactory
    {
        public Calculater getCaler(int calType)
        {
            Calculater er = null;

            switch (calType)
            {
                case 0:
                    er = new Linear();
                    break;
                case 1:
                    er = new PointToPoint();
                    break;
                case 2:
                    er = new Quadratic();
                    break;
                case 3:
                    er = new Cubic();
                    break;
                case 4:
                    er = new LogIt_Log();
                    break;
                case 5:
                    er = new FourPL();
                    break;
                case 6:
                    er = new FivePL();
                    break;
                case 7:
                    er = new newFourPL();
                    break;
            }

            return er;
        }
        public Calculater getCaler(List<double> pars)
        {
            Calculater er = null;

            er = new FourPL(pars);//四参数

            return er;
        }
    }
}
