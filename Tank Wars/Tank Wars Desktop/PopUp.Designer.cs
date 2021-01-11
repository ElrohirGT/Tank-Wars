namespace Tank_Wars_Desktop
{
    partial class PopUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUp));
            this.Mensaje = new System.Windows.Forms.Label();
            this.Boton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Mensaje
            // 
            this.Mensaje.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Mensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mensaje.Location = new System.Drawing.Point(0, 0);
            this.Mensaje.Name = "Mensaje";
            this.Mensaje.Size = new System.Drawing.Size(800, 450);
            this.Mensaje.TabIndex = 0;
            this.Mensaje.Text = "MENSAJE";
            this.Mensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Boton
            // 
            this.Boton.AutoSize = true;
            this.Boton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.Boton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Boton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Boton.Location = new System.Drawing.Point(358, 409);
            this.Boton.Name = "Boton";
            this.Boton.Size = new System.Drawing.Size(75, 29);
            this.Boton.TabIndex = 1;
            this.Boton.Text = "BOTON";
            this.Boton.UseVisualStyleBackColor = false;
            // 
            // PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Boton);
            this.Controls.Add(this.Mensaje);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PopUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ALERTA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Mensaje;
        private System.Windows.Forms.Button Boton;
    }
}