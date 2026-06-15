
namespace BodegaBA
{
    partial class VentanaprincipalBA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Lbuenosaires = new System.Windows.Forms.Label();
            this.Ltituloventanaprincipal = new System.Windows.Forms.Label();
            this.Bconsultarproductos = new System.Windows.Forms.Button();
            this.Badministrardespacho = new System.Windows.Forms.Button();
            this.Breservarequipo = new System.Windows.Forms.Button();
            this.Bsalir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Lbuenosaires
            // 
            this.Lbuenosaires.AutoSize = true;
            this.Lbuenosaires.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbuenosaires.Location = new System.Drawing.Point(12, 25);
            this.Lbuenosaires.Name = "Lbuenosaires";
            this.Lbuenosaires.Size = new System.Drawing.Size(184, 32);
            this.Lbuenosaires.TabIndex = 0;
            this.Lbuenosaires.Text = "Buenos Aires";
            this.Lbuenosaires.Click += new System.EventHandler(this.label1_Click);
            // 
            // Ltituloventanaprincipal
            // 
            this.Ltituloventanaprincipal.AutoSize = true;
            this.Ltituloventanaprincipal.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ltituloventanaprincipal.Location = new System.Drawing.Point(147, 78);
            this.Ltituloventanaprincipal.Name = "Ltituloventanaprincipal";
            this.Ltituloventanaprincipal.Size = new System.Drawing.Size(482, 32);
            this.Ltituloventanaprincipal.TabIndex = 1;
            this.Ltituloventanaprincipal.Text = " Sistema de Bodega - Menu Principal";
            // 
            // Bconsultarproductos
            // 
            this.Bconsultarproductos.Location = new System.Drawing.Point(295, 130);
            this.Bconsultarproductos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bconsultarproductos.Name = "Bconsultarproductos";
            this.Bconsultarproductos.Size = new System.Drawing.Size(227, 69);
            this.Bconsultarproductos.TabIndex = 2;
            this.Bconsultarproductos.Text = "Consultar Productos en Bodega";
            this.Bconsultarproductos.UseVisualStyleBackColor = true;
            this.Bconsultarproductos.Click += new System.EventHandler(this.Bconsultarproductos_Click);
            // 
            // Badministrardespacho
            // 
            this.Badministrardespacho.Location = new System.Drawing.Point(295, 206);
            this.Badministrardespacho.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Badministrardespacho.Name = "Badministrardespacho";
            this.Badministrardespacho.Size = new System.Drawing.Size(227, 69);
            this.Badministrardespacho.TabIndex = 3;
            this.Badministrardespacho.Text = "Administrar Guias de Despacho";
            this.Badministrardespacho.UseVisualStyleBackColor = true;
            this.Badministrardespacho.Click += new System.EventHandler(this.Badministrardespacho_Click);
            // 
            // Breservarequipo
            // 
            this.Breservarequipo.Location = new System.Drawing.Point(295, 281);
            this.Breservarequipo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Breservarequipo.Name = "Breservarequipo";
            this.Breservarequipo.Size = new System.Drawing.Size(227, 69);
            this.Breservarequipo.TabIndex = 4;
            this.Breservarequipo.Text = "Reservar Equipos de ANWO";
            this.Breservarequipo.UseVisualStyleBackColor = true;
            this.Breservarequipo.Click += new System.EventHandler(this.Breservarequipo_Click);
            // 
            // Bsalir
            // 
            this.Bsalir.Location = new System.Drawing.Point(295, 356);
            this.Bsalir.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bsalir.Name = "Bsalir";
            this.Bsalir.Size = new System.Drawing.Size(227, 69);
            this.Bsalir.TabIndex = 5;
            this.Bsalir.Text = "Salir";
            this.Bsalir.UseVisualStyleBackColor = true;
            this.Bsalir.Click += new System.EventHandler(this.Bsalir_Click);
            // 
            // VentanaprincipalBA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 466);
            this.Controls.Add(this.Bsalir);
            this.Controls.Add(this.Breservarequipo);
            this.Controls.Add(this.Badministrardespacho);
            this.Controls.Add(this.Bconsultarproductos);
            this.Controls.Add(this.Ltituloventanaprincipal);
            this.Controls.Add(this.Lbuenosaires);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "VentanaprincipalBA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema BodegaBA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbuenosaires;
        private System.Windows.Forms.Label Ltituloventanaprincipal;
        private System.Windows.Forms.Button Bconsultarproductos;
        private System.Windows.Forms.Button Badministrardespacho;
        private System.Windows.Forms.Button Breservarequipo;
        private System.Windows.Forms.Button Bsalir;
    }
}