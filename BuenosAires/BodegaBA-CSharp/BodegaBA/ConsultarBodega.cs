using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuenosAires.Model;
using BuenosAires.Model.Utiles;
using BuenosAires.ServiceProxy;

namespace BodegaBA
{
    public partial class ConsultarBodega : Form
    {
        public ConsultarBodega(string correo)
        {
            InitializeComponent();
            //this.Text = $"Mantenedor de productos - Usuario: {correo}";
            //btnbuscar.click += (sender, e) => buscar();
            //btnnuevo.click += (sender, e) => nuevo();
            //btnguardar.click += (sender, e) => guardar();
            //btneliminar.click += (sender, e) => eliminar();
            //btncargarproductos.click += (sender, e) => cargarproductos();
            Tablaproductosbodega1.SelectionChanged += (sender, e) => Seleccionar();
            Tablaproductosbodega1.ConfigurarDataGridView("idprod:ID,nomprod:Nombre del producto,descprod:Descripción,precio:Precio,imagen:Imagen,cantidad:Cantidad,disponibilidad:Disponibilidad");
            cargarproductos();
            //this.CentrarVentana();


        }

        //private void Nuevo()
        //{
        //    this.Limpiar(new TextBox[]
        //    {
        //        txtIdProd,
        //        txtNomProd,
        //        txtDescProd,
        //        txtPrecio,
        //        txtImagen
        //    });
        //}

        //public bool ValidarCamposNumericos()
        //{
        //    if (txtIdProd.Text != "" && !txtIDProd.EsNumero())
        //    {
        //        return this.ErrEntero("ID");
        //    }
        //    if (!txtIDProd.EsNumero())
        //    {
        //        return this.ErrEntero("Precio");
        //    }
        //    return true;
        //}

        private void Seleccionar()
        {
            if (Tablaproductosbodega1.SelectedRows.Count <= 0) return;

            DataGridViewRow row = Tablaproductosbodega1.SelectedRows[0];
            if (row.GetString("idprod") != "0")
            {
                this.AsignarValoresTextBox(row);
            }
        }


        //private bool Buscar()
        //{
        //    int id = new VentanaBuscarID().MostrarVentanaModal();
        //    if (id == -1) return false;

        //    var bc = new BcProducto();
        //    bc.Leer(id);

        //    if (bc.Producto == null) return this.MensajeInfo(bc.Mensaje);

        //    CargarProductos();
        //    this.AsignarValoresTextBox(bc.Producto);

        //    grid.SeleccionarId("idprod", txtIdProd.ToInt());
        //    txtNomprod.FocusToEnd();
        //    return true;
        //}


        //private void Guardar()
        //{

        //    if (!ValidarCamposNumericos()) return;

        //    var prod = new Producto();
        //    prod.idprod = txtIdProd.ToIntOrDefault();
        //    prod.nomprod = txtNomProd.Text;
        //    prod.descprod = txtDescProd.Text;
        //    prod.precio = txtPrecio.ToInt();
        //    prod.imagen = txtImagen.Text;

        //    var bc = new BcProducto();

        //    if (txtIdProd.Text.Trim() == "")
        //    {
        //        bc.Crear(prod);
        //    }
        //    else
        //    {
        //        bc.Actualizar(prod);
        //    }

        //    if (!bc.HayErrores)
        //    {
        //        txtIdProd.SetText(bc.Producto.idProd);
        //        CargarProductos();
        //        grid.SelecionarId("idprod", bc.Producto.idprod);
        //        txtNomProd.FocusToEnd();
        //    }

        //    this.MensajeInfo(bc.Mensaje);
        //}

        //private bool Eliminar()
        //{
        //    var bc = new BcProducto();
        //    if (txtIdProd.Text.Trim() == "")
        //    {
        //        return this.ErrAccionID("ID", "eliminar");
        //    }
        //    bc.Eliminar(txtIdProd.ToInt());
        //    CargarProductos();
        //    this.MensajeInfo(bc.Mensaje);
        //    return true;
        //}

        public void cargarproductos()
        {
            var bc = new ScProducto();
            bc.ObtenerEquiposEnBodega();
            Tablaproductosbodega1.DataSource = bc.ListaStockProducto;
            Tablaproductosbodega1.RefrescarYajustar();
            if (bc.HayErrores == true) this.MensajeInfo(bc.Mensaje);
        }

        private void ConsultarBodega_Load(object sender, EventArgs e)
        {
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void Bvolveralmenu_Click(object sender, EventArgs e)
        {
            VentanaprincipalBA ventana = new VentanaprincipalBA();
            ventana.Show();
            this.Hide();
        }

       
    }
}
