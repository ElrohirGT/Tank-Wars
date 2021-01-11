using System;
using System.Collections.Generic;
using Tank_Wars_Desktop.Tanques;
using System.Drawing;

namespace Tank_Wars_Desktop.Celdas
{
    public class Celda
    {
        public CeldaEstados Estado { get; private set; }
        public Tanque Tanque { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public Celda(int y, int x, CeldaEstados estado = CeldaEstados.Libre)
	    {
            this.Estado = estado;
            this.X = x;
            this.Y = y;
	    }
        public void CambiarEstado(CeldaEstados estado)
        {
            this.Estado = estado;
        }
        public void AñadirTanque(Tanque tanque, int turno)
        {
            Tanque = tanque;
            if (tanque.Dueño == turno) { CambiarEstado(CeldaEstados.TanqueTurno); }
            else { CambiarEstado(CeldaEstados.Ocupada); }
        }
        public void QuitarTanque()
        {
            Tanque = null;
            CambiarEstado(CeldaEstados.Libre);
        }
        public bool Esta(CeldaEstados estado)
        {
            if(Estado == estado) { return true; }
            return false;
        }
    }
    [Flags]
    public enum CeldaEstados
    {
        None = 0,
        Libre = 1, //Celda a la que un tanque se puede mover.
        Deshabilitado = 2, //Celda con la que no se puede interactuar.
        Ocupada = 3, //Celda ocupada por algun otro tanque.
        TanqueTurno = 4, //Celda en donde esta un tanque del jugador de turno.
        Avanzar = 5, //Celda en la que el tanque seleccionado se puede mover.
        Disparable = 6, //Celda a la que se le puede disparar.
        Seleccionada = 7 //Celda con el tanque del jugador de turno al que se le hizo click.
    }
}

