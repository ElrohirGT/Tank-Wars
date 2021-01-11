using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tank_Wars_Desktop
{
    static class ColoresJugadores
    {
        public static Color Jugador1 = Color.SkyBlue;
        public static Color Jugador2 = Color.Red;
        public static Color Jugador3 = Color.LightGreen;
        public static Color Jugador4 = Color.MediumPurple;
    }

    public class Jugadores
    {
        public List<string> Nombres { get; private set; }
        public int Turno { get; private set; }
	    public Jugadores(List<string> nombres)
	    {
            this.Nombres = nombres;
            this.Turno = 0;
	    }
        public void CambiarTurno()
        {
            int Posibleturno = this.Turno += (this.Turno == Nombres.Count-1) ? -(Nombres.Count-1) : 1;//Reiniciar el contador
            if(Nombres[Posibleturno] == null) { CambiarTurno(); }
        }
        public void EliminarJugador(int index)
        {
            Nombres[index] = null;
        }
    }
}
