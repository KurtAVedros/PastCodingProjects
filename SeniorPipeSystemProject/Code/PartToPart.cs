namespace SystemBuildingDevelopment
{
    public class PartToPart
    {
        private int _PartToPartID;
        private int _FirstPartID;
        private int _SecondPartID;

        public PartToPart(int partToPartID, int firstPartID, int secondPartID)
        {
            _PartToPartID = partToPartID;
            _FirstPartID = firstPartID;
            _SecondPartID = secondPartID;
        }

        public int PartToPartID
        {
            get
            {
                return _PartToPartID;
            }
        }

        public int FirstPartID
        {
            get
            {
                return _FirstPartID;
            }
            set
            {
                if (value >= 0)
                {
                    _FirstPartID = value;
                }
            }
        }

        public int SecondPartID
        {
            get
            {
                return _SecondPartID;
            }
            set
            {
                if (value >= 0)
                {
                    _SecondPartID = value;
                }
            }
        }
    }
}