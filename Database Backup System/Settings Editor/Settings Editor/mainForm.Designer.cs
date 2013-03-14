namespace Settings_Editor
{
    partial class mainForm
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
            this.infoStrip = new System.Windows.Forms.StatusStrip();
            this.currDBStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.scrollBar = new System.Windows.Forms.VScrollBar();
            this.sideMenuBox = new System.Windows.Forms.Label();
            this.summaryLabel = new System.Windows.Forms.Label();
            this.dbInfoBox = new System.Windows.Forms.Label();
            this.dbInfoHeader = new System.Windows.Forms.Label();
            this.fileExtensionHeader = new System.Windows.Forms.Label();
            this.fileExtensionBox = new System.Windows.Forms.Label();
            this.rarInfoHeader = new System.Windows.Forms.Label();
            this.rarInfoBox = new System.Windows.Forms.Label();
            this.copyInfoHeader = new System.Windows.Forms.Label();
            this.copyInfoBox = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFile = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.databasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dbOneTagLabel = new System.Windows.Forms.Label();
            this.dbTwoTagLabel = new System.Windows.Forms.Label();
            this.dbThreeTagLabel = new System.Windows.Forms.Label();
            this.dbTagSixLabel = new System.Windows.Forms.Label();
            this.dbFiveTagLabel = new System.Windows.Forms.Label();
            this.dbFourTagLabel = new System.Windows.Forms.Label();
            this.dbTagNineLabel = new System.Windows.Forms.Label();
            this.dbTagEightLabel = new System.Windows.Forms.Label();
            this.dbTagSevenLabel = new System.Windows.Forms.Label();
            this.dbTagTenLabel = new System.Windows.Forms.Label();
            this.saveLocationBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.infoStrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoStrip
            // 
            this.infoStrip.AutoSize = false;
            this.infoStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.currDBStrip});
            this.infoStrip.Location = new System.Drawing.Point(0, 546);
            this.infoStrip.Name = "infoStrip";
            this.infoStrip.Size = new System.Drawing.Size(792, 20);
            this.infoStrip.TabIndex = 0;
            this.infoStrip.Text = "statusStrip1";
            // 
            // currDBStrip
            // 
            this.currDBStrip.Name = "currDBStrip";
            this.currDBStrip.Size = new System.Drawing.Size(109, 15);
            this.currDBStrip.Text = "Select A Database...";
            // 
            // scrollBar
            // 
            this.scrollBar.Location = new System.Drawing.Point(772, 22);
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size(20, 524);
            this.scrollBar.TabIndex = 2;
            this.scrollBar.Visible = false;
            this.scrollBar.ValueChanged += new System.EventHandler(this.scrollBar_ValueChanged);
            // 
            // sideMenuBox
            // 
            this.sideMenuBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.sideMenuBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sideMenuBox.Location = new System.Drawing.Point(0, 25);
            this.sideMenuBox.Name = "sideMenuBox";
            this.sideMenuBox.Size = new System.Drawing.Size(275, 521);
            this.sideMenuBox.TabIndex = 3;
            // 
            // summaryLabel
            // 
            this.summaryLabel.Location = new System.Drawing.Point(0, 0);
            this.summaryLabel.Name = "summaryLabel";
            this.summaryLabel.Size = new System.Drawing.Size(100, 23);
            this.summaryLabel.TabIndex = 20;
            // 
            // dbInfoBox
            // 
            this.dbInfoBox.BackColor = System.Drawing.Color.OldLace;
            this.dbInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbInfoBox.Location = new System.Drawing.Point(281, 43);
            this.dbInfoBox.Name = "dbInfoBox";
            this.dbInfoBox.Size = new System.Drawing.Size(488, 15);
            this.dbInfoBox.TabIndex = 11;
            // 
            // dbInfoHeader
            // 
            this.dbInfoHeader.BackColor = System.Drawing.SystemColors.HighlightText;
            this.dbInfoHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbInfoHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dbInfoHeader.Location = new System.Drawing.Point(301, 35);
            this.dbInfoHeader.Name = "dbInfoHeader";
            this.dbInfoHeader.Size = new System.Drawing.Size(110, 16);
            this.dbInfoHeader.TabIndex = 12;
            this.dbInfoHeader.Text = "Database Information";
            this.dbInfoHeader.Click += new System.EventHandler(this.dbInfoHeader_Click);
            // 
            // fileExtensionHeader
            // 
            this.fileExtensionHeader.BackColor = System.Drawing.SystemColors.HighlightText;
            this.fileExtensionHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileExtensionHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileExtensionHeader.Location = new System.Drawing.Point(301, 61);
            this.fileExtensionHeader.Name = "fileExtensionHeader";
            this.fileExtensionHeader.Size = new System.Drawing.Size(79, 16);
            this.fileExtensionHeader.TabIndex = 14;
            this.fileExtensionHeader.Text = "File Extensions";
            this.fileExtensionHeader.Click += new System.EventHandler(this.fileExtensionHeader_Click);
            // 
            // fileExtensionBox
            // 
            this.fileExtensionBox.BackColor = System.Drawing.Color.OldLace;
            this.fileExtensionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fileExtensionBox.Location = new System.Drawing.Point(281, 69);
            this.fileExtensionBox.Name = "fileExtensionBox";
            this.fileExtensionBox.Size = new System.Drawing.Size(488, 15);
            this.fileExtensionBox.TabIndex = 13;
            // 
            // rarInfoHeader
            // 
            this.rarInfoHeader.BackColor = System.Drawing.SystemColors.HighlightText;
            this.rarInfoHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rarInfoHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rarInfoHeader.Location = new System.Drawing.Point(301, 87);
            this.rarInfoHeader.Name = "rarInfoHeader";
            this.rarInfoHeader.Size = new System.Drawing.Size(81, 16);
            this.rarInfoHeader.TabIndex = 16;
            this.rarInfoHeader.Text = "Rar Information";
            this.rarInfoHeader.Click += new System.EventHandler(this.rarInfoHeader_Click);
            // 
            // rarInfoBox
            // 
            this.rarInfoBox.BackColor = System.Drawing.Color.OldLace;
            this.rarInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rarInfoBox.Location = new System.Drawing.Point(281, 95);
            this.rarInfoBox.Name = "rarInfoBox";
            this.rarInfoBox.Size = new System.Drawing.Size(488, 15);
            this.rarInfoBox.TabIndex = 15;
            // 
            // copyInfoHeader
            // 
            this.copyInfoHeader.BackColor = System.Drawing.SystemColors.HighlightText;
            this.copyInfoHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.copyInfoHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.copyInfoHeader.Location = new System.Drawing.Point(301, 113);
            this.copyInfoHeader.Name = "copyInfoHeader";
            this.copyInfoHeader.Size = new System.Drawing.Size(88, 16);
            this.copyInfoHeader.TabIndex = 18;
            this.copyInfoHeader.Text = "Copy Information";
            this.copyInfoHeader.Click += new System.EventHandler(this.copyInfoHeader_Click);
            // 
            // copyInfoBox
            // 
            this.copyInfoBox.BackColor = System.Drawing.Color.OldLace;
            this.copyInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.copyInfoBox.Location = new System.Drawing.Point(281, 121);
            this.copyInfoBox.Name = "copyInfoBox";
            this.copyInfoBox.Size = new System.Drawing.Size(488, 15);
            this.copyInfoBox.TabIndex = 17;
            // 
            // mainMenu
            // 
            this.mainMenu.AutoSize = false;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.databasesToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(792, 25);
            this.mainMenu.TabIndex = 19;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFile,
            this.loadFile,
            this.saveFile,
            this.exitMenu});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFile
            // 
            this.newFile.Name = "newFile";
            this.newFile.Size = new System.Drawing.Size(152, 22);
            this.newFile.Text = "New...";
            this.newFile.Click += new System.EventHandler(this.newFile_Click);
            // 
            // loadFile
            // 
            this.loadFile.Name = "loadFile";
            this.loadFile.Size = new System.Drawing.Size(152, 22);
            this.loadFile.Text = "Load...";
            this.loadFile.Click += new System.EventHandler(this.loadFile_Click);
            // 
            // saveFile
            // 
            this.saveFile.Name = "saveFile";
            this.saveFile.Size = new System.Drawing.Size(152, 22);
            this.saveFile.Text = "Save";
            this.saveFile.Click += new System.EventHandler(this.saveFile_Click);
            // 
            // exitMenu
            // 
            this.exitMenu.Name = "exitMenu";
            this.exitMenu.Size = new System.Drawing.Size(152, 22);
            this.exitMenu.Text = "Exit";
            // 
            // databasesToolStripMenuItem
            // 
            this.databasesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addDatabaseToolStripMenuItem,
            this.removeDatabaseToolStripMenuItem,
            this.clearDatabaseToolStripMenuItem});
            this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
            this.databasesToolStripMenuItem.Size = new System.Drawing.Size(72, 21);
            this.databasesToolStripMenuItem.Text = "Databases";
            // 
            // addDatabaseToolStripMenuItem
            // 
            this.addDatabaseToolStripMenuItem.Name = "addDatabaseToolStripMenuItem";
            this.addDatabaseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.addDatabaseToolStripMenuItem.Text = "Add Database";
            this.addDatabaseToolStripMenuItem.Click += new System.EventHandler(this.addDatabaseToolStripMenuItem_Click);
            // 
            // removeDatabaseToolStripMenuItem
            // 
            this.removeDatabaseToolStripMenuItem.Name = "removeDatabaseToolStripMenuItem";
            this.removeDatabaseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.removeDatabaseToolStripMenuItem.Text = "Remove Database";
            this.removeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.removeDatabaseToolStripMenuItem_Click);
            // 
            // clearDatabaseToolStripMenuItem
            // 
            this.clearDatabaseToolStripMenuItem.Name = "clearDatabaseToolStripMenuItem";
            this.clearDatabaseToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.clearDatabaseToolStripMenuItem.Text = "Clear Database";
            this.clearDatabaseToolStripMenuItem.Click += new System.EventHandler(this.clearDatabaseToolStripMenuItem_Click);
            // 
            // dbOneTagLabel
            // 
            this.dbOneTagLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbOneTagLabel.Location = new System.Drawing.Point(0, 25);
            this.dbOneTagLabel.Name = "dbOneTagLabel";
            this.dbOneTagLabel.Size = new System.Drawing.Size(265, 25);
            this.dbOneTagLabel.TabIndex = 21;
            this.dbOneTagLabel.Text = "Database 1";
            this.dbOneTagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbOneTagLabel.Click += new System.EventHandler(this.dbOneTagLabel_Click);
            // 
            // dbTwoTagLabel
            // 
            this.dbTwoTagLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbTwoTagLabel.Location = new System.Drawing.Point(0, 49);
            this.dbTwoTagLabel.Name = "dbTwoTagLabel";
            this.dbTwoTagLabel.Size = new System.Drawing.Size(265, 25);
            this.dbTwoTagLabel.TabIndex = 22;
            this.dbTwoTagLabel.Text = "Database 2";
            this.dbTwoTagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbTwoTagLabel.Click += new System.EventHandler(this.dbTwoTagLabel_Click);
            // 
            // dbThreeTagLabel
            // 
            this.dbThreeTagLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbThreeTagLabel.Location = new System.Drawing.Point(0, 73);
            this.dbThreeTagLabel.Name = "dbThreeTagLabel";
            this.dbThreeTagLabel.Size = new System.Drawing.Size(265, 25);
            this.dbThreeTagLabel.TabIndex = 23;
            this.dbThreeTagLabel.Text = "Database 3";
            this.dbThreeTagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbThreeTagLabel.Click += new System.EventHandler(this.dbThreeTagLabel_Click);
            // 
            // dbTagSixLabel
            // 
            this.dbTagSixLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbTagSixLabel.Location = new System.Drawing.Point(0, 145);
            this.dbTagSixLabel.Name = "dbTagSixLabel";
            this.dbTagSixLabel.Size = new System.Drawing.Size(265, 25);
            this.dbTagSixLabel.TabIndex = 26;
            this.dbTagSixLabel.Text = "Database 6";
            this.dbTagSixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbTagSixLabel.Click += new System.EventHandler(this.dbTagSixLabel_Click);
            // 
            // dbFiveTagLabel
            // 
            this.dbFiveTagLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbFiveTagLabel.Location = new System.Drawing.Point(0, 121);
            this.dbFiveTagLabel.Name = "dbFiveTagLabel";
            this.dbFiveTagLabel.Size = new System.Drawing.Size(265, 25);
            this.dbFiveTagLabel.TabIndex = 25;
            this.dbFiveTagLabel.Text = "Database 5";
            this.dbFiveTagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbFiveTagLabel.Click += new System.EventHandler(this.dbFiveTagLabel_Click);
            // 
            // dbFourTagLabel
            // 
            this.dbFourTagLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbFourTagLabel.Location = new System.Drawing.Point(0, 97);
            this.dbFourTagLabel.Name = "dbFourTagLabel";
            this.dbFourTagLabel.Size = new System.Drawing.Size(265, 25);
            this.dbFourTagLabel.TabIndex = 24;
            this.dbFourTagLabel.Text = "Database 4";
            this.dbFourTagLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbFourTagLabel.Click += new System.EventHandler(this.dbFourTagLabel_Click);
            // 
            // dbTagNineLabel
            // 
            this.dbTagNineLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbTagNineLabel.Location = new System.Drawing.Point(0, 217);
            this.dbTagNineLabel.Name = "dbTagNineLabel";
            this.dbTagNineLabel.Size = new System.Drawing.Size(265, 25);
            this.dbTagNineLabel.TabIndex = 29;
            this.dbTagNineLabel.Text = "Database 9";
            this.dbTagNineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbTagNineLabel.Click += new System.EventHandler(this.dbTagNineLabel_Click);
            // 
            // dbTagEightLabel
            // 
            this.dbTagEightLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbTagEightLabel.Location = new System.Drawing.Point(0, 193);
            this.dbTagEightLabel.Name = "dbTagEightLabel";
            this.dbTagEightLabel.Size = new System.Drawing.Size(265, 25);
            this.dbTagEightLabel.TabIndex = 28;
            this.dbTagEightLabel.Text = "Database 8";
            this.dbTagEightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbTagEightLabel.Click += new System.EventHandler(this.dbTagEightLabel_Click);
            // 
            // dbTagSevenLabel
            // 
            this.dbTagSevenLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbTagSevenLabel.Location = new System.Drawing.Point(0, 169);
            this.dbTagSevenLabel.Name = "dbTagSevenLabel";
            this.dbTagSevenLabel.Size = new System.Drawing.Size(265, 25);
            this.dbTagSevenLabel.TabIndex = 27;
            this.dbTagSevenLabel.Text = "Database 7";
            this.dbTagSevenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbTagSevenLabel.Click += new System.EventHandler(this.dbTagSevenLabel_Click);
            // 
            // dbTagTenLabel
            // 
            this.dbTagTenLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dbTagTenLabel.Location = new System.Drawing.Point(0, 241);
            this.dbTagTenLabel.Name = "dbTagTenLabel";
            this.dbTagTenLabel.Size = new System.Drawing.Size(265, 25);
            this.dbTagTenLabel.TabIndex = 30;
            this.dbTagTenLabel.Text = "Database 10";
            this.dbTagTenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dbTagTenLabel.Click += new System.EventHandler(this.dbTagTenLabel_Click);
            // 
            // saveLocationBrowser
            // 
            this.saveLocationBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.dbTagTenLabel);
            this.Controls.Add(this.dbTagNineLabel);
            this.Controls.Add(this.dbTagEightLabel);
            this.Controls.Add(this.dbTagSevenLabel);
            this.Controls.Add(this.dbTagSixLabel);
            this.Controls.Add(this.dbFiveTagLabel);
            this.Controls.Add(this.dbFourTagLabel);
            this.Controls.Add(this.dbThreeTagLabel);
            this.Controls.Add(this.dbTwoTagLabel);
            this.Controls.Add(this.dbOneTagLabel);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.infoStrip);
            this.Controls.Add(this.scrollBar);
            this.Controls.Add(this.copyInfoHeader);
            this.Controls.Add(this.rarInfoHeader);
            this.Controls.Add(this.fileExtensionHeader);
            this.Controls.Add(this.dbInfoHeader);
            this.Controls.Add(this.summaryLabel);
            this.Controls.Add(this.copyInfoBox);
            this.Controls.Add(this.rarInfoBox);
            this.Controls.Add(this.fileExtensionBox);
            this.Controls.Add(this.dbInfoBox);
            this.Controls.Add(this.sideMenuBox);
            this.Location = new System.Drawing.Point(100, 100);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "mainForm";
            this.Text = "Settings Configurator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Resize += new System.EventHandler(this.mainForm_Resize);
            this.infoStrip.ResumeLayout(false);
            this.infoStrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip infoStrip;
        private System.Windows.Forms.ToolStripStatusLabel currDBStrip;
        private System.Windows.Forms.VScrollBar scrollBar;
        private System.Windows.Forms.Label sideMenuBox;
        private System.Windows.Forms.Label summaryLabel;
        private System.Windows.Forms.Label dbInfoBox;
        private System.Windows.Forms.Label dbInfoHeader;
        private System.Windows.Forms.Label fileExtensionHeader;
        private System.Windows.Forms.Label fileExtensionBox;
        private System.Windows.Forms.Label rarInfoHeader;
        private System.Windows.Forms.Label rarInfoBox;
        private System.Windows.Forms.Label copyInfoHeader;
        private System.Windows.Forms.Label copyInfoBox;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFile;
        private System.Windows.Forms.ToolStripMenuItem loadFile;
        private System.Windows.Forms.ToolStripMenuItem saveFile;
        private System.Windows.Forms.ToolStripMenuItem exitMenu;
        private System.Windows.Forms.ToolStripMenuItem databasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearDatabaseToolStripMenuItem;
        private System.Windows.Forms.Label dbOneTagLabel;
        private System.Windows.Forms.Label dbTwoTagLabel;
        private System.Windows.Forms.Label dbThreeTagLabel;
        private System.Windows.Forms.Label dbTagSixLabel;
        private System.Windows.Forms.Label dbFiveTagLabel;
        private System.Windows.Forms.Label dbFourTagLabel;
        private System.Windows.Forms.Label dbTagNineLabel;
        private System.Windows.Forms.Label dbTagEightLabel;
        private System.Windows.Forms.Label dbTagSevenLabel;
        private System.Windows.Forms.Label dbTagTenLabel;
        private System.Windows.Forms.FolderBrowserDialog saveLocationBrowser;
    }
}

