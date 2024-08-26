namespace CoupangWeb.Menus
{
    partial class MenuEditForm
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPhotos = new System.Windows.Forms.Label();
            this.lblOptions = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.chkHiddenStatus = new System.Windows.Forms.CheckBox();
            this.chkSoldOutToday = new System.Windows.Forms.CheckBox();
            this.chkOnSaleStatus = new System.Windows.Forms.CheckBox();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.pnlImages = new System.Windows.Forms.FlowLayoutPanel();
            this.lstOptions = new System.Windows.Forms.ListView();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(30, 420);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(300, 30);
            this.lblDescription.TabIndex = 65;
            this.lblDescription.Text = "메뉴 설명";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPhotos
            // 
            this.lblPhotos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhotos.Location = new System.Drawing.Point(30, 270);
            this.lblPhotos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPhotos.Name = "lblPhotos";
            this.lblPhotos.Size = new System.Drawing.Size(300, 30);
            this.lblPhotos.TabIndex = 66;
            this.lblPhotos.Text = "메뉴 사진";
            this.lblPhotos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOptions
            // 
            this.lblOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOptions.Location = new System.Drawing.Point(400, 20);
            this.lblOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(300, 30);
            this.lblOptions.TabIndex = 67;
            this.lblOptions.Text = "옵션";
            this.lblOptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInfo.Location = new System.Drawing.Point(30, 20);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(300, 30);
            this.lblInfo.TabIndex = 68;
            this.lblInfo.Text = "메뉴 정보";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(30, 105);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(60, 30);
            this.lblName.TabIndex = 70;
            this.lblName.Text = "메뉴명";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(110, 107);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(260, 26);
            this.txtName.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 150);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 30);
            this.label1.TabIndex = 70;
            this.label1.Text = "가격(원)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(110, 154);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(260, 26);
            this.txtPrice.TabIndex = 69;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // chkHiddenStatus
            // 
            this.chkHiddenStatus.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkHiddenStatus.Location = new System.Drawing.Point(260, 200);
            this.chkHiddenStatus.Name = "chkHiddenStatus";
            this.chkHiddenStatus.Size = new System.Drawing.Size(110, 40);
            this.chkHiddenStatus.TabIndex = 78;
            this.chkHiddenStatus.Tag = "NOT_EXPOSE";
            this.chkHiddenStatus.Text = "(&H) 메뉴 숨김";
            this.chkHiddenStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkHiddenStatus.UseVisualStyleBackColor = true;
            this.chkHiddenStatus.Click += new System.EventHandler(this.chkStatus_Click);
            // 
            // chkSoldOutToday
            // 
            this.chkSoldOutToday.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSoldOutToday.Location = new System.Drawing.Point(145, 200);
            this.chkSoldOutToday.Name = "chkSoldOutToday";
            this.chkSoldOutToday.Size = new System.Drawing.Size(110, 40);
            this.chkSoldOutToday.TabIndex = 79;
            this.chkSoldOutToday.Tag = "SOLD_OUT_TODAY";
            this.chkSoldOutToday.Text = "(&O) 오늘만 품절";
            this.chkSoldOutToday.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSoldOutToday.UseVisualStyleBackColor = true;
            this.chkSoldOutToday.Click += new System.EventHandler(this.chkStatus_Click);
            // 
            // chkOnSaleStatus
            // 
            this.chkOnSaleStatus.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkOnSaleStatus.Location = new System.Drawing.Point(30, 200);
            this.chkOnSaleStatus.Name = "chkOnSaleStatus";
            this.chkOnSaleStatus.Size = new System.Drawing.Size(110, 40);
            this.chkOnSaleStatus.TabIndex = 77;
            this.chkOnSaleStatus.Tag = "ON_SALE";
            this.chkOnSaleStatus.Text = "(&S) 판매중";
            this.chkOnSaleStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkOnSaleStatus.UseVisualStyleBackColor = true;
            this.chkOnSaleStatus.Click += new System.EventHandler(this.chkStatus_Click);
            // 
            // cmbGroup
            // 
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(30, 60);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(340, 32);
            this.cmbGroup.TabIndex = 80;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Location = new System.Drawing.Point(30, 460);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(340, 80);
            this.txtDescription.TabIndex = 81;
            // 
            // pnlImages
            // 
            this.pnlImages.AutoScroll = true;
            this.pnlImages.Location = new System.Drawing.Point(30, 310);
            this.pnlImages.Name = "pnlImages";
            this.pnlImages.Size = new System.Drawing.Size(340, 80);
            this.pnlImages.TabIndex = 82;
            this.pnlImages.WrapContents = false;
            // 
            // lstOptions
            // 
            this.lstOptions.FullRowSelect = true;
            this.lstOptions.HideSelection = false;
            this.lstOptions.Location = new System.Drawing.Point(400, 60);
            this.lstOptions.Name = "lstOptions";
            this.lstOptions.Size = new System.Drawing.Size(480, 480);
            this.lstOptions.TabIndex = 83;
            this.lstOptions.UseCompatibleStateImageBehavior = false;
            this.lstOptions.View = System.Windows.Forms.View.Details;
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApply.Location = new System.Drawing.Point(470, 570);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(180, 50);
            this.btnApply.TabIndex = 84;
            this.btnApply.Text = "저장 (&A)";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(260, 570);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(180, 50);
            this.btnCancel.TabIndex = 84;
            this.btnCancel.Text = "취소 (&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // MenuEditForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(909, 641);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.lstOptions);
            this.Controls.Add(this.pnlImages);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.chkHiddenStatus);
            this.Controls.Add(this.chkSoldOutToday);
            this.Controls.Add(this.chkOnSaleStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblPhotos);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.lblInfo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MenuEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "메뉴상세";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuEditForm_FormClosing);
            this.Load += new System.EventHandler(this.MenuEditForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPhotos;
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.CheckBox chkHiddenStatus;
        private System.Windows.Forms.CheckBox chkSoldOutToday;
        private System.Windows.Forms.CheckBox chkOnSaleStatus;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.FlowLayoutPanel pnlImages;
        private System.Windows.Forms.ListView lstOptions;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}