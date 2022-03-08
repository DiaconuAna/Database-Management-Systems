namespace Lab1take2
{
    partial class Form1
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
            this.TypeGrid = new System.Windows.Forms.DataGridView();
            this.AntiquityGrid = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TypeGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AntiquityGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // TypeGrid
            // 
            this.TypeGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TypeGrid.Location = new System.Drawing.Point(42, 45);
            this.TypeGrid.Name = "TypeGrid";
            this.TypeGrid.RowHeadersWidth = 51;
            this.TypeGrid.RowTemplate.Height = 24;
            this.TypeGrid.Size = new System.Drawing.Size(356, 283);
            this.TypeGrid.TabIndex = 0;
            // 
            // AntiquityGrid
            // 
            this.AntiquityGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AntiquityGrid.Location = new System.Drawing.Point(503, 45);
            this.AntiquityGrid.Name = "AntiquityGrid";
            this.AntiquityGrid.RowHeadersWidth = 51;
            this.AntiquityGrid.RowTemplate.Height = 24;
            this.AntiquityGrid.Size = new System.Drawing.Size(812, 283);
            this.AntiquityGrid.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(503, 353);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 67);
            this.button1.TabIndex = 2;
            this.button1.Text = "Update";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(114, 349);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(284, 71);
            this.button2.TabIndex = 3;
            this.button2.Text = "Connect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.AntiquityGrid);
            this.Controls.Add(this.TypeGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.TypeGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AntiquityGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView TypeGrid;
        private System.Windows.Forms.DataGridView AntiquityGrid;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

