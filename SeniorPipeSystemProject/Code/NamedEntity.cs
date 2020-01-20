using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public abstract class NamedEntity
    {
        private String _Name;
 
        public NamedEntity( String name)
        {
            _Name = name;
        }

        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
    }
}
}

