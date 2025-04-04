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
    public partial class FormAñadirReservas : Form
    {
        public FormAñadirReservas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 RegresarEmpleado = new Form1();
            if (RegresarEmpleado != null)
            {
                RegresarEmpleado.Show();
                this.Close();
            }
            else
            {
                Form anteriorEmpleado = new Form1();
                anteriorEmpleado.Show();
                this.Close();
            }
        }
    }
}
