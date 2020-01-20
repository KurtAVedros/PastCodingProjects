using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Transporter
    {
        private int _TransporterID;
        private int _ConnectionID;

        public Transporter(int transporterID, int connectionID)
        {
            _TransporterID = transporterID;
            _ConnectionID = connectionID;
        }

        public int TransporterID
        {
            get { return _TransporterID; }
            set { _TransporterID = value; }
        }

        public int ConnectionID
        {
            get
            {
                return _ConnectionID;
            }

            set
            {
                if (value >= 0) {
                    _ConnectionID = value;
                }
            }
        }

    }
}
