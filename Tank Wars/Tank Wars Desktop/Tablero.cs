using System;
using System.Collections.Generic;
using System.ComponentModel;
using Tank_Wars_Desktop.Celdas;
using Tank_Wars_Desktop.Tanques;
using System.Windows.Forms;
using System.Drawing;

namespace Tank_Wars_Desktop
{
    public class Tablero
    {
        public Celda[,] Celdas { get; private set; }
        public TableLayoutPanel Contenedor { get; private set; }
        public Tanque TanqueSeleccionado
        {
            get
            {
                for (int i = 0; i < Celdas.GetLength(1); i++)
                {
                    for(int j = 0; j<Celdas.GetLength(1); j++)
                    {
                        Celda celda = Celdas[i, j];
                        if (celda.Esta(CeldaEstados.Seleccionada)) { return celda.Tanque; }
                    }
                }
                return null;
            }
            set { }
        }
        public Tablero(int cantidadJugadores, TableLayoutPanel contenedor, int turno)
        {
            Contenedor = contenedor;
            int largoTablero = (cantidadJugadores == 2) ? 11 : 15;//El largo debe ser 1 mas para que el for funcione
            this.Celdas = new Celda[largoTablero, largoTablero];

            for (int i = 0; i < largoTablero; i++)
            {
                for (int j = 0; j < largoTablero; j++)
                {
                    this.Celdas[i, j] = new Celda(i, j);
                }
            }

            if (cantidadJugadores == 4)
            {
                for (int i = 0; i < 2; i++)//Esquina superior izquierda y esquina superior derecha
                {
                    for (int j = 0; j < 2; j++)
                    {
                        this.Celdas[i, j].CambiarEstado(CeldaEstados.Deshabilitado);
                    }
                    for (int j = largoTablero - 2; j < largoTablero; j++)
                    {
                        this.Celdas[i, j].CambiarEstado(CeldaEstados.Deshabilitado);
                    }
                }
                for (int i = largoTablero - 2; i < largoTablero; i++)//Esquina inferior izquierda y esquina inferior derecha
                {
                    for (int j = 0; j < 2; j++) { this.Celdas[i, j].CambiarEstado(CeldaEstados.Deshabilitado); }
                    for (int j = largoTablero - 2; j < largoTablero; j++) { this.Celdas[i, j].CambiarEstado(CeldaEstados.Deshabilitado); }
                }
                PosicionarTanques(7, largoTablero - 2, 0, TanqueDirecciones.Norte, turno);
                PosicionarTanques(7, 1, 1, TanqueDirecciones.Sur, turno);

                PosicionarTanques(1, 7, 2, TanqueDirecciones.Este, turno);
                PosicionarTanques(largoTablero - 2, 7, 3, TanqueDirecciones.Oeste, turno);

            } else
            {
                PosicionarTanques(5, largoTablero - 2, 0, TanqueDirecciones.Norte, turno);
                PosicionarTanques(5, 1, 1, TanqueDirecciones.Sur, turno);
            }

        }
        private void PosicionarTanques(int x, int y, int dueño, TanqueDirecciones apunta, int turno)
        {
            Celdas[x, y].AñadirTanque(new TanqueMadre(apunta, dueño), turno);
            if (apunta == TanqueDirecciones.Norte)
            {
                Celdas[x - 3, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 3, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
            } else if (apunta == TanqueDirecciones.Sur)
            {
                Celdas[x - 3, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 3, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
            } else if (apunta == TanqueDirecciones.Oeste)
            {
                Celdas[x + 1, y - 3].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y + 3].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
            } else
            {
                Celdas[x - 1, y - 3].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x - 1, y + 3].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y - 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
                Celdas[x + 1, y + 1].AñadirTanque(new TanquePequeño(apunta, dueño), turno);
            }

        }
        public void MostrarMovimientos(int[] coordenadas)
        {
            var coordenadasMovimientos = this.CoordenadasMovimientos(coordenadas);
            var coordenadasDisparos = this.CoordenadasDisparos(coordenadas);

            foreach(var coordenadasEnfrente in coordenadasMovimientos)
            {
                var celdaEnfrente = this.Celdas[coordenadasEnfrente[0], coordenadasEnfrente[1]];
                Control controladorMovimiento = Contenedor.Controls.Find($"{coordenadasEnfrente[0]}/{coordenadasEnfrente[1]}", true)[0];
                
                celdaEnfrente.CambiarEstado(CeldaEstados.Avanzar);
                controladorMovimiento.BackColor = Color.Aqua;
            }
            foreach(var coordenadasDisparo in coordenadasDisparos)
            {
                Celda celda = this.Celdas[coordenadasDisparo[0], coordenadasDisparo[1]];
                Control controladorDisparo = Contenedor.Controls.Find($"{coordenadasDisparo[0]}/{coordenadasDisparo[1]}", true)[0];

                celda.CambiarEstado(CeldaEstados.Disparable);
                controladorDisparo.ForeColor = Color.Black;
            }
        }
        public void CambiarTurno(int turno)
        {
            for (int i = 0; i < Celdas.GetLength(1); i++)
            {
                for (int j = 0; j < Celdas.GetLength(1); j++)
                {
                    Celda celda = Celdas[i, j];
                    if (celda.Esta(CeldaEstados.TanqueTurno)) { celda.CambiarEstado(CeldaEstados.Ocupada); }
                    else if (celda.Esta(CeldaEstados.Ocupada))
                    {
                        if (celda.Tanque.Pertenece(turno)) { celda.CambiarEstado(CeldaEstados.TanqueTurno); }
                    }
                }
            }
        }
        public List<int[]> CoordenadasDisparos(int[] coordenadas)
        {
            var celda = this.Celdas[coordenadas[0], coordenadas[1]];
            List<int[]> coordenadasDisparos = new List<int[]>();
            if (celda.Tanque == null) { throw new NullReferenceException($"ERROR: al intentar obtener el tanque de la celda {coordenadas[0]}/{coordenadas[1]}"); }
            try
            {
                for (int i = 1; i <= celda.Tanque.Rango; i++)
                {
                    int[] nuevasCoordenadas;
                    if (celda.Tanque.Apunta == TanqueDirecciones.Norte) { nuevasCoordenadas = new int[] { coordenadas[0], coordenadas[1] - i }; }
                    else if (celda.Tanque.Apunta == TanqueDirecciones.Sur) { nuevasCoordenadas = new int[] { coordenadas[0], coordenadas[1] + i }; }
                    else if (celda.Tanque.Apunta == TanqueDirecciones.Oeste) { nuevasCoordenadas = new int[] { coordenadas[0] - i, coordenadas[1] }; }
                    else { nuevasCoordenadas = new int[] { coordenadas[0] + i, coordenadas[1] }; }
                    Celda posibleCelda = Celdas[nuevasCoordenadas[0], nuevasCoordenadas[1]];
                    if (posibleCelda.Esta(CeldaEstados.Ocupada) || posibleCelda.Esta(CeldaEstados.Disparable)) { coordenadasDisparos.Add(nuevasCoordenadas); }
                }
            }
            catch (IndexOutOfRangeException) { /*Asi se sale del for*/ }
            return coordenadasDisparos;
        }
        public List<int[]> CoordenadasMovimientos(int[] coordenadas)
        {
            var celda = this.Celdas[coordenadas[0], coordenadas[1]];
            List<int[]> coordenadasMovimientos = new List<int[]>();
            if (celda.Tanque == null) { throw new NullReferenceException($"ERROR: al intentar obtener el tanque de la celda {coordenadas[0]}/{coordenadas[1]}"); }
            try
            {
                for (int i = 1; i <= celda.Tanque.Gas; i++)
                {
                    int[] nuevasCoordenadas;
                    if (celda.Tanque.Apunta == TanqueDirecciones.Norte) { nuevasCoordenadas = new int[] { coordenadas[0], coordenadas[1] - i }; }
                    else if (celda.Tanque.Apunta == TanqueDirecciones.Sur) { nuevasCoordenadas = new int[] { coordenadas[0], coordenadas[1] + i }; }
                    else if (celda.Tanque.Apunta == TanqueDirecciones.Oeste) { nuevasCoordenadas = new int[] { coordenadas[0] - i, coordenadas[1] }; }
                    else { nuevasCoordenadas = new int[] { coordenadas[0] + i, coordenadas[1] }; }
                    if (Celdas[nuevasCoordenadas[0], nuevasCoordenadas[1]].Tanque != null
                        || Celdas[nuevasCoordenadas[0], nuevasCoordenadas[1]].Esta(CeldaEstados.Deshabilitado))
                    { throw new IndexOutOfRangeException(); }

                    coordenadasMovimientos.Add(nuevasCoordenadas);
                }
            }
            catch (IndexOutOfRangeException) { /*Asi se sale del for*/ }
            return coordenadasMovimientos;
        }
        public int[] CoordenadasTanque(int[] coordenadas)
        {
            for (int i=0; i < Celdas.GetLength(1); i++)
            {
                for (int j=0; j < Celdas.GetLength(1); j++)
                {
                    Celda celda = Celdas[i, j];
                    if (celda.Esta(CeldaEstados.Seleccionada)) { return new int[] { i, j }; }
                }
            }
            throw new Exception($"ERROR: Al obtener coordenadas del tanque de la casilla de movimiento con coordenadas {coordenadas[0]}/{coordenadas[1]}");
        }
        public int CantidadTanquesDeJugador(int dueño)
        {
            int cantidad = 0;
            for (int i = 0; i < Celdas.GetLength(1); i++)
            {
                for (int j = 0; j < Celdas.GetLength(1); j++)
                {
                    if (Celdas[i, j].Tanque != null)
                    {
                        if(Celdas[i,j].Tanque.Pertenece(dueño)) { cantidad++; }
                    }
                }
            }
            return cantidad;
            //throw new NotImplementedException();
        }
        public bool GanoJugador(int jugador)
        {
            for (int i = 0; i < Celdas.GetLength(1); i++)
            {
                for (int j = 0; j < Celdas.GetLength(1); j++)
                {
                    Celda celda = Celdas[i, j];
                    if (celda.Tanque != null)
                    {
                        if(!celda.Tanque.Pertenece(jugador)) { return false; }
                    }
                }
            }
            return true;
        }
    }
}
