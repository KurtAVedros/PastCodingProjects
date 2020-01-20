using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/// <summary>
/// DAL class Created by Kurt A Vedros
/// DAL for connecting the database of CS4488 Fall 2018 to the program software.
/// </summary
namespace SystemBuildingDevelopment
{
    public class DAL
    {
        private static string ReadOnlyConnectionString = "CANNOTSTATEHERE";
        private static string EditOnlyConnectionString = "CANNOTSTATEHERE";

        private DAL()
        {
        }

        #region Parts
        #region Assessments
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Assessment> AssessmentsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Assessment> retList = new List<Assessment>();
            try
            {
                comm.CommandText = "sprocGetAssessments";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Assessment newObj = FillAssessment(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Assessment with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Assessment AssessmentGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Assessment retObj = null;
            try
            {
                comm.CommandText = "sprocGetAssessment";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@AssessmentID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillAssessment(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an assessment to the Database table Assessment
        /// </summary>
        /// <param name="Assess"></param>
        /// <returns></returns>
        public static int AssessmentAdd(Assessment Assess)
        {
            int retInt = -1;
            if (Assess == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_AssessmentAdd";
                comm.Parameters.AddWithValue("@ContentID", Assess.ContentID);
                comm.Parameters.AddWithValue("@Value", Assess.Value);
                comm.Parameters.AddWithValue("@Threshold", Assess.Threshold);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@AssessmentID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                // Assess.AssessmentID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Assessment in the Database Assessment table by given ID
        /// </summary>
        /// <param name="Assess"></param>
        /// <returns></returns>
        public static int AssessmentUpdate(Assessment Assess)
        {
            int retInt;
            if (Assess == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_AssessmentUpdate";
                comm.Parameters.AddWithValue("@AssessmentID", Assess.AssessmentID);
                comm.Parameters.AddWithValue("@ContentID", Assess.ContentID);
                comm.Parameters.AddWithValue("@Value", Assess.Value);
                comm.Parameters.AddWithValue("@Threshold", Assess.Threshold);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Assessment from the DataBase Assessment Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int AssessmentRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_AssessmentRemove";
                comm.Parameters.AddWithValue("@AssessmentID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Assessment
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Assessment FillAssessment(SqlDataReader dr)
        {
            Assessment newObj = new Assessment((int)dr["AssessmentID"], (int)dr["ContentID"], (string)dr["Name"]
                    , (float)((double)dr["Value"]), (float)((double)dr["ThreshHold"]));
            return newObj;
        }
        #endregion

        #region Connections (REMOVED)
        /*
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Connection> ConnectionsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Connection> retList = new List<Connection>();
            try
            {
                comm.CommandText = "sprocGetConnections";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Connection newObj = FillConnection(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Connection with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Connection ConnectionGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Connection retObj = null;
            try
            {
                comm.CommandText = "sprocGetConnection";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ConnectionID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillConnection(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Connection to the Database table Connection
        /// </summary>
        /// <param name="Conn"></param>
        /// <returns></returns>
        public static int ConnectionAdd(Connection Conn)
        {
            int retInt = -1;
            if (Conn == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ConnectionAdd";
                //Change features
                comm.Parameters.AddWithValue("@Name", Conn.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ConnectionID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                Conn.ConnectionID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Connection in the Database Connection table by given ID
        /// </summary>
        /// <param name="Conn"></param>
        /// <returns></returns>
        public static int ConnectionUpdate(Connection Conn)
        {
            int retInt;
            if (Conn == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ConnectionUpdate";
                // change features
                comm.Parameters.AddWithValue("@ConnectionID", Conn.ConnectionID);
                comm.Parameters.AddWithValue("@Flow", Conn.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Connection from the DataBase Connection Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ConnectionRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ConnectionRemove";
                comm.Parameters.AddWithValue("@ConnectionID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Connection
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Connection FillConnection(SqlDataReader dr)
        {
            //Change Feature
            Connection newObj = new Connection((int)dr["ConnectionID"], (string)dr["Flow"]);
            return newObj;
        }
        */
        #endregion

        #region PartToParts
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.PartToPart> PartToPartsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<PartToPart> retList = new List<PartToPart>();
            try
            {
                comm.CommandText = "sprocGetPartToParts";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    PartToPart newObj = FillPartToParts(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the ConnectionToParts with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static PartToPart PartToPartsGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            PartToPart retObj = null;
            try
            {
                comm.CommandText = "sprocGetPartToPart";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PartToPartID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillPartToParts(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an ConnectionToParts to the Database table ConnectionToParts
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PartToPartsAdd(PartToPart TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PartToPartAdd";
                comm.Parameters.AddWithValue("@FromPartID", TempObject.FirstPartID);
                comm.Parameters.AddWithValue("@ToPartID", TempObject.SecondPartID);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@PartToPartID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                //TempObject.PartToPartID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an ConnectionToParts in the Database ConnectionToParts table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PartToPartsUpdate(PartToPart TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PartToPartUpdate";
                comm.Parameters.AddWithValue("@PartToPartID", TempObject.PartToPartID);
                comm.Parameters.AddWithValue("@FromPartID", TempObject.FirstPartID);
                comm.Parameters.AddWithValue("@ToPartID", TempObject.SecondPartID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an ConnectionToParts from the DataBase ConnectionToParts Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int PartToPartsRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PartToPartRemove";
                comm.Parameters.AddWithValue("@PartToPartID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the ConnectionToParts
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static PartToPart FillPartToParts(SqlDataReader dr)
        {
            PartToPart newObj = new PartToPart((int)dr["PartToPartID"], (int)dr["FromPartID"], (int)dr["ToPartID"]);
            return newObj;
        }
        #endregion

        #region Clients
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Client> ClientsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Client> retList = new List<Client>();
            try
            {
                comm.CommandText = "sprocGetClients";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Client newObj = FillClient(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Client with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Client ClientGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Client retObj = null;
            try
            {
                comm.CommandText = "sprocGetClient";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ClientID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillClient(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Client to the Database table Client
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ClientAdd(Client TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ClientAdd";
                comm.Parameters.AddWithValue("@ClientName", TempObject.Name);
                comm.Parameters.AddWithValue("@ClientMutex", TempObject.ClientMutex);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ClientID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.ClientID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Client in the Database Client table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ClientUpdate(Client TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ClientUpdate";
                comm.Parameters.AddWithValue("@ClientID", TempObject.ClientID);
                comm.Parameters.AddWithValue("@ClientName", TempObject.Name);
                comm.Parameters.AddWithValue("@ClientMutex", TempObject.ClientMutex);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Client from the DataBase Client Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ClientRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ClientRemove";
                comm.Parameters.AddWithValue("@ClientID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        public static int ClientMutexUpdate(int ClientID, bool ClientMutex)
        {
            int retInt;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ClientMutexUpdate";
                comm.Parameters.AddWithValue("@ClientID", ClientID);
                comm.Parameters.AddWithValue("@ClientMutex", ClientMutex);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        public static Client GetClientByName(string Name)
        {
            SqlCommand comm = new SqlCommand();
            Client retObj = null;
            try
            {
                comm.CommandText = "sprocGetClientByName";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ClientName", Name);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillClient(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        public static bool GetClientMutexByID(int ID)
        {
            SqlCommand comm = new SqlCommand();
            bool retObj = false;
            try
            {
                comm.CommandText = "sprocGetClientMutex";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ClientID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = (bool)dr["ClientMutex"];
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Auto fills the Client
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Client FillClient(SqlDataReader dr)
        {
            Client newObj = new Client((int)dr["ClientID"], (string)dr["ClientName"], (bool)dr["ClientMutex"]);
            return newObj;
        }
        #endregion

        #region Contents
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Content> ContentsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Content> retList = new List<Content>();
            try
            {
                comm.CommandText = "sprocGetContents";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Content newObj = FillContent(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Content with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Content ContentGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Content retObj = null;
            try
            {
                comm.CommandText = "sprocGetContent";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ContentID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillContent(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Content to the Database table Content
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ContentAdd(Content TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ContentAdd";
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ContentID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.ContentID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Content in the Database Content table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ContentUpdate(Content TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ContentUpdate";
                comm.Parameters.AddWithValue("@ContentID", TempObject.ContentID);
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Content from the DataBase Content Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ContentRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ContentRemove";
                comm.Parameters.AddWithValue("@ContentID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Content
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Content FillContent(SqlDataReader dr)
        {
            Content newObj = new Content((int)dr["ContentID"], (int)dr["PartID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Converters
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Converter> ConvertersGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Converter> retList = new List<Converter>();
            try
            {
                comm.CommandText = "sprocGetConverters";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Converter newObj = FillConverter(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Converter with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Converter ConverterGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Converter retObj = null;
            try
            {
                comm.CommandText = "sprocGetConverter";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ConverterID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillConverter(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Converter to the Database table Converter
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ConverterAdd(Converter TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ConverterAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@FromDiameter", TempObject.FromDiameter);
                comm.Parameters.AddWithValue("@ToDiameter", TempObject.ToDiameter);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ConverterID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.ConverterID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Converter in the Database Converter table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ConverterUpdate(Converter TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ConverterUpdate";
                comm.Parameters.AddWithValue("@ConverterID", TempObject.ConverterID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@FromDiameter", TempObject.FromDiameter);
                comm.Parameters.AddWithValue("@ToDiameter", TempObject.ToDiameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Converter from the DataBase Converter Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ConverterRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ConverterRemove";
                comm.Parameters.AddWithValue("@ConverterID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Converter
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Converter FillConverter(SqlDataReader dr)
        {
            Converter newObj = new Converter((int)dr["ConverterID"], (string)dr["Name"], (float)((double)dr["FromDiameter"])
                    , (float)((double)dr["ToDiameter"]));
            return newObj;
        }
        #endregion

        #region Cords
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Cord> CordsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Cord> retList = new List<Cord>();
            try
            {
                comm.CommandText = "sprocGetCords";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Cord newObj = FillCord(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Cord with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Cord CordGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Cord retObj = null;
            try
            {
                comm.CommandText = "sprocGetCord";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@CordID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillCord(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Cord to the Database table Cord
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int CordAdd(Cord TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_CordAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Length", TempObject.Length);
                comm.Parameters.AddWithValue("@VoltageFropRate", TempObject.VoltageDropRate);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@CordID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.CordID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Cord in the Database Cord table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int CordUpdate(Cord TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_CordUpdate";
                comm.Parameters.AddWithValue("@CordID", TempObject.CordID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Length", TempObject.Length);
                comm.Parameters.AddWithValue("@VoltageFropRate", TempObject.VoltageDropRate);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Cord from the DataBase Cord Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int CordRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_CordRemove";
                comm.Parameters.AddWithValue("@CordID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Cord
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Cord FillCord(SqlDataReader dr)
        {
            Cord newObj = new Cord((int)dr["CordID"], (string)dr["Name"]
                    , (float)((double)dr["Length"]), (float)((double)dr["VoltageDropRate"]));
            return newObj;
        }
        #endregion

        #region Exits
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Exit> ExitsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Exit> retList = new List<Exit>();
            try
            {
                comm.CommandText = "sprocGetExits";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Exit newObj = FillExit(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Exit with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Exit ExitGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Exit retObj = null;
            try
            {
                comm.CommandText = "sprocGetExit";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ExitID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillExit(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Exit to the Database table Exit
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ExitAdd(Exit TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ExitAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ExitID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.ExitID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Exit in the Database Exit table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ExitUpdate(Exit TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ExitUpdate";
                comm.Parameters.AddWithValue("ExitID", TempObject.ExitID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Exit from the DataBase Exit Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ExitRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ExitRemove";
                comm.Parameters.AddWithValue("@ExitID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Exit
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Exit FillExit(SqlDataReader dr)
        {
            Exit newObj = new Exit((int)dr["ExitID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Fittings (REMOVED)
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /*
        public static List<SystemBuildingDevelopment.Fitting> FittingsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Fitting> retList = new List<Fitting>();
            try
            {
                comm.CommandText = "sprocGetFittings";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Fitting newObj = FillFitting(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Fitting with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Fitting FittingGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Fitting retObj = null;
            try
            {
                comm.CommandText = "sprocGetFitting";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@FittingID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillFitting(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Fitting to the Database table Fitting
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int FittingAdd(Fitting TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_FittingAdd";
                comm.Parameters.AddWithValue("@SplitterID", TempObject.SplitterID);
                comm.Parameters.AddWithValue("@PlugID", TempObject.PlugID);
                comm.Parameters.AddWithValue("@ConverterID", TempObject.ConverterID);
                comm.Parameters.AddWithValue("@TransporterID", TempObject.TransporterID);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@FittingID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.FittingID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Fitting in the Database Fitting table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int FittingUpdate(Fitting TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_FittingUpdate";
                comm.Parameters.AddWithValue("@FittingID", TempObject.FittingID);
                comm.Parameters.AddWithValue("@SplitterID", TempObject.SplitterID);
                comm.Parameters.AddWithValue("@PlugID", TempObject.PlugID);
                comm.Parameters.AddWithValue("@ConverterID", TempObject.ConverterID);
                comm.Parameters.AddWithValue("@TransporterID", TempObject.TransporterID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Fitting from the DataBase Fitting Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int FittingRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_FittingRemove";
                comm.Parameters.AddWithValue("@FittingID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Fitting
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Fitting FillFitting(SqlDataReader dr)
        {
            Fitting newObj = new Fitting((int)dr["FittingID"], (int)dr["SplitterID"], (int)dr["PlugID"]
                , (int)dr["ConverterID"], (int)dr["TransporterID"]);
            return newObj;
        }
        #endregion

        #region FlowReguators
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.FlowRegulator> FlowRegulatorsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<FlowRegulator> retList = new List<FlowRegulator>();
            try
            {
                comm.CommandText = "sprocGetFlowRegulator";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    FlowRegulator newObj = FillFlowRegulator(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }
        */
        #endregion

        #region FlowRegulator
        /// <summary>
        /// Gets the FlowRegulator with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static FlowRegulator FlowRegulatorGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            FlowRegulator retObj = null;
            try
            {
                comm.CommandText = "sprocGetFlowRegulator";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@FlowRegulatorID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillFlowRegulator(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an FlowRegulator to the Database table FlowRegulator
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int FlowRegulatorAdd(FlowRegulator TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_FlowRegulatorAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@IsOpen", TempObject.IsOpen);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@FlowRegulatorID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.FlowRegulatorID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an FlowRegulator in the Database FlowRegulator table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int FlowRegulatorUpdate(FlowRegulator TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_FlowRegulatorUpdate";
                comm.Parameters.AddWithValue("@FlowRegulaterID", TempObject.FlowRegulatorID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@IsOpen", TempObject.IsOpen);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an FlowRegulator from the DataBase FlowRegulator Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int FlowRegulatorRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_FlowRegulatorRemove";
                comm.Parameters.AddWithValue("@FlowRegulatorID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the FlowRegulator
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static FlowRegulator FillFlowRegulator(SqlDataReader dr)
        {
            FlowRegulator newObj = new FlowRegulator((int)dr["FlowRegulatorID"], (string)dr["Name"], (bool)dr["IsOpen"]);
            return newObj;
        }
        #endregion

        #region Grades
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Grade> GradesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Grade> retList = new List<Grade>();
            try
            {
                comm.CommandText = "sprocGetGrades";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Grade newObj = FillGrade(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Grade with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Grade GradeGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Grade retObj = null;
            try
            {
                comm.CommandText = "sprocGetGrade";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@GradeID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillGrade(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Grade to the Database table Grade
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int GradeAdd(Grade TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GradeAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@MinYieldStr", TempObject.MinYieldStrength);
                comm.Parameters.AddWithValue("@MaxYieldStr", TempObject.MaxYieldStrength);
                comm.Parameters.AddWithValue("@MinTensileStr", TempObject.MinTensileStrength);
                comm.Parameters.AddWithValue("@MaxTensileStr", TempObject.MaxTensileStrength);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@GradeID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.GradeID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Grade in the Database Grade table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int GradeUpdate(Grade TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GradeUpdate";
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@MinYieldStr", TempObject.MinYieldStrength);
                comm.Parameters.AddWithValue("@MaxYieldStr", TempObject.MaxYieldStrength);
                comm.Parameters.AddWithValue("@MinTensileStr", TempObject.MinTensileStrength);
                comm.Parameters.AddWithValue("@MaxTensileStr", TempObject.MaxTensileStrength);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Grade from the DataBase Grade Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int GradeRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GradeRemove";
                comm.Parameters.AddWithValue("@GradeID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Grade
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Grade FillGrade(SqlDataReader dr)
        {
            Grade newObj = new Grade((int)dr["GradeID"], (string)dr["Name"], (float)((double)dr["MinYieldStr"])
                , (float)((double)dr["MaxYieldStr"]), (float)((double)dr["MinTensileStr"]), (float)((double)dr["MaxTensileStr"]));
            return newObj;
        }
        #endregion

        #region Grids
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Grid> GridsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Grid> retList = new List<Grid>();
            try
            {
                comm.CommandText = "sprocGetGrids";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Grid newObj = FillGrid(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Grid with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Grid GridGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Grid retObj = null;
            try
            {
                comm.CommandText = "sprocGetGrid";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@GridID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillGrid(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Grid to the Database table Grid
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int GridAdd(Grid TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GridAdd";
                comm.Parameters.AddWithValue("@SystemID", TempObject.SystemID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@GridID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.GridID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Grid in the Database Grid table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int GridUpdate(Grid TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GridUpdate";
                comm.Parameters.AddWithValue("@GridID", TempObject.GridID);
                comm.Parameters.AddWithValue("@SystemID", TempObject.SystemID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Grid from the DataBase Grid Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int GridRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GridRemove";
                comm.Parameters.AddWithValue("@GridID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Grid
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Grid FillGrid(SqlDataReader dr)
        {
            Grid newObj = new Grid((int)dr["GridID"], (int)dr["SystemID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region GridToParts
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.GridToPart> GridToPartsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<GridToPart> retList = new List<GridToPart>();
            try
            {
                comm.CommandText = "sprocGetGridToParts";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    GridToPart newObj = FillGridToPart(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the GridToPart with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static GridToPart GridToPartGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            GridToPart retObj = null;
            try
            {
                comm.CommandText = "sprocGetGridToPart";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@GridToPartID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillGridToPart(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an GridToPart to the Database table GridToPart
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int GridToPartAdd(GridToPart TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GridToPartAdd";
                comm.Parameters.AddWithValue("@GridID", TempObject.GridID);
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@XCoordinate", TempObject.XCoordinate);
                comm.Parameters.AddWithValue("@YCoordinate", TempObject.YCoordinate);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@GridToPartID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.GridToPartID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an GridToPart in the Database GridToPart table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int GridToPartUpdate(GridToPart TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GridToPartUpdate";
                comm.Parameters.AddWithValue("@GridToPartID", TempObject.GridToPartID);
                comm.Parameters.AddWithValue("@GridID", TempObject.GridID);
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@XCoordinate", TempObject.XCoordinate);
                comm.Parameters.AddWithValue("@YCoordinate", TempObject.YCoordinate);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an GridToPart from the DataBase GridToPart Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int GridToPartRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_GridToPartRemove";
                comm.Parameters.AddWithValue("@GridToPartID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the GridToPart
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static GridToPart FillGridToPart(SqlDataReader dr)
        {
            GridToPart newObj = new GridToPart((int)dr["GridToPartID"], (int)dr["GridID"], (int)dr["PartID"], 
                (int)dr["XCoordinate"], (int)dr["YCoordinate"]);
            return newObj;
        }
        #endregion

        #region Materials
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Material> MaterialsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Material> retList = new List<Material>();
            try
            {
                comm.CommandText = "sprocGetMaterials";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Material newObj = FillMaterial(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Material with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Material MaterialGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Material retObj = null;
            try
            {
                comm.CommandText = "sprocGetMaterial";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@MaterialID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillMaterial(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Material to the Database table Material
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int MaterialAdd(Material TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_MaterialAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@MaterialID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.MaterialID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Material in the Database Material table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int MaterialUpdate(Material TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_MaterialUpdate";
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Material from the DataBase Material Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int MaterialRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_MaterialRemove";
                comm.Parameters.AddWithValue("@MaterialID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Material
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Material FillMaterial(SqlDataReader dr)
        {
            Material newObj = new Material((int)dr["MaterialID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Parts
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Part> PartsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Part> retList = new List<Part>();
            try
            {
                comm.CommandText = "sprocGetParts";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Part newObj = FillPart(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Part with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Part PartGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Part retObj = null;
            try
            {
                comm.CommandText = "sprocGetPart";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PartID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillPart(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Part to the Database table Part
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PartAdd(Part TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PartAdd";
                comm.Parameters.AddWithValue("@SourceID", TempObject.SourceID);
                comm.Parameters.AddWithValue("@PipeID", TempObject.PipeID);
                comm.Parameters.AddWithValue("@TankID", TempObject.TankID);
                comm.Parameters.AddWithValue("@TubeID", TempObject.TubeID);
                comm.Parameters.AddWithValue("@ConverterID", TempObject.ConverterID);
                comm.Parameters.AddWithValue("SplitterID", TempObject.SplitterID);
                comm.Parameters.AddWithValue("@ValveID", TempObject.ValveID);
                comm.Parameters.AddWithValue("@PumpID", TempObject.PumpID);
                comm.Parameters.AddWithValue("@ExitID", TempObject.ExitID);
                comm.Parameters.AddWithValue("@CordID", TempObject.CordID);
                comm.Parameters.AddWithValue("@SwitchBoxID", TempObject.SwitchboxID);
                comm.Parameters.AddWithValue("@SwitchID", TempObject.SwitchID);


                SqlParameter retParameter;
                retParameter = new SqlParameter("@PartID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.PartID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Part in the Database Part table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PartUpdate(Part TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PartUpdate";
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@SourceID", TempObject.SourceID);
                comm.Parameters.AddWithValue("@PipeID", TempObject.PipeID);
                comm.Parameters.AddWithValue("@TankID", TempObject.TankID);
                comm.Parameters.AddWithValue("@TubeID", TempObject.TubeID);
                comm.Parameters.AddWithValue("@ConverterID", TempObject.ConverterID);
                comm.Parameters.AddWithValue("SplitterID", TempObject.SplitterID);
                comm.Parameters.AddWithValue("@ValveID", TempObject.ValveID);
                comm.Parameters.AddWithValue("@PumpID", TempObject.PumpID);
                comm.Parameters.AddWithValue("@ExitID", TempObject.ExitID);
                comm.Parameters.AddWithValue("@CordID", TempObject.CordID);
                comm.Parameters.AddWithValue("@SwitchBoxID", TempObject.SwitchboxID);
                comm.Parameters.AddWithValue("@SwitchID", TempObject.SwitchID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Part from the DataBase Part Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int PartRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PartRemove";
                comm.Parameters.AddWithValue("@PartID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Part
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Part FillPart(SqlDataReader dr)
        {
            Part newObj = new Part((int)dr["PartID"], (int)dr["SourceID"], (int)dr["PipeID"]
                , (int)dr["TankID"], (int)dr["TubeID"], (int)dr["ConverterID"], (int)dr["SplitterID"]
                , (int)dr["ValveID"], (int)dr["PumpID"], (int)dr["ExitID"]
                , (int)dr["CordID"], (int)dr["SwitchBoxID"], (int)dr["SwitchID"]);
            return newObj;
        }
        #endregion

        #region Pipes
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Pipe> PipesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Pipe> retList = new List<Pipe>();
            try
            {
                comm.CommandText = "sprocGetPipes";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Pipe newObj = FillPipe(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Pipe with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Pipe PipeGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Pipe retObj = null;
            try
            {
                comm.CommandText = "sprocGetPipe";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PipeID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillPipe(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Pipe to the Database table Pipe
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PipeAdd(Pipe TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PipeAdd";
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Diameter", TempObject.Diameter);
                comm.Parameters.AddWithValue("@Distance", TempObject.Length);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@PipeID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.PipeID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Pipe in the Database Pipe table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PipeUpdate(Pipe TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PipeUpdate";
                comm.Parameters.AddWithValue("@PipeID", TempObject.PipeID);
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Diameter", TempObject.Diameter);
                comm.Parameters.AddWithValue("@Distance", TempObject.Length);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Pipe from the DataBase Pipe Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int PipeRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PipeRemove";
                comm.Parameters.AddWithValue("@PipeID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Pipe
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Pipe FillPipe(SqlDataReader dr)
        {
            Pipe newObj = new Pipe((int)dr["PipeID"], (int)dr["GradeID"]
                , (int)dr["MaterialID"], (string)dr["Name"], (float)((double)dr["Diameter"]), (float)((double)dr["Distance"]));
            return newObj;
        }
        #endregion

        #region CAPs (REMOVED)
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        /*
        public static List<SystemBuildingDevelopment.Plug> PlugsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Plug> retList = new List<Plug>();
            try
            {
                comm.CommandText = "sprocGetPlugs";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Plug newObj = FillPlug(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Plug with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Plug PlugGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Plug retObj = null;
            try
            {
                comm.CommandText = "sprocGetPlug";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PlugID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillPlug(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Plug to the Database table Plug
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PlugAdd(Plug TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PlugAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@PlugID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.PlugID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Plug in the Database Plug table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PlugUpdate(Plug TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PlugUpdate";
                comm.Parameters.AddWithValue("@PlugID", TempObject.PlugID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Plug from the DataBase Plug Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int PlugRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PlugRemove";
                comm.Parameters.AddWithValue("@PlugID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Plug
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Plug FillPlug(SqlDataReader dr)
        {
            Plug newObj = new Plug((int)dr["PlugID"], (string)dr["Name"]);
            return newObj;
        }
        */
        #endregion

        #region Pumps
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Pump> PumpsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Pump> retList = new List<Pump>();
            try
            {
                comm.CommandText = "sprocGetPumps";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Pump newObj = FillPump(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Pump with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Pump PumpGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Pump retObj = null;
            try
            {
                comm.CommandText = "sprocGetPump";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PumpID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillPump(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Pump to the Database table Pump
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PumpAdd(Pump TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PumpAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@VerticalSuctionLift", TempObject.VerticalSuctionLift);
                comm.Parameters.AddWithValue("@MaximumHeadLift", TempObject.MaximumHeadLift);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@PumpID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.PumpID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Pump in the Database Pump table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PumpUpdate(Pump TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PumpUpdate";
                comm.Parameters.AddWithValue("@PumpID", TempObject.PumpID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@VerticalSuctionLift", TempObject.VerticalSuctionLift);
                comm.Parameters.AddWithValue("@MaximumHeadLift", TempObject.MaximumHeadLift);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Pump from the DataBase Pump Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int PumpRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PumpRemove";
                comm.Parameters.AddWithValue("@PumpID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Pump
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Pump FillPump(SqlDataReader dr)
        {
            Pump newObj = new Pump((int)dr["PumpID"], (string)dr["Name"]
                    , (float)((double)dr["VerticalSunctionLift"]), (float)((double)dr["MaximumHeadLift"]));
            return newObj;
        }
        #endregion

        #region Readings
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Reading> ReadingsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Reading> retList = new List<Reading>();
            try
            {
                comm.CommandText = "sprocGetReadings";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Reading newObj = FillReading(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Reading with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Reading ReadingGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Reading retObj = null;
            try
            {
                comm.CommandText = "sprocGetReading";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ReadingID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillReading(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Reading to the Database table Pump
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ReadingAdd(Reading TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ReadingAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ReadingID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.ReadingID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Reading in the Database Pump table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ReadingUpdate(Reading TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ReadingUpdate";
                comm.Parameters.AddWithValue("@ReadingID", TempObject.ReadingID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Reading from the DataBase Pump Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ReadingRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ReadingRemove";
                comm.Parameters.AddWithValue("@ReadingID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Pump
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Reading FillReading(SqlDataReader dr)
        {
            Reading newObj = new Reading((int)dr["ReadingID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Sensors
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Sensor> SensorsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Sensor> retList = new List<Sensor>();
            try
            {
                comm.CommandText = "sprocGetSensors";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Sensor newObj = FillSensor(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Sensor with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Sensor SensorGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Sensor retObj = null;
            try
            {
                comm.CommandText = "sprocGetSensor";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SensorID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSensor(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Sensor to the Database table Sensor
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SensorAdd(Sensor TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SensorAdd";
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SensorID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SensorID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Sensor in the Database Sensor table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SensorUpdate(Sensor TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SensorUpdate";
                comm.Parameters.AddWithValue("@SensorID", TempObject.SensorID);
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Sensor from the DataBase Sensor Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SensorRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SensorRemove";
                comm.Parameters.AddWithValue("@SensorID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Sensor
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Sensor FillSensor(SqlDataReader dr)
        {
            Sensor newObj = new Sensor((int)dr["SensorID"], (int)dr["PartID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region SensorToValues
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.SensorToValue> SensorToValuesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<SensorToValue> retList = new List<SensorToValue>();
            try
            {
                comm.CommandText = "sprocGetSensorToValues";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    SensorToValue newObj = FillSensorToValue(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the SensorToValue with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static SensorToValue SensorToValueGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            SensorToValue retObj = null;
            try
            {
                comm.CommandText = "sprocGetSensorToValue";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SensorToValueID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSensorToValue(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an SensorToValue to the Database table Pump
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SensorToValueAdd(SensorToValue TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SensorToValueAdd";
                comm.Parameters.AddWithValue("SensorID", TempObject.SensorID);
                comm.Parameters.AddWithValue("ValueID", TempObject.ValueID);
                comm.Parameters.AddWithValue("Threshold", TempObject.Threshold);
                comm.Parameters.AddWithValue("IsAboveCheck", TempObject.IsCheckingIfAbove);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SensorToValueID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SensorToValueID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an SensorToValue in the Database Pump table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SensorToValueUpdate(SensorToValue TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SensorToValueUpdate";
                comm.Parameters.AddWithValue("@SensorToValueID", TempObject.SensorToValueID);
                comm.Parameters.AddWithValue("SensorID", TempObject.SensorID);
                comm.Parameters.AddWithValue("ValueID", TempObject.ValueID);
                comm.Parameters.AddWithValue("Threshold", TempObject.Threshold);
                comm.Parameters.AddWithValue("IsAboveCheck", TempObject.IsCheckingIfAbove);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an SensorToValue from the DataBase Pump Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SensorToValueRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SensorToValueRemove";
                comm.Parameters.AddWithValue("@SensorToValueID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Pump
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static SensorToValue FillSensorToValue(SqlDataReader dr)
        {
            SensorToValue newObj = new SensorToValue((int)dr["SensorToValueID"], (int)dr["SensorID"], (int)dr["ValueID"], (float)((double)dr["Threshold"]), (bool)dr["IsAboveCheck"]);
            return newObj;
        }
        #endregion

        #region Sources
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Source> SourcesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Source> retList = new List<Source>();
            try
            {
                comm.CommandText = "sprocGetSources";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Source newObj = FillSource(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Source with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Source SourceGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Source retObj = null;
            try
            {
                comm.CommandText = "sprocGetSource";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SourceID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSource(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Source to the Database table Source
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SourceAdd(Source TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SourceAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SourceID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SourceID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Source in the Database Source table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SourceUpdate(Source TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SourceUpdate";
                comm.Parameters.AddWithValue("@SourceID", TempObject.SourceID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Source from the DataBase Source Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SourceRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SourceRemove";
                comm.Parameters.AddWithValue("@SourceID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Source
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Source FillSource(SqlDataReader dr)
        {
            Source newObj = new Source((int)dr["SourceID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Splitters
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Splitter> SplittersGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Splitter> retList = new List<Splitter>();
            try
            {
                comm.CommandText = "sprocGetSplitters";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Splitter newObj = FillSplitter(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Splitter with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Splitter SplitterGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Splitter retObj = null;
            try
            {
                comm.CommandText = "sprocGetSplitter";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SplitterID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSplitter(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Splitter to the Database table Splitter
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SplitterAdd(Splitter TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SplitterAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@NumberOfSplits", TempObject.NumberOfSplits);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SplitterID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SplitterID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Splitter in the Database Splitter table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SplitterUpdate(Splitter TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SplitterUpdate";
                comm.Parameters.AddWithValue("@SplitterID", TempObject.SplitterID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@NumberOfSplits", TempObject.NumberOfSplits);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Splitter from the DataBase Splitter Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SplitterRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SplitterRemove";
                comm.Parameters.AddWithValue("@SplitterID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Splitter
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Splitter FillSplitter(SqlDataReader dr)
        {
            Splitter newObj = new Splitter((int)dr["SplitterID"], (string)dr["Name"], (int)dr["NumberOfSplits"]);
            return newObj;
        }
        #endregion

        #region SwitchBoxes
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Switchbox> SwitchBoxsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Switchbox> retList = new List<Switchbox>();
            try
            {
                comm.CommandText = "sprocGetSwitchBoxs";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Switchbox newObj = FillSwitchBox(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the SwitchBox with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Switchbox SwitchBoxGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Switchbox retObj = null;
            try
            {
                comm.CommandText = "sprocGetSwitchBox";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SwitchBoxID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSwitchBox(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an SwitchBox to the Database table SwitchBox
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SwitchBoxAdd(Switchbox TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SwitchBoxAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SwitchBoxID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SwitchboxID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an SwitchBox in the Database SwitchBox table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SwitchBoxUpdate(Switchbox TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SwitchBoxUpdate";
                comm.Parameters.AddWithValue("SwitchBox", TempObject.SwitchboxID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an SwitchBox from the DataBase SwitchBox Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SwitchBoxRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SwitchBoxRemove";
                comm.Parameters.AddWithValue("@SwitchBoxID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the SwitchBox
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Switchbox FillSwitchBox(SqlDataReader dr)
        {
            Switchbox newObj = new Switchbox((int)dr["SwitchBoxID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Switches
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Switch> SwitchsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Switch> retList = new List<Switch>();
            try
            {
                comm.CommandText = "sprocGetSwitchs";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Switch newObj = FillSwitch(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Switch with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Switch SwitchGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Switch retObj = null;
            try
            {
                comm.CommandText = "sprocGetSwitch";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SwitchID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSwitch(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Switch to the Database table Switch
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SwitchAdd(Switch TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SwitchAdd";
                comm.Parameters.AddWithValue("@SwitchBoxID", TempObject.SwitchboxID);
                comm.Parameters.AddWithValue("@FlowRegulatorID", TempObject.FlowRegulatorID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SwitchID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SwitchID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Switch in the Database Switch table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SwitchUpdate(Switch TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SwitchUpdate";
                comm.Parameters.AddWithValue("@SwitchID", TempObject.SwitchID);
                comm.Parameters.AddWithValue("@SwitchBoxID", TempObject.SwitchboxID);
                comm.Parameters.AddWithValue("@FlowRegulatorID", TempObject.FlowRegulatorID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Switch from the DataBase Switch Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SwitchRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SwitchRemove";
                comm.Parameters.AddWithValue("@SwitchID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Switch
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Switch FillSwitch(SqlDataReader dr)
        {
            Switch newObj = new Switch((int)dr["SwitchID"], (int)dr["SwitchBoxID"], (int)dr["FlowRegulatorID"]
                    , (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #region Systems
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.CoreSystem> SystemsGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<CoreSystem> retList = new List<CoreSystem>();
            try
            {
                comm.CommandText = "sprocGetSystems";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    CoreSystem newObj = FillSystem(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the System with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static CoreSystem SystemGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            CoreSystem retObj = null;
            try
            {
                comm.CommandText = "sprocGetSystem";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SystemID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillSystem(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an System to the Database table System
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SystemAdd(CoreSystem TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SystemAdd";
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Mutex", TempObject.Mutex);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SystemID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.SystemID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an System in the Database System table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int SystemUpdate(CoreSystem TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SystemUpdate";
                comm.Parameters.AddWithValue("@SystemID", TempObject.SystemID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Mutex", TempObject.Mutex);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }



        /// <summary>
        /// Removes an System from the DataBase System Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int SystemRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SystemRemove";
                comm.Parameters.AddWithValue("@SystemID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        public static bool GetSystemMutexByID(int ID)
        {
            SqlCommand comm = new SqlCommand();
            bool retObj = false;
            try
            {
                comm.CommandText = "sprocGetSystemMutex";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SystemID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = (bool)dr["Mutex"];
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        public static List<int> GetAllClientsBySystem(int ID)
        {
            SqlCommand comm = new SqlCommand();
            List<int> retList = new List<int>();
            try
            {
                comm.CommandText = "sproc_GetAllClientsBySystem";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SystemID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    int newObj = (int)dr["ClientID"];
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Auto fills the System
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static CoreSystem FillSystem(SqlDataReader dr)
        {
            CoreSystem newObj = new CoreSystem((int)dr["SystemID"], (string)dr["Name"], (bool)dr["Mutex"]);
            return newObj;
        }
        #endregion

        #region SystemToClients
        public static int SystemToClientsAdd(int SystemID, int ClientID)
        {
            int retInt = -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_SystemToClientAdd";
                comm.Parameters.AddWithValue("@SystemID", SystemID);
                comm.Parameters.AddWithValue("@ClientID", ClientID);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@SystemToClientID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        #endregion

        #region Tanks
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Tank> TanksGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Tank> retList = new List<Tank>();
            try
            {
                comm.CommandText = "sprocGetTanks";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Tank newObj = FillTank(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Tank with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Tank TankGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Tank retObj = null;
            try
            {
                comm.CommandText = "sprocGetTank";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@TankID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillTank(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Tank to the Database table Tank
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int TankAdd(Tank TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TankAdd";
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Diameter", TempObject.Diameter);
                comm.Parameters.AddWithValue("@Distance", TempObject.Distance);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@TankID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.TankID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Tank in the Database Tank table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int TankUpdate(Tank TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TankUpdate";
                comm.Parameters.AddWithValue("@TankID", TempObject.TankID);
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Diameter", TempObject.Diameter);
                comm.Parameters.AddWithValue("@Distance", TempObject.Distance);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Tank from the DataBase Tank Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int TankRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TankRemove";
                comm.Parameters.AddWithValue("@TankID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Tank
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Tank FillTank(SqlDataReader dr)
        {
            Tank newObj = new Tank((int)dr["TankID"], (int)dr["GradeID"]
                , (int)dr["MaterialID"], (string)dr["Name"], (float)((double)dr["Diameter"]), (float)((double)dr["Distance"]));
            return newObj;
        }
        #endregion

        #region Transporters (REMOVED)
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        /*
        public static List<SystemBuildingDevelopment.Transporter> TransportersGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Transporter> retList = new List<Transporter>();
            try
            {
                comm.CommandText = "sprocGetTransporters";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Transporter newObj = FillTransporter(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Transporter with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Transporter TransporterGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Transporter retObj = null;
            try
            {
                comm.CommandText = "sprocGetTransporter";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@TransporterID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillTransporter(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Transporter to the Database table Transporter
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int TransporterAdd(Transporter TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TransporterAdd";
                comm.Parameters.AddWithValue("@ConnectionID", TempObject.ConnectionID);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@TransporterID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.TransporterID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Transporter in the Database Transporter table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int TransporterUpdate(Transporter TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TransporterUpdate";
                comm.Parameters.AddWithValue("@TransporterID", TempObject.TransporterID);
                comm.Parameters.AddWithValue("@ConnectionID", TempObject.ConnectionID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Transporter from the DataBase Transporter Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int TransporterRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TransporterRemove";
                comm.Parameters.AddWithValue("@TransporterID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Transporter
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Transporter FillTransporter(SqlDataReader dr)
        {
            Transporter newObj = new Transporter((int)dr["TransporterID"], (int)dr["ConnectionID"]);
            return newObj;
        }*/
        #endregion

        #region Tubes
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Tube> TubesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Tube> retList = new List<Tube>();
            try
            {
                comm.CommandText = "sprocGetTubes";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Tube newObj = FillTube(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Tube with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Tube TubeGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Tube retObj = null;
            try
            {
                comm.CommandText = "sprocGetTube";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@TubeID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillTube(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Tube to the Database table Tube
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int TubeAdd(Tube TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TubeAdd";
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Diameter", TempObject.Diameter);
                comm.Parameters.AddWithValue("@Distance", TempObject.Length);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@TubeID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.TubeID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Tube in the Database Tube table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int TubeUpdate(Tube TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TubeUpdate";
                comm.Parameters.AddWithValue("@TubeID", TempObject.TubeID);
                comm.Parameters.AddWithValue("@GradeID", TempObject.GradeID);
                comm.Parameters.AddWithValue("@MaterialID", TempObject.MaterialID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);
                comm.Parameters.AddWithValue("@Diameter", TempObject.Diameter);
                comm.Parameters.AddWithValue("@Distance", TempObject.Length);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Tube from the DataBase Tube Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int TubeRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_TubeRemove";
                comm.Parameters.AddWithValue("@TubeID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Tube
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Tube FillTube(SqlDataReader dr)
        {
            Tube newObj = new Tube((int)dr["TubeID"], (int)dr["GradeID"]
                , (int)dr["MaterialID"], (string)dr["Name"], (float)((double)dr["Diameter"]), (float)((double)dr["Distance"]));
            return newObj;
        }
        #endregion

        #region Values
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.DataValue> ValuesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<DataValue> retList = new List<DataValue>();
            try
            {
                comm.CommandText = "sprocGetValues";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    DataValue newObj = FillValue(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Value with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataValue ValueGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            DataValue retObj = null;
            try
            {
                comm.CommandText = "sprocGetValue";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ValueID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillValue(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Value to the Database table Value
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ValueAdd(DataValue TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ValueAdd";
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@ReadingID", TempObject.ReadingID);
                comm.Parameters.AddWithValue("@LocationFromStart", TempObject.LocationFromStart);
                comm.Parameters.AddWithValue("@Value", TempObject.Value);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ValueID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.DataValueID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Value in the Database Value table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ValueUpdate(DataValue TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ValueUpdate";
                comm.Parameters.AddWithValue("@ValueID", TempObject.DataValueID);
                comm.Parameters.AddWithValue("@PartID", TempObject.PartID);
                comm.Parameters.AddWithValue("@ReadingID", TempObject.ReadingID);
                comm.Parameters.AddWithValue("@LocationFromStart", TempObject.LocationFromStart);
                comm.Parameters.AddWithValue("@Value", TempObject.Value);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Value from the DataBase Value Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ValueRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ValueRemove";
                comm.Parameters.AddWithValue("@ValueID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Value
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static DataValue FillValue(SqlDataReader dr)
        {
            DataValue newObj = new DataValue((int)dr["ValueID"], (int)dr["PartID"], (int)dr["ReadingID"]
                    , (float)((double)dr["LocationFromStart"]), (float)((double)dr["Value"]));
            return newObj;
        }
        #endregion

        #region Valves
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Valve> ValvesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Valve> retList = new List<Valve>();
            try
            {
                comm.CommandText = "sprocGetValves";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Valve newObj = FillValve(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Valve with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Valve ValveGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Valve retObj = null;
            try
            {
                comm.CommandText = "sprocGetValve";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ValveID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillValve(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Valve to the Database table Valve
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ValveAdd(Valve TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ValveAdd";
                comm.Parameters.AddWithValue("@FlowRegulatorID", TempObject.FlowRegulatorID);
                comm.Parameters.AddWithValue("@NameID", TempObject.Name);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@ValveID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.ValveID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Valve in the Database Valve table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int ValveUpdate(Valve TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ValveUpdate";
                comm.Parameters.AddWithValue("@ValveID", TempObject.ValveID);
                comm.Parameters.AddWithValue("@FlowRegulatorID", TempObject.FlowRegulatorID);
                comm.Parameters.AddWithValue("@Name", TempObject.Name);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Valve from the DataBase Valve Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int ValveRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_ValveRemove";
                comm.Parameters.AddWithValue("@ValveID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Valve
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Valve FillValve(SqlDataReader dr)
        {
            Valve newObj = new Valve((int)dr["ValveID"], (int)dr["FlowRegulatorID"], (string)dr["Name"]);
            return newObj;
        }
        #endregion

        #endregion

        #region Pictures
        /// <summary>
        /// Gets a list of all objects from the database.
        /// </summary>
        /// <returns></returns>
        public static List<SystemBuildingDevelopment.Picture> PicturesGetAll()
        {
            SqlCommand comm = new SqlCommand();
            List<Picture> retList = new List<Picture>();
            try
            {
                comm.CommandText = "sprocGetPictures";
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Picture newObj = FillPicture(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Gets the Picture with ID number given
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Picture PictureGet(int ID)
        {
            SqlCommand comm = new SqlCommand();
            Picture retObj = null;
            try
            {
                comm.CommandText = "sprocGetPicture";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PictureID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    retObj = FillPicture(dr);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retObj;
        }

        /// <summary>
        /// Adds an Picture to the Database table Picture
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PictureAdd(Picture TempObject)
        {
            int retInt = -1;
            if (TempObject == null) return -1;

            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PictureAdd";
                comm.Parameters.AddWithValue("@PictureID", TempObject.PictureID);
                comm.Parameters.AddWithValue("@FileName", TempObject.FileName);
                comm.Parameters.AddWithValue("@PartName", TempObject.PartName);

                SqlParameter retParameter;
                retParameter = new SqlParameter("@PictureID", System.Data.SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(retParameter);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                int rowsAffected = comm.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    //There was a problem
                    retInt = -1;
                }
                else
                    retInt = (int)retParameter.Value;
                TempObject.PictureID = retInt;
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// This updates an Picture in the Database Picture table by given ID
        /// </summary>
        /// <param name="TempObject"></param>
        /// <returns></returns>
        public static int PictureUpdate(Picture TempObject)
        {
            int retInt;
            if (TempObject == null) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PictureUpdate";
                comm.Parameters.AddWithValue("@PictureID", TempObject.PictureID);
                comm.Parameters.AddWithValue("@FileName", TempObject.FileName);
                comm.Parameters.AddWithValue("@PartName", TempObject.PartName);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Removes an Picture from the DataBase Picture Table.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int PictureRemove(int ID)
        {
            int retInt;
            if (ID < 1) return -1;
            SqlCommand comm = new SqlCommand();
            try
            {
                comm.CommandText = "sproc_PictureRemove";
                comm.Parameters.AddWithValue("@PictureID", ID);

                comm.Connection = new SqlConnection(EditOnlyConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();

                retInt = comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retInt;
        }

        /// <summary>
        /// Auto fills the Picture
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static Picture FillPicture(SqlDataReader dr)
        {
            Picture newObj = new Picture((int)dr["PictureID"], (string)dr["FileName"], (string)dr["PartName"]);
            return newObj;
        }
        #endregion

        #region AdditionalProcedures
        public static List<DataValue> GetValuesByPartID(int ID)
        {
            SqlCommand comm = new SqlCommand();
            List<DataValue> retList = new List<DataValue>();
            try
            {
                comm.CommandText = "sprocGetValuesByPartID";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PartID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    DataValue newObj = FillValue(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Given a ContentID, returns all Assesments that has the ContentID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static List<Assessment> GetAssessmentsByContentID(int ID)
        {
            SqlCommand comm = new SqlCommand();
            List<Assessment> retList = new List<Assessment>();
            try
            {
                comm.CommandText = "sprocGetAssessmentsByContentID";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@ContentID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Assessment newObj = FillAssessment(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        /// <summary>
        /// Given an actual part name. EX Pipe. It will return all Database
        /// Part Tables that is that part.
        /// </summary>
        /// <param name="PartName"></param>
        /// <returns></returns>
        public static List<Part> GetAllPartsIs(String PartName)
        {
            String PartID = PartName + "ID";
            SqlCommand comm = new SqlCommand();
            List<Part> retList = new List<Part>();
            try
            {
                comm.CommandText = "sprocGetAllPartsIs";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue(@PartName, PartID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Part newObj = FillPart(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        public static List<Part> GetConnectsOfPart(int ID)
        {;
            SqlCommand comm = new SqlCommand();
            List<Part> retList = new List<Part>();
            try
            {
                comm.CommandText = "sprocGetConnentsOfPart";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PartID", ID);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    Part newObj = FillPart(dr);
                    retList.Add(newObj);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        public static List<float> GetValueOfPartWithReading(int ID, string ReadingName)
        {
            ;
            SqlCommand comm = new SqlCommand();
            List<float> retList = new List<float>();
            try
            {
                comm.CommandText = "sprocGetValueOfPartWithReading";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@PartID", ID);
                comm.Parameters.AddWithValue("@ReadingName", ReadingName);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    float ans = (float)((double)dr["Value"]);
                    retList.Add(ans);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }

        public static List<float> GetValueOfSensorWithReading(int ID, string ReadingName)
        {
            ;
            SqlCommand comm = new SqlCommand();
            List<float> retList = new List<float>();
            try
            {
                comm.CommandText = "sprocGetValueOfPartWithReading";
                comm.CommandType = System.Data.CommandType.StoredProcedure;

                comm.Parameters.AddWithValue("@SensorID", ID);
                comm.Parameters.AddWithValue("@ReadingName", ReadingName);
                comm.Connection = new SqlConnection(ReadOnlyConnectionString);
                comm.Connection.Open();
                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    float ans = (float)((double)dr["Value"]);
                    retList.Add(ans);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (comm.Connection != null)
                    comm.Connection.Close();
            }
            return retList;
        }
        #endregion
    }
}
