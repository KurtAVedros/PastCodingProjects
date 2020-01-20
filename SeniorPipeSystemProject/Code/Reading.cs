using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    // Nathan Thompson. 10/26/18
    // Records the type of reading.

    public class Reading : NamedEntity
    {
        private int _ReadingID;

        public Reading(int readingID, String name) : base(name)
        {
            _ReadingID = readingID;
        }

        public int ReadingID
        {
            get
            {
                return _ReadingID;
            }
            set
            {
                if (value > -1)
                {
                    _ReadingID = value;
                }
            }
        }
    }
}
