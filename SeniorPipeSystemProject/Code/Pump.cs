using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Pump : NamedEntity
    {
        /// <summary>
        /// Kurt A Vedros
        /// removed DischargeRate as this will be recorded in Values Table/Class
        /// </summary>
        private int _PumpID;
        private float _VerticalSuctionLift;
        private float _MaximumHeadLift;

        /// <summary>
        /// Kurt Vedros
        /// Added TransporterID
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="name"></param>
        /// <param name="verticalSuctionLift"></param>
        /// <param name="dischargeRate"></param>
        /// <param name="maxHeadLift"></param>
        public Pump(int pumpID, String name, float verticalSuctionLift, float maxHeadLift) : base(name)
        {
            _PumpID = pumpID;
            _VerticalSuctionLift = verticalSuctionLift;
            _MaximumHeadLift = maxHeadLift;
        }

        /// <summary>
        /// Kurt Vedros
        /// Added all get and set methods
        /// </summary>
        public int PumpID
        {
            get
            {
                return _PumpID;
            }
            set
            {
                _PumpID = value;
            }
        }

        public float VerticalSuctionLift
        {
            get
            {
                return _VerticalSuctionLift;
            }
            set
            {
                _VerticalSuctionLift = value;
            }
        }

        public float MaximumHeadLift
        {
            get
            {
                return _MaximumHeadLift;
            }
            set
            {
                _MaximumHeadLift = value;
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
