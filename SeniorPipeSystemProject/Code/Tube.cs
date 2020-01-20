﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Tube : NamedEntity
    {
        private int _TubeID;
        private int _MaterialID;
        private int _GradeID;
        private float _Diameter;
        private float _Length;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="name"></param>
        /// <param name="material"></param>
        /// <param name="diameter"></param>
        /// <param name="gradient"></param>
        public Tube(int tubeID, int materialID, int gradeID, String name, float diameter, float length) : base(name)
        {
            _TubeID = tubeID;
            _GradeID = gradeID;
            _MaterialID = materialID;
            _Diameter = diameter;
            _Length = length;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to TubeID
        /// </summary>
        public int TubeID
        {
            get
            {
                return _TubeID;
            }
            set
            {
                _TubeID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaterialID
        {
            get
            {
                return _MaterialID;
            }
            set
            {
                if (value >= 0)
                {
                    _MaterialID = value;
                }
            }
        }

        public int GradeID
        {
            get
            {
                return _GradeID;
            }
            set
            {
                if (value >= 0)
                {
                    _GradeID = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float Diameter
        {
            get
            {
                return _Diameter;
            }
            set
            {
                if (value >= 0)
                {
                    _Diameter = value;
                }
            }
        }

        public float Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (value >= 0)
                {
                    _Length = value;
                }
            }
        }

        public double Circumference
        {
            get
            {
                return Math.PI * _Diameter;
            }
        }

        public double Area
        {
            get
            {
                return Math.PI * Math.Pow(_Diameter / 2, 2);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param>radiusOfPrevious</param>
        /// <param>Height of previous</param>
        /// <param>Height of self</param>
        /// <param>radius of self</param>
        /// <param>Fluid density (depends on temperature)</param>
        /// <param>Inlet_Pressure</param>
        /// <returns></returns>
        public double calculatePressure()
        {

            throw new NotImplementedException();
        }

        // calculate density of water by varying temperature.
        // https://sciencing.com/calculate-densities-various-temperatures-5999071.html
        // params are: initial density, initial temperature, final temperature, volumetric temperature expansion coefficient (of water) (0.0002 m3/m3 degrees C)
        // equation: final density = initial density / ((final temperature - initial temperature) * .0002 + 1)
        public double calculateDensity(double initialDensity, double initialTemp, double finalTemp)
        {
            return initialDensity / ((finalTemp - initialTemp) * 0.0002 + 1);
        }
        public double calculateDensity(double finalTemp)
        {
            // at 4 C, density of water is 1000 kg/m^3
            return 1000 / ((finalTemp - 4) * 0.0002 + 1);
        }
    }
}
