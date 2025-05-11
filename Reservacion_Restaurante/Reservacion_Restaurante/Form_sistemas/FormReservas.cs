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

            this.Load += FormReservas_Load;
        }
       
        private void FormReservas_Load(object sender, EventArgs e)
        {
            Mostar(ref TabReserva, Ruta); // Llama al método Mostrar al cargar el formulario
                                          // Ajustar al contenedor
           
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

                        long Apartado = br.ReadInt64();

                        int numPersonas = br.ReadInt32();

                        int longitudFecha = br.ReadInt32();
                        string fecha = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longitudFecha);

                        bool estado = br.ReadBoolean();

                        if (estado == true)
                        {
                            reserva.Rows.Add(mesa, numPersonas, nombre, apellidoP, apellidoM, telefono, fecha, Apartado, estado);
                        }
                        //reserva.Rows.Add(mesa, numPersonas, nombre, apellidoP, apellidoM, telefono, fecha, Apartado, estado);
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
            long Apartado = 0;
            int numPersonas = 0;
            bool estado = false;

            // Realizar la búsqueda
            string resultado = BuscarMesaPorNumero(Ruta, numeroMesa, ref nombre, ref apellidoP,
                                                 ref apellidoM, ref telefono,ref Apartado, ref numPersonas,
                                                 ref fecha, ref estado);
            txtBuscar.Clear();

            if (resultado == "SiExiste")
            {
                TabReserva.Rows.Clear();
               
                TabReserva.Rows.Add(numeroMesa, numPersonas, nombre, apellidoP, apellidoM, telefono, fecha, Apartado, estado);
                btnBorrar.Enabled = true;
                Tag = numeroMesa;
                //listBox.HorizontalExtent = 1000; 
            }
            else if (resultado == "NoExiste")
            {
                MessageBox.Show("Mesa no asignada", "Error");
                btnBorrar.Enabled = false;
            }

            else
            {
                MessageBox.Show("Ocurrió un error al buscar la mesa", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnBorrar.Enabled = false;
            }

        }


        public string BuscarMesaPorNumero(string ruta, int mesa, ref string nombre, ref string apellidoP, ref string apellidoM, ref long telefono, ref long Apartado,ref int numPersonas,
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

                            Apartado = br.ReadInt64();

                            numPersonas = br.ReadInt32();

                            int longFecha = br.ReadInt32();
                            fecha = Encoding.ASCII.GetString(br.ReadBytes(20), 0, longFecha);

                            estado = br.ReadBoolean();

                            return "SiExiste";
                        }
                        else
                        {

                            fs.Seek(127, SeekOrigin.Current);
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

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }
        public bool ActualizarEstadoMesa(string ruta, int numeroMesa, bool nuevoEstado)
        {
            try
            {
                string rutaTemporal = Path.GetTempFileName();

                using (var fsLectura = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fsLectura))
                using (var fsEscritura = new FileStream(rutaTemporal, FileMode.Create, FileAccess.Write))
                using (var bw = new BinaryWriter(fsEscritura))
                {
                    while (fsLectura.Position < fsLectura.Length)
                    {
                        int mesaActual = br.ReadInt32();
                        bw.Write(mesaActual);

                        int longNombre = br.ReadInt32();
                        bw.Write(longNombre);
                        bw.Write(br.ReadBytes(30));

                        int longApellidoP = br.ReadInt32();
                        bw.Write(longApellidoP);
                        bw.Write(br.ReadBytes(20));

                        int longApellidoM = br.ReadInt32();
                        bw.Write(longApellidoM);
                        bw.Write(br.ReadBytes(20));

                        long telefono = br.ReadInt64();
                        bw.Write(telefono);

                        long Apartado = br.ReadInt64();
                        bw.Write(Apartado);

                        int numPersonas = br.ReadInt32();
                        bw.Write(numPersonas);

                        int longFecha = br.ReadInt32();
                        bw.Write(longFecha);
                        bw.Write(br.ReadBytes(20));

                        bool estadoActual = br.ReadBoolean();
                        if (mesaActual == numeroMesa)
                        {
                            bw.Write(nuevoEstado);
                        }
                        else
                        {
                            bw.Write(estadoActual);
                        }
                    }
                }

                File.Delete(ruta);
                File.Move(rutaTemporal, ruta);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la mesa {numeroMesa}: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Verificar si la busqueda fue exitosa
            if (Tag != null && int.TryParse(Tag.ToString(), out int numeroMesaParaActualizar))
            {
                // Preguntar al usuario si realmente quiere eliminar la mesa
                DialogResult resultado = MessageBox.Show($"¿Desea eliminar la mesa número {numeroMesaParaActualizar} ?",
                                                        "Confirmar Acción", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    if (ActualizarEstadoMesa(Ruta, numeroMesaParaActualizar, false))
                    {
                        MessageBox.Show($"La mesa número {numeroMesaParaActualizar} ha sido Eliminada.", "Éxito");
                        TabReserva.Rows.Clear();
                        btnBorrar.Enabled = false; // boton eliminar desabilitado
                    }
                    else
                    {
                        MessageBox.Show($"Ocurrió un error al eliminar la mesa número {numeroMesaParaActualizar}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, busca una mesa primero antes de eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TabReserva_Resize(object sender, EventArgs e)
        {
            
        }

        private void FormReservas_Resize(object sender, EventArgs e)
        {
            TabReserva.AutoResizeColumns();
        }
       
    }
}
