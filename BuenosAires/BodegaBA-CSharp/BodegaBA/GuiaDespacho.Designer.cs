
namespace BodegaBA
{
    partial class Guiadespacho
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
            this.Ltitulo = new System.Windows.Forms.Label();
            this.Lguiasdedespacho = new System.Windows.Forms.Label();
            this.Bvolveralmenuprincipal = new System.Windows.Forms.Button();
            this.Tguiasdedespacho = new System.Windows.Forms.DataGridView();
            this.btnDespachar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnImprimir = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnEntregar = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Tguiasdedespacho)).BeginInit();
            this.SuspendLayout();
            // 
            // Ltitulo
            // 
            this.Ltitulo.AutoSize = true;
            this.Ltitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ltitulo.Location = new System.Drawing.Point(9, 7);
            this.Ltitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Ltitulo.Name = "Ltitulo";
            this.Ltitulo.Size = new System.Drawing.Size(142, 26);
            this.Ltitulo.TabIndex = 0;
            this.Ltitulo.Text = "Buenos Aires";
            this.Ltitulo.Click += new System.EventHandler(this.label1_Click);
            // 
            // Lguiasdedespacho
            // 
            this.Lguiasdedespacho.AutoSize = true;
            this.Lguiasdedespacho.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lguiasdedespacho.Location = new System.Drawing.Point(195, 61);
            this.Lguiasdedespacho.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Lguiasdedespacho.Name = "Lguiasdedespacho";
            this.Lguiasdedespacho.Size = new System.Drawing.Size(203, 26);
            this.Lguiasdedespacho.TabIndex = 1;
            this.Lguiasdedespacho.Text = "Guias de Despacho";
            // 
            // Bvolveralmenuprincipal
            // 
            this.Bvolveralmenuprincipal.Location = new System.Drawing.Point(229, 332);
            this.Bvolveralmenuprincipal.Margin = new System.Windows.Forms.Padding(2);
            this.Bvolveralmenuprincipal.Name = "Bvolveralmenuprincipal";
            this.Bvolveralmenuprincipal.Size = new System.Drawing.Size(150, 24);
            this.Bvolveralmenuprincipal.TabIndex = 3;
            this.Bvolveralmenuprincipal.Text = "Volver al Menu Principal";
            this.Bvolveralmenuprincipal.UseVisualStyleBackColor = true;
            this.Bvolveralmenuprincipal.Click += new System.EventHandler(this.Bvolveralmenuprincipal_Click);
            // 
            // Tguiasdedespacho
            // 
            this.Tguiasdedespacho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tguiasdedespacho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnDespachar,
            this.btnImprimir,
            this.btnEntregar});
            this.Tguiasdedespacho.Location = new System.Drawing.Point(41, 120);
            this.Tguiasdedespacho.Name = "Tguiasdedespacho";
            this.Tguiasdedespacho.RowHeadersVisible = false;
            this.Tguiasdedespacho.Size = new System.Drawing.Size(518, 207);
            this.Tguiasdedespacho.TabIndex = 4;
            this.Tguiasdedespacho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tguiasdedespacho_CellContentClick);
            // 
            // btnDespachar
            // 
            this.btnDespachar.HeaderText = "Despachar";
            this.btnDespachar.Name = "btnDespachar";
            this.btnDespachar.Text = "Despachar";
            this.btnDespachar.UseColumnTextForButtonValue = true;
            // 
            // btnImprimir
            // 
            this.btnImprimir.HeaderText = "Imprimir";
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.UseColumnTextForButtonValue = true;
            // 
            // btnEntregar
            // 
            this.btnEntregar.HeaderText = "Entregar";
            this.btnEntregar.Name = "btnEntregar";
            this.btnEntregar.Text = "Entregar";
            this.btnEntregar.UseColumnTextForButtonValue = true;
            // 
            // Guiadespacho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.Tguiasdedespacho);
            this.Controls.Add(this.Bvolveralmenuprincipal);
            this.Controls.Add(this.Lguiasdedespacho);
            this.Controls.Add(this.Ltitulo);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Guiadespacho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sistema BodegaBA";
            this.Load += new System.EventHandler(this.Guiadespacho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tguiasdedespacho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Ltitulo;
        private System.Windows.Forms.Label Lguiasdedespacho;
        private System.Windows.Forms.Button Bvolveralmenuprincipal;
        private System.Windows.Forms.DataGridView Tguiasdedespacho;
        private System.Windows.Forms.DataGridViewButtonColumn btnDespachar;
        private System.Windows.Forms.DataGridViewButtonColumn btnImprimir;
        private System.Windows.Forms.DataGridViewButtonColumn btnEntregar;
    }
}