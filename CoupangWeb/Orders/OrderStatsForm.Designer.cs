namespace CoupangWeb.Orders
{
    partial class OrderStatsForm
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
            this.btnInquiry = new System.Windows.Forms.Button();
            this.dtcStart = new System.Windows.Forms.DateTimePicker();
            this.dtcEnds = new System.Windows.Forms.DateTimePicker();
            this.cmbStores = new System.Windows.Forms.ComboBox();
            this.txtStats = new System.Windows.Forms.Label();
            this.btnLastPage = new System.Windows.Forms.Button();
            this.btnNextPage = new System.Windows.Forms.Button();
            this.btnFirstPage = new System.Windows.Forms.Button();
            this.btnPrevPage = new System.Windows.Forms.Button();
            this.txtPageNo = new System.Windows.Forms.Label();
            this.lstOrders = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // btnInquiry
            // 
            this.btnInquiry.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInquiry.Location = new System.Drawing.Point(810, 20);
            this.btnInquiry.Name = "btnInquiry";
            this.btnInquiry.Size = new System.Drawing.Size(220, 85);
            this.btnInquiry.TabIndex = 46;
            this.btnInquiry.Text = "조회 (&I)";
            this.btnInquiry.UseVisualStyleBackColor = true;
            this.btnInquiry.Click += new System.EventHandler(this.btnInquiry_Click);
            // 
            // dtcStart
            // 
            this.dtcStart.CustomFormat = "yyyy-MM-dd";
            this.dtcStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtcStart.Location = new System.Drawing.Point(30, 72);
            this.dtcStart.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtcStart.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtcStart.Name = "dtcStart";
            this.dtcStart.Size = new System.Drawing.Size(140, 26);
            this.dtcStart.TabIndex = 44;
            this.dtcStart.Value = new System.DateTime(2022, 12, 8, 1, 23, 33, 0);
            this.dtcStart.ValueChanged += new System.EventHandler(this.dtcStart_ValueChanged);
            // 
            // dtcEnds
            // 
            this.dtcEnds.CustomFormat = "yyyy-MM-dd";
            this.dtcEnds.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtcEnds.Location = new System.Drawing.Point(180, 72);
            this.dtcEnds.MaxDate = new System.DateTime(2099, 12, 31, 0, 0, 0, 0);
            this.dtcEnds.MinDate = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            this.dtcEnds.Name = "dtcEnds";
            this.dtcEnds.Size = new System.Drawing.Size(140, 26);
            this.dtcEnds.TabIndex = 45;
            this.dtcEnds.Value = new System.DateTime(2022, 12, 8, 1, 23, 33, 0);
            this.dtcEnds.ValueChanged += new System.EventHandler(this.dtcEnds_ValueChanged);
            // 
            // cmbStores
            // 
            this.cmbStores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStores.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStores.FormattingEnabled = true;
            this.cmbStores.Location = new System.Drawing.Point(30, 20);
            this.cmbStores.Name = "cmbStores";
            this.cmbStores.Size = new System.Drawing.Size(760, 32);
            this.cmbStores.TabIndex = 43;
            // 
            // txtStats
            // 
            this.txtStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStats.Location = new System.Drawing.Point(359, 70);
            this.txtStats.Name = "txtStats";
            this.txtStats.Size = new System.Drawing.Size(431, 40);
            this.txtStats.TabIndex = 51;
            this.txtStats.Text = "5건 (12000원)";
            this.txtStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLastPage
            // 
            this.btnLastPage.Location = new System.Drawing.Point(719, 665);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Size = new System.Drawing.Size(60, 40);
            this.btnLastPage.TabIndex = 56;
            this.btnLastPage.Text = ">| (&L)";
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Location = new System.Drawing.Point(639, 665);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(60, 40);
            this.btnNextPage.TabIndex = 55;
            this.btnNextPage.Text = "> (&N)";
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Location = new System.Drawing.Point(279, 665);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Size = new System.Drawing.Size(60, 40);
            this.btnFirstPage.TabIndex = 53;
            this.btnFirstPage.Text = "|< (&F)";
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnPrevPage
            // 
            this.btnPrevPage.Location = new System.Drawing.Point(359, 665);
            this.btnPrevPage.Name = "btnPrevPage";
            this.btnPrevPage.Size = new System.Drawing.Size(60, 40);
            this.btnPrevPage.TabIndex = 54;
            this.btnPrevPage.Text = "< (&P)";
            this.btnPrevPage.UseVisualStyleBackColor = true;
            this.btnPrevPage.Click += new System.EventHandler(this.btnPrevPage_Click);
            // 
            // txtPageNo
            // 
            this.txtPageNo.Location = new System.Drawing.Point(449, 670);
            this.txtPageNo.Name = "txtPageNo";
            this.txtPageNo.Size = new System.Drawing.Size(160, 30);
            this.txtPageNo.TabIndex = 57;
            this.txtPageNo.Text = "1 / 10";
            this.txtPageNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstOrders
            // 
            this.lstOrders.FullRowSelect = true;
            this.lstOrders.HideSelection = false;
            this.lstOrders.Location = new System.Drawing.Point(29, 130);
            this.lstOrders.Name = "lstOrders";
            this.lstOrders.Size = new System.Drawing.Size(1000, 525);
            this.lstOrders.TabIndex = 52;
            this.lstOrders.UseCompatibleStateImageBehavior = false;
            this.lstOrders.View = System.Windows.Forms.View.Details;
            // 
            // OrderStatsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1059, 721);
            this.Controls.Add(this.btnLastPage);
            this.Controls.Add(this.btnNextPage);
            this.Controls.Add(this.btnFirstPage);
            this.Controls.Add(this.btnPrevPage);
            this.Controls.Add(this.txtPageNo);
            this.Controls.Add(this.lstOrders);
            this.Controls.Add(this.txtStats);
            this.Controls.Add(this.btnInquiry);
            this.Controls.Add(this.dtcStart);
            this.Controls.Add(this.dtcEnds);
            this.Controls.Add(this.cmbStores);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "OrderStatsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "매출관리";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrderStatsForm_FormClosing);
            this.Load += new System.EventHandler(this.OrderStatsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInquiry;
        private System.Windows.Forms.DateTimePicker dtcStart;
        private System.Windows.Forms.DateTimePicker dtcEnds;
        private System.Windows.Forms.ComboBox cmbStores;
        private System.Windows.Forms.Label txtStats;
        private System.Windows.Forms.Button btnLastPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Label txtPageNo;
        private System.Windows.Forms.ListView lstOrders;
    }
}