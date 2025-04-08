using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservacion_Restaurante.Form_sistemas
{
    public partial class OpcionesSistema : Form
    {
        
        public OpcionesSistema()
        {
            InitializeComponent();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            
            Form Reserva=new FormReservas();
            Reserva.Show();
            this.Close();
            //OpcionesSistema opcionesSistema=new OpcionesSistema();
            //opcionesSistema.Close();
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            Form Mesas=new FormMesas();
            Mesas.Show();
        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            if (form1 != null)
            {
                form1.Show();
                this.Close(); 
            }
            else
            {
                Form anterior = new Form1();
                anterior.Show();
                this.Close();
            }
        }

        private void OpcionesSistema_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
