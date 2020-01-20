using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    // Kurt Vedros
    // Added TransporterID
    /// <summary>
    /// Cord
    /// </summary>
    public class Cord : NamedEntity
    {
        private int _CordID;
        private float _VoltageDropRate;
        private float _Length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <param name="voltageDropRate"></param>
        public Cord(int cordID, String name, float length, float voltageDropRate) : base(name)
        {
            _VoltageDropRate = voltageDropRate;
            _CordID = cordID;
            _Length = 0;
            if (length >= 0)
            {
                _Length = length;
            }
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to CordID
        /// </summary>
        public int CordID
        {
            get
            {
                return _CordID;
            }
            set
            {
                _CordID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Length
        {
            get
            {
                return _Length;
            }

            set
            {
                if (value >= 0)
                {
                    _Length = value;
                }
            }
        }

        /// <summary>
        /// Kurt A Vedros
        /// Fixed VoltageDropRate
        /// </summary>
        public float VoltageDropRate
        {
            get
            {
                return _VoltageDropRate; 
            }
            set
            {
                _VoltageDropRate = value;
            }
        }

        // Nathan Thompson. Added property for voltage drop. 10/17/18
        public float VoltageDrop
        {
            get
            {
                return VoltageDropRate * Length;
            }
        }
    }
}
