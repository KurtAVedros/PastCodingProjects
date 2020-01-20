using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    // removed isFlowing because this needs to  match the database.

    //
    public class Sensor : NamedEntity
    {
        private int _SensorID;
        private int _PartID;
        

        public Sensor(int sensorID, int partID, String name) : base (name)
        {
            _SensorID = sensorID;
            _PartID = partID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to SensorID
        /// </summary>
        public int SensorID
        {
            get
            {
                return _SensorID;
            }
            set
            {
                _SensorID = value;
            }
        }

        public int PartID
        {
            get
            {
                return _PartID;
            }
            set {
                if (value >= 0) {
                    _PartID = value;
                }
            }
        }

    }
}