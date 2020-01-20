using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class CoreSystem : NamedEntity
    {
        private int _SystemID;
        private bool _Mutex;
        
        public CoreSystem(int systemID, String name, bool Mutex) : base(name)
        {
            _SystemID = systemID;
            _Mutex = Mutex;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to ConnectionID
        /// </summary>
        public int SystemID
        {
            get
            {
                return _SystemID;
            }
            set
            {
                _SystemID = value;
            }
        }

        public bool Mutex
        {
            get
            {
                return _Mutex;
            }
            set
            {
                _Mutex = value;
            }
        }
    }
}
