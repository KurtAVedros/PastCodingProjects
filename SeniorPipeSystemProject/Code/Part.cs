using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemBuildingDevelopment
{
    public class Part
    {
        private int _PartID;
        private int _ValveID;
        private int _SourceID;
        private int _SwitchID;
        private int _SwitchboxID;
        private int _PipeID;
        private int _TankID;
        private int _CordID;
        private int _PumpID;
        private int _TubeID;
        private int _ExitID;
        private int _ConverterID;
        private int _SplitterID;
        private string _Type;

        /// <summary>
        /// Kurt A Vedros
        /// Needed to swich order to Alphabetical order otherwise it seems random
        /// and hard to use.
        /// </summary>
        /// <param name="partID"></param>
        /// <param name="sourceID"></param>
        /// <param name="pipeID"></param>
        /// <param name="tubeID"></param>
        /// <param name="exitID"></param>
        public Part(int partID, int sourceID, int pipeID, int splitterID, 
            int converterID, int tankID, int tubeID, int valveID, int pumpID,
            int exitID, int cordID, int switchboxID, int switchID)
        {
            _PartID = partID;
            _ValveID = valveID;
            _SourceID = sourceID;
            _SwitchID = switchID;
            _SwitchboxID = switchboxID;
            _PipeID = pipeID;
            _TankID = tankID;
            _CordID = cordID;
            _PumpID = pumpID;
            _TubeID = tubeID;
            _ExitID = exitID;
            _ConverterID = converterID;
            _SplitterID = splitterID;
            setType();
        }

        /// <summary>
        /// Kurt A Vedros
        /// Added the set method, changed ID to PartID
        /// </summary>
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

        public int ValveID
        {
            get
            {
                return _ValveID;
            }
            set
            {
                    _ValveID = value;
            }
        }

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

        public int SwitchID
        {
            get
            {
                return _SwitchID;
            }
            set
            {
                    _SwitchID = value;
            }
        }
        public int SwitchboxID
        {
            get
            {
                return _SwitchboxID;
            }
            set
            {
                _SwitchboxID = value;
            }
        }
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
        public int PipeID
        {
            get
            {
                return _PipeID;
            }
            set
            {
                _PipeID = value;
            }
        }
        public int PumpID
        {
            get
            {
                return _PumpID;
            }
            set
            {
                _PumpID = value;
            }
        }
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

        public int CordID
        {
            get
            {
                return _CordID;
            }
            set
            {
                _CordID = value;
            }
        }
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

        public String Type
        {
            get
            {
                return _Type;
            }
        }

        private void setType()
        {
            int defaultID = UICodeInterface.DefaultID;
            if (ConverterID > defaultID)
            {
                _Type = "Converter";
            }
            else
            {
                if (CordID > defaultID)
                {
                    _Type = "Cord";
                }
                else
                {
                    if (ExitID > defaultID)
                    {
                        _Type = "Exit";
                    }
                    else
                    {
                        if (PipeID > defaultID)
                        {
                            _Type = "Pipe";
                        }
                        else
                        {
                            if (PumpID > defaultID)
                            {
                                _Type = "Pump";
                            }
                            else
                            {
                                if (SourceID > defaultID)
                                {
                                    _Type = "Source";
                                }
                                else
                                {
                                    if (SplitterID > defaultID)
                                    {
                                        _Type = "Splitter";
                                    }
                                    else
                                    {
                                        if (SwitchboxID > defaultID)
                                        {
                                            _Type = "Switchbox";
                                        }
                                        else
                                        {
                                            if (SwitchID > defaultID)
                                            {
                                                _Type = "Switch";
                                            }
                                            else
                                            {
                                                if (TankID > defaultID)
                                                {
                                                    _Type = "Tank";
                                                }
                                                else
                                                {
                                                    if (TubeID > defaultID)
                                                    {
                                                        _Type = "Tube";
                                                    }
                                                    else
                                                    {
                                                        if (ValveID > defaultID)
                                                        {
                                                            _Type = "Valve";
                                                        }
                                                        else
                                                        {
                                                            throw new Exception("Invalid Part Type");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
