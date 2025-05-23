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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Reservacion_Restaurante.Form_sistemas
{

    public partial class FormAñadirReservas : Form
    {
        private const int NUM_MESA = 4;
        private const int TAM_NUM_PERSONAS = 4;
        private const int TAM_LOGITUD_NOMBRE = 4;
        private const int TAM_NOMBRE = 30;
        private const int TAM_LONGITUD_APELLIDO1 = 4;
        private const int TAM_APELLIDO1 = 20;
        private const int TAM_LONGITUD_APELLIDO2 = 4;
        private const int TAM_APELLIDO2 = 20;
        private const int TAM_TELEFONO = 8;
        private const int TAM_APARTADO = 8;
        private const int TAM_LONGITUD_FECHA = 4;
        private const int TAM_FECHA = 20;
        private const int TAM_ESTADO = 1;

        private const int TamañoRegistro = NUM_MESA + TAM_NUM_PERSONAS + TAM_LOGITUD_NOMBRE + TAM_NOMBRE +
            TAM_LONGITUD_APELLIDO1 + TAM_APELLIDO1 + TAM_LONGITUD_APELLIDO2 +
            TAM_APELLIDO2 + TAM_TELEFONO +TAM_APARTADO+ TAM_LONGITUD_FECHA + TAM_FECHA + TAM_ESTADO;
        static string Ruta = "Tienda.Dat";
        public LinkedList<Nodo> ListaIndices;
        FileStream fs;
        BinaryReader Br;
        BinaryWriter Bw;

        Byte[] BytesNumeroMesa;
        Byte[] BytesNumeroDePersonas;
        Byte[] BytesLogitudNombre;
        Byte[] BytesNombre;
        Byte[] BytesLongitudApellido1;
        Byte[] BytesApellido1;
        Byte[] BytesLongitudApellido2;
        Byte[] BytesApellido2;
        Byte[] BytesTelefonoCliente;
        Byte[] bytesApartado;
        Byte[] BytesLongitudFecha;
        Byte[] BytesFechaCliente;
        Byte[] BytesEstado;

        Byte[] BytesTamañoRegistro;

        string NombreCliente;
        string ApellidoPaterno;
        string ApellidoMaterno;
        long Telefono;
        long Apartado;
        int NumeroPersonas;
        int NumeroMesa;
        string Fecha;
        bool EstadoReserva;
        int lgNombre;
        int lgApellido1;
        int lgApellido2;
        int lgFecha;

        public class Nodo
        {
            public int NumeroMesa { get; set; }
            public int Indice { get; set; }
        }
        public FormAñadirReservas()
        {
            InitializeComponent();
            CrearListaIndices();

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

                        BytesNumeroMesa = Br.ReadBytes(NUM_MESA);
                        int Mesa = BitConverter.ToInt32(BytesNumeroMesa, 0);

                        BytesLogitudNombre = Br.ReadBytes(TAM_LOGITUD_NOMBRE);
                        Br.BaseStream.Seek(TamañoRegistro - TAM_LOGITUD_NOMBRE, SeekOrigin.Current);

                        fs.Seek((contadorIndice * TamañoRegistro) + TamañoRegistro - TAM_ESTADO, SeekOrigin.Begin);
                        BytesEstado = Br.ReadBytes(TAM_ESTADO);
                        bool estado = BitConverter.ToBoolean(BytesEstado, 0);
                        if (estado == true)
                        {
                            ListaIndices.AddLast(new Nodo { NumeroMesa = contadorIndice, Indice = Mesa });
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
                    if (item.NumeroMesa == mesa)
                    {
                        nodo = new Nodo();
                        nodo.Indice = item.Indice;
                        nodo.NumeroMesa = item.NumeroMesa;
                        break;
                    }
                }
                if(nodo != null)
                {
                    return nodo;
                }
                else
                { return null; }
            }
            else { return null; }
        }

        public string InsertarNuevaReservacion(string Ruta, int mesa, string nombre, string apellido1, string apellido2, long telefono,long apartado, int numPersonas, string fecha, bool estado)
        {
            try
            {
                GenerarListaIndices(Ruta);
                Nodo nodo = BuscarNodoExistente(mesa);
                if (nodo == null)
                {
                    fs = new FileStream(Ruta, FileMode.Append, FileAccess.Write);
                    Bw = new BinaryWriter(fs);
                    BytesTamañoRegistro = new Byte[TamañoRegistro];


                    BytesNumeroMesa = BitConverter.GetBytes(mesa);

                    lgNombre = nombre.Length;
                    BytesLogitudNombre = BitConverter.GetBytes(lgNombre);
                    BytesNombre = Encoding.ASCII.GetBytes(nombre.PadRight(TAM_NOMBRE));

                    lgApellido1 = apellido1.Length;
                    BytesLongitudApellido1 = BitConverter.GetBytes(lgApellido1);
                    BytesApellido1 = Encoding.ASCII.GetBytes(apellido1.PadRight(TAM_APELLIDO1));

                    lgApellido2 = apellido2.Length;
                    BytesLongitudApellido2 = BitConverter.GetBytes(lgApellido2);
                    BytesApellido2 = Encoding.ASCII.GetBytes(apellido2.PadRight(TAM_APELLIDO2));

                    BytesTelefonoCliente = BitConverter.GetBytes(telefono);
                    bytesApartado = BitConverter.GetBytes(apartado);
                    BytesNumeroDePersonas = BitConverter.GetBytes(numPersonas);


                    lgFecha = fecha.Length;
                    BytesLongitudFecha = BitConverter.GetBytes(lgFecha);
                    BytesFechaCliente = Encoding.ASCII.GetBytes(fecha.PadRight(TAM_FECHA));

                    BytesEstado = BitConverter.GetBytes(estado);

                    
                    int pos = 0;
                    BytesNumeroMesa.CopyTo(BytesTamañoRegistro, pos); pos += NUM_MESA;
                    BytesLogitudNombre.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LOGITUD_NOMBRE;
                    BytesNombre.CopyTo(BytesTamañoRegistro, pos); pos += TAM_NOMBRE;
                    BytesLongitudApellido1.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_APELLIDO1;
                    BytesApellido1.CopyTo(BytesTamañoRegistro, pos); pos += TAM_APELLIDO1;
                    BytesLongitudApellido2.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_APELLIDO2;
                    BytesApellido2.CopyTo(BytesTamañoRegistro, pos); pos += TAM_APELLIDO2;

                    BytesTelefonoCliente.CopyTo(BytesTamañoRegistro, pos); pos += TAM_TELEFONO;

                    bytesApartado.CopyTo(BytesTamañoRegistro,pos);pos += TAM_APARTADO;
                    BytesNumeroDePersonas.CopyTo(BytesTamañoRegistro, pos); pos += TAM_NUM_PERSONAS;
                    BytesLongitudFecha.CopyTo(BytesTamañoRegistro, pos); pos += TAM_LONGITUD_FECHA;
                    BytesFechaCliente.CopyTo(BytesTamañoRegistro, pos); pos += TAM_FECHA;
                    BytesEstado.CopyTo(BytesTamañoRegistro, pos);

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

        private void button1_Click(object sender, EventArgs e)
        {
            Login RegresarEmpleado = new Login();
            if (RegresarEmpleado != null)
            {
                RegresarEmpleado.Show();
                this.Close();
            }
            else
            {
                Form anteriorEmpleado = new Login();
                anteriorEmpleado.Show();
                this.Close();
            }


        }
       
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los datos de los controles del formulario para agregarlos al documento
                NombreCliente = txtNombre.Text;
                ApellidoPaterno = txtApellidoPaterno.Text;
                ApellidoMaterno = txtApellidoMaterno.Text;

                // Validar datos requeridos y que los campos no esten vacios 
                if (string.IsNullOrWhiteSpace(NombreCliente) || string.IsNullOrWhiteSpace(ApellidoPaterno))
                {
                    MessageBox.Show("Debe ingresar al menos el nombre y apellido paterno", "Datos incompletos",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validar que el teléfono sea un número válido y no se  vayha una letra o se intente ingresar letras en vez de numeros
                if (!long.TryParse(txtTelefono.Text, out Telefono))
                {
                    MessageBox.Show("El teléfono debe ser un número válido", "Datos incorrectos",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NumeroPersonas = (int)numPersonas.Value;
                NumeroMesa = (int)numMesa.Value;
                //Apartado=(long)listApartado.SelectedIndex;
                if (listApartado.SelectedItem != null & long.TryParse(listApartado.SelectedItem.ToString(), out long apartadoValue))
                {
                    Apartado = apartadoValue;
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un monto válido para el apartado", "Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Fecha = dtpFecha.Value.ToString("yyyy-MM-dd HH:mm");

                EstadoReserva = true;

                
                int nuevoId = 1;
                if (ListaIndices.Count > 0)
                {
                    nuevoId = ListaIndices.Max(n => n.NumeroMesa) + 1;
                }

                // Insertar la reservación
                string resultado = InsertarNuevaReservacion(Ruta, NumeroMesa, NombreCliente, ApellidoPaterno, ApellidoMaterno,
                                                    Telefono,Apartado, NumeroPersonas, Fecha, EstadoReserva);


                MessageBox.Show(resultado, "Información", MessageBoxButtons.OK,
                               resultado.Contains("éxito") ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

                

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
            txtNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtTelefono.Clear();
            numPersonas.Value = numPersonas.Minimum;
            numMesa.Value = numMesa.Minimum;
            dtpFecha.Value = DateTime.Now;

        }

        private void Ticket_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardarpdf = new SaveFileDialog();
            guardarpdf.FileName = string.Format("{0}.pdf", DateTime.Now.ToString("ddMMyyyyHHmmss"));


            string paginaHtml_texto = Properties.Resources.plantilla.ToString();
            if (!paginaHtml_texto.Contains("<meta charset="))
            {
                paginaHtml_texto = paginaHtml_texto.Replace("<head>", "<head>\n    <meta charset=\"UTF-8\">");

            }
            paginaHtml_texto = paginaHtml_texto.Replace("@CLIENTE", txtNombre.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@APELLIDO1", txtApellidoPaterno.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@APELLIDO2", txtApellidoMaterno.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@NumMesa", numMesa.Value.ToString());
            paginaHtml_texto = paginaHtml_texto.Replace("@NumPersonas", numPersonas.Value.ToString());

            paginaHtml_texto = paginaHtml_texto.Replace("@NumTelefono", txtTelefono.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@Precio", listApartado.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@Total", listApartado.Text);
            paginaHtml_texto = paginaHtml_texto.Replace("@totales", listApartado.Text);

            paginaHtml_texto = paginaHtml_texto.Replace("@Fecha", dtpFecha.Value.ToString("dd/MM/yyyy"));

            

            if (guardarpdf.ShowDialog() == DialogResult.OK)
            {
                using(FileStream stream=new FileStream(guardarpdf.FileName,FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 25, 25, 25, 25);
                    PdfWriter write = PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    try
                    {
                        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(paginaHtml_texto);
                        using (MemoryStream ms = new MemoryStream(byteArray))
                        {
                           
                            XMLWorkerHelper.GetInstance().ParseXHtml(
                                write,
                                pdfdoc,
                                ms,
                                System.Text.Encoding.UTF8);
                        }
                    }
                    catch (Exception ex)
                     {

                            MessageBox.Show("Error al procesar el HTML: " + ex.Message);
                     }
                       
                    pdfdoc.Close();
                    stream.Close(); 
                }
               
            }
        }

       //ESTOS MET0D0S SON PARA GENERAR LA VENTANA DE MESAS OCUPADAS

        private void btnVerMesas_Click(object sender, EventArgs e)
        {
            
            try
            {
                
                GenerarListaIndices(Ruta);

               
                Form estadoMesasForm = new Form()
                {
                    Text = "Estado de Mesas",
                    Width = 400,
                    Height = 500,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterParent,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                
                Panel mainPanel = new Panel()
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.White
                };

               
                Label titulo = new Label()
                {
                    Text = "ESTADO DE MESAS",
                    Font = new System.Drawing.Font("Arial", 14, FontStyle.Bold),
                    ForeColor = Color.DarkSlateBlue,
                    AutoSize = true,
                    Top = 20,
                    Left = (estadoMesasForm.Width - 150) / 2
                };

                
                Label subtituloOcupadas = new Label()
                {
                    Text = "Mesas Ocupadas",
                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Red,
                    AutoSize = true,
                    Top = 60,
                    Left = 20
                };

               
                FlowLayoutPanel panelOcupadas = new FlowLayoutPanel()
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Top = 90,
                    Left = 20,
                    Width = estadoMesasForm.Width - 40,
                    WrapContents = true
                };

                
                Label subtituloDisponibles = new Label()
                {
                    Text = "Mesas Disponibles",
                    Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold),
                    ForeColor = Color.Green,
                    AutoSize = true,
                    Top = 200,
                    Left = 20
                };

                
                FlowLayoutPanel panelDisponibles = new FlowLayoutPanel()
                {
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Top = 230,
                    Left = 20,
                    Width = estadoMesasForm.Width - 40,
                    WrapContents = true
                };

                
                int totalMesas = 20;
                var mesasOcupadas = new List<int>();

                
                foreach (var nodo in ListaIndices)
                {
                    if (nodo != null)
                    {
                        mesasOcupadas.Add(nodo.Indice);
                    }
                }

                
                foreach (int mesa in mesasOcupadas)
                {
                    Panel mesaPanel = CreateMesaPanel(mesa, Color.Red, "Ocupada");
                    panelOcupadas.Controls.Add(mesaPanel);
                }

                if (panelOcupadas.Controls.Count == 0)
                {
                    panelOcupadas.Controls.Add(new Label()
                    {
                        Text = "No hay mesas ocupadas",
                        ForeColor = Color.Gray,
                        AutoSize = true
                    });
                }

                
                var mesasDisponibles = Enumerable.Range(1, totalMesas).Except(mesasOcupadas).ToList();
                foreach (int mesa in mesasDisponibles)
                {
                    Panel mesaPanel = CreateMesaPanel(mesa, Color.Green, "Disponible");
                    panelDisponibles.Controls.Add(mesaPanel);
                }

                if (panelDisponibles.Controls.Count == 0)
                {
                    panelDisponibles.Controls.Add(new Label()
                    {
                        Text = "No hay mesas disponibles",
                        ForeColor = Color.Gray,
                        AutoSize = true
                    });
                }

                
                Button btnCerrar = new Button()
                {
                    Text = "Cerrar",
                    DialogResult = DialogResult.OK,
                    Top = estadoMesasForm.Height - 80,
                    Left = (estadoMesasForm.Width - 100) / 2,
                    Size = new Size(100, 30),
                    BackColor = Color.DarkSlateBlue,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat
                };
                btnCerrar.FlatAppearance.BorderSize = 0;
                btnCerrar.Click += (s, ev) => estadoMesasForm.Close();

                
                mainPanel.Controls.Add(titulo);
                mainPanel.Controls.Add(subtituloOcupadas);
                mainPanel.Controls.Add(panelOcupadas);
                mainPanel.Controls.Add(subtituloDisponibles);
                mainPanel.Controls.Add(panelDisponibles);
                mainPanel.Controls.Add(btnCerrar);

                estadoMesasForm.Controls.Add(mainPanel);

                
                estadoMesasForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el estado de las mesas: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


            
            private Panel CreateMesaPanel(int numeroMesa, Color colorEstado, string estado)
            {
                Panel panel = new Panel()
                {
                    Width = 80,
                    Height = 80,
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.WhiteSmoke
                };

                
                PictureBox circle = new PictureBox()
                {
                    Width = 20,
                    Height = 20,
                    Top = 10,
                    Left = (panel.Width - 20) / 2,
                    BackColor = colorEstado,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };

                
                Bitmap bmp = new Bitmap(circle.Width, circle.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.FillEllipse(new SolidBrush(colorEstado), 0, 0, circle.Width, circle.Height);
                }
                circle.Image = bmp;

                
                Label lblMesa = new Label()
                {
                    Text = $"Mesa {numeroMesa}",
                    Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold),
                    ForeColor = Color.Black,
                    AutoSize = true,
                    Top = 35,
                    Left = (panel.Width - 50) / 2
                };

               
                Label lblEstado = new Label()
                {
                    Text = estado,
                    Font = new System.Drawing.Font("Arial", 8),
                    ForeColor = colorEstado,
                    AutoSize = true,
                    Top = 55,
                    Left = (panel.Width - 50) / 2
                };

                panel.Controls.Add(circle);
                panel.Controls.Add(lblMesa);
                panel.Controls.Add(lblEstado);

                return panel;
            }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void numMesa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                numPersonas.Focus();
            }
        }

        private void numPersonas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtNombre.Focus();
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtApellidoPaterno.Focus();
            }
        }

        private void txtApellidoPaterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtApellidoMaterno.Focus();
            }
        }
        private void txtApellidoMaterno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                txtTelefono.Focus();
            }
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                listApartado.Focus();
            }
        }

        private void listApartado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                dtpFecha.Focus();
            }
        }

        private void dtpFecha_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
    
