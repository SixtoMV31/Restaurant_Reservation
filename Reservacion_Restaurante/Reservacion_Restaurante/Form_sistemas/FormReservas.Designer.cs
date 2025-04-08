namespace Reservacion_Restaurante.Form_sistemas
{
    partial class FormReservas
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
            this.lstReservas = new System.Windows.Forms.ListBox();
            this.btnVer = new System.Windows.Forms.Button();
            this.btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lstReservas
            // 
            this.lstReservas.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lstReservas.FormattingEnabled = true;
            this.lstReservas.ItemHeight = 16;
            this.lstReservas.Location = new System.Drawing.Point(190, 7);
            this.lstReservas.Name = "lstReservas";
            this.lstReservas.Size = new System.Drawing.Size(609, 436);
            this.lstReservas.TabIndex = 0;
            // 
            // btnVer
            // 
            this.btnVer.Location = new System.Drawing.Point(28, 399);
            this.btnVer.Name = "btnVer";
            this.btnVer.Size = new System.Drawing.Size(131, 23);
            this.btnVer.TabIndex = 1;
            this.btnVer.Text = "Actualizar";
            this.btnVer.UseVisualStyleBackColor = true;
            this.btnVer.Click += new System.EventHandler(this.btnVer_Click);
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(58, 92);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(75, 23);
            this.btn.TabIndex = 2;
            this.btn.Text = "Buscar";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 46);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(156, 22);
            this.textBox1.TabIndex = 3;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(40, 316);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(111, 32);
            this.btnRegresar.TabIndex = 4;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkOrange;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 450);
            this.panel1.TabIndex = 5;
            // 
            // FormReservas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.btnVer);
            this.Controls.Add(this.lstReservas);
            this.Controls.Add(this.panel1);
            this.Name = "FormReservas";
            this.Text = "FormReservas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstReservas;
        private System.Windows.Forms.Button btnVer;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.Panel panel1;
    }
}