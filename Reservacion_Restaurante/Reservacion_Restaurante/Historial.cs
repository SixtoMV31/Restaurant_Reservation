using Reservacion_Restaurante.Form_sistemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reservacion_Restaurante
{
    public partial class Historial : Form
    {
        static string Ruta = "Tienda.Dat";
        public Historial()
        {
            InitializeComponent();
            //datHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        }

        private void Historial_Load(object sender, EventArgs e)
        {
            MostrarHistorial(ref datHistorial, Ruta);
        }
        public void MostrarHistorial(ref DataGridView reserva, string ruta)
        {
            long SumaDePagos = 0;
            try
            {
                reserva.Rows.Clear();

                using (var fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length)
                    {

                        int mesa = br.ReadInt32();

                        int longitudNombre = br.ReadInt32();
                        string nombre = Encoding.ASCII.GetString(br.ReadBytes(30), 0, longitudNombre);

                        int longitudApellido1 = br.ReadInt32();
                        string apellidoP = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudApellido1);

                        int longitudApellido2 = br.ReadInt32();
                        string apellidoM = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudApellido2);

                        long telefono = br.ReadInt64();

                        long Apartado = br.ReadInt64();

                        int numPersonas = br.ReadInt32();

                        int longitudFecha = br.ReadInt32();
                        string fecha = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudFecha);

                        bool estado = br.ReadBoolean();

                        if (estado == true)
                        {
                            reserva.Rows.Add(mesa, nombre, apellidoP, apellidoM, fecha, Apartado);
                            SumaDePagos += Apartado;
                        }
                        
                        //reserva.Rows.Add(mesa, numPersonas, nombre, apellidoP, apellidoM, telefono, fecha, Apartado, estado);
                    }
                    txtSuma.Text = $"{SumaDePagos}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    
}
