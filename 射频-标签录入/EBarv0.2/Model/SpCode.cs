using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EBarv0._2.Model
{
    class SpCode
    {
        string reagentInfo;
        string projectFlow;
        string[] scaling = new string[2];
        string[] lightVal = new string[4];
        string qualityControls1;
        string qualityControls2;

        public SpCode(string reagent, string pro, string[] sca , string[] light, string QC1 = "", string QC2 = "")
        {
            reagentInfo = reagent;
            projectFlow = pro;
            scaling = sca;
            lightVal = light;
            qualityControls1 = QC1;
            qualityControls2 = QC2;
        }
        public string getReagentInfo()
        {
            return this.reagentInfo;
        }
        public string getProjectFlow()
        {
            return this.projectFlow;
        }
        public string getScaling_1()
        {
            return this.scaling[0];
        }
        public string getScaling_2()
        {
            return this.scaling[1];
        }
        public string getValue_1()
        {
            return this.lightVal[0];
        }
        public string getValue_2()
        {
            return this.lightVal[1];
        }
        public string getValue_3()
        {
            return this.lightVal[2];
        }
        public string getValue_4()
        {
            return this.lightVal[3];
        }
        public string getQualityControls1()
        {
            return this.qualityControls1;
        }
        public string getQualityControls2()
        {
            return this.qualityControls2;
        }
    }
}
