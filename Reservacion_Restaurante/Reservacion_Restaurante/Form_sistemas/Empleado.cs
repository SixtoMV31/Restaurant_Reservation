﻿using System;
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
    public partial class Empleado : Form
    {
        public Empleado()
        {
            InitializeComponent();
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            Form añadirreserva = new FormAñadirReservas();
            añadirreserva.Show();
        }

        private void btnMesas_Click(object sender, EventArgs e)
        {
            Form mesas = new FormMesas();
            mesas.Show();
        }
    }
}
