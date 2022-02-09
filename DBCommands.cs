using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ProiectMediiVizuale
{
    public static class DbCommands
    {
        #region fields
        //connectionString="data source=CI123\SQLEXPRESS01; Initial Catalog=master; integrated security=SSPI"
        //Master location so we can create a new database
        private static string mainConString = ConfigurationManager.ConnectionStrings["MasterConnectionString"].ConnectionString;
        //Location of our database named alexdatabase
        //private static string dbConString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;

        //database we're using
        private static string currentDatabase;

        #endregion

        #region properties

        public static string ErrorFeed { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Creates the database we're using. If it exists, catch the exception.
        /// </summary>
        /// <param name="dbName">Database name</param>
        public static void CreateDatabase(string dbName)
        {
            currentDatabase = dbName;
            using (var con = new SqlConnection(mainConString))
            {
                string createDb = "CREATE DATABASE " + dbName;
                var cmd = new SqlCommand(createDb, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in CreateDatabase, details:\n" + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(dbName);
                    //Create the tables in the previously changed database
                    string createTeamTable =
                        @"CREATE TABLE dbo.Team
                            (   
                                TeamID int IDENTITY(1,1) NOT NULL,
                                TeamName nvarchar(20) NOT NULL,
                                CONSTRAINT pk_teamid PRIMARY KEY (TeamID)
                            );";
                    string createRoleTable =
                        @"CREATE TABLE dbo.Role
                            (
                                RoleID int IDENTITY(1,1) NOT NULL,
                                RoleDescription nvarchar(100) NOT NULL,
                                CONSTRAINT pk_roleid PRIMARY KEY (RoleID)
                            );";
                    string createMemberTable =
                        @"CREATE TABLE dbo.Member
                            (
                                MemberID int IDENTITY(1,1) NOT NULL,
                                MemberFirstName nvarchar(20) NOT NULL,
                                MemberLastName nvarchar(20) NOT NULL,
                                TeamID int NULL,
                                RoleID int NULL,
                                CONSTRAINT pk_memberid PRIMARY KEY (MemberID)
                            );";

                    cmd.CommandText = createTeamTable;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = createRoleTable;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = createMemberTable;
                    cmd.ExecuteNonQuery();
                    string relationMemberRole =
                        @"ALTER TABLE dbo.Member WITH CHECK ADD CONSTRAINT FK_Member_Role FOREIGN KEY(RoleID)
                          REFERENCES dbo.Role (RoleID);";
                    string relationMemberTeam =
                        @"ALTER TABLE dbo.Member WITH CHECK ADD CONSTRAINT FK_Member_Team FOREIGN KEY(TeamID)
                          REFERENCES dbo.Team (TeamID);";
                    cmd.CommandText = relationMemberRole;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = relationMemberTeam;
                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't create database in CreateDatabase, details:\n" + ex.ToString() + '\n';
                }
            }
        }

        /// <summary>
        /// For testing purpose. Delete the database we want
        /// </summary>
        /// <param name="dbName">Database name</param>
        public static void DeleteDatabase(string dbName)
        {
            using (var con = new SqlConnection(mainConString))
            {
                string deleteDb = "DROP DATABASE " + dbName;
                var cmd = new SqlCommand(deleteDb, con);
                con.Open();
                try
                {
                    
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't delete database, details: " + ex.ToString() + '\n';
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTableNames()
        {
            using (var con = new SqlConnection(mainConString))
            {
                var tableNameList = new List<string>();
                try
                {
                    con.Open();
                }
                catch(SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in GetTableNames, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    var dbSchema = con.GetSchema("Tables");
                    var dbSchemaRowCount = dbSchema.Rows.Count;
                    for (int row = 0; row < dbSchemaRowCount; row++)
                    {
                        tableNameList.Add(dbSchema.Rows[row]["TABLE_NAME"].ToString());
                    }
                    return tableNameList;
                }
                catch(SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in GetTableNames, details: " + ex.ToString() + '\n';
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<string> GetTableColumnNames(string tableName)
        {
            using(var con = new SqlConnection(mainConString))
            {
                var tableColumnNameList = new List<string>();
                try
                {
                    con.Open();
                }
                catch(SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in GetTableColumnNames, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    var schemaRestriction = new String[4];
                    schemaRestriction[2] = tableName;
                    var tableSchema = con.GetSchema("Columns", schemaRestriction);
                    var tableRowCount = tableSchema.Rows.Count;
                    for (int row = 0; row < tableRowCount; row++)
                    {
                        tableColumnNameList.Add(tableSchema.Rows[row]["COLUMN_NAME"].ToString());
                    }
                    return tableColumnNameList;
                }
                catch(SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in GetTableColumnNames, details: " + ex.ToString() + '\n';
                }
            }
            return null;
        }
        /// <summary>
        /// Insert data into a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="itemList"></param>
        public static void InsertData(string tableName, List<KeyValuePair<string, object>> itemList)
        {
            using (var con = new SqlConnection(mainConString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in InsertData, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    string getItems = "SELECT * FROM " + tableName;
                    var adapter = new SqlDataAdapter(getItems, con);
                    var set = new DataSet();
                    adapter.Fill(set, tableName);

                    DataRow row = set.Tables[tableName].NewRow();
                    foreach (var item in itemList)
                    {
                        row[item.Key] = item.Value;
                    }
                    set.Tables[tableName].Rows.Add(row);

                    var builder = new SqlCommandBuilder(adapter);
                    adapter.Update(set.Tables[tableName]);
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in InsertData, details: " + ex.ToString() + '\n';
                }
            }
        }
        /// <summary>
        /// Returns the table data as a DataSet
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <returns>Dataset with the items filtered</returns>
        public static DataSet GetTableData(string tableName)
        {
            using (var con = new SqlConnection(mainConString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in GetTableData, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    string getItems = "SELECT * FROM " + tableName;
                    var adapter = new SqlDataAdapter(getItems, con);
                    var set = new DataSet();
                    adapter.Fill(set, tableName);

                    return set;
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in GetTableData, details: " + ex.ToString() + '\n';
                }
            }
            return null;
        }
        /// <summary>
        /// Gets the table data filtered based on the values specified
        /// </summary>
        /// <param name="tableName">Name of table</param>
        /// <param name="primaryField">Name of field and ID</param>
        /// <returns>Dataset with the items filtered</returns>
        public static DataSet FilterTableData(string tableName, KeyValuePair<string, object> primaryField)
        {
            using (var con = new SqlConnection(mainConString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in GetTableData, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    string getItems = "SELECT * FROM " + tableName + " WHERE " + primaryField.Key + " = " + primaryField.Value;
                    var adapter = new SqlDataAdapter(getItems, con);
                    var set = new DataSet();
                    adapter.Fill(set, tableName);

                    return set;
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in GetTableData, details: " + ex.ToString() + '\n';
                }
            }
            return null;
        }
        /// <summary>
        /// Method to filter a table
        /// </summary>
        /// <param name="tableName">Table name</param>
        /// <param name="filter">List where [0] and [1] are table columns and [3] is a value</param>
        /// <returns>DataSet with the items filtered</returns>
        public static DataSet FilterTableData(string tableName, List<object> filter)
        {
            using (var con = new SqlConnection(mainConString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in GetTableData, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    string getItems = "SELECT * FROM " + tableName + " WHERE " + filter[0] + " IS " + filter[2]
                                                                   + " OR " + filter[1] + " IS " + filter[2];
                    var adapter = new SqlDataAdapter(getItems, con);
                    var set = new DataSet();
                    adapter.Fill(set, tableName);

                    return set;
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in GetTableData, details: " + ex.ToString() + '\n';
                }
            }
            return null;
        }
        /// <summary>
        /// Delete the items that contain the given IDs.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="listOfIDs">List of IDs</param>
        public static void DeleteTableData(string tableName, List<int> listOfIDs, string primaryField)
        {
            using (var con = new SqlConnection(mainConString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in DeleteTableData, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    var getItems = "SELECT * FROM " + tableName;
                    var adapter = new SqlDataAdapter(getItems, con);
                    var set = new DataSet();
                    adapter.Fill(set, tableName);

                    var rowCount = set.Tables[tableName].Rows.Count;

                    for (int row = rowCount - 1; row >= 0; row--)
                    {
                        if (listOfIDs.Contains((int)set.Tables[tableName].Rows[row][primaryField]))
                        {
                            if (tableName != "Member")
                            {
                                var memberTableName = "Member";
                                var memberSet = GetTableData(memberTableName);
                                var memberRowCount = memberSet.Tables[memberTableName].Rows.Count;
                                var memberColCount = memberSet.Tables[memberTableName].Columns.Count;
                                var itemList = new List<KeyValuePair<string, object>>();
                                var currentPrimaryFieldID = set.Tables[tableName].Rows[row][primaryField];
                                var memberPrimaryFieldName = memberSet.Tables[memberTableName].Columns[0].ColumnName;
                                for (int rowM = memberRowCount - 1; rowM >= 0; rowM--)
                                {
                                    var memberCurrentPrimaryFieldVal = memberSet.Tables[memberTableName].Rows[rowM][memberPrimaryFieldName];
                                    var memberPrimaryFieldIDVal = new KeyValuePair<string, object>(memberPrimaryFieldName, memberCurrentPrimaryFieldVal);
                                    object noVal = DBNull.Value;
                                    switch (tableName)
                                    {
                                        case "Team":
                                            if (memberSet.Tables[memberTableName].Rows[rowM][primaryField] == noVal) break;
                                            if ((int)currentPrimaryFieldID == (int)memberSet.Tables[memberTableName].Rows[rowM][primaryField])
                                            {
                                                for (int colM = memberColCount - 1; colM > 0; colM--)
                                                {
                                                    var memberFieldColName = memberSet.Tables[memberTableName].Columns[colM].ColumnName;
                                                    var memberFieldColValue = memberSet.Tables[memberTableName].Rows[rowM][colM];
                                                    if (memberFieldColName != primaryField)
                                                        itemList.Add(new KeyValuePair<string, object>(memberFieldColName, memberFieldColValue));
                                                    else
                                                        itemList.Add(new KeyValuePair<string, object>(memberFieldColName, noVal));
                                                }
                                                EditTableData(memberTableName, itemList, memberPrimaryFieldIDVal);
                                            }
                                            break;
                                        case "Role":
                                            if (memberSet.Tables[memberTableName].Rows[rowM][primaryField] == noVal) break;
                                            if ((int)currentPrimaryFieldID == (int)memberSet.Tables[memberTableName].Rows[rowM][primaryField])
                                            {
                                                for (int colM = memberColCount - 1; colM > 0; colM--)
                                                {
                                                    var memberFieldColName = memberSet.Tables[memberTableName].Columns[colM].ColumnName;
                                                    var memberFieldColValue = memberSet.Tables[memberTableName].Rows[rowM][colM];
                                                    if (memberFieldColName != primaryField)
                                                        itemList.Add(new KeyValuePair<string, object>(memberFieldColName, memberFieldColValue));
                                                    else
                                                        itemList.Add(new KeyValuePair<string, object>(memberFieldColName, noVal));
                                                }
                                                EditTableData(memberTableName, itemList, memberPrimaryFieldIDVal);
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            set.Tables[tableName].Rows[row].Delete();
                        }

                    }

                    var builder = new SqlCommandBuilder(adapter);
                    adapter.Update(set.Tables[tableName]);
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in DeleteTableData, details: " + ex.ToString() + '\n';
                }
            }
        }
        /// <summary>
        /// Edit field data in the given table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="itemList">List if items with FieldName and FieldValue</param>
        /// <param name="primaryField">PrimaryKey field for the table with FieldName and FieldID</param>
        public static void EditTableData(string tableName, List<KeyValuePair<string, object>> itemList, KeyValuePair<string, object> primaryField)
        {
            using (var con = new SqlConnection(mainConString))
            {
                try
                {
                    con.Open();
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't open database in EditTableData, details: " + ex.ToString() + '\n';
                }
                try
                {
                    con.ChangeDatabase(currentDatabase);
                    string getItems = "SELECT * FROM " + tableName;
                    var adapter = new SqlDataAdapter(getItems, con);
                    var set = new DataSet();
                    adapter.Fill(set, tableName);
                    int rowCount = set.Tables[tableName].Rows.Count;
                    int primaryFieldPos = set.Tables[tableName].Columns.IndexOf(primaryField.Key);
                    for (int row = rowCount - 1; row >= 0; row--)
                    {
                        if ((int)primaryField.Value == (int)set.Tables[tableName].Rows[row].ItemArray[primaryFieldPos])
                        {
                            foreach (var item in itemList)
                            {
                                set.Tables[tableName].Rows[row][item.Key] = item.Value;
                            }
                        }
                    }
                    var builder = new SqlCommandBuilder(adapter);
                    adapter.Update(set.Tables[tableName]);
                }
                catch (SqlException ex)
                {
                    ErrorFeed += "Couldn't change database in EditTableData, details: " + ex.ToString() + '\n';
                }
            }
        }
        #endregion
    }
}
