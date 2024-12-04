namespace m2
{
    partial class UserAndSellerManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAndSellerManagementForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSuspendUser = new System.Windows.Forms.Button();
            this.btnSearchUsers = new System.Windows.Forms.Button();
            this.cmbFilterUsers = new System.Windows.Forms.ComboBox();
            this.Search = new System.Windows.Forms.Label();
            this.txtSearchUsers = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Edit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnApproveSeller = new System.Windows.Forms.Button();
            this.btnSuspendSeller = new System.Windows.Forms.Button();
            this.btnSearchSellers = new System.Windows.Forms.Button();
            this.cmbFilterSellers = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchSellers = new System.Windows.Forms.TextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.BusinessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email_Seller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Edit_Seller = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Delete_Seller = new System.Windows.Forms.DataGridViewButtonColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 143);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(954, 413);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSuspendUser);
            this.tabPage1.Controls.Add(this.btnSearchUsers);
            this.tabPage1.Controls.Add(this.cmbFilterUsers);
            this.tabPage1.Controls.Add(this.Search);
            this.tabPage1.Controls.Add(this.txtSearchUsers);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(946, 384);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "User";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSuspendUser
            // 
            this.btnSuspendUser.Location = new System.Drawing.Point(761, 159);
            this.btnSuspendUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSuspendUser.Name = "btnSuspendUser";
            this.btnSuspendUser.Size = new System.Drawing.Size(79, 26);
            this.btnSuspendUser.TabIndex = 5;
            this.btnSuspendUser.Text = "Suspend";
            this.btnSuspendUser.UseVisualStyleBackColor = true;
            this.btnSuspendUser.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSearchUsers
            // 
            this.btnSearchUsers.Location = new System.Drawing.Point(516, 21);
            this.btnSearchUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearchUsers.Name = "btnSearchUsers";
            this.btnSearchUsers.Size = new System.Drawing.Size(67, 21);
            this.btnSearchUsers.TabIndex = 4;
            this.btnSearchUsers.Text = "Search";
            this.btnSearchUsers.UseVisualStyleBackColor = true;
            // 
            // cmbFilterUsers
            // 
            this.cmbFilterUsers.Enabled = false;
            this.cmbFilterUsers.FormattingEnabled = true;
            this.cmbFilterUsers.Items.AddRange(new object[] {
            "Active",
            "Suspended",
            "Inactive"});
            this.cmbFilterUsers.Location = new System.Drawing.Point(391, 19);
            this.cmbFilterUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterUsers.Name = "cmbFilterUsers";
            this.cmbFilterUsers.Size = new System.Drawing.Size(108, 24);
            this.cmbFilterUsers.TabIndex = 3;
            this.cmbFilterUsers.Text = "Choose";
            // 
            // Search
            // 
            this.Search.AutoSize = true;
            this.Search.Location = new System.Drawing.Point(249, 22);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(89, 16);
            this.Search.TabIndex = 2;
            this.Search.Text = "Search Users";
            // 
            // txtSearchUsers
            // 
            this.txtSearchUsers.Location = new System.Drawing.Point(244, 19);
            this.txtSearchUsers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchUsers.Name = "txtSearchUsers";
            this.txtSearchUsers.Size = new System.Drawing.Size(130, 22);
            this.txtSearchUsers.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name,
            this.Email,
            this.AccountStatus,
            this.Edit,
            this.Delete});
            this.dataGridView1.Location = new System.Drawing.Point(15, 56);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(726, 243);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.MinimumWidth = 8;
            this.Name.Name = "Name";
            this.Name.Width = 150;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 8;
            this.Email.Name = "Email";
            this.Email.Width = 150;
            // 
            // AccountStatus
            // 
            this.AccountStatus.HeaderText = "Account Status";
            this.AccountStatus.MinimumWidth = 8;
            this.AccountStatus.Name = "AccountStatus";
            this.AccountStatus.Width = 150;
            // 
            // Edit
            // 
            this.Edit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Edit.HeaderText = "Edit";
            this.Edit.MinimumWidth = 8;
            this.Edit.Name = "Edit";
            this.Edit.Text = "Edit";
            this.Edit.Width = 150;
            // 
            // Delete
            // 
            this.Delete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Delete.HeaderText = "Delete";
            this.Delete.MinimumWidth = 8;
            this.Delete.Name = "Delete";
            this.Delete.Text = "Delete";
            this.Delete.Width = 150;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnApproveSeller);
            this.tabPage2.Controls.Add(this.btnSuspendSeller);
            this.tabPage2.Controls.Add(this.btnSearchSellers);
            this.tabPage2.Controls.Add(this.cmbFilterSellers);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtSearchSellers);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(946, 384);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Seller";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // btnApproveSeller
            // 
            this.btnApproveSeller.Location = new System.Drawing.Point(764, 202);
            this.btnApproveSeller.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApproveSeller.Name = "btnApproveSeller";
            this.btnApproveSeller.Size = new System.Drawing.Size(76, 24);
            this.btnApproveSeller.TabIndex = 6;
            this.btnApproveSeller.Text = "Approve";
            this.btnApproveSeller.UseVisualStyleBackColor = true;
            // 
            // btnSuspendSeller
            // 
            this.btnSuspendSeller.Location = new System.Drawing.Point(764, 149);
            this.btnSuspendSeller.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSuspendSeller.Name = "btnSuspendSeller";
            this.btnSuspendSeller.Size = new System.Drawing.Size(76, 25);
            this.btnSuspendSeller.TabIndex = 5;
            this.btnSuspendSeller.Text = "Suspend";
            this.btnSuspendSeller.UseVisualStyleBackColor = true;
            this.btnSuspendSeller.Click += new System.EventHandler(this.btnSuspendSeller_Click);
            // 
            // btnSearchSellers
            // 
            this.btnSearchSellers.Location = new System.Drawing.Point(521, 23);
            this.btnSearchSellers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearchSellers.Name = "btnSearchSellers";
            this.btnSearchSellers.Size = new System.Drawing.Size(67, 21);
            this.btnSearchSellers.TabIndex = 4;
            this.btnSearchSellers.Text = "Search";
            this.btnSearchSellers.UseVisualStyleBackColor = true;
            // 
            // cmbFilterSellers
            // 
            this.cmbFilterSellers.FormattingEnabled = true;
            this.cmbFilterSellers.Location = new System.Drawing.Point(390, 22);
            this.cmbFilterSellers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbFilterSellers.Name = "cmbFilterSellers";
            this.cmbFilterSellers.Size = new System.Drawing.Size(108, 24);
            this.cmbFilterSellers.TabIndex = 3;
            this.cmbFilterSellers.Text = "Choose";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search Seller";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtSearchSellers
            // 
            this.txtSearchSellers.Location = new System.Drawing.Point(231, 22);
            this.txtSearchSellers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchSellers.Name = "txtSearchSellers";
            this.txtSearchSellers.Size = new System.Drawing.Size(142, 22);
            this.txtSearchSellers.TabIndex = 1;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BusinessName,
            this.Email_Seller,
            this.Status,
            this.Edit_Seller,
            this.Delete_Seller});
            this.dataGridView2.Location = new System.Drawing.Point(14, 63);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 28;
            this.dataGridView2.Size = new System.Drawing.Size(724, 244);
            this.dataGridView2.TabIndex = 0;
            // 
            // BusinessName
            // 
            this.BusinessName.HeaderText = "Business Name";
            this.BusinessName.MinimumWidth = 8;
            this.BusinessName.Name = "BusinessName";
            this.BusinessName.Width = 150;
            // 
            // Email_Seller
            // 
            this.Email_Seller.HeaderText = "Email";
            this.Email_Seller.MinimumWidth = 8;
            this.Email_Seller.Name = "Email_Seller";
            this.Email_Seller.Width = 150;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 8;
            this.Status.Name = "Status";
            this.Status.Width = 150;
            // 
            // Edit_Seller
            // 
            this.Edit_Seller.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Edit_Seller.HeaderText = "Edit";
            this.Edit_Seller.MinimumWidth = 8;
            this.Edit_Seller.Name = "Edit_Seller";
            this.Edit_Seller.Width = 150;
            // 
            // Delete_Seller
            // 
            this.Delete_Seller.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Delete_Seller.HeaderText = "Delete";
            this.Delete_Seller.MinimumWidth = 8;
            this.Delete_Seller.Name = "Delete_Seller";
            this.Delete_Seller.Width = 150;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Impact", 12F);
            this.button1.ForeColor = System.Drawing.Color.Crimson;
            this.button1.Location = new System.Drawing.Point(1000, 505);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 50);
            this.button1.TabIndex = 1;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // UserAndSellerManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1132, 603);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            //this.Name = "UserAndSellerManagementForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label Search;
        private System.Windows.Forms.TextBox txtSearchUsers;
        private System.Windows.Forms.Button btnSearchUsers;
        private System.Windows.Forms.ComboBox cmbFilterUsers;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBusinessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email_Seller;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewButtonColumn Edit_Seller;
        private System.Windows.Forms.DataGridViewButtonColumn Delete_Seller;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewComboBoxColumn AccountStatus;
        private System.Windows.Forms.DataGridViewButtonColumn Edit;
        private System.Windows.Forms.DataGridViewButtonColumn Delete;
        private System.Windows.Forms.TextBox txtSearchSellers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFilterSellers;
        private System.Windows.Forms.Button btnSearchSellers;
        private System.Windows.Forms.Button btnSuspendUser;
        private System.Windows.Forms.Button btnApproveSeller;
        private System.Windows.Forms.Button btnSuspendSeller;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn BusinessName;
        private System.Windows.Forms.Button button1;
    }
}