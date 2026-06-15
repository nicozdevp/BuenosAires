using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuenosAires.Model.Utiles;
using BuenosAires.ServiceProxy;

namespace BodegaBA
{
    public partial class Reservaequipos : Form
    {
        public Reservaequipos()
        {
            InitializeComponent();
            //this.Text = $"Mantenedor de productos - Usuario: {correo}";
            //btnbuscar.click += (sender, e) => buscar();
            //btnnuevo.click += (sender, e) => nuevo();
            //btnguardar.click += (sender, e) => guardar();
            //btneliminar.click += (sender, e) => eliminar();
            //btncargarproductos.click += (sender, e) => cargarproductos();
            //Tablaproductosbodega1.SelectionChanged += (sender, e) => Seleccionar();
            Tablaequipoanwo.ConfigurarDataGridView(
                "nroserieanwo:Numero de Serie, "
                + "nomprodanwo:nombre, "
                + "precioanwo:Precio,"
                + "reservado:Reservado"
            );
            cargarproductos();
            this.CentrarVentana();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VentanaprincipalBA ventana = new VentanaprincipalBA();
            ventana.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegura que no se haga clic en el encabezado o fuera de rango
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (Tablaequipoanwo.Columns[e.ColumnIndex].Name == "ReservarLnk")
            {
                // Obtiene el número de serie del producto de la fila seleccionada
                string nroserie = Tablaequipoanwo.Rows[e.RowIndex].Cells["nroserieanwo"].Value.ToString();
                string reservado = Tablaequipoanwo.Rows[e.RowIndex].Cells["reservado"].Value.ToString().Trim().ToUpper();

                if (reservado == "S")
                {
                    MessageBox.Show("Este producto ya está reservado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (reservado == "N")
                {
                    try
                    {
                        var ws = new BuenosAires.ServiceLayer.WsAnwo();
                        var resp = ws.ReservarProducto(nroserie);

                        if (resp.HayErrores)
                        {
                            MessageBox.Show("Error: " + resp.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("Producto reservado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cargarproductos(); // Refresca tabla
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El estado de reserva del producto es desconocido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        public void cargarproductos()
        {
            var bc = new ScAnwo();
            bc.ObtenerAnwoListaProducto();
            Tablaequipoanwo.DataSource = bc.ListaAnwoStockProducto;
            Tablaequipoanwo.RefrescarYajustar();
            if (bc.HayErrores == true) this.MensajeInfo(bc.Mensaje);

            Tablaequipoanwo.Columns["ReservarLnk"].DisplayIndex = Tablaequipoanwo.Columns.Count - 1;
        }


        private void ReservaEquipos_Load(object sender, EventArgs e)
        {

        }
    }
}
