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
    /// Removed COnsumptionRate as this will be the same as 
    /// Flow going out and can be stated in Value Table and Connection Table
    /// </summary>
    public class Exit : NamedEntity
    {
        private int _ExitID;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="name"></param>
        /// <param name="consumptionRate"></param>
        public Exit(int exitID, String name) : base(name)
        {
            _ExitID = exitID;

        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to ExitID
        /// </summary>
        public int ExitID
        {
            get
            {
                return _ExitID;
            }
            set
            {
                _ExitID = value;
            }
        }
    }
}
