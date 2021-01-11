using System;
using Tank_Wars_Desktop.Celdas;
using System.Drawing;

namespace Tank_Wars_Desktop.Tanques
{
    [Flags]
    public enum TanqueDirecciones
    {
        Norte = 180,
        Este = -90,
        Sur = 0,
        Oeste = 90
    }
    [Flags]
    public enum TanqueSimbolos
    {
        None = 0,
        TanqueMadre = 'Ͳ',
        TanquePequeño = '╤'
    }
    public class Tanque
    {
        public decimal Vida { get; set; }
        public decimal Daño { get; protected set; }
        public int Gas { get; protected set; }
        public int Rango { get; protected set; }
        public int Racha { get; protected set; }
        public TanqueDirecciones Apunta { get; set; }
        public int Dueño { get; set; }
        public char Simbolo { get; protected set; }

        public void Atacar(Celda celdaTurno, Celda celdaEnemiga, int turno, Color ColorTurno)
        {
            Tanque tanqueEnemigo = celdaEnemiga.Tanque;
            tanqueEnemigo.Vida -= Daño;
            celdaEnemiga.CambiarEstado(CeldaEstados.Ocupada);
            if (tanqueEnemigo.Vida <= 0)
            {
                if (celdaEnemiga.Tanque.GetType().Name == "TanqueMadre")
                {
                    celdaEnemiga.Tanque.Desevolucionar(turno, celdaEnemiga);
                    return;
                }
                celdaEnemiga.QuitarTanque(); Racha++;
            }
            if (celdaTurno.Tanque.Racha == 2)
            {
                celdaTurno.Tanque.Evolucionar(turno, celdaTurno);
            }
            //throw new NotImplementedException();
        }
        public void Girar(bool RightClick)
        {
            if (!RightClick)
            {
                switch (this.Apunta)
                {
                    case TanqueDirecciones.Norte:
                        Apunta = TanqueDirecciones.Oeste;
                        break;
                    case TanqueDirecciones.Oeste:
                        Apunta = TanqueDirecciones.Sur;
                        break;
                    case TanqueDirecciones.Sur:
                        Apunta = TanqueDirecciones.Este;
                        break;
                    case TanqueDirecciones.Este:
                        Apunta = TanqueDirecciones.Norte;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"ERROR: Al girar tanque del jugador con index: {Dueño}.");
                }
            }
            else
            {
                switch (this.Apunta)
                {
                    case TanqueDirecciones.Norte:
                        Apunta = TanqueDirecciones.Este;
                        break;
                    case TanqueDirecciones.Oeste:
                        Apunta = TanqueDirecciones.Norte;
                        break;
                    case TanqueDirecciones.Sur:
                        Apunta = TanqueDirecciones.Oeste;
                        break;
                    case TanqueDirecciones.Este:
                        Apunta = TanqueDirecciones.Sur;
                        break;
                    default:
                        throw new IndexOutOfRangeException($"ERROR: Al girar tanque del jugador con index: {Dueño}.");
                }
            }
            //throw new NotImplementedException();
        }

        public void Mover(Celda[,] tablero, int[] coordenadasActuales, int[] coordenadasDestino, int turno)
        {
            if (tablero == null) { throw new ArgumentNullException(nameof(tablero), $"ERROR: Al intentar mover el tanque de: {coordenadasActuales[0]}/{coordenadasActuales[1]} a: {coordenadasDestino[0]}/{coordenadasDestino[1]}"); }
            tablero[coordenadasActuales[0], coordenadasActuales[1]].QuitarTanque();
            tablero[coordenadasDestino[0], coordenadasDestino[1]].AñadirTanque(this, turno);

            //throw new NotImplementedException();
        }
        public void Evolucionar(int turno, Celda celda)
        {
            if (this.GetType().Name == "TanquePequeño")
            {
                celda.AñadirTanque(new TanqueMadre(Apunta, Dueño), turno);
            }
        }
        public void Desevolucionar(int turno, Celda celda)
        {
            if(this.GetType().Name == "TanqueMadre")
            {
                celda.AñadirTanque(new TanquePequeño(Apunta, Dueño), turno);
            }
        }
        public bool Pertenece(int dueño)
        {
            if(Dueño == dueño) { return true; }
            return false;
        }
    }
    public class TanquePequeño : Tanque
    {
	    public TanquePequeño(TanqueDirecciones apunta, int dueño)
	    {
            Vida = 2;
            Daño = 1;
            Gas = 2;
            Rango = 1;
            Apunta = apunta;
            Dueño = dueño;
            Simbolo = (char)TanqueSimbolos.TanquePequeño;
	    }
    }

    public class TanqueMadre : Tanque
    {
        public TanqueMadre(TanqueDirecciones apunta, int dueño)
        {
            Vida = 4;
            Daño = 2;
            Gas = 3;
            Rango = 2;
            Apunta = apunta;
            Dueño = dueño;
            Simbolo = (char)TanqueSimbolos.TanqueMadre;
        }
    }
}
