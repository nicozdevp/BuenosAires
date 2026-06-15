using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BodegaBA;
using BuenosAires.Model.Utiles;
using BuenosAires.ServiceProxy;


namespace BodegaBA
{
    public partial class LoginBodegaBA : Form
    {
        public LoginBodegaBA()
        {
            InitializeComponent();
            this.AcceptButton = Bingresar;
            this.StartPosition = FormStartPosition.CenterScreen;
            Tcorreo.Text = "creyes";
            Tpassword.Text = "123";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Groupboxinicioseccion_Enter(object sender, EventArgs e)
        {

        }

        private void Bingresar_Click(object sender, EventArgs e)
        {
            var sc = new ScAutenticacion();
            sc.Autenticar("Bodeguero", Tcorreo.Text, Tpassword.Text);
            if (sc.Autenticado)
            {
                new VentanaprincipalBA().Show();
                Hide();
            }
            else
            {
                this.MensajeInfo(sc.Mensaje);
            }
        }

        private void Tcorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void LLolvidemicontraseña_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
