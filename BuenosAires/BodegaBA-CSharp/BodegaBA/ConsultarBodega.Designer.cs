
namespace BodegaBA
{
    partial class ConsultarBodega
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
            this.Lproductosdisponibles = new System.Windows.Forms.Label();
            this.Bvolveralmenu = new System.Windows.Forms.Button();
            this.Tablaproductosbodega1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Tablaproductosbodega1)).BeginInit();
            this.SuspendLayout();
            // 
            // Lbuenosaires
            // 
            this.Lbuenosaires.AutoSize = true;
            this.Lbuenosaires.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lbuenosaires.Location = new System.Drawing.Point(-1, 7);
            this.Lbuenosaires.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lbuenosaires.Name = "Lbuenosaires";
            this.Lbuenosaires.Size = new System.Drawing.Size(142, 26);
            this.Lbuenosaires.TabIndex = 0;
            this.Lbuenosaires.Text = "Buenos Aires";
            // 
            // Lproductosdisponibles
            // 
            this.Lproductosdisponibles.AutoSize = true;
            this.Lproductosdisponibles.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lproductosdisponibles.Location = new System.Drawing.Point(126, 33);
            this.Lproductosdisponibles.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lproductosdisponibles.Name = "Lproductosdisponibles";
            this.Lproductosdisponibles.Size = new System.Drawing.Size(340, 26);
            this.Lproductosdisponibles.TabIndex = 1;
            this.Lproductosdisponibles.Text = "Productos Disponibles en Bodega";
            // 
            // Bvolveralmenu
            // 
            this.Bvolveralmenu.Location = new System.Drawing.Point(214, 321);
            this.Bvolveralmenu.Margin = new System.Windows.Forms.Padding(2);
            this.Bvolveralmenu.Name = "Bvolveralmenu";
            this.Bvolveralmenu.Size = new System.Drawing.Size(168, 28);
            this.Bvolveralmenu.TabIndex = 3;
            this.Bvolveralmenu.Text = "Volver al Menu Principal";
            this.Bvolveralmenu.UseVisualStyleBackColor = true;
            this.Bvolveralmenu.Click += new System.EventHandler(this.Bvolveralmenu_Click);
            // 
            // Tablaproductosbodega1
            // 
            this.Tablaproductosbodega1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablaproductosbodega1.Location = new System.Drawing.Point(12, 82);
            this.Tablaproductosbodega1.Name = "Tablaproductosbodega1";
            this.Tablaproductosbodega1.Size = new System.Drawing.Size(576, 234);
            this.Tablaproductosbodega1.TabIndex = 4;
            // 
            // ConsultarBodega
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.Tablaproductosbodega1);
            this.Controls.Add(this.Bvolveralmenu);
            this.Controls.Add(this.Lproductosdisponibles);
            this.Controls.Add(this.Lbuenosaires);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ConsultarBodega";
            this.Text = " Sistema BodegaBA";
            this.Load += new System.EventHandler(this.ConsultarBodega_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tablaproductosbodega1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Lbuenosaires;
        private System.Windows.Forms.Label Lproductosdisponibles;
        private System.Windows.Forms.Button Bvolveralmenu;
        private System.Windows.Forms.DataGridView Tablaproductosbodega1;
    }
}