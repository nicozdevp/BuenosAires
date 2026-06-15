
namespace BodegaBA
{
    partial class Reservaequipos
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Tablaequipoanwo = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.ReservarLnk = new System.Windows.Forms.DataGridViewLinkColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Tablaequipoanwo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buenos Aires";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(219, 71);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(358, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reservar Equipos de ANWO";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(215, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(369, 29);
            this.label3.TabIndex = 2;
            this.label3.Text = "Productos Disponibles en ANWO";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Tablaequipoanwo
            // 
            this.Tablaequipoanwo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Tablaequipoanwo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablaequipoanwo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ReservarLnk});
            this.Tablaequipoanwo.Location = new System.Drawing.Point(16, 178);
            this.Tablaequipoanwo.Margin = new System.Windows.Forms.Padding(4);
            this.Tablaequipoanwo.Name = "Tablaequipoanwo";
            this.Tablaequipoanwo.ReadOnly = true;
            this.Tablaequipoanwo.RowHeadersVisible = false;
            this.Tablaequipoanwo.RowHeadersWidth = 51;
            this.Tablaequipoanwo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tablaequipoanwo.Size = new System.Drawing.Size(765, 218);
            this.Tablaequipoanwo.TabIndex = 3;
            this.Tablaequipoanwo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(305, 404);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Volver al menu principal";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReservarLnk
            // 
            this.ReservarLnk.HeaderText = "Opciones";
            this.ReservarLnk.MinimumWidth = 6;
            this.ReservarLnk.Name = "ReservarLnk";
            this.ReservarLnk.ReadOnly = true;
            this.ReservarLnk.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReservarLnk.Text = "Reservar";
            this.ReservarLnk.TrackVisitedState = false;
            this.ReservarLnk.UseColumnTextForLinkValue = true;
            // 
            // Reservaequipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Tablaequipoanwo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Reservaequipos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema BodegaBA";
            this.Load += new System.EventHandler(this.ReservaEquipos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tablaequipoanwo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView Tablaequipoanwo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewLinkColumn ReservarLnk;
    }
}