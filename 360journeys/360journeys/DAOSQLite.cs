using System;
using System.Data.SQLite;

namespace _360journeys
{
    class DAOSQLite
    {
        public static SQLiteConnection connSQLite;

        public bool Conectado()
        {
            if (connSQLite != null)
                return connSQLite.State == System.Data.ConnectionState.Open;
            else
                return false;
        }

        public bool Conectar(string db)
        {
            string cadenaConexion = "Data Source=" + db + ";Version=3;FailIfMissing=true;";

            try
            {
                connSQLite = new SQLiteConnection(cadenaConexion);
                connSQLite.Open();
                return true;
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Error de conexión: " + ex.Data);
            }
        }

        public void Desconectar()
        {
            try
            {
                connSQLite.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
