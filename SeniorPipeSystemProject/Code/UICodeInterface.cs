using System;
using System.Collections.Generic;

namespace SystemBuildingDevelopment
{

    /// <summary>
    /// Collection of methods handling the various "add" interactions between UI, classes, and DAL.
    /// </summary>
    public static class UICodeInterface
    {
        #region globals
        private static readonly int defaultID = 0;

        //---------------------------------------------------------------------
        // Kurt Vedros
        /// <summary>
        /// Local Client, activeSystem and tempClientList information.
        /// </summary>
        private static Client localClient = new Client(0, "Temp", false);
        private static CoreSystem activeSystem = DAL.SystemGet(1);
        private static List<int> tempClientsIDs = new List<int>();
        private static Grid activeGrid = DAL.GridGet(1);//new Grid(0,0,"Temp");
        //DAL.GridAdd(activeGrid);
        #endregion

        public static int DefaultID { get { return defaultID; } }



        #region client
        // Kurt Vedros
        /// <summary>
        /// Returns the Local Clients Mutex
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool getClientMutex()
        {
            return DAL.GetClientMutexByID(localClient.ClientID);
        }

        // Kurt Vedros
        /// <summary>
        /// Does this on program startup.
        /// Fixes the null system/client error.
        /// </summary>
        /// <param name="Name"></param>
        public static void startProgram(string Name)
        {
            changeSystem();
            changeClient(Name);
        }

        // Kurt Vedros
        public static void changeSystem()
        {
            activeSystem = DAL.SystemGet(1);
            activeGrid = DAL.GridGet(1);
        }

        // Kurt Vedros
        /// <summary>
        /// Changes the Local Client Information to the information Provided.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Name"></param>
        public static void changeClient(string Name)
        {
            localClient = DAL.GetClientByName(Name);
            if (localClient == null)
            {
                localClient = new Client(0, Name, false);
                DAL.ClientAdd(localClient);
                localClient = DAL.GetClientByName(Name);

                DAL.SystemToClientsAdd(activeSystem.SystemID, localClient.ClientID);
            }
        }

        /*
        // Not Implemented, instead uses one system
        // called Demo
        public static void changeSystem(string Name)
        {
            localClient = DAL.GetSystemByName(Name);
            if (localClient == null)
            {
                localClient = new Client(0, Name, false);
                DAL.ClientAdd(localClient);
                localClient = DAL.GetClientByName(Name);
            }
        }
        */

        //Kurt A Vedros
        /// <summary>
        /// Changes the system mutex to 1 so that no
        /// one else can update the database
        /// </summary>
        public static void lockSystemMutex()
        {
            activeSystem.Mutex = true;
            DAL.SystemUpdate(activeSystem);
        }

        //Kurt A Vedros
        /// <summary>
        /// Changes the system mutex to 0 so that
        /// someone else can update the database
        /// </summary>
        public static void unlockSystemMutex()
        {
            activeSystem.Mutex = false;
            DAL.SystemUpdate(activeSystem);
        }

        //Kurt A Vedros
        /// <summary>
        /// Changes the client mutex to 0 so that
        /// the program knows its up to date
        /// </summary>
        public static void unlockClientMutex()
        {
            localClient.ClientMutex = false;
            DAL.SystemUpdate(activeSystem);
        }

        //Kurt A Vedros
        /// <summary>
        /// gets the current system mutex
        /// </summary>
        /// <returns></returns>
        public static bool getSystemMutex()
        {
            return DAL.GetSystemMutexByID(activeSystem.SystemID);

        }

        // Kurt A Vedros
        /// <summary>
        /// Changes all of the clients Mutex to 1 so that they
        /// are notified to update the grid
        /// </summary>
        /// <param name="ID"></param>
        public static void changeAllClientsMutexBySystemToTrue()
        {
            tempClientsIDs = DAL.GetAllClientsBySystem(activeSystem.SystemID);
            foreach (int ClientID in tempClientsIDs)
            {
                DAL.ClientMutexUpdate(ClientID, true);
            }
        }

        //---------------------------------------------------------------------
        #endregion

        #region add
        // Nathan Thompson. Created and wrote method. 10/15/18
        /// <summary>
        /// Adds a new system to the database.
        /// </summary>
        /// <param name="systemName">The name of the system to add</param>
        /// <returns>ID of the created System.</returns>
        public static int addSystemToDatabase(String systemName)
        {
            return getIDOfSystem(systemName);
        }
        public static int addPartToPartToDatabase(int firstPartID, int secondPartID)
        {
            int id = defaultID;
            if (DAL.PartToPartsGet(firstPartID) != null && DAL.PartToPartsGet(secondPartID) != null)
            {
                id = checkForExistingPartToPart(DAL.PartToPartsGetAll(), firstPartID, secondPartID);
                if (id == defaultID)
                {
                    id = DAL.PartToPartsAdd(new PartToPart(defaultID, firstPartID, secondPartID));
                }
            }
            return id;
        }

        // Nathan Thompson. Created method. 10/15/18
        /// <summary>
        /// Creates a new exit based on the required attributes.
        /// </summary>
        /// <returns>ID of the Exit.</returns>
        public static int addExitToDatabase(String exitName)
        {
            int id = DAL.ExitAdd(new Exit(defaultID, exitName));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created method and wrote method. 10/15/18
        public static int addGridToDatabase(String systemName, String gridName)
        {
            int systemID = getIDOfSystem(systemName);
            // need check for duplicate.
            return DAL.GridAdd(new Grid(defaultID, systemID, gridName));
        }

        public static int addGridToPartToDatabase(int gridID, int partID, int x, int y)
        {

            if (DAL.GridGet(gridID) == null)
            {
                gridID = activeGrid.GridID;
            }
            int id = defaultID;
            id = checkForExistingGridToPart(DAL.GridToPartsGetAll(), gridID, partID);
            if (id <= 0)
            {
                id = DAL.GridToPartAdd(new GridToPart(defaultID, gridID, partID, x, y));
            }
            return id;
        }
        // Nathan Thompson. Created method. 10/15/18
        /// <summary>
        /// Creates a new source based on the required attributes.
        /// </summary>
        /// <returns>ID of the Source.</returns>
        public static int addSourceToDatabase(String sourceName)
        {
            int id = DAL.SourceAdd(new Source(defaultID, sourceName));
            int partID = DAL.PartAdd(new Part(defaultID, id, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID));
            return partID;
        }
        //// Nathan Thompson
        //public int addPlugToDatabase(String plugName)
        //{
        //    // could have this called from Fitting, in which case fitting would need decision logic
        //    // or could have caller do it, and quietly add fitting in the background.
        //    // thus skipping decision logic. Also, since fitting requires no data of its own but IDs,
        //    // if the user will not need to explicitly create a fitting, but will need a plug, converter, or splitter.
        //    int returnObject = DAL.TransporterAdd(new Transporter(defaultID, defaultID));

        //    // would check for identical plugs first, but fittings and plugs have a 1:1 relationship.
        //    int plugID = DAL.PlugAdd(new Plug(defaultID, plugName));
        //    int fittingID = DAL.FittingAdd(new Fitting(defaultID,defaultID, plugID, defaultID, returnObject));

        //    // slight problem: with only plug ID, can only get fittingID by a select on Fittings WHERE PlugID = plugID.
        //    return plugID;
        //}

        // Nathan Thompson. Created and wrote method. 10/15/18
        /// <summary>
        /// Adds a splitter with the designated attributes to the database.
        /// </summary>
        /// <param name="splitterName">Name of the splitter</param>
        /// <param name="connectionName">Name of the connection</param>
        /// <param name="flow">Direction of the flow</param>
        /// <param name="numberOfSplits">Number of splits. 0 means cap, 1 straight, 2 bifurcation, etc.</param>
        /// <returns>ID of created splitter</returns>
        public static int addSplitterToDatabase(String splitterName, int numberOfSplits)
        {

            int splitterID = DAL.SplitterAdd(new Splitter(defaultID, splitterName, numberOfSplits));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, splitterID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created and wrote method. 10/15/18
        /// <summary>
        /// Creates a converter in the database.
        /// </summary>
        /// <param name="converterName">Name of the converter.</param>
        /// <param name="toDiameter">Final diameter</param>
        /// <param name="fromDiameter">Initial diameter</param>
        /// <returns>Id of created converter</returns>
        public static int addConverterToDatabase(String converterName, float toDiameter, float fromDiameter)
        {
            int converterID = DAL.ConverterAdd(new Converter(defaultID, converterName, toDiameter, fromDiameter));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, converterID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created method. 10/15/18
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gradeValue">The grade value</param>
        /// <param name="gradeName">Name of the grade.</param>
        /// <param name="materialName">Name of the material</param>
        /// <param name="tubeName">Name of the tube</param>
        /// <param name="connectionName">Name of the connection</param>
        /// <param name="flow">Direciton of flow</param>
        /// <param name="diameter">Diameter of tube</param>
        /// <param name="length">Length of tube</param>
        /// <returns></returns>
        public static int addTubeToDatabase(float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr, String gradeName, String materialName, String tubeName, float diameter, float length)
        {
            int gradeID = getIDOfGrade(gradeName, minYieldStr, maxYieldStr, minTensileStr, maxTensileStr);
            int materialID = getIDOfMaterial(materialName);
            int id = DAL.TubeAdd(new Tube(defaultID, gradeID, materialID, tubeName, diameter, length));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created method. 10/15/18
        public static int addPumpToDatabase(float verticalSuctionLift, float maxHeadLift, String pumpName)
        {

            int id = DAL.PumpAdd(new Pump(defaultID, pumpName, verticalSuctionLift, maxHeadLift));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created method. 10/15/18
        public static int addCordToDatabase(String cordName, float length, float voltageDropRate)
        {
            int id = DAL.CordAdd(new Cord(defaultID, cordName, length, voltageDropRate));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created Aand wrote method. 10/15/18
        public static int addValveToDatabase(String valveName, String regulatorName, Boolean isOpen)
        {
            int flowRegulatorID = DAL.FlowRegulatorAdd(new FlowRegulator(defaultID, regulatorName, isOpen));
            int id = DAL.ValveAdd(new Valve(defaultID, flowRegulatorID, valveName));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created method. 10/15/18
        public static int addSwitchToDatabase(String switchName, String regulatorName, String switchboxName, String switchboxConnectionName, String switchboxFlow, Boolean isOpen)
        {
            int flowRegulatorID = DAL.FlowRegulatorAdd(new FlowRegulator(defaultID, regulatorName, isOpen));
            int id = DAL.SwitchAdd(new Switch(defaultID, addSwitchboxToDatabase(switchboxName), flowRegulatorID, switchName));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, -id));
            return partID;
        }

        // Nathan Thompson. Created method. 10/19/18
        public static int addSwitchToDatabase(String switchName, String regulatorName, int switchboxID, Boolean isOpen)
        {
            int flowRegulatorID = DAL.FlowRegulatorAdd(new FlowRegulator(defaultID, regulatorName, isOpen));
            int id = DAL.SwitchAdd(new Switch(defaultID, switchboxID, flowRegulatorID, switchName));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id));
            return partID;
        }

        // Nathan Thompson. Created method. 10/15/18
        // feels weird.
        public static int addSwitchboxToDatabase(String switchboxName)
        {
            int id = DAL.SwitchBoxAdd(new Switchbox(defaultID, switchboxName));
            DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID));
            return id;
        }

        public static int addReadingToDatabase(String name)
        {
            return DAL.ReadingAdd(new Reading(defaultID, name));
        }

        public static int addSensorToValueToDatabase(int sensorID, int valueID, float threshold, bool isCheckingIfAbove)
        {
            return DAL.SensorToValueAdd(new SensorToValue(defaultID, sensorID, valueID, threshold, isCheckingIfAbove));
        }

        public static int addSensorToValueToDatabase(int sensorID, float threshold, bool isCheckingIfAbove, int partID, int readingID, String valueName, float locationFromStart, float value)
        {
            int valueID = DAL.ValueAdd(new DataValue(defaultID, partID, readingID, locationFromStart, value));

            return DAL.SensorToValueAdd(new SensorToValue(defaultID, sensorID, valueID, threshold, isCheckingIfAbove));
        }

        public static int addSensorToValueToDatabase(int partID, String sensorName, int valueID, float threshold, bool isCheckingIfAbove)
        {
            int sensorID = DAL.SensorAdd(new Sensor(defaultID, partID, sensorName));
            return DAL.SensorToValueAdd(new SensorToValue(defaultID, sensorID, valueID, threshold, isCheckingIfAbove));
        }

        public static int addSensorToValueToDatabase(int sensorPartID, String sensorName, float threshold, bool isCheckingIfAbove, int valuePartID, int readingID, String valueName, float locationFromStart, float value)
        {
            int valueID = DAL.ValueAdd(new DataValue(defaultID, valuePartID, readingID, locationFromStart, value));
            int sensorID = DAL.SensorAdd(new Sensor(defaultID, sensorPartID, sensorName));
            return DAL.SensorToValueAdd(new SensorToValue(defaultID, sensorID, valueID, threshold, isCheckingIfAbove));
        }

        // need check if sensor is present; if value is present


        // Nathan Thompson. Created method. 10/15/18
        // Nathan Thompson. Wrote method. 10/16/18

        public static int addValueToDatabase(int partID, int readingID, String valueName, float locationFromStart, float value)
        {
            //
            // sensorToValue

            return DAL.ValueAdd(new DataValue(defaultID, partID, readingID, locationFromStart, value));
        }

        // Nathan Thompson. Created method. 10/15/18
        public static int addSensorToDatabase(int partID, String sensorName)
        {
            // requires an existing transporter
            // what about value?
            return DAL.SensorAdd(new Sensor(defaultID, partID, sensorName));
        }

        // Nathan Thompson. Created method. 10/15/18
        public static int addTankToDatabase(float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr, String gradeName, String materialName, String tankName, float diameter, float length)
        {
            int materialID = getIDOfMaterial(materialName);
            int gradeID = getIDOfGrade(gradeName, minYieldStr, maxYieldStr, minTensileStr, maxTensileStr);
            int id = DAL.TankAdd(new Tank(defaultID, materialID, gradeID, tankName, diameter, length));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, defaultID, defaultID, defaultID, id, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID));
            return partID;
        }

        // Nathan Thompson. Created method. 10/15/18
        public static int addContentToDatabase(int partID, String contentName)
        {
            int id = -1;
            // requires an existing transporter
            id = DAL.ContentAdd(new Content(defaultID, partID, contentName));
            return id;
        }
        // Nathan Thompson. Created method. 10/15/18
        /// <summary>
        /// Adds an assessment to the database; depends on knowing the transporter which it describes. 
        /// </summary>
        /// <param name="assessmentName">Name of the assessment</param>
        /// <param name="value">Value of the assessment</param>
        /// <param name="threshold">Threshold at which a value is no longer safe</param>
        /// <param name="returnObject">ID of the trasnporter with which the assessment is associated.</param>
        /// <param name="contentName">Name of the content with which the assessment should be associated.</param>
        /// <returns></returns>
        public static int addAssessmentToDatabase(String assessmentName, float value, float threshold, int partID, String contentName)
        {
            int contentID = getIDOfContent(partID, contentName);
            return DAL.AssessmentAdd(new Assessment(defaultID, contentID, assessmentName, value, threshold));
        }

        // Nathan Thompson. Created method. 10/19/18
        /// <summary>
        /// Adds an assessment to the database; depends on knowing the content which it describes. 
        /// </summary>
        /// <param name="assessmentName">Name of the assessment</param>
        /// <param name="value">Value of the assessment</param>
        /// <param name="threshold">Threshold at which a value is no longer safe</param>
        /// <param name="contentID">Id of associated content</param>
        /// <returns></returns>
        public static int addAssessmentToDatabase(String assessmentName, float value, float threshold, int contentID)
        {
            return DAL.AssessmentAdd(new Assessment(defaultID, contentID, assessmentName, value, threshold));
        }

        // Nathan Thompson. Created and wrote method. 10/15/18
        public static int addGradeToDatabase(float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr, String gradeName)
        {
            return getIDOfGrade(gradeName, minYieldStr, maxYieldStr, minTensileStr, maxTensileStr);
        }
        // Nathan Thompson. Created and wrote method. 10/14/18
        /// <summary>
        /// Adds the pipe using the provided data. No validation. Adds any necessary rows.
        /// </summary>
        /// <param name="gradeValue">Grade value</param>
        /// <param name="gradeName">Name of grade</param>
        /// <param name="materialName">Name of material</param>
        /// <param name="pipeName">Name of pipe</param>
        /// <param name="connectionName">Name of connection</param>
        /// <param name="flow">Direction of flow</param>
        /// <param name="diameter">Diameter of pipe</param>
        /// <param name="length">Length of pipe</param>
        /// <returns></returns>
        public static int addPipeToDatabase(float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr, String gradeName, String materialName, String pipeName, float diameter, float length, int gridID, int xCoord, int yCoord)
        {
            // Each pipe can reasonably have an unique transporter associated with it. 
            // Even if not, when it is new, it has no connections yet.
            int gradeID = getIDOfGrade(gradeName, minYieldStr, maxYieldStr, minTensileStr, maxTensileStr);
            int materialID = getIDOfMaterial(materialName);
            int id = DAL.PipeAdd(new Pipe(defaultID, gradeID, materialID, pipeName, diameter, length));
            int partID = DAL.PartAdd(new Part(defaultID, defaultID, id, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID, defaultID));
            insertPartIntoGrid(gridID, partID, xCoord, yCoord);
            return partID;
        }

        // gridID - if system has no grids, add a grid. Set it via attr in the .gridWrapper
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridID">id for the grid in the ui</param>
        /// <param name="partID">id for part at the x,y location in the grid</param>
        /// <param name="x"> x coord on the grid in the UI</param>
        /// <param name="y"> y coord on the grid in the UI</param>
        private static void insertPartIntoGrid(int gridID, int partID, int x, int y)
        {
            // check for grid?
            // system?
            // client?
            addGridToPartToDatabase(gridID, partID, x, y);
        }

        // Nathan Thompson. Created and wrote method. 10/14/18
        public static int addMaterialToDatabase(String materialName)
        {
            return getIDOfMaterial(materialName);
        }
        public static int addPictureToDatabase(String fileName, String partName)
        {
            int id = checkForExistingPicture(DAL.PicturesGetAll(), fileName);
            if (id == defaultID)
            {
                id = DAL.PictureAdd(new Picture(defaultID, fileName, partName));
            }
            return DAL.PictureAdd(new Picture(defaultID, fileName, partName));
        }
        #endregion

        #region checkFor
        // Nathan Thompson. Created and wrote method. 10/15/18
        /// <summary>
        /// Checks to see if the material exists and adds it to the appropriate table if it doesn't.
        /// </summary>
        /// <param name="systemName">The name of the system to add</param>
        /// <returns>ID of the created System.</returns>
        private static int getIDOfSystem(String systemName)
        {
            // if non-zero, system already exists.
            int id = checkForSystem(DAL.SystemsGetAll(), systemName);
            if (id < 0)
            {
                id = DAL.SystemAdd(new CoreSystem(defaultID, systemName, false));
            }
            return id;
        }


        // checks to if an identical Content already exists in the database. If not, adds it and returns the ID.
        private static int getIDOfContent(int partID, String contentName)
        {
            int id = DAL.ContentAdd(new Content(defaultID, partID, contentName));
            return id;
        }

        // Nathan Thompson. Created and wrote method. 10/14/18
        // checks to see if the material exists and adds it to the appropriate table if it doesn't.
        private static int getIDOfMaterial(String materialName)
        {
            int id = checkForMaterial(DAL.MaterialsGetAll(), materialName);
            if (id < 0)
            {
                id = DAL.MaterialAdd(new Material(0, materialName));
            }
            return id;
        }
        // Nathan Thompson. Created and wrote method. 10/14/18
        // Checks to if an identical grade already exists in the database. If not, adds it and returns the ID.
        private static int getIDOfGrade(String gradeName, float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr)
        {
            int id = checkForGrade(DAL.GradesGetAll(), gradeName, minYieldStr, maxYieldStr, minTensileStr, maxTensileStr);
            if (id < 0)
            {
                id = DAL.GradeAdd(new Grade(defaultID, gradeName, minYieldStr, maxYieldStr, minTensileStr, maxTensileStr));
            }
            return id;
        }

        // Nathan Thompson. Created and wrote method. 10/14/18
        // gets the id of the material if the material exists.
        private static int checkForMaterial(List<Material> materials, String materialName)
        {
            int id = -1;
            foreach (Material material in materials)
            {
                if (material.Name.ToLower() == materialName.ToLower())
                {
                    id = material.MaterialID;
                }
            }
            return id;
        }

        public static bool updateGridToPart(int gridToPartID, int gridID, int partID, int x, int y)
        {
            // if equal to 0, no part was affected.
            // if equal to -1, exception occurred.
            return (DAL.GridToPartUpdate(new GridToPart(gridToPartID, gridID, partID, x, y)) == 1);
        }

        // note if x and y are different then should have used an update statement.
        private static int checkForExistingGridToPart(List<GridToPart> gridToParts, int gridID, int partID)
        {
            int id = defaultID;
            foreach (GridToPart gridToPart in gridToParts)
            {
                if (gridToPart.GridID == gridID && gridToPart.PartID == partID)
                {
                    id = gridToPart.GridToPartID;
                }
            }
            return id;
        }
        // Nathan Thompson. Created and wrote method. 10/14/18
        // checks to if an identical grade already exists in the database.
        private static int checkForGrade(List<Grade> grades, String gradeName, float minYieldStr, float maxYieldStr, float minTensileStr, float maxTensileStr)
        {
            int id = -1;
            foreach (Grade grade in grades)
            {
                if (grade.MinYieldStrength == minYieldStr
                    && grade.MinTensileStrength == minTensileStr
                    && grade.MaxYieldStrength == maxYieldStr
                    && grade.MaxTensileStrength == maxTensileStr
                    && grade.Name.ToLower() == gradeName.ToLower())
                {
                    id = grade.GradeID;
                }
            }
            return id;
        }

        // Nathan Thompson. Created and wrote method. 10/15/18
        /// <summary>
        /// Gets the id of the material if the material exists.
        /// </summary>
        /// <param name="systems">The collection of systems from databse</param>
        /// <param name="systemName">Name of the system to add.</param>
        /// <returns>ID of system if it is in the databse. defaultID if not present.</returns>
        private static int checkForSystem(List<CoreSystem> systems, String systemName)
        {
            int id = defaultID;
            foreach (CoreSystem system in systems)
            {
                if (system.Name.ToLower() == systemName.ToLower())
                {
                    id = system.SystemID;
                }
            }
            return id;
        }

        private static int checkForExistingPartToPart(List<PartToPart> partToParts, int firstPartID, int secondPartID)
        {
            int id = defaultID;
            foreach (PartToPart partToPart in partToParts)
            {
                if ((partToPart.FirstPartID == firstPartID && partToPart.SecondPartID == secondPartID)
                    || (partToPart.SecondPartID == firstPartID && partToPart.SecondPartID == firstPartID))
                {
                    id = partToPart.PartToPartID;
                }
            }
            return id;
        }

        private static int checkForExistingPicture(List<Picture> pictures, string fileName)
        {
            int id = -1;
            foreach (Picture picture in pictures)
            {
                if (picture.FileName.Equals(fileName))
                {
                    id = picture.PictureID;
                }
            }
            return id;
        }
        #endregion

        #region get
        // returns the specific object associated with a part iD
        public static object fetchObjectByPartID(int id)
        {
            Part part = DAL.PartGet(id);

            object returnObject = null;

            // -1 may not be the best value to check against.
            if (part.ConverterID > defaultID)
            {
                returnObject = DAL.ConverterGet(part.ConverterID);
            }
            else
            {
                if (part.CordID > defaultID)
                {
                    returnObject = DAL.CordGet(part.CordID);
                }
                else
                {
                    if (part.ExitID > defaultID)
                    {
                        returnObject = DAL.ExitGet(part.ExitID);
                    }
                    else
                    {
                        if (part.PipeID > defaultID)
                        {
                            returnObject = DAL.PipeGet(part.PipeID);
                        }
                        else
                        {
                            if (part.PumpID > defaultID)
                            {
                                returnObject = DAL.PumpGet(part.PumpID);
                            }
                            else
                            {
                                if (part.SourceID > defaultID)
                                {
                                    returnObject = DAL.SourceGet(part.SourceID);
                                }
                                else
                                {
                                    if (part.SplitterID > defaultID)
                                    {
                                        returnObject = DAL.SplitterGet(part.SplitterID);
                                    }
                                    else
                                    {
                                        if (part.SwitchboxID > defaultID)
                                        {
                                            returnObject = DAL.SwitchBoxGet(part.SwitchboxID);
                                        }
                                        else
                                        {
                                            if (part.SwitchID > defaultID)
                                            {
                                                returnObject = DAL.SwitchGet(part.SwitchID);
                                            }
                                            else
                                            {
                                                if (part.TankID > defaultID)
                                                {
                                                    returnObject = DAL.TankGet(part.TankID);
                                                }
                                                else
                                                {
                                                    if (part.TubeID > defaultID)
                                                    {
                                                        returnObject = DAL.TubeGet(part.TubeID);
                                                    }
                                                    else
                                                    {
                                                        if (part.ValveID > defaultID)
                                                        {
                                                            returnObject = DAL.ValveGet(part.ValveID);
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

            return returnObject;
        }

        public static object fetchGridToPartByPartID(int id)
        {
            GridToPart gridToPart = DAL.GridToPartGet(id);
            return gridToPart;
        }

        // returns a list of structs containing the type of the part, the gridToPart object associated with the part, and the picture associated with the part
        public static List<PictureGridPart> associatePictureAndGridToPart(List<GridToPart> gridToParts)
        {
            List<PictureGridPart> pictureGridParts = new List<PictureGridPart>();
            foreach (GridToPart gridToPart in gridToParts)
            {
                String partType = DAL.PartGet(gridToPart.PartID).Type;
                String fileName = selectPictureOnType(partType).FileName;
                pictureGridParts.Add(new PictureGridPart(fileName, partType, gridToPart));
            }

            return pictureGridParts;
        }

        // if no picture, returns generic.
        public static Picture selectPictureOnType(String type)
        {
            List<Picture> pictures = DAL.PicturesGetAll();
            int index = 0;
            Picture picture = pictures[index];
            // note: grabs the first picture associated with its part.
            while (!type.Equals(picture.PartName) && index < pictures.Count - 1)
            {
                index++;
                picture = pictures[index];
            }
            if (!type.Equals(picture.PartName))
            {
                // no right picture; use generic or select one arbitrarily. Like the first or last.
                //without something specific here, it defaults to the last picture.
            }
            return picture;
        }

        public struct PictureGridPart
        {
            public String pictureFileName;
            public String partType;
            public GridToPart gridToPart;

            public PictureGridPart(String fileName, String partType, GridToPart gtp)
            {
                pictureFileName = fileName;
                this.partType = partType;
                gridToPart = gtp;
            }

        }
        #endregion


    }
}
