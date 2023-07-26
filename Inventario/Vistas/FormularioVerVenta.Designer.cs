namespace Inventario.Vistas
{
    partial class FormularioVerVenta
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
            this.lblVenta = new System.Windows.Forms.Label();
            this.dgvProductosVendidos = new System.Windows.Forms.DataGridView();
            this.btnCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosVendidos)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVenta
            // 
            this.lblVenta.AutoSize = true;
            this.lblVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVenta.Location = new System.Drawing.Point(12, 9);
            this.lblVenta.Name = "lblVenta";
            this.lblVenta.Size = new System.Drawing.Size(107, 25);
            this.lblVenta.TabIndex = 0;
            this.lblVenta.Text = "Ver Venta";
            // 
            // dgvProductosVendidos
            // 
            this.dgvProductosVendidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductosVendidos.Location = new System.Drawing.Point(12, 56);
            this.dgvProductosVendidos.Name = "dgvProductosVendidos";
            this.dgvProductosVendidos.Size = new System.Drawing.Size(925, 547);
            this.dgvProductosVendidos.TabIndex = 1;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(862, 13);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(75, 23);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // FormularioVerVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 615);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.dgvProductosVendidos);
            this.Controls.Add(this.lblVenta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormularioVerVenta";
            this.Text = "FormularioVerVenta";
            this.Load += new System.EventHandler(this.FormularioVerVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductosVendidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.DataGridView dgvProductosVendidos;
        private System.Windows.Forms.Button btnCerrar;
    }
}