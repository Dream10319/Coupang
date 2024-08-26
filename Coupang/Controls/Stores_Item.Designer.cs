namespace Coupang.Restaurants_Controls
{
    partial class Stores_Item
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
            this.components = new System.ComponentModel.Container();
            this.R_Name = new System.Windows.Forms.Label();
            this.R_Status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Status_Buttons = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.displayItemDTO = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Status)).BeginInit();
            this.SuspendLayout();
            // 
            // R_Name
            // 
            this.R_Name.AutoSize = true;
            this.R_Name.Location = new System.Drawing.Point(50, 9);
            this.R_Name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.R_Name.Name = "R_Name";
            this.R_Name.Size = new System.Drawing.Size(55, 18);
            this.R_Name.TabIndex = 0;
            this.R_Name.Text = "label1";
            // 
            // R_Status
            // 
            this.R_Status.AutoSize = true;
            this.R_Status.Location = new System.Drawing.Point(64, 31);
            this.R_Status.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.R_Status.Name = "R_Status";
            this.R_Status.Size = new System.Drawing.Size(23, 18);
            this.R_Status.TabIndex = 1;
            this.R_Status.Text = "...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "상태변경 해주세요";
            // 
            // Status_Buttons
            // 
            this.Status_Buttons.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Status_Buttons.AutoSize = true;
            this.Status_Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Status_Buttons.Location = new System.Drawing.Point(5, 23);
            this.Status_Buttons.MinimumSize = new System.Drawing.Size(217, 35);
            this.Status_Buttons.Name = "Status_Buttons";
            this.Status_Buttons.Size = new System.Drawing.Size(217, 35);
            this.Status_Buttons.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Status_Buttons);
            this.panel1.Location = new System.Drawing.Point(0, 82);
            this.panel1.MinimumSize = new System.Drawing.Size(210, 61);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 61);
            this.panel1.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // displayItemDTO
            // 
            this.displayItemDTO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayItemDTO.Location = new System.Drawing.Point(5, 51);
            this.displayItemDTO.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.displayItemDTO.Name = "displayItemDTO";
            this.displayItemDTO.Size = new System.Drawing.Size(217, 18);
            this.displayItemDTO.TabIndex = 7;
            this.displayItemDTO.Text = "...";
            this.displayItemDTO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      
            // 
            // Status
            // 
            this.Status.Location = new System.Drawing.Point(15, 18);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(20, 20);
            this.Status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Status.TabIndex = 6;
            this.Status.TabStop = false;
            // 
            // Stores_Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.displayItemDTO);
            this.Controls.Add(this.Status);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.R_Status);
            this.Controls.Add(this.R_Name);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(226, 82);
            this.Name = "Stores_Item";
            this.Size = new System.Drawing.Size(226, 80);
            this.Load += new System.EventHandler(this.Restaurant_Item_Load);
            this.Click += new System.EventHandler(this.Restaurant_Item_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label R_Name;
        private System.Windows.Forms.Label R_Status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox Status;
        internal System.Windows.Forms.FlowLayoutPanel Status_Buttons;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label displayItemDTO;
    }
}
