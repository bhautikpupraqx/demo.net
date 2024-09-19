using NewMexicoAPI.Common;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace NewMexicoAPI
{
    public class DB_WorkingClass
    {
        public DB_WorkingClass()
        {

        }

        public int DB_Return_RecCount(string SQL, params SqlParameter[] commandParameters)
        {
            int success = 0;
            string queryString2 = SQL;
            using (SqlConnection connection2 = DBConnection.GetDBConnection())
            {
                using (SqlCommand command2 = new SqlCommand(queryString2, connection2))
                {
                    if (commandParameters != null) command2.Parameters.AddRange(commandParameters);
                    connection2.Open();
                    try
                    {
                        //command2.ExecuteNonQuery();
                        Int32 count = (Int32)command2.ExecuteScalar();
                        if (count > 0)
                        {
                            success = count;
                        }
                    }
                    finally
                    {
                        connection2.Close();// Always call Close when done reading.
                    }
                }
            }
            return success;
        }

        public bool DB_ExecuteNonQuery_Record(string SQL, params SqlParameter[] commandParameters)
        {
            bool success = false;
            string queryString2 = SQL;
            using (SqlConnection connection2 = DBConnection.GetDBConnection())
            {
                using (SqlCommand command2 = new SqlCommand(queryString2, connection2))
                {
                    command2.CommandTimeout = 0;
                    if (commandParameters != null) command2.Parameters.AddRange(commandParameters);

                    connection2.Open();
                    try
                    {
                        command2.ExecuteNonQuery();
                        success = true;
                    }
                    finally
                    {
                        connection2.Close();// Always call Close when done reading.
                    }
                }
            }
            return success;
        }

        public bool DB_ExecuteNonQuery_Record_XML(string SQL, XElement xmlData)
        {
            bool success = false;
            string queryString2 = SQL;
            //string connectionString2 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection connection2 = DBConnection.GetDBConnection())
            {
                using (SqlCommand command2 = new SqlCommand(queryString2, connection2))
                {
                    command2.CommandTimeout = 0;
                    SqlParameter[] commandParameters = SQLParameters(xmlData);
                    if (commandParameters != null) command2.Parameters.AddRange(commandParameters);

                    connection2.Open();
                    try
                    {
                        command2.ExecuteNonQuery();
                        success = true;
                    }
                    finally
                    {
                        connection2.Close();// Always call Close when done reading.
                    }
                }
            }
            return success;
        }

        // SELECT
        public int Select(string sql, XElement xmlids)
        {
            // string newList =
            //   "WHERE UserKey IN (SELECT b.value('.', 'VARCHAR(80)') FROM @xmlids.nodes('//s') as v(b)) ";
            // string sql =
            //" SELECT UserKey, UserName, UserFirstName, UserLastName " +
            //" FROM tbl_SampleTest "
            //+ newList;
            SqlParameter[] sqlparam = SQLParameters(xmlids);
            //new SqlParameter[] { new SqlParameter("@salesReasonKey", salesReasonKey) };
            DataTable dt = getDataTableParams(sql, sqlparam);
            int rows = dt.Rows.Count;
            return rows;
        }
        SqlParameter[] SQLParameters(XElement xmlids)
        {
            List<SqlParameter> paramCollection = new List<SqlParameter>();

            var childNodes = xmlids.Elements().ToList();

            string nodeValues = "";
            foreach (var childNode in childNodes)
            {
                paramCollection.Add(new SqlParameter("@" + childNode.Name.LocalName, childNode.Value));
            }
            //XElement axmlids = xmlids ?? new XElement("ss");
            //SqlXml sqlXml = null;
            //using (XmlTextReader xtreader =
            //  new XmlTextReader(xmlids.ToString(), XmlNodeType.Document, null))
            //    sqlXml = new SqlXml(xtreader);
            return paramCollection.ToArray();
        }

        // UPDATE
        int UPDATECount = -1;
        public int Update(string asalesReasonKey, string asalesReasonValue)
        {
            string sql =
             "UPDATE DimSalesReason SET SalesReasonName=@salesReasonValue WHERE SalesReasonKey = @SalesReasonID";
            //+ " ORDER BY SalesReasonName"     
            SqlParameter[] sqlparam = new SqlParameter[]
            { new SqlParameter("@SalesReasonID", asalesReasonKey),
    new SqlParameter("@salesReasonValue", asalesReasonValue) };
            int count = ExecuteNonQuery(sql, sqlparam);
            UPDATECount = count;
            return UPDATECount;
            //update();
        }

        public int count(string salesReasonReasonType) // COUNT
        {
            int iSELECTCount = -1;
            string sql =
           "SELECT count(*) FROM DimSalesReason WHERE SalesReasonReasonType = @salesReasonReasonType";
            SqlParameter[] sqlparam =
              new SqlParameter[] { new SqlParameter("@salesReasonReasonType", salesReasonReasonType) };
            object ob = getScalar(sql, sqlparam);
            try { iSELECTCount = Convert.ToInt32(ob); }
            catch (Exception) { iSELECTCount = 0; }
            return iSELECTCount;
        }


        public DataTable getDataTableParams(string commandText, SqlParameter[] commandParameters)
        {
            SqlConnection connection = DBConnection.GetDBConnection();
            try { return getDataTableBase(connection, CommandType.Text, commandText, commandParameters); }
            catch (Exception ex) { /* log */ return null; }
        }

        public DataTable getDataTableBase(SqlConnection connection, CommandType commandType,
          string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                if (commandParameters != null) command.Parameters.AddRange(commandParameters);
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                connection.Close();
                command.Parameters.Clear();
                return ds.Tables[0];
            }
            catch (Exception ex) { connection.Close(); /* log */ return null; }
        }


        public int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            Int32 nEffected = 0;
            try
            {
                SqlConnection connection = DBConnection.GetDBConnection();
                nEffected = ExecuteNonQueryBase(connection, CommandType.Text, commandText, commandParameters);
                if (nEffected == 0)
                    throw new Exception();
                return nEffected;
            }
            catch (Exception ex) { /* log */ return -1; }
        }
        public int ExecuteNonQueryBase(SqlConnection connection,
         CommandType commandType, string commandText,
         params SqlParameter[] commandParameters)
        {
            Int32 nEffected = 0;
            try
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                if (commandParameters != null) command.Parameters.AddRange(commandParameters);
                connection.Open();
                nEffected = command.ExecuteNonQuery();
                connection.Close();
                return nEffected;
            }
            catch (Exception ex) { connection.Close(); /* log */ return -1; }
        }


        public object getScalar(string commandText, params SqlParameter[] commandParameters)
        {
            SqlConnection connection = DBConnection.GetDBConnection();
            try
            {
                return getScalarBase(connection, CommandType.Text, commandText, commandParameters);
            }
            catch (Exception ex) { /* log */ return null; }
        }
        public object getScalarBase(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            try
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                if (commandParameters != null) command.Parameters.AddRange(commandParameters);
                connection.Open();
                object retval = command.ExecuteScalar();
                connection.Close();
                return retval;
            }
            catch (Exception ex) { connection.Close(); /* log */ return null; }
        }
    }
}
