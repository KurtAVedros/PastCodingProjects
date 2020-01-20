using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Switchbox : NamedEntity
    {
        private int _SwitchboxID;


        public Switchbox(int switchboxID, String name) : base(name)
        {
            _SwitchboxID = switchboxID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to SwitchBoxID
        /// </summary>
        public int SwitchboxID
        {
            get
            {
                return _SwitchboxID;
            }
            set
            {
                _SwitchboxID = value;
            }
        }
    }
}
