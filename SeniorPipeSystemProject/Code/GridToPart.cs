using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class GridToPart
    {
        private int _GridToPartID;
        private int _GridID;
        private int _PartID;
        private int _XAxis;
        private int _YAxis;

        public GridToPart(int gridToPartID, int gridID, int partID, int xAxis, int yAxis)
        {
            _GridToPartID = gridToPartID;
            _GridID = gridID;
            _PartID = partID;
            _XAxis = xAxis;
            _YAxis = yAxis;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to GridToPartID
        /// </summary>
        public int GridToPartID
        {
            get
            {
                return _GridToPartID;
            }
            set
            {
                _GridToPartID = value;
            }
        }

        public int GridID
        {
            get
            {
                return _GridID;
            }
            set
            {
                if (value >= 0)
                {
                    _GridID = value;
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

        public int YCoordinate
        {
            get
            {
                return _YAxis;
            }
            set
            {
                _YAxis = value;
            }
        }

        public int XCoordinate
        {
            get
            {
                return _XAxis;
            }
            set
            {
                _XAxis = value;
            }
        }

    }
}
