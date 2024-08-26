namespace Coupang.Controls
{
    partial class O_Items
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.M_Price = new System.Windows.Forms.Label();
            this.M_Amount = new System.Windows.Forms.Label();
            this.M_Name = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Items = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // M_Price
            // 
            this.M_Price.AutoSize = true;
            this.M_Price.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.M_Price.Location = new System.Drawing.Point(466, 0);
            this.M_Price.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.M_Price.Name = "M_Price";
            this.M_Price.Size = new System.Drawing.Size(46, 16);
            this.M_Price.TabIndex = 8;
            this.M_Price.Text = "label6";
            // 
            // M_Amount
            // 
            this.M_Amount.AutoSize = true;
            this.M_Amount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.M_Amount.Location = new System.Drawing.Point(351, 0);
            this.M_Amount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.M_Amount.Name = "M_Amount";
            this.M_Amount.Size = new System.Drawing.Size(46, 16);
            this.M_Amount.TabIndex = 7;
            this.M_Amount.Text = "label2";
            // 
            // M_Name
            // 
            this.M_Name.AutoSize = true;
            this.M_Name.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.M_Name.Location = new System.Drawing.Point(16, 0);
            this.M_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.M_Name.Name = "M_Name";
            this.M_Name.Size = new System.Drawing.Size(46, 16);
            this.M_Name.TabIndex = 6;
            this.M_Name.Text = "label8";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.M_Name);
            this.panel1.Controls.Add(this.M_Price);
            this.panel1.Controls.Add(this.M_Amount);
            this.panel1.Location = new System.Drawing.Point(0, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(525, 22);
            this.panel1.TabIndex = 9;
            // 
            // Items
            // 
            this.Items.AutoSize = true;
            this.Items.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Items.Location = new System.Drawing.Point(3, 57);
            this.Items.MinimumSize = new System.Drawing.Size(522, 25);
            this.Items.Name = "Items";
            this.Items.Size = new System.Drawing.Size(522, 25);
            this.Items.TabIndex = 10;
            // 
            // O_Items
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Items);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "O_Items";
            this.Size = new System.Drawing.Size(528, 85);
            this.Load += new System.EventHandler(this.O_Items_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label M_Price;
        internal System.Windows.Forms.Label M_Amount;
        internal System.Windows.Forms.Label M_Name;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.FlowLayoutPanel Items;
    }
}
