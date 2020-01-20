using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    // Kurt A Vedros
    // Removed FromDiameter, ToDiameter and Fitting
    // Added NamedEntity parent and NumberOfSplits
    // Think Nathon got the splitters and Converters swapped.

    // Nathan Thompson
    // Edited class to reflect changes to the ERD: addition of transporterID.

    public class Splitter : NamedEntity
    {
        private int _SplitterID;
        // interpreted as the number of number of pipes exiting a splitter.
        private int _NumberOfSplits;

        public Splitter(int splitterID, string name, int numberOfSplits): base(name)
        {
            _SplitterID = splitterID;
            _NumberOfSplits = numberOfSplits;
        }

        public bool IsCap
        {
            get
            {
                return (_NumberOfSplits == 0);
            }
        }

        public bool IsStraight
        {
            get
            {
                return (_NumberOfSplits == 1);
            }
        }

        public String Classification
        {
            get
            {
                if (IsCap)
                {
                    return "Cap";
                } else if (IsStraight)
                {
                    return "Straight";
                } else if (_NumberOfSplits == 2)
                {
                    return "T";
                } else if  (_NumberOfSplits == 3)
                {
                    return "Three-Way";
                }
                else
                {
                    return "Four-Way";
                }
            }
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to SplitterID
        /// </summary>
        public int SplitterID
        {
            get
            {
                return _SplitterID;
            }
            set
            {
                _SplitterID = value;
            }
        }

        public int NumberOfSplits
        {
            get
            {
                return _NumberOfSplits;
            }
            set
            {
                if (value >= 0 && value <= 4)
                {
                    _NumberOfSplits = value;
                }
            }
        }
    }
}
