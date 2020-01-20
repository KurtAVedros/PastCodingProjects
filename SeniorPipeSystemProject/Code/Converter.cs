using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    /// <summary>
    /// Kurt Vedros
    /// Had to change features because there were the wrong ones.
    /// Removed: FittingID, _NumberOfSplits
    /// Added: NAme, FromDiameter, ToDiameter and parent NamedEntity
    /// </summary>

    // Nathan Thompson
    // Added set condition ot diameters. Fixed some capitalization and typos.
    // Class now reflects the addition of transporterID.
    public class Converter : NamedEntity
    {
        private int _ConverterID;
        private float _FromDiameter;
        private float _ToDiameter;

        public Converter(int converterID, string name, float fromDiameter, float toDiameter): base(name)
        {
            _ConverterID = converterID;
            _FromDiameter = fromDiameter;
            _ToDiameter = toDiameter;
        }

        /// <summary>
        /// Kurt Vedros
        /// Added set method
        /// </summary>
        public int ConverterID
        {
            get
            {
                return _ConverterID;
            }
            set
            {
                _ConverterID = value;
            }
        }

        /// <summary>
        /// Kurt A Vedros
        /// Get and Sets for From Diameter and To Diameter
        /// </summary>
        public float FromDiameter
        {
            get
            {
                return _FromDiameter;
            }
            set
            {
                if (value >= 0) {
                    _FromDiameter = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public float ToDiameter
        {
            get
            {
                return _ToDiameter;
            }
            set
            {
                if (value >= 0)
                {
                    _ToDiameter = value;
                }
            }
        }
    }
}
