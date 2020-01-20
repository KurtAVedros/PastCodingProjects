using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class Grade : NamedEntity
    {
        private int _GradeID;
        private float _MinYieldStr;
        private float _MaxYieldStr;
        private float _MinTensileStr;
        private float _MaxTensileStr;
        public Grade(int gradeID, String name, float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr) : base(name)
        {
            _GradeID = gradeID;
            _MinYieldStr = 0;
            if (minYieldStr >= _MinYieldStr)
            {
                _MinYieldStr = minYieldStr;
            }
            _MaxYieldStr = _MinYieldStr;
            if (maxYieldStr >= _MaxYieldStr)
            {
                _MaxYieldStr = maxYieldStr;
            }
            _MinTensileStr = 0;
            if (minTensileStr >= _MinTensileStr)
            {
                _MinTensileStr = minTensileStr;
            }
            _MaxTensileStr = _MinTensileStr;
            if (maxTensileStr >= _MaxTensileStr)
            {
                _MaxTensileStr = maxTensileStr;
            }
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to GradeID
        /// </summary>
        public int GradeID
        {
            get
            {
                return _GradeID;
            }
            set
            {
                _GradeID = value;
            }
        }

        public float MinYieldStrength
        {
            get
            {
                return _MinYieldStr;
            }

            set
            {
                if (value >= 0)
                {
                    _MinYieldStr = value;
                }
                
            }
        }
        public float MaxYieldStrength
        {
            get
            {
                return _MaxYieldStr;
            }

            set
            {
                if (value >= _MaxYieldStr)
                {
                    _MaxYieldStr = value;
                }
            }
        }
        public float MinTensileStrength
        {
            get
            {
                return _MinTensileStr;
            }

            set
            {
                if (value >= 0)
                {
                    _MinTensileStr = value;
                }
            }
        }
        public float MaxTensileStrength
        {
            get
            {
                return _MaxTensileStr;
            }

            set
            {
                if (value >= _MinTensileStr)
                {
                     _MaxTensileStr = value;
                }
               
            }
        }
    }
}
