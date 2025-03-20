namespace Reservacion_Restaurante.Form_sistemas
{
    partial class OpcionesSistema
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
            this.btnReservas = new System.Windows.Forms.Button();
            this.btnMesas = new System.Windows.Forms.Button();
            this.BtnCerrarSesion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReservas
            // 
            this.btnReservas.Location = new System.Drawing.Point(242, 152);
            this.btnReservas.Name = "btnReservas";
            this.btnReservas.Size = new System.Drawing.Size(137, 39);
            this.btnReservas.TabIndex = 0;
            this.btnReservas.Text = "Gestion de Reservas";
            this.btnReservas.UseVisualStyleBackColor = true;
            this.btnReservas.Click += new System.EventHandler(this.btnReservas_Click);
            // 
            // btnMesas
            // 
            this.btnMesas.Location = new System.Drawing.Point(442, 152);
            this.btnMesas.Name = "btnMesas";
            this.btnMesas.Size = new System.Drawing.Size(137, 39);
            this.btnMesas.TabIndex = 1;
            this.btnMesas.Text = "Gestion de mesas";
            this.btnMesas.UseVisualStyleBackColor = true;
            this.btnMesas.Click += new System.EventHandler(this.btnMesas_Click);
            // 
            // BtnCerrarSesion
            // 
            this.BtnCerrarSesion.Location = new System.Drawing.Point(345, 229);
            this.BtnCerrarSesion.Name = "BtnCerrarSesion";
            this.BtnCerrarSesion.Size = new System.Drawing.Size(137, 39);
            this.BtnCerrarSesion.TabIndex = 2;
            this.BtnCerrarSesion.Text = "Cerrar sesion";
            this.BtnCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // OpcionesSistema
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnCerrarSesion);
            this.Controls.Add(this.btnMesas);
            this.Controls.Add(this.btnReservas);
            this.Name = "OpcionesSistema";
            this.Text = "OpcionesSistema";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReservas;
        private System.Windows.Forms.Button btnMesas;
        private System.Windows.Forms.Button BtnCerrarSesion;
    }
}