using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace Library.FileHelpers
{
    public class ExcelHelper : IDisposable
    {
        /// <summary>
        /// Gets or sets the connection.
        /// </summary>
        /// <value>The connection.</value>
        public OleDbConnection Connection { get; private set; }

        /// <summary>
        /// Gets or sets the sheets.
        /// </summary>
        /// <value>The sheets.</value>
        public IEnumerable<String> Sheets { get; private set; }

        /// <summary>
        /// Gets or sets the tables.
        /// </summary>
        /// <value>The tables.</value>
        public Dictionary<String, DataTable> Tables { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Excel"/> class.
        /// </summary>
        /// <param name="Filepath">The filepath.</param>
        public ExcelHelper(String Filepath)
        {
            if (File.Exists(Filepath) == false)
                throw new FileNotFoundException("File does not exist.", Filepath);

            Connection = GetConnection(Filepath);
            Sheets = GetSheetNames();
            Tables = new Dictionary<String, DataTable>();

            foreach (var Sheet in Sheets)
                Tables.Add(Sheet, GetTable(Sheet));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }

            foreach (var Table in Tables.Values)
                Table.Dispose();
        }

        /// <summary>
        /// Generates the connection string.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <returns></returns>
        private static String GenerateConnectionString(String Input)
        {
            var Result = String.Empty;

            if (File.Exists(Input))
            {
                switch (Path.GetExtension(Input).ToLower())
                {
                    case ".xls":
                        Result = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\";", Input);
                        break;

                    case ".xlsx":
                        Result = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\";", Input);
                        break;
                }
            }

            return Result;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <param name="Input">The input.</param>
        /// <returns></returns>
        private OleDbConnection GetConnection(String Input)
        {
            var ConnString = GenerateConnectionString(Input);

            if (ConnString == null)
                return null;

            var Result = new OleDbConnection(ConnString);
            Result.Open();

            return Result;
        }

        /// <summary>
        /// Gets the sheet names.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<String> GetSheetNames()
        {
            //get connection and result variables
            var Result = new List<String>();

            //get schema table
            var Table = Connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //add each sheet name
            if (Table != null)
                Result.AddRange(Table.Rows.Cast<DataRow>().Select(row => row["TABLE_NAME"].ToString()));

            return Result;
        }

        /// <summary>
        /// Gets the table.
        /// </summary>
        /// <param name="Sheet">The sheet.</param>
        /// <returns></returns>
        private DataTable GetTable(String Sheet)
        {
            //create a new table and open a connection
            var Result = new DataTable(Sheet);

            //create a connection and a command
            using (var Command = new OleDbCommand())
            {
                //set command properties
                Command.Connection = Connection;
                Command.CommandText = String.Format("SELECT * FROM [{0}];", Sheet);     //TODO: change this to parameter

                //load excel into data table
                Result.Load(Command.ExecuteReader());
            }

            return Result;
        }
    }
}