using fourLogistic.Caculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fourLogistic
{
    public class CalculateResult
    {
        /// <summary>
        /// 计算浓度
        /// </summary>
        /// <param name="PMTDatas">定标点数据</param>
        /// <returns></returns>
        double CalConcentration(List<Data_Value>  PMTDatas)
        {
            Calculater calculater = new FourPL();
            PMTDatas.Sort(new Data_ValueDataAsc());
            return 1;
        }
    }
}
