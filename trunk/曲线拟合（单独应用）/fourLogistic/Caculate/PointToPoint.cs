using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace fourLogistic.Caculate
{
    public class PointToPoint:Calculater
    {
        private List<Linear> _listFit = new List<Linear>();
        private int _pointNum;
        //public PointToPoint(int PointNum)
        //{
        //    _pointNum = PointNum;
        //    _pars = new double[(PointNum-1) * 2];
        //    for (int i = 0; i < PointNum - 1; i++)
        //        _listFit.Add(new Linear());
        //}
        public PointToPoint()
        {
        }
        public PointToPoint(List<double> pars, List<fourLogistic.Caculate.Data_Value> DataValue)
        {
            _pointNum = pars.Count / 2;
            _pars = new double[pars.Count];
            for (int i = 0; i < _pointNum; i++)
            {
                Linear linea = new Linear();
                linea._pars[1] = pars[2 * i];
                linea._pars[0] = pars[2 * i + 1];
                _listFit.Add(linea);
            }
            for (int j = 0; j < DataValue.Count; j++)
                _fitData.Add(DataValue[j]);
        }
        //private string _scalingID;
        //public override string scalingID
        //{
        //    get
        //    {
        //        return _scalingID;
        //    }
        //    set
        //    {
        //        _fitData.Clear();
        //        //DataTable dt = Elisa.DBUtility.DbHelperSQL.Query("select consistence,absorbency from scalingResult where scalingInfoID='" + value + "' order by consistence").Tables[0];
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            _fitData.Add(new Data_Value() { Data = double.Parse(dr[0].ToString()), DataValue = double.Parse(dr[1].ToString()) });
        //        }
        //        _scalingID = value;
        //    }
        //}
        public override void AddData(List<Data_Value> data)
        {
            _pointNum = data.Count - 1;
            if (_pars==null)
            {
                _pars = new double[_pointNum * 2];
                for (int i = 0; i < _pointNum; i++)
                    _listFit.Add(new Linear());
            }
            _fitData.Clear();
            foreach (Data_Value dv in data)
                _fitData.Add(new Data_Value() { Data = dv.Data, DataValue = dv.DataValue });
        }
        public override void Fit()
        {
            int j = 0;
            for (int i = 0; i < _pointNum; i++)
            {
                List<Data_Value> lt=new List<Data_Value>();
                lt.Add(_fitData[i]);
                lt.Add(_fitData[i+1]);
                _listFit[i].AddData(lt);
                _listFit[i].Fit();
                _pars[j * 2] = _listFit[i]._pars[0];
                _pars[j * 2 + 1] = _listFit[i]._pars[1];
                j++;
            }
        }
        public override double GetResult(double xValue)
        {
            int tPostion = GetPostion(xValue);
            if (tPostion == -1) tPostion = 0;
            if (tPostion == _fitData.Count-1) tPostion = _fitData.Count - 2;
            return _listFit[tPostion].GetResult(xValue);
        }
        public override string StrFunc { get { return string.Empty; } }
        public override double GetResultInverse(double yValue)
        {
            int tPostion = GetPostionByY(yValue);
            if (tPostion == -1) tPostion = 0;
            if (tPostion == _fitData.Count - 1) tPostion = _fitData.Count - 2;
            return _listFit[tPostion].GetResultInverse(yValue);//问题所在
        }
        public override string StrPars
        {
            get
            {
                StringBuilder strPar = new StringBuilder();
                foreach (Linear ear in _listFit)
                {
                    strPar.Append(ear.StrPars + "|");
                }
                strPar.Length -= 1;
                return strPar.ToString();
            }
        }
        public override int LeastNum
        {
            get { return 2; }
        }
        private int GetPostion(double xValue)
        {
            int temp = -1;
            foreach (Data_Value dv in _fitData)
            {
                if(xValue<=dv.Data)
                    break;
                temp++;
            }
            return temp;
        }
        private int GetPostionByY(double yValue)
        {
            int temp = -1;
            foreach (Data_Value dv in _fitData)
            {
                if (yValue <= dv.DataValue)
                    break;
                temp++;
            }
            return temp;
        }
        public override double R
        {
            get
            {
                return 1;
            }
        }
    }
}
