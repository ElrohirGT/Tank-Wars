using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tank_Wars_Desktop.Tanques;

namespace Tank_Wars_Desktop
{
    public partial class MainWindow : Form
    {
        public MainWindow(int CantidadJugadores, List<string> nombres)
        {
            InitializeComponent();
            this.SimboloTanquePequeño.Text += (char)TanqueSimbolos.TanquePequeño;
            this.SimboloTanqueMadre.Text += (char)TanqueSimbolos.TanqueMadre;
            new Juego(CantidadJugadores, nombres, this, ContenedorTablero, Titulo, LabelJugadasRestantes, ContenedorInfoTanque, BotonSaltarTurno);
        }
    }
}
