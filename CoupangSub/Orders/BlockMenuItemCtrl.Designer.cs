namespace CoupangSub.Orders
{
    partial class BlockMenuItemCtrl
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
            this.pnlOptions = new System.Windows.Forms.FlowLayoutPanel();
            this.chkMenu = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pnlOptions
            // 
            this.pnlOptions.AutoScroll = true;
            this.pnlOptions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlOptions.Location = new System.Drawing.Point(30, 45);
            this.pnlOptions.Name = "pnlOptions";
            this.pnlOptions.Size = new System.Drawing.Size(210, 145);
            this.pnlOptions.TabIndex = 36;
            this.pnlOptions.WrapContents = false;
            // 
            // chkMenu
            // 
            this.chkMenu.Location = new System.Drawing.Point(10, 10);
            this.chkMenu.Name = "chkMenu";
            this.chkMenu.Size = new System.Drawing.Size(230, 30);
            this.chkMenu.TabIndex = 35;
            this.chkMenu.Text = "Menu";
            this.chkMenu.UseVisualStyleBackColor = true;
            // 
            // BlockMenuItemCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.chkMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BlockMenuItemCtrl";
            this.Size = new System.Drawing.Size(250, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlOptions;
        private System.Windows.Forms.CheckBox chkMenu;
    }
}
