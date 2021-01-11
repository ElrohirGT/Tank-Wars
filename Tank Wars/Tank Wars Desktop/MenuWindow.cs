using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tank_Wars_Desktop
{
    public partial class MenuWindow : Form
    {
        public int CantidadJugadores {
            get
            {
                return Convert.ToInt32((this.RadioButton2Players.Checked) ? this.RadioButton2Players.Text: this.RadioButton4Players.Text);
            }
            private set { }
        }
        public List<string> Nombres
        {
            get
            {
                var nombres = new List<string>();
                nombres.Add(TextBoxPlayer1.Text);
                nombres.Add(TextBoxPlayer2.Text);
                if (!RadioButton2Players.Checked)
                {
                    nombres.Add(TextBoxPlayer3.Text);
                    nombres.Add(TextBoxPlayer4.Text);
                }
                return nombres;
            }
            private set { }
        }
        public MenuWindow()
        {
            InitializeComponent();
            this.Player1Icon.BackColor = ColoresJugadores.Jugador1;
            this.Player2Icon.BackColor = ColoresJugadores.Jugador2;
            this.Player3Icon.BackColor = ColoresJugadores.Jugador3;
            this.Player4Icon.BackColor = ColoresJugadores.Jugador4;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MainWindow form = new MainWindow(CantidadJugadores, Nombres);
            this.Hide();
            form.ShowDialog(this);
            form.Dispose();
            this.Show();
        }

        private void RadioButton2Players_CheckedChanged(object sender, EventArgs e)
        {
            this.Player3Container.Visible = false;
            this.Player4Container.Visible = false;
        }

        private void RadioButton4Players_CheckedChanged(object sender, EventArgs e)
        {
            this.Player3Container.Visible = true;
            this.Player4Container.Visible = true;
        }
    }
}
