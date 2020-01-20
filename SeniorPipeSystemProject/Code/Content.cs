using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Created by Kurt A Vedros
/// This is a class to hold a content Object
/// </summary>
namespace SystemBuildingDevelopment
{
    public class Content : NamedEntity
    {
        /// <summary>
        /// Private variables of the features in the database
        /// </summary>
        private int _ContentID;
        private int _PartID;

        /// <summary>
        /// Constructor to the features
        /// </summary>
        /// <param name="ContentID"></param>
        /// <param name="TransporterID"></param>
        /// <param name="Name"></param>
        public Content(int contentID, int partID, string name) : base(name)
        {
            _ContentID = contentID;
            _PartID = partID;
        }

        /// <summary>
        /// Get and set methods of each feature.
        /// </summary>
        public int ContentID
        {
            get
            {
                return _ContentID;
            }
            set
            {
                _ContentID = value;
            }
        }

        public int PartID
        {
            get
            {
                return _PartID;
            }
            set
            {
                _PartID = value;
            }
        }
    }
}