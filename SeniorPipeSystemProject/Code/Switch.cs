using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    /// <summary>
    /// Kurt A Vedros
    /// Added SwitchBoxID
    /// Removed IsOpen as it is in FlowRegulator
    /// </summary>
    public class Switch : NamedEntity
    {
        private int _SwitchID;
        private int _SwitchboxID;
        private int _FlowRegulatorID;

        /// <summary>
        /// Kurt A Vedros
        /// Added features in this method as it only had Name SwitchID and IsOpen
        /// </summary>
        /// <param name="switchID"></param>
        /// <param name="SwitchBoxID"></param>
        /// <param name="FlowRegulatorID"></param>
        /// <param name="TransporterID"></param>
        /// <param name="name"></param>
        public Switch(int switchID, int switchBoxID, int flowRegulatorID, String name) : base(name)
        {
            _SwitchID = switchID;
            _SwitchboxID = switchBoxID;
            _FlowRegulatorID = flowRegulatorID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to SwitchID
        /// </summary>
        public int SwitchID
        {
            get
            {
                return _SwitchID;
            }
            set
            {
                _SwitchID = value;
            }
        }

        public int SwitchboxID
        {
            get
            {
                return _SwitchboxID;
            }
            set
            {
                _SwitchboxID = value;
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
    }
}
