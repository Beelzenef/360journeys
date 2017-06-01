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

        public ObservableCollection<Reino> SeleccionarReinos()
        {
            ObservableCollection<Reino> listaReinos = new ObservableCollection<Reino>();

            string orden;
            orden = "select id, nombre, capital, gobernante from reino";

            SQLiteCommand comandoAEjecutar = new SQLiteCommand(orden, connSQLite);

            try
            {
                SQLiteDataReader lectorDatos = comandoAEjecutar.ExecuteReader();

                while (lectorDatos.Read())
                {
                    Reino reinoTMP = new Reino();
                    
                    reinoTMP.ID = ushort.Parse(lectorDatos["id"].ToString());
                    reinoTMP.Nombre = lectorDatos["nombre"].ToString();
                    reinoTMP.Capital = ushort.Parse(lectorDatos["capital"].ToString());
                    reinoTMP.Gobernante = ushort.Parse(lectorDatos["gobernante"].ToString());

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

        public List<string> SeleccionarCiudades(int codigoReino)
        {
            List<string> listaCiudades = new List<string>();

            string orden;
            orden = "select nombre from ciudad where enReino = '" + codigoReino + "'";

            SQLiteCommand comandoAEjecutar = new SQLiteCommand(orden, connSQLite);

            try
            {
                SQLiteDataReader lectorDatos = comandoAEjecutar.ExecuteReader();

                while (lectorDatos.Read())
                {
                    string ciudadTMP = lectorDatos["nombre"].ToString();

                    listaCiudades.Add(ciudadTMP);
                }

                lectorDatos.Close();
                return listaCiudades;
            }
            catch (SQLiteException)
            {
                throw new Exception("No tiene permisos para ejecutar esta orden");
            }

        }

    }
}
