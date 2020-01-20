using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    public class Tank : NamedEntity
    {
        private int _TankID;
        private int _MaterialID;
        private int  _GradeID;
        private float _Diameter;
        private float _Distance;
        private double _Volume;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentID"></param>
        /// <param name="name"></param>
        /// <param name="materialID"></param>
        /// <param name="gradient"></param>
        /// <param name="diameter"></param>
        /// <param name="Distance"></param>
        public Tank(int tankID,  int gradeID, int materialID, String name, float diameter, float distance) : base(name)
        {
            _TankID = tankID;
            _MaterialID = materialID;
            _GradeID = gradeID;
            _Diameter = diameter;
            _Distance = distance;
            _Volume = -1;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to TankID
        /// </summary>
        public int TankID
        {
            get
            {
                return _TankID;
            }
            set
            {
                _TankID = value;
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

        public float Distance
        {
            get
            {
                return _Distance;
            }
            set
            {
                if (value >= 0)
                {
                    _Distance = value;
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

        public double TotalVolume
        {
            get
            {
                if (_Volume < 0)
                {
                    _Volume = Area * Distance;
                }
                return _Volume;
            }
        }

        public double getVolumeFilled(float Distance)
        {
            return Area * Distance;
        }

        //public double getPressureAtDistance(float Distance)
        //{
        //    return 1.422 * Distance;
        //}

        //https://sciencing.com/calculate-water-pressure-tank-volume-6326635.html
        //https://sciencing.com/calculate-elevated-water-storage-tanks-5858171.html
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double calculatePressure()
        {
            throw new NotImplementedException();
            // need only Distance of water in tank. 
            // p = 1.422 * Distance
            // 1.422 is measured in meters.
        }
    }
}
