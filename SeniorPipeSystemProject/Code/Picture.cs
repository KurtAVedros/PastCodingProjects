using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Kurt A Vedros
/// Class that contains all information on the name of the 
/// Picture that the sensor is trying to read.
/// </summary>
namespace SystemBuildingDevelopment
{
    public class Picture
    {
        private int _PictureID;
        private String _FileName;
        private String _PartName;

        public Picture(int PictureID, String FileName, String PartName)
        {
            _PictureID = PictureID;
            _FileName = FileName;
            _PartName = PartName;
        }

        public int PictureID
        {
            get
            {
                return _PictureID;
            }
            set
            {
                _PictureID = value;
            }
        }

        public String FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
            }
        }

        public String PartName
        {
            get
            {
                return _PartName;
            }
            set
            {
                _PartName = value;
            }
        }
    }
}