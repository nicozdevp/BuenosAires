
namespace BodegaBA
{
    partial class LoginBodegaBA
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.LbuenosAires = new System.Windows.Forms.Label();
            this.Tituloinicioseccion = new System.Windows.Forms.Label();
            this.Groupboxinicioseccion = new System.Windows.Forms.GroupBox();
            this.LLolvidemicontraseña = new System.Windows.Forms.LinkLabel();
            this.Bingresar = new System.Windows.Forms.Button();
            this.Lpassword = new System.Windows.Forms.Label();
            this.Tcorreo = new System.Windows.Forms.TextBox();
            this.Tpassword = new System.Windows.Forms.TextBox();
            this.Lcorreo = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Groupboxinicioseccion.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbuenosAires
            // 
            this.LbuenosAires.AutoSize = true;
            this.LbuenosAires.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbuenosAires.Location = new System.Drawing.Point(9, 7);
            this.LbuenosAires.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LbuenosAires.Name = "LbuenosAires";
            this.LbuenosAires.Size = new System.Drawing.Size(139, 26);
            this.LbuenosAires.TabIndex = 0;
            this.LbuenosAires.Text = "Buenos aires";
            this.LbuenosAires.Click += new System.EventHandler(this.label1_Click);
            // 
            // Tituloinicioseccion
            // 
            this.Tituloinicioseccion.AutoSize = true;
            this.Tituloinicioseccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tituloinicioseccion.Location = new System.Drawing.Point(133, 79);
            this.Tituloinicioseccion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Tituloinicioseccion.Name = "Tituloinicioseccion";
            this.Tituloinicioseccion.Size = new System.Drawing.Size(311, 26);
            this.Tituloinicioseccion.TabIndex = 1;
            this.Tituloinicioseccion.Text = "Ingresar al Sistema de Bodega";
            // 
            // Groupboxinicioseccion
            // 
            this.Groupboxinicioseccion.Controls.Add(this.LLolvidemicontraseña);
            this.Groupboxinicioseccion.Controls.Add(this.Bingresar);
            this.Groupboxinicioseccion.Controls.Add(this.Lpassword);
            this.Groupboxinicioseccion.Controls.Add(this.Tcorreo);
            this.Groupboxinicioseccion.Controls.Add(this.Tpassword);
            this.Groupboxinicioseccion.Controls.Add(this.Lcorreo);
            this.Groupboxinicioseccion.Location = new System.Drawing.Point(137, 137);
            this.Groupboxinicioseccion.Margin = new System.Windows.Forms.Padding(2);
            this.Groupboxinicioseccion.Name = "Groupboxinicioseccion";
            this.Groupboxinicioseccion.Padding = new System.Windows.Forms.Padding(2);
            this.Groupboxinicioseccion.Size = new System.Drawing.Size(298, 186);
            this.Groupboxinicioseccion.TabIndex = 2;
            this.Groupboxinicioseccion.TabStop = false;
            this.Groupboxinicioseccion.Enter += new System.EventHandler(this.Groupboxinicioseccion_Enter);
            // 
            // LLolvidemicontraseña
            // 
            this.LLolvidemicontraseña.AutoSize = true;
            this.LLolvidemicontraseña.Location = new System.Drawing.Point(89, 156);
            this.LLolvidemicontraseña.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LLolvidemicontraseña.Name = "LLolvidemicontraseña";
            this.LLolvidemicontraseña.Size = new System.Drawing.Size(106, 13);
            this.LLolvidemicontraseña.TabIndex = 5;
            this.LLolvidemicontraseña.TabStop = true;
            this.LLolvidemicontraseña.Text = "Olvide mi contraseña";
            this.LLolvidemicontraseña.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LLolvidemicontraseña_LinkClicked);
            // 
            // Bingresar
            // 
            this.Bingresar.Location = new System.Drawing.Point(114, 122);
            this.Bingresar.Margin = new System.Windows.Forms.Padding(2);
            this.Bingresar.Name = "Bingresar";
            this.Bingresar.Size = new System.Drawing.Size(56, 32);
            this.Bingresar.TabIndex = 4;
            this.Bingresar.Text = "Ingresar";
            this.Bingresar.UseVisualStyleBackColor = true;
            this.Bingresar.Click += new System.EventHandler(this.Bingresar_Click);
            // 
            // Lpassword
            // 
            this.Lpassword.AutoSize = true;
            this.Lpassword.Location = new System.Drawing.Point(40, 84);
            this.Lpassword.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lpassword.Name = "Lpassword";
            this.Lpassword.Size = new System.Drawing.Size(61, 13);
            this.Lpassword.TabIndex = 1;
            this.Lpassword.Text = "Contraseña";
            // 
            // Tcorreo
            // 
            this.Tcorreo.Location = new System.Drawing.Point(144, 38);
            this.Tcorreo.Margin = new System.Windows.Forms.Padding(2);
            this.Tcorreo.Name = "Tcorreo";
            this.Tcorreo.Size = new System.Drawing.Size(76, 20);
            this.Tcorreo.TabIndex = 2;
            this.Tcorreo.TextChanged += new System.EventHandler(this.Tcorreo_TextChanged);
            // 
            // Tpassword
            // 
            this.Tpassword.Location = new System.Drawing.Point(144, 81);
            this.Tpassword.Margin = new System.Windows.Forms.Padding(2);
            this.Tpassword.Name = "Tpassword";
            this.Tpassword.Size = new System.Drawing.Size(76, 20);
            this.Tpassword.TabIndex = 3;
            this.Tpassword.UseSystemPasswordChar = true;
            this.Tpassword.TextChanged += new System.EventHandler(this.Tpassword_TextChanged);
            // 
            // Lcorreo
            // 
            this.Lcorreo.AutoSize = true;
            this.Lcorreo.Location = new System.Drawing.Point(40, 41);
            this.Lcorreo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lcorreo.Name = "Lcorreo";
            this.Lcorreo.Size = new System.Drawing.Size(38, 13);
            this.Lcorreo.TabIndex = 0;
            this.Lcorreo.Text = "Correo";
            // 
            // LoginBodegaBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.Groupboxinicioseccion);
            this.Controls.Add(this.Tituloinicioseccion);
            this.Controls.Add(this.LbuenosAires);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LoginBodegaBA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Sistema BodegaBA";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Groupboxinicioseccion.ResumeLayout(false);
            this.Groupboxinicioseccion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbuenosAires;
        private System.Windows.Forms.Label Tituloinicioseccion;
        private System.Windows.Forms.GroupBox Groupboxinicioseccion;
        private System.Windows.Forms.Label Lpassword;
        private System.Windows.Forms.Label Lcorreo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.LinkLabel LLolvidemicontraseña;
        private System.Windows.Forms.Button Bingresar;
        private System.Windows.Forms.TextBox Tpassword;
        private System.Windows.Forms.TextBox Tcorreo;
    }
}

