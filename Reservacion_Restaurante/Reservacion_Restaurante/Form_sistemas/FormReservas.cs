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

namespace Reservacion_Restaurante.Form_sistemas
{
    public partial class FormReservas : Form
    {
        FormAñadirReservas reservas;
        static string Ruta = "Tienda.Dat";

        public FormReservas()
        {
            
            InitializeComponent();
            //reservas = _reservas;
            
            TabReserva.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

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

                        int mesa = br.ReadInt32();

                        int longitudNombre = br.ReadInt32();
                        string nombre = Encoding.ASCII.GetString(br.ReadBytes(30), 0, longitudNombre);

                        int longitudApellido1 = br.ReadInt32();
                        string apellidoP = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudApellido1);

                        int longitudApellido2 = br.ReadInt32();
                        string apellidoM = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudApellido2);

                        long telefono = br.ReadInt64();

                        int numPersonas = br.ReadInt32();

                        int longitudFecha = br.ReadInt32();
                        string fecha = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudFecha);

                        bool estado = br.ReadBoolean();

                         
                        reserva.Rows.Add(mesa, numPersonas, nombre, apellidoP, apellidoM, telefono, fecha, estado);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MostrarEnListBox(string ruta, ListBox listBox)
        {
            
        }
        
        private void btnVer_Click(object sender, EventArgs e)
        {
            FormReservas archivo = new FormReservas();

            // Llama al método pasando la ruta y el ListBox
            archivo.Mostar(ref TabReserva, Ruta);
           

        }

        private void btn_Click(object sender, EventArgs e)
        {
           
                // Validar que se haya ingresado un número válido
                if (!int.TryParse(txtBuscar.Text, out int numeroMesa) || numeroMesa <= 0)
                {
                    MessageBox.Show("Por favor ingrese un número de mesa válido (mayor que cero)",
                                  "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                
                string nombre = "", apellidoP = "", apellidoM = "", fecha = "";
                long telefono = 0;
                int numPersonas = 0;
                bool estado = false;

                // Realizar la búsqueda
                string resultado = BuscarMesaPorNumero(Ruta, numeroMesa, ref nombre, ref apellidoP,
                                                     ref apellidoM, ref telefono, ref numPersonas,
                                                     ref fecha, ref estado);

                
                if (resultado == "SiExiste")
                {
                    TabReserva.Rows.Clear(); 

                    
                    

                    TabReserva.Rows.Add(numeroMesa, numPersonas, nombre, apellidoP, apellidoM, telefono, fecha, estado);
                    //listBox.HorizontalExtent = 1000; 
                }
                else if (resultado == "NoExiste")
            {
                MessageBox.Show("Mesa no asignada","Error");
            }
                
                else
                {
                    MessageBox.Show("Ocurrió un error al buscar la mesa", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

            
            public string BuscarMesaPorNumero(string ruta, int mesa, ref string nombre, ref string apellidoP,ref string apellidoM, ref long telefono, ref int numPersonas,
                                            ref string fecha, ref bool estado)
            {
                try
                {
                    using (var fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                    using (var br = new BinaryReader(fs))
                    {
                        while (fs.Position < fs.Length)
                        {
                            int mesaActual = br.ReadInt32();

                            if (mesaActual == mesa)
                            {
                                // Leer todos los campos del registro
                                int longNombre = br.ReadInt32();
                                nombre = Encoding.ASCII.GetString(br.ReadBytes(30), 0, longNombre);

                                int longApellidoP = br.ReadInt32();
                                apellidoP = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longApellidoP);

                                int longApellidoM = br.ReadInt32();
                                apellidoM = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longApellidoM);

                                telefono = br.ReadInt64();
                                numPersonas = br.ReadInt32();

                                int longFecha = br.ReadInt32();
                                fecha = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longFecha);

                                estado = br.ReadBoolean();

                                return "SiExiste";
                            }
                            else
                            {
                                
                                fs.Seek(119, SeekOrigin.Current);
                            }
                        }
                    }
                    return "NoExiste";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar: {ex.Message}", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "Error";
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
