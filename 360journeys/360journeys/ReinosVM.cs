using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace _360journeys
{
    class ReinosVM : INotifyPropertyChanged
    {
        DAOSQLite _dao;
        Reino _reino;
        Gobernante _gob;
        string _mensaje = "Sin informacion";

        ObservableCollection<Reino> listaReinos;
        List<string> listaCiudades;

        #region

        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                if (_mensaje != value)
                {
                    _mensaje = value;
                    NotificarCambioDePropiedad("Mensaje");
                }
            }
        }

        public bool Conectado
        {
            get
            {
                if (_dao == null)
                    return false;
                else
                    return _dao.Conectado();
            }
            set
            {
                NotificarCambioDePropiedad("Conectado");
            }
        }

        public string ColorConexion
        {
            get
            {
                if (Conectado)
                    return "greenyellow";
                else
                    return "gray";
            }
            set
            {
                NotificarCambioDePropiedad("ColorConexion");
            }
        }

        public ObservableCollection<Reino> ListaReinos
        {
            get { return listaReinos; }
            set
            {
                listaReinos = value;
                NotificarCambioDePropiedad("ListaReinos");
            }
        }

        public List<string> ListaCiudades
        {
            get { return listaCiudades; }
            set
            {
                listaCiudades = value;
                NotificarCambioDePropiedad("ListaCiudades");
            }
        }

        public Reino ReinoSeleccionado
        {
            get { return _reino; }
            set
            {
                if (_reino != value)
                {
                    _reino = value;
                    if (_dao.Conectado() && _reino != null)
                    {
                        ListaCiudades = _dao.SeleccionarCiudades(_reino.ID);
                        GobernanteSeleccionado = _dao.SeleccionarGobernante(_reino.Gobernante);
                    }
                }
            }
        }

        public Gobernante GobernanteSeleccionado
        {
            get { return _gob; }
            set
            {
                _gob = value;
                NotificarCambioDePropiedad("GobernanteSeleccionado");
            }
        }

        #endregion

        private void Conectar()
        {
            try
            {
                _dao = null;
                _dao = new DAOSQLite();
                _dao.Conectar("talessya.db");
                Mensaje = "Conectado a BD";
            }
            catch (Exception e)
            {
                Mensaje = e.Message;
            }
            NotificarCambioDePropiedad("ColorConexion");
            NotificarCambioDePropiedad("Conectado");
        }

        public void Desconectar()
        {

            _dao.Desconectar();
            ListaReinos = null;
            ListaCiudades = null;
            GobernanteSeleccionado = null;

            Mensaje = "Desconectado de BD";

            NotificarCambioDePropiedad("ColorConexion");
            NotificarCambioDePropiedad("Conectado");
        }

        public void SelectReinos()
        {
            ListaReinos = _dao.SeleccionarReinos();
            Mensaje = "Datos cargados";
        }

        #region Comandos

        public ICommand ConectarBD_Click
        {
            get
            {               
                return new RelayCommand(o => Conectar(), o => true);
            }
        }

        public ICommand DesconectarBD_Click
        {
            get
            {
                return new RelayCommand(o => Desconectar(), o => true);
            }
        }

        public ICommand ListarReinos_Click
        {
            get
            {
                return new RelayCommand(o => SelectReinos(), o => true);
            }
        }

        #endregion

        #region Notificador de cambios
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotificarCambioDePropiedad(string p)
        {
            PropertyChangedEventHandler manejador = PropertyChanged;

            if (manejador != null)
            {
                manejador(this, new PropertyChangedEventArgs(p));
            }
        }

        #endregion

    }
}
