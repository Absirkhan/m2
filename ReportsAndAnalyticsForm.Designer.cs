namespace m2
{
    partial class ReportsAndAnalyticsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportsAndAnalyticsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvSalesReport = new System.Windows.Forms.DataGridView();
            this.btnGenerateSalesReport = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chartUserActivity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnGenerateUserActivityReport = new System.Windows.Forms.Button();
            this.cmbUserTypeFilter = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.chartPerformanceMetrics = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnViewPerformanceMetric = new System.Windows.Forms.Button();
            this.lstPerformanceMetrics = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartUserActivity)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformanceMetrics)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 134);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(926, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Bisque;
            this.tabPage1.Controls.Add(this.dgvSalesReport);
            this.tabPage1.Controls.Add(this.btnGenerateSalesReport);
            this.tabPage1.Controls.Add(this.dtpEndDate);
            this.tabPage1.Controls.Add(this.dtpStartDate);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Size = new System.Drawing.Size(918, 393);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sales Reports";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // dgvSalesReport
            // 
            this.dgvSalesReport.BackgroundColor = System.Drawing.Color.Bisque;
            this.dgvSalesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesReport.Location = new System.Drawing.Point(139, 83);
            this.dgvSalesReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSalesReport.Name = "dgvSalesReport";
            this.dgvSalesReport.RowHeadersWidth = 62;
            this.dgvSalesReport.RowTemplate.Height = 28;
            this.dgvSalesReport.Size = new System.Drawing.Size(535, 246);
            this.dgvSalesReport.TabIndex = 3;
            this.dgvSalesReport.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalesReport_CellContentClick);
            // 
            // btnGenerateSalesReport
            // 
            this.btnGenerateSalesReport.Location = new System.Drawing.Point(533, 14);
            this.btnGenerateSalesReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerateSalesReport.Name = "btnGenerateSalesReport";
            this.btnGenerateSalesReport.Size = new System.Drawing.Size(149, 27);
            this.btnGenerateSalesReport.TabIndex = 2;
            this.btnGenerateSalesReport.Text = "Generate Report";
            this.btnGenerateSalesReport.UseVisualStyleBackColor = true;
            this.btnGenerateSalesReport.Click += new System.EventHandler(this.btnGenerateSalesReport_Click);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Location = new System.Drawing.Point(332, 14);
            this.dtpEndDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(178, 22);
            this.dtpEndDate.TabIndex = 1;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Location = new System.Drawing.Point(139, 14);
            this.dtpStartDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(178, 22);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Bisque;
            this.tabPage2.Controls.Add(this.chartUserActivity);
            this.tabPage2.Controls.Add(this.btnGenerateUserActivityReport);
            this.tabPage2.Controls.Add(this.cmbUserTypeFilter);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(918, 393);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "User Activity";
            // 
            // chartUserActivity
            // 
            chartArea1.Name = "ChartArea1";
            this.chartUserActivity.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartUserActivity.Legends.Add(legend1);
            this.chartUserActivity.Location = new System.Drawing.Point(130, 51);
            this.chartUserActivity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartUserActivity.Name = "chartUserActivity";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartUserActivity.Series.Add(series1);
            this.chartUserActivity.Size = new System.Drawing.Size(545, 240);
            this.chartUserActivity.TabIndex = 2;
            this.chartUserActivity.Text = "chart1";
            this.chartUserActivity.Click += new System.EventHandler(this.chartUserActivity_Click);
            // 
            // btnGenerateUserActivityReport
            // 
            this.btnGenerateUserActivityReport.Location = new System.Drawing.Point(270, 11);
            this.btnGenerateUserActivityReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGenerateUserActivityReport.Name = "btnGenerateUserActivityReport";
            this.btnGenerateUserActivityReport.Size = new System.Drawing.Size(140, 30);
            this.btnGenerateUserActivityReport.TabIndex = 1;
            this.btnGenerateUserActivityReport.Text = "Generate Report";
            this.btnGenerateUserActivityReport.UseVisualStyleBackColor = true;
            this.btnGenerateUserActivityReport.Click += new System.EventHandler(this.btnGenerateUserActivityReport_Click);
            // 
            // cmbUserTypeFilter
            // 
            this.cmbUserTypeFilter.FormattingEnabled = true;
            this.cmbUserTypeFilter.Location = new System.Drawing.Point(130, 15);
            this.cmbUserTypeFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbUserTypeFilter.Name = "cmbUserTypeFilter";
            this.cmbUserTypeFilter.Size = new System.Drawing.Size(108, 24);
            this.cmbUserTypeFilter.TabIndex = 0;
            this.cmbUserTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cmbUserTypeFilter_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Bisque;
            this.tabPage3.Controls.Add(this.chartPerformanceMetrics);
            this.tabPage3.Controls.Add(this.btnViewPerformanceMetric);
            this.tabPage3.Controls.Add(this.lstPerformanceMetrics);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(918, 393);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Platform Performance";
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // chartPerformanceMetrics
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPerformanceMetrics.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartPerformanceMetrics.Legends.Add(legend2);
            this.chartPerformanceMetrics.Location = new System.Drawing.Point(317, 30);
            this.chartPerformanceMetrics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartPerformanceMetrics.Name = "chartPerformanceMetrics";
            this.chartPerformanceMetrics.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartPerformanceMetrics.Series.Add(series2);
            this.chartPerformanceMetrics.Size = new System.Drawing.Size(565, 326);
            this.chartPerformanceMetrics.TabIndex = 2;
            this.chartPerformanceMetrics.Text = "chart1";
            this.chartPerformanceMetrics.Click += new System.EventHandler(this.chartPerformanceMetrics_Click);
            // 
            // btnViewPerformanceMetric
            // 
            this.btnViewPerformanceMetric.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewPerformanceMetric.ForeColor = System.Drawing.Color.Crimson;
            this.btnViewPerformanceMetric.Location = new System.Drawing.Point(6, 184);
            this.btnViewPerformanceMetric.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnViewPerformanceMetric.Name = "btnViewPerformanceMetric";
            this.btnViewPerformanceMetric.Size = new System.Drawing.Size(125, 77);
            this.btnViewPerformanceMetric.TabIndex = 1;
            this.btnViewPerformanceMetric.Text = "View Metric";
            this.btnViewPerformanceMetric.UseVisualStyleBackColor = true;
            this.btnViewPerformanceMetric.Click += new System.EventHandler(this.btnViewPerformanceMetric_Click);
            // 
            // lstPerformanceMetrics
            // 
            this.lstPerformanceMetrics.BackColor = System.Drawing.Color.White;
            this.lstPerformanceMetrics.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPerformanceMetrics.ForeColor = System.Drawing.Color.Crimson;
            this.lstPerformanceMetrics.FormattingEnabled = true;
            this.lstPerformanceMetrics.ItemHeight = 23;
            this.lstPerformanceMetrics.Items.AddRange(new object[] {
            "Active Users (Last 30 Days).",
            "Total Transactions."});
            this.lstPerformanceMetrics.Location = new System.Drawing.Point(6, 67);
            this.lstPerformanceMetrics.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lstPerformanceMetrics.Name = "lstPerformanceMetrics";
            this.lstPerformanceMetrics.Size = new System.Drawing.Size(279, 96);
            this.lstPerformanceMetrics.TabIndex = 0;
            this.lstPerformanceMetrics.SelectedIndexChanged += new System.EventHandler(this.lstPerformanceMetrics_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Impact", 12F);
            this.button1.ForeColor = System.Drawing.Color.Crimson;
            this.button1.Location = new System.Drawing.Point(996, 518);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 37);
            this.button1.TabIndex = 1;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ReportsAndAnalyticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1132, 603);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ReportsAndAnalyticsForm";
            this.Text = "ReportsAndAnalyticsForm";
            this.Load += new System.EventHandler(this.ReportsAndAnalyticsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesReport)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartUserActivity)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPerformanceMetrics)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Button btnGenerateSalesReport;
        private System.Windows.Forms.DataGridView dgvSalesReport;
        private System.Windows.Forms.ComboBox cmbUserTypeFilter;
        private System.Windows.Forms.Button btnGenerateUserActivityReport;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartUserActivity;
        private System.Windows.Forms.ListBox lstPerformanceMetrics;
        private System.Windows.Forms.Button btnViewPerformanceMetric;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPerformanceMetrics;
        private System.Windows.Forms.Button button1;
    }
}