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
    public partial class Guiadespacho : Form
    {
        public Guiadespacho()
        {
            InitializeComponent();
            //Tguiasdedespacho.SelectionChanged += (sender, e) => Seleccionar();
            Tguiasdedespacho.ConfigurarDataGridView("nrogd:Número GD,nomprod:Producto,estadogd:Estado GD,nrofac:Número Factura,Cliente:Cliente");

            cargarGuiasDespacho();
        }

        public void cargarGuiasDespacho()
        {
            Tguiasdedespacho.Columns["btnDespachar"].DisplayIndex = Tguiasdedespacho.Columns.Count - 1;
            Tguiasdedespacho.Columns["btnImprimir"].DisplayIndex = Tguiasdedespacho.Columns.Count - 1;
            Tguiasdedespacho.Columns["btnEntregar"].DisplayIndex = Tguiasdedespacho.Columns.Count - 1;
            var bc = new ScGuiaConsulta();
            bc.ConsultarGuiasDespacho();
            Tguiasdedespacho.DataSource = bc.ListaGuiasDespacho;
            Tguiasdedespacho.RefrescarYajustar();
            if (bc.HayErrores == true) this.MensajeInfo(bc.Mensaje);
            if (Tguiasdedespacho.Columns["btnDespachar"] is DataGridViewButtonColumn btn1)
                btn1.UseColumnTextForButtonValue = true;
            if (Tguiasdedespacho.Columns["btnImprimir"] is DataGridViewButtonColumn btn2)
                btn2.UseColumnTextForButtonValue = true;
            if (Tguiasdedespacho.Columns["btnEntregar"] is DataGridViewButtonColumn btn3)
                btn3.UseColumnTextForButtonValue = true;

        }


        private void Guiadespacho_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Bvolveralmenuprincipal_Click(object sender, EventArgs e)
        {
            VentanaprincipalBA ventana = new VentanaprincipalBA();
            ventana.Show();
            this.Hide();
        }

        private void Tguiasdedespacho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string col = Tguiasdedespacho.Columns[e.ColumnIndex].Name;
            string nroGuia = Tguiasdedespacho.Rows[e.RowIndex].Cells["nrogd"].Value.ToString();

            var ws = new WsConsultaDespachoReference.WsGuiaDespachoClient();

            if (col == "btnDespachar")
            {
                try
                {
                    var resp = ws.ActualizarEstadoGuia(nroGuia, "Despachado");

                    if (resp.HayErrores)
                        MessageBox.Show("Error: " + resp.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Guía actualizada a 'Despachado'.", "Despacho", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cargarGuiasDespacho();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al despachar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (col == "btnImprimir")
            {
                MessageBox.Show($"🖨 Imprimiendo guía #{nroGuia}", "Imprimir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (col == "btnEntregar")
            {
                try
                {
                    var resp = ws.ActualizarEstadoGuia(nroGuia, "Entregado");

                    if (resp.HayErrores)
                        MessageBox.Show("Error: " + resp.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Guía actualizada a 'Entregado'.", "Entrega", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    cargarGuiasDespacho();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al entregar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


    }
}
