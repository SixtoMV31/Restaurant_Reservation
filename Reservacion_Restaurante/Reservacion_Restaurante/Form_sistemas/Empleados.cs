using Org.BouncyCastle.Utilities;
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
    public partial class Empleados : Form
    {
        public Empleados()
        {
            InitializeComponent();
            CrearListaIndices();
        }
        private const int ID = 4;
        private const int TAM_LOGITUD_NOMBRE = 4;
        private const int TAM_NOMBRE = 30;

        private const int TAM_LONGITUD_APELLIDO1 = 4;
        private const int TAM_APELLIDO1 = 20;

        private const int TAM_LONGITUD_APELLIDO2 = 4;
        private const int TAM_APELLIDO2 = 20;

        private const int TAM_TELEFONO = 8;

        private const int TAM_LONGITUD_DIRECCION = 4;
        private const int TAM_DIRECCION = 30;

        private const int TAM_LONGITUD_CORREO = 4;
        private const int TAM_CORREO = 35;

        private const int TAM_LONGITUD_CARGO = 4;
        private const int TAM_CARGO = 15;

        private const int TamañoRegistro = ID + TAM_LOGITUD_NOMBRE + TAM_NOMBRE +
            TAM_LONGITUD_APELLIDO1 + TAM_APELLIDO1 + TAM_LONGITUD_APELLIDO2 +
            TAM_APELLIDO2 + TAM_TELEFONO + TAM_LONGITUD_DIRECCION + TAM_DIRECCION + TAM_LONGITUD_CORREO + TAM_CORREO + TAM_LONGITUD_CARGO + TAM_CARGO;
        static string Ruta = "Expedientes.Dat";
        public LinkedList<Nodo> ListaIndices;
        FileStream fs;
        BinaryReader Br;
        BinaryWriter Bw;

        Byte[] BytesID;
        Byte[] BytesLogitudNombre;
        Byte[] BytesNombre;
        Byte[] BytesLongitudApellido1;
        Byte[] BytesApellido1;
        Byte[] BytesLongitudApellido2;
        Byte[] BytesApellido2;
        Byte[] BytesTelefono;
        Byte[] BytesLongitudDireccion;
        Byte[] BytesDireccion;
        Byte[] BytesLongitudCorreo;
        Byte[] BytesCorreo;
        Byte[] BytesLongitudCargo;
        Byte[] BytesCargo;

        Byte[] BytesTamañoRegistro;

        string NombreCliente;
        string ApellidoPaterno;
        string ApellidoMaterno;
        string Direccion;
        string Correo;
        string Cargo;
        long Telefono;
        int id;
        
        int lgNombre;
        int lgApellido1;
        int lgApellido2;
        int lgDirecion;
        int lgCorreo;
        int lgCargo;


        public class Nodo
        {
            public int ID { get; set; }
            public int Indice { get; set; }
        }
       
        public void CrearListaIndices()
        {
            ListaIndices = new LinkedList<Nodo>();
        }

        public bool GenerarListaIndices(string Ruta)
        {
            try
            {
                ListaIndices.Clear();
                if (File.Exists(Ruta) && new FileInfo(Ruta).Length > 0)
                {
                    fs = new FileStream(Ruta, FileMode.Open, FileAccess.Read);
                    Br = new BinaryReader(fs);

                    int contadorIndice = 0;
                    while (Br.BaseStream.Position < Br.BaseStream.Length)
                    {
                        fs.Seek(contadorIndice * TamañoRegistro, SeekOrigin.Begin);

                        BytesID = Br.ReadBytes(ID);
                        int Mesa = BitConverter.ToInt32(BytesID, 0);

                        BytesLogitudNombre = Br.ReadBytes(TAM_LOGITUD_NOMBRE);
                        Br.BaseStream.Seek(TamañoRegistro - TAM_LOGITUD_NOMBRE, SeekOrigin.Current);

                        fs.Seek((contadorIndice * TamañoRegistro) + TamañoRegistro - TAM_CARGO, SeekOrigin.Begin);
                        BytesCargo = Br.ReadBytes(TAM_CARGO);
                        bool estado = BitConverter.ToBoolean(BytesCargo, 0);
                        if (estado == true)
                        {
                            ListaIndices.AddLast(new Nodo { ID = contadorIndice, Indice = Mesa });
                        }
                        contadorIndice++;
                    }
                    fs.Close();
                    Br.Close();
                }
                return true;


            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error al generar indices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        public Nodo BuscarNodoExistente(int mesa)
        {
            Nodo nodo = null;
            if (ListaIndices != null)
            {
                foreach (var item in ListaIndices)
                {
                    if (item.ID == mesa)
                    {
                        nodo = new Nodo();
                        nodo.Indice = item.Indice;
                        nodo.ID = item.ID;
                        break;
                    }
                }
                if (nodo != null)
                {
                    return nodo;
                }
                else
                { return null; }
            }
            else { return null; }
        }

        public string InsertarNuevaReservacion(string Ruta, int id, string nombre, string apellido1, string apellido2, long telefono, string direccion, string correo, string cargo)
        {
            try
            {
                GenerarListaIndices(Ruta);
                Nodo nodo = BuscarNodoExistente(id);
                if (nodo == null)
                {
                    fs = new FileStream(Ruta, FileMode.Append, FileAccess.Write);
                    Bw = new BinaryWriter(fs);
                    BytesTamañoRegistro = new Byte[TamañoRegistro];

                    BytesID = BitConverter.GetBytes(id);

                    lgNombre = nombre.Length;
                    BytesLogitudNombre = BitConverter.GetBytes(lgNombre);
                    BytesNombre = Encoding.ASCII.GetBytes(nombre.PadRight(TAM_NOMBRE));

                    lgApellido1 = apellido1.Length;
                    BytesLongitudApellido1 = BitConverter.GetBytes(lgApellido1);
                    BytesApellido1 = Encoding.ASCII.GetBytes(apellido1.PadRight(TAM_APELLIDO1));

                    lgApellido2 = apellido2.Length;
                    BytesLongitudApellido2 = BitConverter.GetBytes(lgApellido2);
                    BytesApellido2 = Encoding.ASCII.GetBytes(apellido2.PadRight(TAM_APELLIDO2));

                    BytesTelefono = BitConverter.GetBytes(telefono);

                    lgDirecion = direccion.Length;
                    BytesLongitudDireccion = BitConverter.GetBytes(lgDirecion);
                    BytesDireccion = Encoding.ASCII.GetBytes(direccion.PadRight(TAM_DIRECCION));

                    lgCorreo = correo.Length;
                    BytesLongitudCorreo = BitConverter.GetBytes(lgCorreo);
                    BytesCorreo = Encoding.ASCII.GetBytes(correo.PadRight(TAM_CORREO));

                    lgCargo = cargo.Length;
                    BytesLongitudCargo = BitConverter.GetBytes(lgCargo);
                    BytesCargo = Encoding.ASCII.GetBytes(cargo.PadRight(TAM_CARGO));

                    int pos = 0;
                    BytesID.CopyTo(BytesTamañoRegistro, pos); pos += ID;
                    BytesLogitudNombre.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LOGITUD_NOMBRE;
                    BytesNombre.CopyTo(BytesTamañoRegistro, pos); pos += TAM_NOMBRE;
                    BytesLongitudApellido1.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_APELLIDO1;
                    BytesApellido1.CopyTo(BytesTamañoRegistro, pos); pos += TAM_APELLIDO1;
                    BytesLongitudApellido2.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_APELLIDO2;
                    BytesApellido2.CopyTo(BytesTamañoRegistro, pos); pos += TAM_APELLIDO2;
                    BytesTelefono.CopyTo(BytesTamañoRegistro, pos); pos += TAM_TELEFONO;
                    BytesLongitudDireccion.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_DIRECCION;
                    BytesDireccion.CopyTo(BytesTamañoRegistro, pos); pos += TAM_DIRECCION;
                    BytesLongitudCorreo.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_CORREO;
                    BytesCorreo.CopyTo(BytesTamañoRegistro, pos); pos += TAM_CORREO;
                    BytesLongitudCargo.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_CARGO;
                    BytesCargo.CopyTo(BytesTamañoRegistro, pos); pos += TAM_CARGO;

                    // Escribir el registro en el archivo
                    Bw.Write(BytesTamañoRegistro);
                    fs.Close();
                    Bw.Close();

                    return "Reservación registrada con éxito";
                }
                else { return "Ya existe una reservacion de esa mesa"; }
            }
            catch (Exception ex)
            {

                return $"error al insertar reserva: {ex.Message}";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos de los controles del formulario para agregarlos al documento
                NombreCliente = textNOMBRE.Text;
                ApellidoPaterno = textAP.Text;
                ApellidoMaterno = textAM.Text;
                //Telefono = txtNT.Text;
                Telefono = long.Parse(txtNT.Text);
                id = int.Parse(textID.Text);
                Correo = textcorreo.Text;
                Cargo = textCargo.Text;
                Direccion = textDireccion.Text;

                               
                int nuevoId = 1;
                if (ListaIndices.Count > 0)
                {
                    nuevoId = ListaIndices.Max(n => n.ID) + 1;
                }

                // Insertar la reservación
                string resultado = InsertarNuevaReservacion(Ruta, id, NombreCliente, ApellidoPaterno, ApellidoMaterno,
                                                    Telefono, Direccion, Correo, Cargo);


                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                               resultado.Contains("éxito") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                // Si se insertó correctamente esto debe limpiar ls datos
                if (resultado.Contains("éxito"))
                {
                    LimpiarCampos();
                }

                // Regenerar la lista de índices en el archivo
                GenerarListaIndices(Ruta);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la reservación: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LimpiarCampos()
        {
            // Limpiar todos los campos del formulario
            textNOMBRE.Clear();
            txtNT.Clear();
            textCargo.Clear();
            textAM.Clear();
            textAP.Clear();
            textcorreo.Clear();
            textID.Clear();
            textDireccion.Clear();
        }

        private void textID_TextChanged(object sender, EventArgs e)
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
