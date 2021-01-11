using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Tank_Wars_Desktop.Celdas;
using Tank_Wars_Desktop.Tanques;

namespace Tank_Wars_Desktop
{
    //Arreglar la accion "MOVER" para que todas las celdas despues del movimiento pasen a ser normales otra vez
    class Juego
    {
        public Label Titulo { get; private set; }
        public Label Subtitulo { get; private set; }
        public Label VidaTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("VidaTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label DañoTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("DañoTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label RachaTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("RachaTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label TipoTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("TipoTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label DueñoTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("DueñoTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label ApuntaTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("ApuntaTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label RangoTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("RangoTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Label GasolinaTanqueSeleccionado
        {
            get
            {
                return ContenedorInfoTanque.Controls.Find("GasolinaTanqueSeleccionado", true)[0] as Label;
            }
        }
        public Button SaltarTurno { get; private set; }
        public TableLayoutPanel Contenedor { get; private set; }
        public TableLayoutPanel ContenedorInfoTanque { get; private set; }
        public MainWindow Window { get; private set; }
        public Jugadores Jugadores { get; private set; }
        public Tablero Tablero { get; private set; }
        public int JugadasRestantes { get; private set; } = 3;
        public Tanque InfoTanque { get; set; }
        public string NombreTurno
        {
            get
            {
                return this.Jugadores.Nombres[this.Jugadores.Turno];
            }
        }
        public Color ColorTurno
        {
            get
            {
                Color color = Color.Transparent;
                switch (Jugadores.Turno)
                {
                    case 0: color = ColoresJugadores.Jugador1; break;
                    case 1: color = ColoresJugadores.Jugador2; break;
                    case 2: color = ColoresJugadores.Jugador3; break;
                    case 3: color = ColoresJugadores.Jugador4; break;
                }
                return color;
            }
        }
        public Juego(int CantidadJugadores, List<string> nombres, MainWindow Window, TableLayoutPanel Contenedor, Label Titulo, Label Subtitulo, TableLayoutPanel ContenedorInfoTanque, Button SaltarTurno)
        {
            Jugadores = new Jugadores(nombres);
            Tablero = new Tablero(CantidadJugadores, Contenedor, Jugadores.Turno);
            
            Contenedor.ColumnCount = this.Tablero.Celdas.GetLength(1);
            Contenedor.RowCount = this.Tablero.Celdas.GetLength(1);

            this.Window = Window;
            this.Contenedor = Contenedor;
            this.Titulo = Titulo;
            this.Subtitulo = Subtitulo;
            this.ContenedorInfoTanque = ContenedorInfoTanque;
            this.SaltarTurno = SaltarTurno;
            this.SaltarTurno.Click += SaltarTurno_Click;

            ActualizarTitulos();
            Mostrar();
        }

        private void SaltarTurno_Click(object sender, EventArgs e)
        {
            this.CambiarTurno();
            //throw new NotImplementedException();
        }

        public void Jugada(object sender, EventArgs e)
        {
            var celda = (RotateLabel)sender;
            var me = (MouseEventArgs)e;
            string[] split = celda.Name.Split('/');
            int[] coordenadasClick = new int[] { Convert.ToInt32(split[0]), Convert.ToInt32(split[1]) };
            Celda celdaClick = this.Tablero.Celdas[coordenadasClick[0], coordenadasClick[1]];

            if (celdaClick.Esta(CeldaEstados.TanqueTurno))
            {
                Deseleccionar();
                celdaClick.CambiarEstado(CeldaEstados.Seleccionada);
                this.Tablero.MostrarMovimientos(coordenadasClick);
                this.InfoTanque = celdaClick.Tanque;
            }
            else if (celdaClick.Esta(CeldaEstados.Seleccionada))
            {
                celdaClick.Tanque.Girar(me.Button == MouseButtons.Left);
                celda.Invalidate();
                --JugadasRestantes;
                Deseleccionar();
            }
            else if (celdaClick.Esta(CeldaEstados.Avanzar))
            {
                this.MoverTanque(coordenadasClick);
                --JugadasRestantes;
                Deseleccionar();
            }
            else if (celdaClick.Esta(CeldaEstados.Disparable))
            {
                int enemigo = celdaClick.Tanque.Dueño;
                
                this.DispararTanque(coordenadasClick);
                --JugadasRestantes;
                Deseleccionar();
                if (Tablero.CantidadTanquesDeJugador(enemigo)==0)
                {
                    if (Tablero.GanoJugador(Jugadores.Turno)) { this.Gano(); return; }
                    else
                    {
                        PopUp form = new PopUp($"{Jugadores.Nombres[enemigo]} ELIMINADO.", Color.Red, "Aceptar");
                        form.ShowDialog(Window);
                        form.Dispose();
                        Jugadores.EliminarJugador(enemigo);
                    }
                }
            }
            else if (celdaClick.Esta(CeldaEstados.Ocupada))
            {
                this.InfoTanque = celdaClick.Tanque;
            }
            if(JugadasRestantes < 3) { SaltarTurno.Enabled = true; } else { SaltarTurno.Enabled = false; }
            ActualizarTitulos();
            if (JugadasRestantes == 0) { CambiarTurno(); }
        }
        public void CambiarTurno()
        {
            Deseleccionar();
            JugadasRestantes = 3;
            SaltarTurno.Enabled = false;
            Jugadores.CambiarTurno();
            Tablero.CambiarTurno(Jugadores.Turno);
            ActualizarTitulos();
        }
        public void MoverTanque(int[] coordenadasClick)
        {
            int[] coordenadasTanque = Tablero.CoordenadasTanque(coordenadasClick);
            List<int[]> coordenadasMovimientos = Tablero.CoordenadasMovimientos(coordenadasTanque);
            List<int[]> coordenadasDisparos = Tablero.CoordenadasDisparos(coordenadasTanque);
            Celda celdaTanque = Tablero.Celdas[coordenadasTanque[0], coordenadasTanque[1]];

            celdaTanque.Tanque.Mover(Tablero.Celdas, coordenadasTanque, coordenadasClick, Jugadores.Turno);
            
            Control LabelTanque = Contenedor.Controls.Find($"{coordenadasTanque[0]}/{coordenadasTanque[1]}", true)[0];
            Control LabelNuevaUbicacion = Contenedor.Controls.Find($"{coordenadasClick[0]}/{coordenadasClick[1]}", true)[0];
            
            LabelTanque.Text = "";
            LabelNuevaUbicacion.Text = $"{Tablero.Celdas[coordenadasClick[0], coordenadasClick[1]].Tanque.Simbolo}";
            
            EstilosDefaultCelda(LabelTanque as RotateLabel, Tablero);
            foreach(var coordenadasMovimiento in coordenadasMovimientos)
            {
                if (!coordenadasClick.SequenceEqual(coordenadasMovimiento))
                {
                    Tablero.Celdas[coordenadasMovimiento[0], coordenadasMovimiento[1]].CambiarEstado(CeldaEstados.Libre);
                }
                EstilosDefaultCelda(Tablero: Tablero, LabelCelda: Contenedor.Controls.Find($"{coordenadasMovimiento[0]}/{coordenadasMovimiento[1]}", true)[0] as RotateLabel);
            }
            EstilosDefaultCelda(coordenadasDisparos, Tablero, Contenedor);
        }
        public void DispararTanque(int[] coordenadasClick)
        {
            int[] coordenadasTanque = Tablero.CoordenadasTanque(coordenadasClick);
            List<int[]> coordenadasDisparos = Tablero.CoordenadasDisparos(coordenadasTanque);
            List<int[]> coordenadasMovimientos = Tablero.CoordenadasMovimientos(coordenadasTanque);
            Celda celdaTanqueTurno = Tablero.Celdas[coordenadasTanque[0], coordenadasTanque[1]];
            Celda celdaTanqueEnemigo = Tablero.Celdas[coordenadasClick[0], coordenadasClick[1]];

            celdaTanqueTurno.Tanque.Atacar(celdaTanqueTurno, celdaTanqueEnemigo, Jugadores.Turno, ColorTurno);

            EstilosDefaultCelda((RotateLabel)Contenedor.Controls.Find($"{coordenadasTanque[0]}/{coordenadasTanque[1]}", true)[0], Tablero);
            EstilosDefaultCelda(coordenadasDisparos, Tablero, Contenedor);
            EstilosDefaultCelda(coordenadasMovimientos, Tablero, Contenedor);
        }
        public static void EstilosDefaultCelda(RotateLabel LabelCelda, Tablero Tablero)
        {
            int[] coordenadasCelda = Array.ConvertAll(LabelCelda.Name.Split('/'), Convert.ToInt32);            
            Celda celdaTablero = Tablero.Celdas[coordenadasCelda[0], coordenadasCelda[1]];
            Color textColor = Color.Transparent;

            if (celdaTablero.Tanque != null)
            {
                switch (celdaTablero.Tanque.Dueño)
                {
                    case 0: textColor = ColoresJugadores.Jugador1; break;
                    case 1: textColor = ColoresJugadores.Jugador2; break;
                    case 2: textColor = ColoresJugadores.Jugador3; break;
                    case 3: textColor = ColoresJugadores.Jugador4; break;
                }
                LabelCelda.RotateAngle = (float)celdaTablero.Tanque.Apunta;
                LabelCelda.Cursor = Cursors.Hand;
            }
            LabelCelda.Text = (celdaTablero.Tanque != null) ? $"{celdaTablero.Tanque.Simbolo}" : "";
            LabelCelda.Font = new Font(FontFamily.GenericSansSerif, 20f, FontStyle.Bold);
            LabelCelda.ForeColor = textColor;
            LabelCelda.TextAlign = ContentAlignment.MiddleCenter;
            LabelCelda.Visible = true;
            LabelCelda.Margin = new Padding(0);
            LabelCelda.AutoSize = false;
            LabelCelda.Dock = DockStyle.Fill;
            LabelCelda.BackColor = (coordenadasCelda[1] % 2 == 0 && coordenadasCelda[0] % 2 != 0) ? Color.LightGray : (coordenadasCelda[0] % 2 == 0 && coordenadasCelda[1] % 2 != 0) ? Color.LightGray : Color.DimGray;
        }
        public static void EstilosDefaultCelda(List<int[]> coordenadas, Tablero Tablero, TableLayoutPanel Contenedor)
        {
            foreach(var coordenada in coordenadas)
            {
                EstilosDefaultCelda(Tablero: Tablero, LabelCelda: Contenedor.Controls.Find($"{coordenada[0]}/{coordenada[1]}", true)[0] as RotateLabel);
            }
        }
        public void Deseleccionar()
        {
            for (int i = 0; i<Tablero.Celdas.GetLength(1); i++)
            {
                for(int j = 0; j<Tablero.Celdas.GetLength(1); j++)
                {
                    Celda celda = Tablero.Celdas[i, j];
                    RotateLabel LabelCelda = Contenedor.Controls.Find($"{celda.Y}/{celda.X}", true)[0] as RotateLabel;
                    if (celda.Esta(CeldaEstados.Seleccionada)) { celda.CambiarEstado(CeldaEstados.TanqueTurno); EstilosDefaultCelda(LabelCelda, Tablero); }
                    if (celda.Esta(CeldaEstados.Avanzar)) { celda.CambiarEstado(CeldaEstados.Libre); EstilosDefaultCelda(LabelCelda, Tablero); }
                    if (celda.Esta(CeldaEstados.Disparable)) { celda.CambiarEstado(CeldaEstados.Ocupada); EstilosDefaultCelda(LabelCelda, Tablero); }
                }
            }
            InfoTanque = null;
        }
        private void ActualizarTitulos()
        {
            this.Titulo.Text = $"Turno de: {NombreTurno}";
            this.Titulo.ForeColor = ColorTurno;

            this.Subtitulo.Text = $"Jugadas Restantes: {JugadasRestantes}";
            this.Subtitulo.ForeColor = ColorTurno;

            if(InfoTanque != null)
            {
                this.VidaTanqueSeleccionado.Text = $"Vida: {InfoTanque.Vida}";
                this.DañoTanqueSeleccionado.Text = $"Daño: {InfoTanque.Daño}";
                this.DueñoTanqueSeleccionado.Text = $"Dueño: {Jugadores.Nombres[InfoTanque.Dueño]}";
                this.TipoTanqueSeleccionado.Text = $"Tipo: {InfoTanque.GetType().Name}";
                this.ApuntaTanqueSeleccionado.Text = $"Apunta hacia el: {InfoTanque.Apunta.ToString()}";
                this.RachaTanqueSeleccionado.Text = $"Racha: {InfoTanque.Racha}";
                this.RangoTanqueSeleccionado.Text = $"Alcance: {InfoTanque.Rango}";
                this.GasolinaTanqueSeleccionado.Text = $"Gasolina: {InfoTanque.Gas}";
            }
            else
            {
                this.VidaTanqueSeleccionado.Text = "";
                this.DañoTanqueSeleccionado.Text = "";
                this.DueñoTanqueSeleccionado.Text = "";
                this.TipoTanqueSeleccionado.Text = "";
                this.ApuntaTanqueSeleccionado.Text = "";
                this.RachaTanqueSeleccionado.Text = "";
                this.RangoTanqueSeleccionado.Text = "";
                this.GasolinaTanqueSeleccionado.Text = "";
            }
            
        }
        public void Mostrar()
        {
            if (Contenedor == null) { throw new ArgumentNullException(nameof(Contenedor), "No existe un contenedor para mostrar el tablero!"); }
            for (int i = 0; i < Contenedor.ColumnCount; i++)
            {
                for (int k = 0; k < Contenedor.RowCount; k++)
                {
                    var celda = new RotateLabel();
                    var celdaActual = this.Tablero.Celdas[i, k];
                    celda.Name = $"{i}/{k}";
                    EstilosDefaultCelda(celda, Tablero);
                    if (celdaActual.Estado == CeldaEstados.Deshabilitado) { celda.BackColor = Color.Transparent; }
                    celda.Click += new EventHandler(this.Jugada);
                    Contenedor.Controls.Add(celda, i, k);
                    //celda.Dispose();
                }
            }
        }
        public void Gano()
        {
            PopUp popUp = new PopUp($"{NombreTurno} GANA!", ColorTurno, "Regresar a Inicio");
            popUp.ShowDialog(Window);
            popUp.Dispose();
            Window.Close();
            Window.Dispose();
        }
    }
}
