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
        //private string ruta = @"C:\ArchivoBinario\Reservas.dat";
        private string ruta = @"C:\ArchivoBinario\Tienda.dat";
        //private const char DELIMITADOR = ',';
        
        public FormReservas()
        {
            
            InitializeComponent();
        }
        public void MostrarEnListBox(string ruta, ListBox listBox)
        {
            try
            {
                listBox.Items.Clear();

                using (var fs = new FileStream(ruta, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    while (fs.Position < fs.Length) 
                    {
                        // Leer la estructura fija de 33 bytes
                        int id = br.ReadInt32();
                        int lgNombre = br.ReadInt32();
                        string nombre = Encoding.ASCII.GetString(br.ReadBytes(20), 0, lgNombre);
                        float precio = br.ReadSingle();
                        bool estado = br.ReadBoolean();

                        
                        listBox.Items.Add($"ID: {id.ToString("").PadRight(4)} | Nombre: {nombre.PadRight(20)} | Precio: {precio.ToString("C").PadRight(10)} | Estado: {(estado ? "Activo" : "Inactivo")}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
           
            FormReservas archivo = new FormReservas();

            // Llama al método pasando la ruta y el ListBox
            archivo.MostrarEnListBox(ruta, lstReservas);

           
            lstReservas.HorizontalExtent = 500;

                                   
        }

        private void btn_Click(object sender, EventArgs e)
        {

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
