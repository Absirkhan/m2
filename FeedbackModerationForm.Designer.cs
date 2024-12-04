namespace m2
{
    partial class FeedbackModerationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FeedbackModerationForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSearchFeedback = new System.Windows.Forms.Button();
            this.txtSearchFeedback = new System.Windows.Forms.TextBox();
            this.cmbReviewStatus = new System.Windows.Forms.ComboBox();
            this.dgvFeedbackList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDeleteReview = new System.Windows.Forms.Button();
            this.rtbSelectedReview = new System.Windows.Forms.RichTextBox();
            this.btnFlagReview = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnApproveReview = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedbackList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearchFeedback);
            this.groupBox1.Controls.Add(this.txtSearchFeedback);
            this.groupBox1.Controls.Add(this.cmbReviewStatus);
            this.groupBox1.Location = new System.Drawing.Point(12, 102);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(333, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter Reviews";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // btnSearchFeedback
            // 
            this.btnSearchFeedback.Location = new System.Drawing.Point(247, 30);
            this.btnSearchFeedback.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearchFeedback.Name = "btnSearchFeedback";
            this.btnSearchFeedback.Size = new System.Drawing.Size(67, 23);
            this.btnSearchFeedback.TabIndex = 2;
            this.btnSearchFeedback.Text = "Search";
            this.btnSearchFeedback.UseVisualStyleBackColor = true;
            this.btnSearchFeedback.Click += new System.EventHandler(this.btnSearchFeedback_Click);
            // 
            // txtSearchFeedback
            // 
            this.txtSearchFeedback.Location = new System.Drawing.Point(140, 30);
            this.txtSearchFeedback.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearchFeedback.Name = "txtSearchFeedback";
            this.txtSearchFeedback.Size = new System.Drawing.Size(89, 22);
            this.txtSearchFeedback.TabIndex = 1;
            this.txtSearchFeedback.TextChanged += new System.EventHandler(this.txtSearchFeedback_TextChanged);
            // 
            // cmbReviewStatus
            // 
            this.cmbReviewStatus.FormattingEnabled = true;
            this.cmbReviewStatus.Items.AddRange(new object[] {
            "All Reviews",
            "Pending Moderation",
            "Approved",
            "Flagged/Inappropriate"});
            this.cmbReviewStatus.Location = new System.Drawing.Point(15, 30);
            this.cmbReviewStatus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbReviewStatus.Name = "cmbReviewStatus";
            this.cmbReviewStatus.Size = new System.Drawing.Size(108, 24);
            this.cmbReviewStatus.TabIndex = 0;
            this.cmbReviewStatus.SelectedIndexChanged += new System.EventHandler(this.cmbReviewStatus_SelectedIndexChanged);
            // 
            // dgvFeedbackList
            // 
            this.dgvFeedbackList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFeedbackList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgvFeedbackList.Location = new System.Drawing.Point(12, 226);
            this.dgvFeedbackList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvFeedbackList.Name = "dgvFeedbackList";
            this.dgvFeedbackList.RowHeadersWidth = 62;
            this.dgvFeedbackList.RowTemplate.Height = 28;
            this.dgvFeedbackList.Size = new System.Drawing.Size(592, 120);
            this.dgvFeedbackList.TabIndex = 1;
            this.dgvFeedbackList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFeedbackList_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Feedback ID";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "User";
            this.Column2.MinimumWidth = 8;
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Review Content";
            this.Column3.MinimumWidth = 8;
            this.Column3.Name = "Column3";
            this.Column3.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Status";
            this.Column4.MinimumWidth = 8;
            this.Column4.Name = "Column4";
            this.Column4.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDeleteReview);
            this.groupBox2.Controls.Add(this.rtbSelectedReview);
            this.groupBox2.Controls.Add(this.btnFlagReview);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnApproveReview);
            this.groupBox2.Location = new System.Drawing.Point(12, 383);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(717, 106);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Moderation Tools";
            // 
            // btnDeleteReview
            // 
            this.btnDeleteReview.Location = new System.Drawing.Point(563, 75);
            this.btnDeleteReview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDeleteReview.Name = "btnDeleteReview";
            this.btnDeleteReview.Size = new System.Drawing.Size(67, 26);
            this.btnDeleteReview.TabIndex = 4;
            this.btnDeleteReview.Text = "Delete";
            this.btnDeleteReview.UseVisualStyleBackColor = true;
            this.btnDeleteReview.Click += new System.EventHandler(this.btnDeleteReview_Click);
            // 
            // rtbSelectedReview
            // 
            this.rtbSelectedReview.Location = new System.Drawing.Point(140, 20);
            this.rtbSelectedReview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rtbSelectedReview.Name = "rtbSelectedReview";
            this.rtbSelectedReview.ReadOnly = true;
            this.rtbSelectedReview.Size = new System.Drawing.Size(321, 78);
            this.rtbSelectedReview.TabIndex = 1;
            this.rtbSelectedReview.Text = "";
            // 
            // btnFlagReview
            // 
            this.btnFlagReview.Location = new System.Drawing.Point(518, 49);
            this.btnFlagReview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFlagReview.Name = "btnFlagReview";
            this.btnFlagReview.Size = new System.Drawing.Size(151, 23);
            this.btnFlagReview.TabIndex = 5;
            this.btnFlagReview.Text = "Flag as Inappropriate";
            this.btnFlagReview.UseVisualStyleBackColor = true;
            this.btnFlagReview.Click += new System.EventHandler(this.btnFlagReview_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selected Review:";
            // 
            // btnApproveReview
            // 
            this.btnApproveReview.Location = new System.Drawing.Point(556, 18);
            this.btnApproveReview.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApproveReview.Name = "btnApproveReview";
            this.btnApproveReview.Size = new System.Drawing.Size(74, 26);
            this.btnApproveReview.TabIndex = 2;
            this.btnApproveReview.Text = "Approve";
            this.btnApproveReview.UseVisualStyleBackColor = true;
            this.btnApproveReview.Click += new System.EventHandler(this.btnApproveReview_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Impact", 13F);
            this.button1.ForeColor = System.Drawing.Color.Crimson;
            this.button1.Location = new System.Drawing.Point(889, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 82);
            this.button1.TabIndex = 3;
            this.button1.Text = "Go Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FeedbackModerationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1132, 603);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvFeedbackList);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FeedbackModerationForm";
            this.Text = "FeedbackModerationForm";
            this.Load += new System.EventHandler(this.FeedbackModerationForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedbackList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbReviewStatus;
        private System.Windows.Forms.TextBox txtSearchFeedback;
        private System.Windows.Forms.Button btnSearchFeedback;
        private System.Windows.Forms.DataGridView dgvFeedbackList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbSelectedReview;
        private System.Windows.Forms.Button btnDeleteReview;
        private System.Windows.Forms.Button btnApproveReview;
        private System.Windows.Forms.Button btnFlagReview;
        private System.Windows.Forms.Button button1;
    }
}