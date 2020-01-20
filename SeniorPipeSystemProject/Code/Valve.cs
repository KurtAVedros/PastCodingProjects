using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Valve : NamedEntity
    {

        private int _ValveID;
        private int _FlowRegulatorID;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="name"></param>
        public Valve(int valveID, int flowRegulatorID, String name) : base(name)
        {
            _ValveID = valveID;
            _FlowRegulatorID = flowRegulatorID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to ValveID
        /// </summary>
        public int ValveID
        {
            get
            {
                return _ValveID;
            }
            set
            {
                _ValveID = value;
            }
        }

        public int FlowRegulatorID
        {
            get
            {
                return _FlowRegulatorID;
            }
            set
            {
                if (value >= 0)
                {
                    _FlowRegulatorID = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double calculatePressure()
        {
            throw new NotImplementedException();
        }
    }
}
