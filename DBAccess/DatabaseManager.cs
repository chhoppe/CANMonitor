
using CANMonitor.DB.DataStructure;
using System;
namespace CANMonitor.DB
{
    /// <summary>
    /// DatabaseManager
    /// </summary>
    public class DatabaseManager
    {
        private static SQLite.SQLiteConnection connection;



        /// <summary>
        /// connect with the database
        /// </summary>
        /// <returns></returns>
        public void Connect ( )
        {
            if (connection == null)
                connection = new SQLite.SQLiteConnection("CANMonitor.sqlite");
            else
                throw new Exception("Databse already connected");

            connection.CreateTable<TableSettings>( );
            connection.CreateTable<TableData>( );
        }

        public static SQLite.SQLiteConnection Connection
        {
            get
            {
                return connection;
            }
        }


        public static bool IsConnected
        {
            get
            {
                return connection != null;
            }
        }

    }
}
