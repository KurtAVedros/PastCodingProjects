using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    /// <summary>
    /// Kurt A Vedros
    /// Added TransporterID
    /// Removed EntityName
    /// </summary>
    public class Fitting
    {
        private int _FittingID;
        private int _SplitterID;
        private int _ConverterID;
        private int _PlugID;
        private int _TransporterID;


        public Fitting(int splitterID, int SplitterID, int plugID, int ConverterID, int TransporterID)
        {
            if (splitterID >= 0 && ConverterID >= 0 && plugID >= 0)
            {
                throw new Exception("Splitter, Converter, and Plug IDs are specified.");
            } else
            if (splitterID >= 0 && ConverterID >= 0)
            {
                throw new Exception("Both splitter and converter ID are specified.");
            } else
            if (splitterID >= 0 && plugID >= 0)
            {
                throw new Exception("Both plug ID and splitter ID are specified.");
            } else
            if (ConverterID >= 0 && plugID >= 0)
            {
                throw new Exception("Both plug ID and converter ID are specified.");
            } else 
            if (splitterID <= 0 && ConverterID <= 0 && plugID <= 0)
            {
                throw new Exception("No subtype of fitting specified.");
            }
            _SplitterID = splitterID;
            _ConverterID = ConverterID;
            _PlugID = plugID;
            _TransporterID = TransporterID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to FittingID
        /// </summary>
        public int FittingID
        {
            get
            {
                return _FittingID;
            }
            set
            {
                _FittingID = value;
            }

        }

        /// <summary>
        /// Kurt Vedros
        /// Added sets
        /// </summary>
        public int PlugID
        {
            get
            {
                return _PlugID;
            }
            set
            {
                _PlugID = value;
            }
        }

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

        public int TransporterID
        {
            get
            {
                return _TransporterID;
            }
            set
            {
                if (value >= 0)
                {
                    _TransporterID = value;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double calculatePressure()
        {
            throw new NotImplementedException();
        }
    }
}
