using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public List<string> SeleccionarReinos()
        {
            List<string> listaReinos = new List<string>();

            string orden;

            orden = "select nombre from reino";

            SQLiteCommand comandoAEjecutar = new SQLiteCommand(orden, connSQLite);

            try
            {
                SQLiteDataReader lectorDatos = comandoAEjecutar.ExecuteReader();

                while (lectorDatos.Read())
                {
                    string reinoTMP = lectorDatos["nombre"].ToString();

                    listaReinos.Add(reinoTMP);
                }

                lectorDatos.Close();
                return listaReinos;
            }
            catch (SQLiteException)
            {
                throw new Exception("No tiene permisos para ejecutar esta orden");
            }
        }

    }
}
