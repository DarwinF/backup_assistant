namespace MessCleaner
{
    partial class configBrowser
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
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.configBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.acceptButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.browseButton = new System.Windows.Forms.Button();
            this.launchEditorButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // fileLocationTextBox
            // 
            this.fileLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileLocationTextBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.fileLocationTextBox.Location = new System.Drawing.Point(12, 11);
            this.fileLocationTextBox.Name = "fileLocationTextBox";
            this.fileLocationTextBox.Size = new System.Drawing.Size(408, 26);
            this.fileLocationTextBox.TabIndex = 1;
            this.fileLocationTextBox.Text = "Configuration File Location";
            // 
            // configBrowserDialog
            // 
            this.configBrowserDialog.ShowNewFolderButton = false;
            // 
            // acceptButton
            // 
            this.acceptButton.Enabled = false;
            this.acceptButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acceptButton.Location = new System.Drawing.Point(68, 43);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(81, 27);
            this.acceptButton.TabIndex = 2;
            this.acceptButton.Text = "Accept";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelButton.Location = new System.Drawing.Point(384, 43);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(81, 27);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Exit";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // browseButton
            // 
            this.browseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browseButton.Location = new System.Drawing.Point(426, 11);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(39, 27);
            this.browseButton.TabIndex = 4;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // launchEditorButton
            // 
            this.launchEditorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchEditorButton.Location = new System.Drawing.Point(155, 43);
            this.launchEditorButton.Name = "launchEditorButton";
            this.launchEditorButton.Size = new System.Drawing.Size(119, 27);
            this.launchEditorButton.TabIndex = 5;
            this.launchEditorButton.Text = "Launch Editor";
            this.launchEditorButton.UseVisualStyleBackColor = true;
            this.launchEditorButton.Click += new System.EventHandler(this.launchEditorButton_Click);
            // 
            // configBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 80);
            this.ControlBox = false;
            this.Controls.Add(this.launchEditorButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.fileLocationTextBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "configBrowser";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Browse To Configuration File";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox fileLocationTextBox;
        private System.Windows.Forms.FolderBrowserDialog configBrowserDialog;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button launchEditorButton;
    }
}