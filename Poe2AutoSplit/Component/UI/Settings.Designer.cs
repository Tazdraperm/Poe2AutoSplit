namespace Poe2AutoSplit.Component.UI
{
    partial class Settings
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.logPathLabel = new System.Windows.Forms.Label();
            this.enabledCheckbox = new System.Windows.Forms.CheckBox();
            this.logPathTextbox = new System.Windows.Forms.TextBox();
            this.generateSplitsButton = new System.Windows.Forms.Button();
            this.browseLogFileButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.checkedSplitEventList = new System.Windows.Forms.CheckedListBox();
            this.selectAllCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // logPathLabel
            // 
            this.logPathLabel.AutoSize = true;
            this.logPathLabel.Location = new System.Drawing.Point(10, 293);
            this.logPathLabel.Name = "logPathLabel";
            this.logPathLabel.Size = new System.Drawing.Size(90, 13);
            this.logPathLabel.TabIndex = 1;
            this.logPathLabel.Text = "Client.txt location:";
            this.logPathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // enabledCheckbox
            // 
            this.enabledCheckbox.AutoSize = true;
            this.enabledCheckbox.Location = new System.Drawing.Point(10, 12);
            this.enabledCheckbox.Name = "enabledCheckbox";
            this.enabledCheckbox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.enabledCheckbox.Size = new System.Drawing.Size(127, 17);
            this.enabledCheckbox.TabIndex = 2;
            this.enabledCheckbox.Text = ":Enable Auto Splitting";
            this.enabledCheckbox.UseVisualStyleBackColor = true;
            this.enabledCheckbox.CheckedChanged += new System.EventHandler(this.enabledCheckbox_CheckedChanged);
            // 
            // logPathTextbox
            // 
            this.logPathTextbox.Location = new System.Drawing.Point(13, 309);
            this.logPathTextbox.Name = "logPathTextbox";
            this.logPathTextbox.Size = new System.Drawing.Size(282, 20);
            this.logPathTextbox.TabIndex = 3;
            this.logPathTextbox.TextChanged += new System.EventHandler(this.logPathTextbox_TextChanged);
            // 
            // generateSplitsButton
            // 
            this.generateSplitsButton.Location = new System.Drawing.Point(157, 248);
            this.generateSplitsButton.Name = "generateSplitsButton";
            this.generateSplitsButton.Size = new System.Drawing.Size(120, 23);
            this.generateSplitsButton.TabIndex = 6;
            this.generateSplitsButton.Text = "Generate Splits";
            this.generateSplitsButton.UseVisualStyleBackColor = true;
            this.generateSplitsButton.Click += new System.EventHandler(this.generateSplitsButton_Click);
            // 
            // browseLogFileButton
            // 
            this.browseLogFileButton.Location = new System.Drawing.Point(304, 308);
            this.browseLogFileButton.Name = "browseLogFileButton";
            this.browseLogFileButton.Size = new System.Drawing.Size(80, 23);
            this.browseLogFileButton.TabIndex = 8;
            this.browseLogFileButton.Text = "Browse";
            this.browseLogFileButton.UseVisualStyleBackColor = true;
            this.browseLogFileButton.Click += new System.EventHandler(this.browseLogFileButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Text files|*.txt";
            // 
            // checkedSplitEventList
            // 
            this.checkedSplitEventList.CheckOnClick = true;
            this.checkedSplitEventList.FormattingEnabled = true;
            this.checkedSplitEventList.Location = new System.Drawing.Point(10, 42);
            this.checkedSplitEventList.Name = "checkedSplitEventList";
            this.checkedSplitEventList.Size = new System.Drawing.Size(285, 199);
            this.checkedSplitEventList.TabIndex = 11;
            this.checkedSplitEventList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedSplitEventList_ItemCheck);
            // 
            // selectAllCheckbox
            // 
            this.selectAllCheckbox.AutoSize = true;
            this.selectAllCheckbox.Location = new System.Drawing.Point(13, 252);
            this.selectAllCheckbox.Name = "selectAllCheckbox";
            this.selectAllCheckbox.Size = new System.Drawing.Size(117, 17);
            this.selectAllCheckbox.TabIndex = 12;
            this.selectAllCheckbox.Text = "Select/Deselect All";
            this.selectAllCheckbox.ThreeState = true;
            this.selectAllCheckbox.UseVisualStyleBackColor = true;
            this.selectAllCheckbox.CheckedChanged += new System.EventHandler(this.selectAllCheckbox_CheckedChanged);
            this.selectAllCheckbox.Click += new System.EventHandler(this.selectAllCheckbox_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.selectAllCheckbox);
            this.Controls.Add(this.checkedSplitEventList);
            this.Controls.Add(this.browseLogFileButton);
            this.Controls.Add(this.generateSplitsButton);
            this.Controls.Add(this.logPathTextbox);
            this.Controls.Add(this.enabledCheckbox);
            this.Controls.Add(this.logPathLabel);
            this.Name = "Settings";
            this.Size = new System.Drawing.Size(412, 374);
            this.Load += new System.EventHandler(this.SettingsControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label logPathLabel;
        private System.Windows.Forms.CheckBox enabledCheckbox;
        private System.Windows.Forms.TextBox logPathTextbox;
        private System.Windows.Forms.Button generateSplitsButton;
        private System.Windows.Forms.Button browseLogFileButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckedListBox checkedSplitEventList;
        private System.Windows.Forms.CheckBox selectAllCheckbox;
    }
}
