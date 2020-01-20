using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class ConnectionToParts
    {
        private int _ConnectionToPartID;
        private int _ConnectionID;
        private int _PartID;

        public ConnectionToParts(int connectionToPartID, int connectionID, int partID)
        {
            _ConnectionToPartID = connectionToPartID;
            _ConnectionID = connectionID;
            _PartID = partID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to ConnectionToPartID
        /// </summary>
        public int ConnectionToPartID
        {
            get
            {
                return _ConnectionToPartID;
            }
            set
            {
                _ConnectionToPartID = value;
            }
        }

        public int ConnectionID
        {
            get
            {
                return _ConnectionID;
            }
            set
            {
                if (value >= 0)
                {
                    _ConnectionID = value;
                }
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
    }
}
