using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    // Nathan Thompson. Added ReadingID. 10/25/18
    // Nathan Thompson. Removed TransporterID. Added PartID. 11/5/18

    /// <summary>
    /// Kurt A Vedros
    /// Added LocationFromStart as this is the indicator as of where the value
    /// is being read from
    /// </summary>
    public class DataValue
    {
        private int _DataValueID;
        private int _ReadingID;
        private float _LocationFromStart;
        private float _Value;
        private int _PartID;

        public DataValue(int dataValueID, int partID, int readingID, float locationFromStart, float value)
        {
            _DataValueID = dataValueID;
            _PartID = partID;
            _ReadingID = readingID;
            _LocationFromStart = locationFromStart;
            _Value = value;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to ConnectionID
        /// </summary>
        public int DataValueID
        {
            get
            {
                return _DataValueID;
            }
            set
            {
                _DataValueID = value;
            }
        }

        public int PartID
        {
            get
            {
                return _PartID;
            }
            set
            {
                if (value >= 0)
                {
                    _PartID = value;
                }
            }
        }

        /// <summary>
        /// get and set method for LocationFromStart
        /// </summary>
        public float LocationFromStart
        {
            get
            {
                return _LocationFromStart;
            }
            set
            {
                _LocationFromStart = value;
            }
        }

        public float Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public int ReadingID
        {
            get
            {
                return _ReadingID;
            }
            set
            {
                _ReadingID = value;
            }
        }
    }

}
