using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BodegaBA
{
    public partial class VentanaprincipalBA : Form
    {
        public VentanaprincipalBA()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Bconsultarproductos_Click(object sender, EventArgs e)
        {
            ConsultarBodega ventana = new ConsultarBodega(""); //
            ventana.Show();   // 
            this.Hide();
        }

        private void Badministrardespacho_Click(object sender, EventArgs e)
        {
            Guiadespacho Ventana = new Guiadespacho();
            Ventana.Show();
            this.Hide();
        }

        private void Breservarequipo_Click(object sender, EventArgs e)
        {
            Reservaequipos Ventana = new Reservaequipos();
            Ventana.Show();
            this.Hide();
        }

        private void Bsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
