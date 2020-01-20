using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Grid : NamedEntity
    {
        private int _GridID;
        private int _SystemID;

        /// <summary>
        /// Kurt A Vedros removed list of PartID Added SystemID
        /// </summary>
        /// <param name="name"></param>
        public Grid(int gridID, int systemID,  String name) : base(name)
        {
            _SystemID = systemID;
            _GridID = gridID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to GridID
        /// </summary>
        public int GridID
        {
            get
            {
                return _GridID;
            }
            set
            {
                _GridID = value;
            }
        }
        public int SystemID
        {
            get
            {
                return _SystemID;
            }
            set
            {
                if (value >= 0)
                {
                    _SystemID = value;
                }
            }
        }
    }
}
