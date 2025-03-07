namespace CostosCompras
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Folio = new TextBox();
            Obtener = new Button();
            Tabla = new DataGridView();
            Column8 = new DataGridViewCheckBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            CbFiltro = new ComboBox();
            Correo = new Button();
            label1 = new Label();
            Update = new Button();
            ((System.ComponentModel.ISupportInitialize)Tabla).BeginInit();
            SuspendLayout();
            // 
            // Folio
            // 
            Folio.Location = new Point(622, 21);
            Folio.Name = "Folio";
            Folio.PlaceholderText = "Folio Compra";
            Folio.Size = new Size(182, 23);
            Folio.TabIndex = 0;
            Folio.KeyDown += Clave_Principal_KeyDown;
            // 
            // Obtener
            // 
            Obtener.BackColor = Color.Indigo;
            Obtener.Cursor = Cursors.Hand;
            Obtener.Font = new Font("Times New Roman", 11.25F);
            Obtener.ForeColor = Color.White;
            Obtener.Location = new Point(810, 12);
            Obtener.Name = "Obtener";
            Obtener.Size = new Size(128, 39);
            Obtener.TabIndex = 1;
            Obtener.Text = "Agregar Compra";
            Obtener.UseVisualStyleBackColor = false;
            Obtener.Click += Obtener_Click;
            // 
            // Tabla
            // 
            Tabla.AllowUserToAddRows = false;
            Tabla.AllowUserToDeleteRows = false;
            Tabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Tabla.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Tabla.Columns.AddRange(new DataGridViewColumn[] { Column8, Column1, Column2, Column3, Column9, Column4, Column5, Column6, Column7 });
            Tabla.Cursor = Cursors.Hand;
            Tabla.Location = new Point(12, 74);
            Tabla.Name = "Tabla";
            Tabla.RowHeadersVisible = false;
            Tabla.Size = new Size(926, 514);
            Tabla.TabIndex = 2;
            Tabla.CellEndEdit += Tabla_CellEndEdit;
            // 
            // Column8
            // 
            Column8.FlatStyle = FlatStyle.Flat;
            Column8.HeaderText = "";
            Column8.Name = "Column8";
            Column8.Width = 30;
            // 
            // Column1
            // 
            Column1.HeaderText = "Id";
            Column1.Name = "Column1";
            Column1.Width = 30;
            // 
            // Column2
            // 
            Column2.HeaderText = "Codigo";
            Column2.Name = "Column2";
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Column3.HeaderText = "Nombre";
            Column3.MinimumWidth = 100;
            Column3.Name = "Column3";
            // 
            // Column9
            // 
            Column9.HeaderText = "Costo Penúltima Compra";
            Column9.Name = "Column9";
            // 
            // Column4
            // 
            Column4.HeaderText = "Costo Actual";
            Column4.Name = "Column4";
            // 
            // Column5
            // 
            Column5.HeaderText = "Margen";
            Column5.Name = "Column5";
            // 
            // Column6
            // 
            Column6.HeaderText = "Precio Lista Anterior";
            Column6.Name = "Column6";
            // 
            // Column7
            // 
            Column7.HeaderText = "Precio Lista Actualizado";
            Column7.Name = "Column7";
            // 
            // CbFiltro
            // 
            CbFiltro.Cursor = Cursors.Hand;
            CbFiltro.FlatStyle = FlatStyle.Flat;
            CbFiltro.Font = new Font("Segoe UI", 12F);
            CbFiltro.FormattingEnabled = true;
            CbFiltro.Items.AddRange(new object[] { "Seleccionar:", "Aumentaron Precio", "Bajaron Precio", "Todos", "Ninguno" });
            CbFiltro.Location = new Point(12, 28);
            CbFiltro.Name = "CbFiltro";
            CbFiltro.Size = new Size(161, 29);
            CbFiltro.TabIndex = 3;
            CbFiltro.Visible = false;
            CbFiltro.SelectedIndexChanged += CbFiltro_SelectedIndexChanged;
            // 
            // Correo
            // 
            Correo.BackColor = Color.Indigo;
            Correo.Cursor = Cursors.Hand;
            Correo.Font = new Font("Times New Roman", 11.25F);
            Correo.ForeColor = Color.White;
            Correo.Location = new Point(362, 12);
            Correo.Name = "Correo";
            Correo.Size = new Size(114, 38);
            Correo.TabIndex = 4;
            Correo.Text = "Enviar Correo";
            Correo.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top;
            label1.AutoSize = true;
            label1.Location = new Point(602, 56);
            label1.Name = "label1";
            label1.Size = new Size(166, 15);
            label1.TabIndex = 5;
            label1.Text = "Margen= (PL- Costo) / PL*100";
            // 
            // Update
            // 
            Update.BackColor = Color.MediumSlateBlue;
            Update.Cursor = Cursors.Hand;
            Update.Font = new Font("Times New Roman", 11.25F);
            Update.ForeColor = Color.White;
            Update.Location = new Point(202, 12);
            Update.Name = "Update";
            Update.Size = new Size(114, 50);
            Update.TabIndex = 6;
            Update.Text = "Actualizar Precios";
            Update.UseVisualStyleBackColor = false;
            Update.Click += Update_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 600);
            Controls.Add(Update);
            Controls.Add(label1);
            Controls.Add(Correo);
            Controls.Add(CbFiltro);
            Controls.Add(Tabla);
            Controls.Add(Obtener);
            Controls.Add(Folio);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Developed By AtziNotFound";
            ((System.ComponentModel.ISupportInitialize)Tabla).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox Folio;
        private Button Obtener;
        private DataGridView Tabla;
        private ComboBox CbFiltro;
        private Button Correo;
        private Label label1;
        private Button Update;
        private DataGridViewCheckBoxColumn Column8;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column9;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
    }
}
