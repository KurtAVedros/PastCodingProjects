using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    // originally written by Nathan Thompson. Date omitted.
    // removed Flow.
    /// <summary>
    /// Maps a connection between two components and the flow direction between them.
    /// </summary>
    public class Connection : NamedEntity
    {
        private int _ConnectionID;

        /// <summary>
        /// Creates a connection object.
        /// </summary>
        /// <param name="from"></param>
        public Connection(int connectionID, String name) : base(name)
        {
            _ConnectionID = connectionID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to ConnectionID
        /// </summary>
        public int ConnectionID {
            get
            {
                return _ConnectionID;
            }
            set
            {
                _ConnectionID = value;
            }
        }

    }
}

