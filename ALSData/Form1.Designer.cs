namespace ALSData
{
    partial class frm_dm
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
            this.btn_SelectDirectory = new System.Windows.Forms.Button();
            this.Label_Path = new System.Windows.Forms.Label();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.btn_Compress = new System.Windows.Forms.Button();
            this.chkbox_DeleteWhenComplete = new System.Windows.Forms.CheckBox();
            this.chkbox_SkipFolders = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txt_status = new System.Windows.Forms.TextBox();
            this.Label_Exclusion = new System.Windows.Forms.Label();
            this.label_Progress = new System.Windows.Forms.Label();
            this.btn_SelectExclusion = new System.Windows.Forms.Button();
            this.chkbox_AllowMultiPath = new System.Windows.Forms.CheckBox();
            this.btn_SelectOutPutPath = new System.Windows.Forms.Button();
            this.Label_OutPut = new System.Windows.Forms.Label();
            this.txt_OutPutPath = new System.Windows.Forms.TextBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_SelectDirectory
            // 
            this.btn_SelectDirectory.Location = new System.Drawing.Point(416, 16);
            this.btn_SelectDirectory.Name = "btn_SelectDirectory";
            this.btn_SelectDirectory.Size = new System.Drawing.Size(154, 30);
            this.btn_SelectDirectory.TabIndex = 0;
            this.btn_SelectDirectory.Text = "Select Input Directory";
            this.btn_SelectDirectory.UseVisualStyleBackColor = true;
            this.btn_SelectDirectory.Click += new System.EventHandler(this.btn_SelectDirectory_Click);
            // 
            // Label_Path
            // 
            this.Label_Path.AutoSize = true;
            this.Label_Path.Location = new System.Drawing.Point(12, 5);
            this.Label_Path.Name = "Label_Path";
            this.Label_Path.Size = new System.Drawing.Size(59, 13);
            this.Label_Path.TabIndex = 1;
            this.Label_Path.Text = "Input Path:";
            // 
            // txt_path
            // 
            this.txt_path.Enabled = false;
            this.txt_path.Location = new System.Drawing.Point(12, 21);
            this.txt_path.Name = "txt_path";
            this.txt_path.Size = new System.Drawing.Size(398, 20);
            this.txt_path.TabIndex = 2;
            // 
            // btn_Compress
            // 
            this.btn_Compress.Location = new System.Drawing.Point(576, 436);
            this.btn_Compress.Name = "btn_Compress";
            this.btn_Compress.Size = new System.Drawing.Size(159, 100);
            this.btn_Compress.TabIndex = 0;
            this.btn_Compress.Text = "Compress All folders in Selected Path";
            this.btn_Compress.UseVisualStyleBackColor = true;
            this.btn_Compress.Click += new System.EventHandler(this.btn_Compress_Click);
            // 
            // chkbox_DeleteWhenComplete
            // 
            this.chkbox_DeleteWhenComplete.AutoSize = true;
            this.chkbox_DeleteWhenComplete.Location = new System.Drawing.Point(576, 364);
            this.chkbox_DeleteWhenComplete.Name = "chkbox_DeleteWhenComplete";
            this.chkbox_DeleteWhenComplete.Size = new System.Drawing.Size(159, 17);
            this.chkbox_DeleteWhenComplete.TabIndex = 3;
            this.chkbox_DeleteWhenComplete.Text = "Delete Folder on Completion";
            this.chkbox_DeleteWhenComplete.UseVisualStyleBackColor = true;
            // 
            // chkbox_SkipFolders
            // 
            this.chkbox_SkipFolders.AutoSize = true;
            this.chkbox_SkipFolders.Checked = true;
            this.chkbox_SkipFolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbox_SkipFolders.Location = new System.Drawing.Point(576, 387);
            this.chkbox_SkipFolders.Name = "chkbox_SkipFolders";
            this.chkbox_SkipFolders.Size = new System.Drawing.Size(174, 17);
            this.chkbox_SkipFolders.TabIndex = 3;
            this.chkbox_SkipFolders.Text = "Skip Folder Sizes less than (kB)";
            this.chkbox_SkipFolders.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.AllowDrop = true;
            this.numericUpDown1.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(576, 410);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 542);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(558, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 5;
            // 
            // txt_status
            // 
            this.txt_status.Location = new System.Drawing.Point(12, 349);
            this.txt_status.Multiline = true;
            this.txt_status.Name = "txt_status";
            this.txt_status.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_status.Size = new System.Drawing.Size(558, 187);
            this.txt_status.TabIndex = 6;
            // 
            // Label_Exclusion
            // 
            this.Label_Exclusion.AutoSize = true;
            this.Label_Exclusion.Location = new System.Drawing.Point(12, 55);
            this.Label_Exclusion.Name = "Label_Exclusion";
            this.Label_Exclusion.Size = new System.Drawing.Size(386, 13);
            this.Label_Exclusion.TabIndex = 1;
            this.Label_Exclusion.Text = "Exclusion Folder Path (Need Complete Network Path and separate with comma):";
            // 
            // label_Progress
            // 
            this.label_Progress.AutoSize = true;
            this.label_Progress.Location = new System.Drawing.Point(643, 552);
            this.label_Progress.Name = "label_Progress";
            this.label_Progress.Size = new System.Drawing.Size(24, 13);
            this.label_Progress.TabIndex = 7;
            this.label_Progress.Text = "0/0";
            // 
            // btn_SelectExclusion
            // 
            this.btn_SelectExclusion.Location = new System.Drawing.Point(416, 136);
            this.btn_SelectExclusion.Name = "btn_SelectExclusion";
            this.btn_SelectExclusion.Size = new System.Drawing.Size(154, 30);
            this.btn_SelectExclusion.TabIndex = 0;
            this.btn_SelectExclusion.Text = "Add Exclusion Directories";
            this.btn_SelectExclusion.UseVisualStyleBackColor = true;
            this.btn_SelectExclusion.Click += new System.EventHandler(this.btn_SelectExclusion_Click);
            // 
            // chkbox_AllowMultiPath
            // 
            this.chkbox_AllowMultiPath.AutoSize = true;
            this.chkbox_AllowMultiPath.Location = new System.Drawing.Point(576, 23);
            this.chkbox_AllowMultiPath.Name = "chkbox_AllowMultiPath";
            this.chkbox_AllowMultiPath.Size = new System.Drawing.Size(143, 17);
            this.chkbox_AllowMultiPath.TabIndex = 8;
            this.chkbox_AllowMultiPath.Text = "Allow Multiple Directories";
            this.chkbox_AllowMultiPath.UseVisualStyleBackColor = true;
            // 
            // btn_SelectOutPutPath
            // 
            this.btn_SelectOutPutPath.Location = new System.Drawing.Point(416, 313);
            this.btn_SelectOutPutPath.Name = "btn_SelectOutPutPath";
            this.btn_SelectOutPutPath.Size = new System.Drawing.Size(154, 30);
            this.btn_SelectOutPutPath.TabIndex = 0;
            this.btn_SelectOutPutPath.Text = "Select Output Directory";
            this.btn_SelectOutPutPath.UseVisualStyleBackColor = true;
            this.btn_SelectOutPutPath.Click += new System.EventHandler(this.btn_SelectOutPutPath_Click);
            // 
            // Label_OutPut
            // 
            this.Label_OutPut.AutoSize = true;
            this.Label_OutPut.Location = new System.Drawing.Point(12, 303);
            this.Label_OutPut.Name = "Label_OutPut";
            this.Label_OutPut.Size = new System.Drawing.Size(67, 13);
            this.Label_OutPut.TabIndex = 1;
            this.Label_OutPut.Text = "Output Path:";
            // 
            // txt_OutPutPath
            // 
            this.txt_OutPutPath.Enabled = false;
            this.txt_OutPutPath.Location = new System.Drawing.Point(12, 319);
            this.txt_OutPutPath.Name = "txt_OutPutPath";
            this.txt_OutPutPath.Size = new System.Drawing.Size(398, 20);
            this.txt_OutPutPath.TabIndex = 2;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 71);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(398, 229);
            this.checkedListBox1.TabIndex = 9;
            // 
            // frm_dm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 577);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.chkbox_AllowMultiPath);
            this.Controls.Add(this.label_Progress);
            this.Controls.Add(this.txt_status);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.chkbox_SkipFolders);
            this.Controls.Add(this.chkbox_DeleteWhenComplete);
            this.Controls.Add(this.txt_OutPutPath);
            this.Controls.Add(this.Label_OutPut);
            this.Controls.Add(this.Label_Exclusion);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.Label_Path);
            this.Controls.Add(this.btn_Compress);
            this.Controls.Add(this.btn_SelectOutPutPath);
            this.Controls.Add(this.btn_SelectExclusion);
            this.Controls.Add(this.btn_SelectDirectory);
            this.MaximizeBox = false;
            this.Name = "frm_dm";
            this.Text = "Archiver";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_SelectDirectory;
        private System.Windows.Forms.Label Label_Path;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Button btn_Compress;
        private System.Windows.Forms.CheckBox chkbox_DeleteWhenComplete;
        private System.Windows.Forms.CheckBox chkbox_SkipFolders;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txt_status;
        private System.Windows.Forms.Label Label_Exclusion;
        private System.Windows.Forms.Label label_Progress;
        private System.Windows.Forms.Button btn_SelectExclusion;
        private System.Windows.Forms.CheckBox chkbox_AllowMultiPath;
        private System.Windows.Forms.Button btn_SelectOutPutPath;
        private System.Windows.Forms.Label Label_OutPut;
        private System.Windows.Forms.TextBox txt_OutPutPath;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
    }
}

