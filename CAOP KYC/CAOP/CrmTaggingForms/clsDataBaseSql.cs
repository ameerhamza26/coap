using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;



namespace Profile
{
    public class clsDataBaseSql
    {
        SqlConnection objCon;
        SqlCommand objCom;
        SqlDataAdapter objDa;
        SqlTransaction sqlTrans;

        #region "Connection Functions"
        private bool OpenConnection(string ConnectionString)
        {
            if (objCon == null)
                objCon = new SqlConnection();
            objCon.ConnectionString = ConnectionString;
            objCon.Open();
            if (objCon.State == ConnectionState.Open)
                return true;
            else
                return false;
        }
        private void CloseConnection()
        {
            objCon.Close();
        }
        #endregion

        #region "Command Functions"
        public void ExecuteCommandNoLog(string ConnectionString, string CommandText)
        {
            try
            {
                OpenConnection(ConnectionString);
                objCom = new SqlCommand(CommandText, objCon);
                objCom.CommandTimeout = 60;
                objCom.ExecuteNonQuery();
                objCom.Dispose();
                CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                CloseConnection();
            }
        }
        public void ExecuteCommand(string ConnectionString, string CommandText)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(),"ExecuteCommand "+ CommandText);
                OpenConnection(ConnectionString);
                objCom = new SqlCommand(CommandText, objCon);
                objCom.ExecuteNonQuery();
                objCom.Dispose();
                CloseConnection();
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteCommand Successfull");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                CloseConnection();
            }
        }
        public string ExecuteScalar(string ConnectionString, string CommandText)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteScalar " + CommandText);
                OpenConnection(ConnectionString);
                objCom = new SqlCommand(CommandText, objCon);
                string ReturnValue = Convert.ToString(objCom.ExecuteScalar());
                objCom.Dispose();
                CloseConnection();
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteScalar Successfull, Returning value= " + ReturnValue);
                return ReturnValue;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                CloseConnection();
            }
        }
        #endregion

        #region "Tabular Functions"
        public DataTable GetTable(string ConnectionString, string CommandText)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetTable " + CommandText);
                DataSet ds = new DataSet();
                OpenConnection(ConnectionString);
                objDa = new SqlDataAdapter(CommandText, objCon);
                objDa.Fill(ds);
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetTable Successfull " + CommandText);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objDa != null)
                    objDa.Dispose();
                CloseConnection();
            }

        }

        public void UpdateTable(string ConnectionString, string CommandTextSelect, string CID, string Name)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetTable " + CommandText);
                DataSet ds = new DataSet();
                OpenConnection(ConnectionString);
                objDa = new SqlDataAdapter(CommandTextSelect, objCon);

                objDa.UpdateCommand = new SqlCommand("UPDATE CIF SET NAM = ? " +
                                                   "WHERE ACN = ?", objCon);

                objDa.UpdateCommand.Parameters.Add("@Name", SqlDbType.VarChar, 15, "NAM");

                SqlParameter workParm = objDa.UpdateCommand.Parameters.Add("@ACN", SqlDbType.VarChar);
                workParm.SourceColumn = "ACN";
                workParm.SourceVersion = DataRowVersion.Original;
                objDa.Fill(ds);

                DataRow cRow = ds.Tables[0].Rows[0];
                cRow["Nam"] = Name;
                objDa.Update(ds.Tables[0]);
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetTable Successfull " + CommandText);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objDa != null)
                    objDa.Dispose();
                CloseConnection();
            }

        }

        public void insertDataBulkCopy(string ConnectionString, DataTable dtACN, DataTable dtCIF, string strACNTableName, string strCIFTableName, string BranchCode, string CID, string acn)
        {
            try
            {
                List<clsParameters> lst = new List<clsParameters>();
                lst.Add(new clsParameters("@BranchCode", BranchCode, "IN"));
                lst.Add(new clsParameters("@acn", acn, "IN"));
                List<string> Queries = new List<string>();
                Queries.Add("Delete from ProfileData_Temp where Boo=" + BranchCode + " and CID in (" + CID + ")");
                Queries.Add("Delete from acn where Boo=" + BranchCode + " and acn in (" + acn + ")");
                Queries.Add("Delete from cif where Boo=" + BranchCode + " and acn in (" + acn + ")");
                ExecuteTransaction(ConnectionString, Queries);

                SqlBulkCopy bulkCopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.TableLock);
                // write acn
                bulkCopy.DestinationTableName = strACNTableName;
                bulkCopy.BulkCopyTimeout = 1200;
                bulkCopy.BatchSize = 5000;
                bulkCopy.WriteToServer(dtACN);

                //// write cif
                bulkCopy.DestinationTableName = strCIFTableName;
                bulkCopy.BulkCopyTimeout = 1200;
                bulkCopy.BatchSize = 5000;
                bulkCopy.WriteToServer(dtCIF);


                ExecuteSP(ConnectionString, "UpdateSqlData", lst);

                //for (int i = 0; i <= dtACN.Rows.Count - 1; i++)
                //{
                //    if ((dtACN.Rows[i]["TYPE"].ToString() == "3534") || (dtACN.Rows[i]["TYPE"].ToString() == "4559"))
                //        ExecuteSP(ConnectionString, "DataAssanComplain", lst);
                //    else
                //        ExecuteSP(ConnectionString, "UpdateSqlData", lst);
                //}

                Queries.Add("Delete from ProfileData_Temp where Boo=" + BranchCode + " and CID in (" + CID + ")");
                ExecuteTransaction(ConnectionString, Queries);
            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        #endregion

        #region "Stored Procedure"
        public string GetValueUsingSP(string ConnectionString, string SPName, List<clsParameters> Parameters)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetValueUsingSP " + SPName);
                DataSet ds = new DataSet();
                OpenConnection(ConnectionString);
                objCom = new SqlCommand(SPName, objCon);
                objCom.CommandType = CommandType.StoredProcedure;
                SqlParameter InParam, OutParam = null;
                for (int i = 0; i < Parameters.Count; i++)
                {
                    if (Parameters[i].Direction.Equals("OUT", StringComparison.OrdinalIgnoreCase))
                    {
                        OutParam = new SqlParameter(Parameters[i].Name, SqlDbType.Text);
                        OutParam.Direction = ParameterDirection.Output;
                        OutParam.Size = 32000;
                        objCom.Parameters.Add(OutParam);
                    }
                    else
                    {
                        InParam = new SqlParameter(Parameters[i].Name, Parameters[i].Value);
                        objCom.Parameters.Add(InParam);
                    }
                }
                objCom.ExecuteNonQuery();
                CloseConnection();
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetValueUsingSP Successfull, Return Value= " + OutParam.Value.ToString());
                return OutParam.Value.ToString();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                CloseConnection();
            }
        }

        public DataTable GetTableUsingSP(string ConnectionString, string SPName, List<clsParameters> Parameters)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetTableUsingSP " + SPName);
                DataSet ds = new DataSet();
                OpenConnection(ConnectionString);
                objCom = new SqlCommand(SPName, objCon);
                objCom.CommandType = CommandType.StoredProcedure;
                objCom.CommandTimeout = 240;
                objDa = new SqlDataAdapter(objCom);
                SqlParameter InParam, OutParam = null;
                for (int i = 0; i < Parameters.Count; i++)
                {
                    if (Parameters[i].Direction.Equals("OUT", StringComparison.OrdinalIgnoreCase))
                    {
                        OutParam = new SqlParameter(Parameters[i].Name, SqlDbType.VarChar);
                        OutParam.Direction = ParameterDirection.Output;
                        OutParam.Size = 10;
                        objCom.Parameters.Add(OutParam);
                    }
                    else
                    {
                        InParam = new SqlParameter(Parameters[i].Name, Parameters[i].Value);
                        objCom.Parameters.Add(InParam);
                    }
                }
                objDa.Fill(ds);
                CloseConnection();
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "GetTableUsingSP Successfull " + SPName);
                return ds.Tables[0];

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                if (objDa != null)
                    objDa.Dispose();
                CloseConnection();
            }
        }

        public void ExecuteSP(string ConnectionString, string SPName, List<clsParameters> Parameters)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteSP " + SPName);
                OpenConnection(ConnectionString);
                objCom = new SqlCommand(SPName, objCon);
                objCom.CommandType = CommandType.StoredProcedure;

                for (int i = 0; i < Parameters.Count; i++)
                    objCom.Parameters.Add(new SqlParameter(Parameters[i].Name, Parameters[i].Value));

                objCom.ExecuteNonQuery();
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteSP Successfull " + SPName);
                CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                CloseConnection();
            }
        }
        #endregion

        public void TestMethod()
        {
            string ConstrTest = ConfigurationManager.ConnectionStrings["ConstrTest"].ConnectionString;
            SqlConnection nwindConn = new SqlConnection(ConstrTest);
            SqlDataAdapter catDA = new SqlDataAdapter("SELECT [ID],[Company] FROM Customers", nwindConn);

            catDA.UpdateCommand = new SqlCommand("UPDATE Customers SET [Company] = ? " +
                                                   "WHERE ID = ?", nwindConn);

            catDA.UpdateCommand.Parameters.Add("@Company", SqlDbType.VarChar, 15, "Company");

            SqlParameter workParm = catDA.UpdateCommand.Parameters.Add("@ID", SqlDbType.Int);
            workParm.SourceColumn = "ID";
            workParm.SourceVersion = DataRowVersion.Original;

            DataSet catDS = new DataSet();
            catDA.Fill(catDS, "Customers");

            DataRow cRow = catDS.Tables["Customers"].Rows[0];
            cRow["Company"] = "CompanyNameAsif";
            catDA.Update(catDS, "Customers");
        }

        #region "Transaction Functions"
        private void CreateTransaction(string ConnectionString)
        {
            OpenConnection(ConnectionString);
            sqlTrans = objCon.BeginTransaction();

        }

        private void RollBackTransaction()
        {
            if (sqlTrans != null)
                sqlTrans.Rollback();
        }
        private void CommitTransaction()
        {
            if (sqlTrans != null)
                sqlTrans.Commit();
        }
        public void ExecuteTransaction(string ConnectionString, List<string> Queries)
        {
            try
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteTransaction ");
                CreateTransaction(ConnectionString);
                objCom = new SqlCommand();
                objCom.Transaction = sqlTrans;
                objCom.Connection = objCon;
                for (int i = 0; i < Queries.Count; i++)
                {
                    objCom.CommandText = Queries[i];
                    objCom.ExecuteNonQuery();
                }
                CommitTransaction();
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteTransaction Commit Successfull");
            }
            catch (Exception)
            {
                //clsUtility.ActivityLog(HttpContext.Current.Session["UserLoggedIn"].ToString(), "ExecuteTransaction RollBack Successfull");
                RollBackTransaction();
                throw;
            }
            finally
            {
                if (objCom != null)
                    objCom.Dispose();
                if (sqlTrans != null)
                    sqlTrans.Dispose();
                CloseConnection();
            }
        }
        #endregion
    }



}
