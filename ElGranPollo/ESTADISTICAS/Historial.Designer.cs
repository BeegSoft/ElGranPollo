namespace ElGranPollo
{
    partial class Historial
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series16 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series17 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series18 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button3 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.listView_fechas = new System.Windows.Forms.ListView();
            this.column_fechas = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_venta_platillo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_platillos_vendidos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox_platillos = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.a = new System.Windows.Forms.Label();
            this.fechaB = new System.Windows.Forms.DateTimePicker();
            this.fechaA = new System.Windows.Forms.DateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_ganancias = new System.Windows.Forms.TextBox();
            this.Ganancias = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_gastos = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox_empanadas = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_clientes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ventas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exportarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gastosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almacenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Image = global::ElGranPollo.Properties.Resources.mostrar;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.Location = new System.Drawing.Point(336, 76);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 57);
            this.button3.TabIndex = 89;
            this.button3.Text = "Mostrar";
            this.button3.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 35);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(466, 22);
            this.label13.TabIndex = 88;
            this.label13.Text = "Historial de las Ventas Realizadas Por Fecha Establecida";
            // 
            // listView_fechas
            // 
            this.listView_fechas.BackColor = System.Drawing.Color.White;
            this.listView_fechas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_fechas});
            this.listView_fechas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_fechas.ForeColor = System.Drawing.SystemColors.WindowText;
            this.listView_fechas.Location = new System.Drawing.Point(23, 155);
            this.listView_fechas.Name = "listView_fechas";
            this.listView_fechas.Size = new System.Drawing.Size(124, 178);
            this.listView_fechas.TabIndex = 86;
            this.listView_fechas.UseCompatibleStateImageBehavior = false;
            this.listView_fechas.View = System.Windows.Forms.View.Details;
            // 
            // column_fechas
            // 
            this.column_fechas.Text = "Fechas Laboradas";
            this.column_fechas.Width = 120;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(525, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "Grafica de las ventas";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.textBox_venta_platillo);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox_platillos_vendidos);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.comboBox_platillos);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(40, 347);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(340, 129);
            this.groupBox3.TabIndex = 84;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Articulos vendidos";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(181, 91);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "$";
            // 
            // textBox_venta_platillo
            // 
            this.textBox_venta_platillo.Enabled = false;
            this.textBox_venta_platillo.Location = new System.Drawing.Point(194, 88);
            this.textBox_venta_platillo.Name = "textBox_venta_platillo";
            this.textBox_venta_platillo.Size = new System.Drawing.Size(62, 20);
            this.textBox_venta_platillo.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(195, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Ganancia";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(195, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Cantidad";
            // 
            // textBox_platillos_vendidos
            // 
            this.textBox_platillos_vendidos.Enabled = false;
            this.textBox_platillos_vendidos.Location = new System.Drawing.Point(194, 36);
            this.textBox_platillos_vendidos.Name = "textBox_platillos_vendidos";
            this.textBox_platillos_vendidos.Size = new System.Drawing.Size(62, 20);
            this.textBox_platillos_vendidos.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Articulo";
            // 
            // comboBox_platillos
            // 
            this.comboBox_platillos.FormattingEnabled = true;
            this.comboBox_platillos.Location = new System.Drawing.Point(8, 34);
            this.comboBox_platillos.Name = "comboBox_platillos";
            this.comboBox_platillos.Size = new System.Drawing.Size(121, 21);
            this.comboBox_platillos.TabIndex = 12;
            this.comboBox_platillos.SelectedIndexChanged += new System.EventHandler(this.comboBox_platillos_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 83;
            this.label6.Text = "Desde";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(279, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "Escoga entre que fechas quiere las estadisticas";
            // 
            // a
            // 
            this.a.AutoSize = true;
            this.a.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.a.Location = new System.Drawing.Point(174, 114);
            this.a.Name = "a";
            this.a.Size = new System.Drawing.Size(40, 13);
            this.a.TabIndex = 81;
            this.a.Text = "Hasta";
            // 
            // fechaB
            // 
            this.fechaB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fechaB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaB.Location = new System.Drawing.Point(217, 110);
            this.fechaB.Name = "fechaB";
            this.fechaB.Size = new System.Drawing.Size(102, 20);
            this.fechaB.TabIndex = 80;
            this.fechaB.Value = new System.DateTime(2016, 4, 18, 0, 0, 0, 0);
            // 
            // fechaA
            // 
            this.fechaA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fechaA.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.fechaA.Location = new System.Drawing.Point(64, 110);
            this.fechaA.Name = "fechaA";
            this.fechaA.Size = new System.Drawing.Size(102, 20);
            this.fechaA.TabIndex = 79;
            this.fechaA.Value = new System.DateTime(2016, 4, 10, 0, 0, 0, 0);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.textBox_ganancias);
            this.groupBox2.Controls.Add(this.Ganancias);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.textBox_gastos);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.textBox_empanadas);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.textBox_clientes);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_ventas);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(177, 155);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 178);
            this.groupBox2.TabIndex = 78;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resumen Total del Periodo Seleccionado";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(124, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(14, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "$";
            // 
            // textBox_ganancias
            // 
            this.textBox_ganancias.Enabled = false;
            this.textBox_ganancias.Location = new System.Drawing.Point(137, 82);
            this.textBox_ganancias.Name = "textBox_ganancias";
            this.textBox_ganancias.Size = new System.Drawing.Size(51, 20);
            this.textBox_ganancias.TabIndex = 16;
            // 
            // Ganancias
            // 
            this.Ganancias.AutoSize = true;
            this.Ganancias.Location = new System.Drawing.Point(66, 85);
            this.Ganancias.Name = "Ganancias";
            this.Ganancias.Size = new System.Drawing.Size(71, 13);
            this.Ganancias.TabIndex = 15;
            this.Ganancias.Text = "Ganancias:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(124, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "$";
            // 
            // textBox_gastos
            // 
            this.textBox_gastos.Enabled = false;
            this.textBox_gastos.Location = new System.Drawing.Point(137, 56);
            this.textBox_gastos.Name = "textBox_gastos";
            this.textBox_gastos.Size = new System.Drawing.Size(51, 20);
            this.textBox_gastos.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(84, 59);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 13);
            this.label16.TabIndex = 12;
            this.label16.Text = "Gastos:";
            // 
            // textBox_empanadas
            // 
            this.textBox_empanadas.Enabled = false;
            this.textBox_empanadas.Location = new System.Drawing.Point(137, 135);
            this.textBox_empanadas.Name = "textBox_empanadas";
            this.textBox_empanadas.Size = new System.Drawing.Size(51, 20);
            this.textBox_empanadas.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(35, 138);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(111, 13);
            this.label14.TabIndex = 10;
            this.label14.Text = "Total de Articulos:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(123, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "$";
            // 
            // textBox_clientes
            // 
            this.textBox_clientes.Enabled = false;
            this.textBox_clientes.Location = new System.Drawing.Point(137, 111);
            this.textBox_clientes.Name = "textBox_clientes";
            this.textBox_clientes.Size = new System.Drawing.Size(51, 20);
            this.textBox_clientes.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Total Clientes:";
            // 
            // textBox_ventas
            // 
            this.textBox_ventas.Enabled = false;
            this.textBox_ventas.Location = new System.Drawing.Point(137, 28);
            this.textBox_ventas.Name = "textBox_ventas";
            this.textBox_ventas.Size = new System.Drawing.Size(51, 20);
            this.textBox_ventas.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ventas:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(251, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(466, 22);
            this.label1.TabIndex = 77;
            this.label1.Text = "Historial de las Ventas Realizadas Por Fecha Establecida";
            this.label1.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Plum;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarToolStripMenuItem,
            this.gastosToolStripMenuItem,
            this.almacenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1088, 24);
            this.menuStrip1.TabIndex = 87;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exportarToolStripMenuItem
            // 
            this.exportarToolStripMenuItem.Name = "exportarToolStripMenuItem";
            this.exportarToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.exportarToolStripMenuItem.Text = "Exportar";
            // 
            // gastosToolStripMenuItem
            // 
            this.gastosToolStripMenuItem.Name = "gastosToolStripMenuItem";
            this.gastosToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.gastosToolStripMenuItem.Text = "Gastos";
            this.gastosToolStripMenuItem.Click += new System.EventHandler(this.gastosToolStripMenuItem_Click);
            // 
            // almacenToolStripMenuItem
            // 
            this.almacenToolStripMenuItem.Name = "almacenToolStripMenuItem";
            this.almacenToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.almacenToolStripMenuItem.Text = "Almacen";
            // 
            // chart1
            // 
            chartArea6.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chart1.Legends.Add(legend6);
            this.chart1.Location = new System.Drawing.Point(482, 91);
            this.chart1.Name = "chart1";
            series16.ChartArea = "ChartArea1";
            series16.Legend = "Legend1";
            series16.Name = "Ventas";
            series17.ChartArea = "ChartArea1";
            series17.Legend = "Legend1";
            series17.Name = "Gastos";
            series18.ChartArea = "ChartArea1";
            series18.Legend = "Legend1";
            series18.Name = "Ganancias";
            this.chart1.Series.Add(series16);
            this.chart1.Series.Add(series17);
            this.chart1.Series.Add(series18);
            this.chart1.Size = new System.Drawing.Size(594, 385);
            this.chart1.TabIndex = 90;
            this.chart1.Text = "chart1";
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(1088, 482);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.listView_fechas);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.a);
            this.Controls.Add(this.fechaB);
            this.Controls.Add(this.fechaA);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Historial";
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.Historial_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ListView listView_fechas;
        private System.Windows.Forms.ColumnHeader column_fechas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_venta_platillo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_platillos_vendidos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox_platillos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label a;
        private System.Windows.Forms.DateTimePicker fechaB;
        private System.Windows.Forms.DateTimePicker fechaA;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_ganancias;
        private System.Windows.Forms.Label Ganancias;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_gastos;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox_empanadas;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_clientes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_ventas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exportarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gastosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almacenToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}