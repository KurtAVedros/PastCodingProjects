using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    // originally written by Nathan Thompson. Date omitted.
    /// <summary>
    /// Ties to Content. Permits the associationo f a given value for a given object with a given attribute and system component.
    /// </summary>
    public class Assessment : NamedEntity
    {
        private int _AssessmentID;
        private int _ContentID;
        private float _Value;
        private float _Threshold;

        public Assessment(int assessmentID, int contentID, String name, float value, float threshold) : base(name)
        {
            _AssessmentID = assessmentID;
            _ContentID = contentID;
            _Value = value;
            _Threshold = threshold;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method
        /// </summary>
        public int AssessmentID
        {
            get
            {
                return _AssessmentID;
            }
            set
            {
                _AssessmentID = value;
            }
        }

        public int ContentID
        {
            get
            {
                return _ContentID;
            }
            set
            {
                if (value >= 0)
                {
                    _ContentID = value;
                }
            }
        }

        public float Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        public float Threshold
        {
            get
            {
                return _Threshold;
            }
            set
            {
                _Threshold = value;
            }
        }

        // may be problematic since thresholds can be upper or lower bounds
        // however, the database only records one threshold, not two.
        public bool IsSafe
        {
            get
            {
                return (Threshold < Value);
            }
        }

    }
}
