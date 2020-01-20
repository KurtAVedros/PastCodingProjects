using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    /// <summary>
    /// Kurt Vedros
    /// Removed FittingID
    /// Added parent NamedEntity
    /// </summary>
    public class Plug : NamedEntity
    {
        private int _PlugID;

        public Plug(int plugID, string Name) : base(Name)
        {
            _PlugID = plugID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to PlugID
        /// </summary>
        public int PlugID
        {
            get
            {
                return _PlugID;
            }
            set
            {
                _PlugID = value;
            }
        }
    }
}
