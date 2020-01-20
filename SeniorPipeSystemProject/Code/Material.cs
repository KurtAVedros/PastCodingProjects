using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class Material : NamedEntity
    {
        private int _MaterialID;

        public Material(int materialID, String name) : base(name)
        {
            _MaterialID = materialID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to MaterialID
        /// </summary>
        public int MaterialID
        {
            get
            {
                return _MaterialID;
            }
            set
            {
                _MaterialID = value;
            }
        }
    }
}
