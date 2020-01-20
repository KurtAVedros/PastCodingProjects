using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    // Nathan Thompson. 10/25/18
    // Associates sensors and values.  SensorID and ValueID do not form a composite key.
    // Nathan Thompson. 10/31/18
    // renamed IsAbove since it kept confusing me as to its purpose. Added comment to property.

    public class SensorToValue
    {
        private int _SensorToValueID;
        private int _SensorID;
        private int _ValueID;
        private float _Threshold;
        private bool _IsCheckingIfAbove;

        public SensorToValue(int sensorToValueID, int sensorID, int valueID, float threshold, bool isCheckingIfAbove)
        {
            _SensorToValueID = sensorToValueID;
            _SensorID = sensorID;
            _ValueID = valueID;
            _Threshold = threshold;
            _IsCheckingIfAbove = isCheckingIfAbove;
        }

        public int SensorToValueID
        {
            get
            {
                return _SensorToValueID;
            }
            set
            {
                if (value > -1)
                {
                    _SensorToValueID = value;
                }
            }
        }

        public int SensorID
        {
            get
            {
                return _SensorID;
            }
            set
            {
                if (value > -1)
                {
                    _SensorID = value;
                }
            }
        }

        public int ValueID
        {
            get
            {
                return _ValueID;
            }
            set
            {
                if (value > -1)
                {
                    _ValueID = value;
                }
            }
        }

        public float Threshold
        {
            get
            {
                return _Threshold;
            }
            set
            {
                _Threshold = value;
            }
        }

        // this says whether (_Threshold > val) or (_Threshold < val) should be used.
        // what about == or <= or >=? Defualt behaviour?
        public bool IsCheckingIfAbove
        {
            get
            {
                return _IsCheckingIfAbove;
            }
            set
            {
                _IsCheckingIfAbove = value;
            }
        }
    }
}
