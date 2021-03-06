﻿using System;

namespace _360journeys
{
    class Reino
    {
        int id;
        string nombre;
        int capital;
        int gobernante;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int Capital
        {
            get { return capital; }
            set { capital = value; }
        }

        public int Gobernante
        {
            get { return gobernante; }
            set { gobernante = value; }
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }

    class Ciudad
    {
        int id;
        string nombre;
        int enReino;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int EnReino
        {
            get { return enReino; }
            set { enReino = value; }
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }

    class Gobernante
    {
        int id;
        string nombre;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public override string ToString()
        {
            return this.Nombre;
        }
    }
}
