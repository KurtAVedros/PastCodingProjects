using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class FlowRegulator : NamedEntity
    {
        private int _FlowRegulatorID;
        private bool _IsOpen;

        /// <summary>
        /// Added IsOpen
        /// </summary>
        /// <param name="flowRegulatorID"></param>
        /// <param name="name"></param>
        /// <param name="IsOpen"></param>
        public FlowRegulator(int flowRegulatorID, String name, bool isOpen) : base(name)
        {
            _FlowRegulatorID = flowRegulatorID;
            _IsOpen = isOpen;
        }

        /// <summary>
        /// Kurt A Vedros
        /// changed ID to FlowRegulatorID
        /// </summary>
        public int FlowRegulatorID
        {
            get
            {
                return _FlowRegulatorID;
            }
            set
            {
                _FlowRegulatorID = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                _IsOpen = value;
            }
        }
    }
}
