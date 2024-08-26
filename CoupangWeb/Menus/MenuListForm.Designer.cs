namespace CoupangWeb.Menus
{
    partial class MenuListForm
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
            this.cmbStores = new System.Windows.Forms.ComboBox();
            this.pnlMenus = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtSoldOutCount = new System.Windows.Forms.Label();
            this.txtOnSaleCount = new System.Windows.Forms.Label();
            this.txtTotalCount = new System.Windows.Forms.Label();
            this.txtHiddenCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbStores
            // 
            this.cmbStores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStores.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStores.FormattingEnabled = true;
            this.cmbStores.Location = new System.Drawing.Point(30, 29);
            this.cmbStores.Name = "cmbStores";
            this.cmbStores.Size = new System.Drawing.Size(560, 32);
            this.cmbStores.TabIndex = 44;
            this.cmbStores.SelectedIndexChanged += new System.EventHandler(this.cmbStores_SelectedIndexChanged);
            // 
            // pnlMenus
            // 
            this.pnlMenus.AutoScroll = true;
            this.pnlMenus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMenus.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlMenus.Location = new System.Drawing.Point(30, 140);
            this.pnlMenus.Name = "pnlMenus";
            this.pnlMenus.Size = new System.Drawing.Size(730, 480);
            this.pnlMenus.TabIndex = 45;
            this.pnlMenus.WrapContents = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(620, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(140, 50);
            this.btnRefresh.TabIndex = 60;
            this.btnRefresh.Text = "새로 고침(&R)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtSoldOutCount
            // 
            this.txtSoldOutCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoldOutCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtSoldOutCount.Location = new System.Drawing.Point(420, 80);
            this.txtSoldOutCount.Name = "txtSoldOutCount";
            this.txtSoldOutCount.Size = new System.Drawing.Size(140, 50);
            this.txtSoldOutCount.TabIndex = 65;
            this.txtSoldOutCount.Text = "오늘만 품절\r\n3개";
            this.txtSoldOutCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtOnSaleCount
            // 
            this.txtOnSaleCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnSaleCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtOnSaleCount.Location = new System.Drawing.Point(220, 80);
            this.txtOnSaleCount.Name = "txtOnSaleCount";
            this.txtOnSaleCount.Size = new System.Drawing.Size(140, 50);
            this.txtOnSaleCount.TabIndex = 66;
            this.txtOnSaleCount.Text = "판매중\r\n1개";
            this.txtOnSaleCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalCount
            // 
            this.txtTotalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtTotalCount.Location = new System.Drawing.Point(30, 80);
            this.txtTotalCount.Name = "txtTotalCount";
            this.txtTotalCount.Size = new System.Drawing.Size(140, 50);
            this.txtTotalCount.TabIndex = 67;
            this.txtTotalCount.Text = "전체\r\n4개";
            this.txtTotalCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHiddenCount
            // 
            this.txtHiddenCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHiddenCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtHiddenCount.Location = new System.Drawing.Point(620, 80);
            this.txtHiddenCount.Name = "txtHiddenCount";
            this.txtHiddenCount.Size = new System.Drawing.Size(140, 50);
            this.txtHiddenCount.TabIndex = 68;
            this.txtHiddenCount.Text = "메뉴 숨김\r\n0개";
            this.txtHiddenCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MenuListForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(789, 641);
            this.Controls.Add(this.txtSoldOutCount);
            this.Controls.Add(this.txtOnSaleCount);
            this.Controls.Add(this.txtTotalCount);
            this.Controls.Add(this.txtHiddenCount);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.pnlMenus);
            this.Controls.Add(this.cmbStores);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MenuListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "메뉴관리";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuListForm_FormClosing);
            this.Load += new System.EventHandler(this.MenuListForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStores;
        private System.Windows.Forms.FlowLayoutPanel pnlMenus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label txtSoldOutCount;
        private System.Windows.Forms.Label txtOnSaleCount;
        private System.Windows.Forms.Label txtTotalCount;
        private System.Windows.Forms.Label txtHiddenCount;
    }
}