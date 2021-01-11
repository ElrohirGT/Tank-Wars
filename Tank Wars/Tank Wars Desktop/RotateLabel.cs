using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Tank_Wars_Desktop
{
    public class RotateLabel : Label
    {
        public float RotateAngle { get; set; } = 0;
        protected override void OnPaint(PaintEventArgs e)
        {
            Brush b = new SolidBrush(ForeColor);
            e.Graphics.TranslateTransform(Width / 2, Height / 2);
            e.Graphics.RotateTransform(RotateAngle);
            e.Graphics.DrawString(Text, Font, b, -Width / 3.5f, -Height / 2.5f);
            b.Dispose();
            //base.OnPaint(e);
        }
    }
}
