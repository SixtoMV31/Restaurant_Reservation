﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservacion_Restaurante.Form_sistemas
{
    public partial class FormMesas : Form
    {
        static string Ruta = "Expedientes.Dat";
        public FormMesas()
        {
            InitializeComponent();
        }

        private void FormMesas_Load(object sender, EventArgs e)
        {
            Mostar(ref datExpedientes, Ruta);
        }
        public void Mostar(ref DataGridView reserva, string ruta)
        {
            try
            {
                reserva.Rows.Clear();

                using (var fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {

                        int id = br.ReadInt32();

                        int longitudNombre = br.ReadInt32();
                        string nombre = Encoding.ASCII.GetString(br.ReadBytes(30), 0, longitudNombre);

                        int longitudApellido1 = br.ReadInt32();
                        string apellidoP = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudApellido1);

                        int longitudApellido2 = br.ReadInt32();
                        string apellidoM = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudApellido2);

                        long telefono = br.ReadInt64();

                        int lgdireccion = br.ReadInt32();
                        string direccion = Encoding.ASCII.GetString(br.ReadBytes(30), 0, lgdireccion);

                        int lgcorreo = br.ReadInt32();
                        string correo = Encoding.ASCII.GetString(br.ReadBytes(35), 0, lgcorreo);

                        int lgcargo = br.ReadInt32();
                        string cargo = Encoding.ASCII.GetString(br.ReadBytes(15), 0, lgcargo);



                       
                            reserva.Rows.Add(id, nombre, apellidoP, apellidoM, telefono, direccion, correo, cargo);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            OpcionesSistema op = new OpcionesSistema();
            if (op != null)
            {
                op.Show();
                this.Close();
            }
            else
            {
                Form anterior = new OpcionesSistema();
                anterior.Show();
                this.Close();
            }
        }
    }
}
