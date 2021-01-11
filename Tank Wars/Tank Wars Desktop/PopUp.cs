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
    public partial class PopUp : Form
    {
        public PopUp(string mensaje, Color color, string boton)
        {
            InitializeComponent();
            this.Mensaje.Text = mensaje;
            this.Mensaje.ForeColor = color;
            this.Boton.Click += Boton_Click;
            this.Boton.Text = boton;
        }

        private void Boton_Click(object sender, EventArgs e)
        {
            this.Close();
            //throw new NotImplementedException();
        }
    }
}
