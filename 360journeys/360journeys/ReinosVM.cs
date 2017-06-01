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
        string _mensaje = "Sin informacion";

        ObservableCollection<Reino> listaReinos;

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

            Mensaje = "Desconectado de BD";

            NotificarCambioDePropiedad("ColorConexion");
            NotificarCambioDePropiedad("Conectado");
            NotificarCambioDePropiedad("ListaReinos");
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
                //"o" es el objeto cualquiera. Se tratan de expresiones lambda. Estamos diciendo que el objeto que use este comando llamará a ConectarBD, y el true
                //es que el CanExecute es true, por lo que puede ejecutarlo.
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
