using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemBuildingDevelopment
{
    /// <summary>
    /// Kurt A Vedros
    /// Removed MaxProductionRate as it is appart of Values which contains the starting
    /// and current flow of content.
    /// Added TransporterID to class
    /// </summary>
     
    // Nathan Thompson
    // 
    public class Source : NamedEntity
    {
        private int _SourceID;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="name"></param>
        /// <param name="maxProductionRate"></param>
        public Source(int sourceID, String name) : base(name)
        {
            _SourceID = sourceID;
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to SourceID
        /// </summary>
        public int SourceID
        {
            get
            {
                return _SourceID;
            }
            set
            {
                _SourceID = value;
            }
        }
    }
}
