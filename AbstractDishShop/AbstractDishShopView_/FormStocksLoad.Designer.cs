﻿namespace AbstractDishShopView_
{
    partial class FormStocksLoad
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
            this.buttonSaveToExcel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMaterials = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveToExcel
            // 
            this.buttonSaveToExcel.Location = new System.Drawing.Point(12, 12);
            this.buttonSaveToExcel.Name = "buttonSaveToExcel";
            this.buttonSaveToExcel.Size = new System.Drawing.Size(325, 23);
            this.buttonSaveToExcel.TabIndex = 0;
            this.buttonSaveToExcel.Text = "Сохранить в Excel";
            this.buttonSaveToExcel.UseVisualStyleBackColor = true;
            this.buttonSaveToExcel.Click += new System.EventHandler(this.buttonSaveToExcel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStock,
            this.ColumnMaterials,
            this.ColumnCount});
            this.dataGridView.Location = new System.Drawing.Point(13, 42);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(324, 303);
            this.dataGridView.TabIndex = 1;
            // 
            // ColumnStock
            // 
            this.ColumnStock.HeaderText = "Склад";
            this.ColumnStock.Name = "ColumnStock";
            this.ColumnStock.ReadOnly = true;
            // 
            // ColumnMaterials
            // 
            this.ColumnMaterials.HeaderText = "Материалы";
            this.ColumnMaterials.Name = "ColumnMaterials";
            this.ColumnMaterials.ReadOnly = true;
            // 
            // ColumnCount
            // 
            this.ColumnCount.HeaderText = "Количество";
            this.ColumnCount.Name = "ColumnCount";
            this.ColumnCount.ReadOnly = true;
            // 
            // FormStocksLoad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 357);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonSaveToExcel);
            this.Name = "FormStocksLoad";
            this.Text = "Загруженность складов";
            this.Load += new System.EventHandler(this.FormStocksLoad_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveToExcel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMaterials;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCount;
    }
}