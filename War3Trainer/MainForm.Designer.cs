namespace War3Trainer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.labGameScanState = new System.Windows.Forms.ToolStripLabel();
            this.cmdScanGame = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDebug1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSplit1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFunctions = new System.Windows.Forms.TreeView();
            this.cmdGetAllObjects = new System.Windows.Forms.Button();
            this.cmdModify = new System.Windows.Forms.Button();
            this.txtIntroduction = new System.Windows.Forms.TextBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.lblEmpty = new System.Windows.Forms.Label();
            this.viewData = new War3Trainer.ListViewEx();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOriginalValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colUnsavedValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtInput = new System.Windows.Forms.TextBox();
            this.toolContainer.ContentPanel.SuspendLayout();
            this.toolContainer.TopToolStripPanel.SuspendLayout();
            this.toolContainer.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.viewData.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolContainer
            // 
            // 
            // toolContainer.ContentPanel
            // 
            this.toolContainer.ContentPanel.Controls.Add(this.toolStripMain);
            this.toolContainer.ContentPanel.Size = new System.Drawing.Size(535, 26);
            this.toolContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolContainer.Location = new System.Drawing.Point(0, 0);
            this.toolContainer.Name = "toolContainer";
            this.toolContainer.Size = new System.Drawing.Size(535, 51);
            this.toolContainer.TabIndex = 0;
            this.toolContainer.Text = "toolStripContainer1";
            // 
            // toolContainer.TopToolStripPanel
            // 
            this.toolContainer.TopToolStripPanel.Controls.Add(this.menuMain);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labGameScanState,
            this.cmdScanGame,
            this.toolStripSeparator1});
            this.toolStripMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Padding = new System.Windows.Forms.Padding(3, 2, 0, 2);
            this.toolStripMain.Size = new System.Drawing.Size(535, 28);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // labGameScanState
            // 
            this.labGameScanState.Name = "labGameScanState";
            this.labGameScanState.Size = new System.Drawing.Size(68, 21);
            this.labGameScanState.Text = "游戏未运行";
            // 
            // cmdScanGame
            // 
            this.cmdScanGame.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdScanGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cmdScanGame.Image = ((System.Drawing.Image)(resources.GetObject("cmdScanGame.Image")));
            this.cmdScanGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdScanGame.Name = "cmdScanGame";
            this.cmdScanGame.Size = new System.Drawing.Size(60, 21);
            this.cmdScanGame.Text = "查找游戏";
            this.cmdScanGame.Click += new System.EventHandler(this.cmdScanGame_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 24);
            // 
            // menuMain
            // 
            this.menuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(535, 25);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDebug1,
            this.menuSplit1,
            this.menuFileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(58, 21);
            this.menuFile.Text = "文件(&F)";
            // 
            // menuDebug1
            // 
            this.menuDebug1.Name = "menuDebug1";
            this.menuDebug1.Size = new System.Drawing.Size(272, 22);
            this.menuDebug1.Text = "调试专用-ReadFromGameMemory";
            this.menuDebug1.Click += new System.EventHandler(this.menuDebug1_Click);
            // 
            // menuSplit1
            // 
            this.menuSplit1.Name = "menuSplit1";
            this.menuSplit1.Size = new System.Drawing.Size(269, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.Size = new System.Drawing.Size(272, 22);
            this.menuFileExit.Text = "退出(&X)";
            this.menuFileExit.Click += new System.EventHandler(this.MenuFileExit_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuHelpAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(61, 21);
            this.menuHelp.Text = "帮助(&H)";
            // 
            // menuHelpAbout
            // 
            this.menuHelpAbout.Name = "menuHelpAbout";
            this.menuHelpAbout.Size = new System.Drawing.Size(152, 22);
            this.menuHelpAbout.Text = "关于修改器(&A)";
            this.menuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
            // 
            // viewFunctions
            // 
            this.viewFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewFunctions.HideSelection = false;
            this.viewFunctions.Location = new System.Drawing.Point(0, 0);
            this.viewFunctions.Name = "viewFunctions";
            this.viewFunctions.Size = new System.Drawing.Size(191, 341);
            this.viewFunctions.TabIndex = 3;
            this.viewFunctions.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.viewFunctions_BeforeSelect);
            // 
            // cmdGetAllObjects
            // 
            this.cmdGetAllObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdGetAllObjects.Location = new System.Drawing.Point(367, 411);
            this.cmdGetAllObjects.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.cmdGetAllObjects.Name = "cmdGetAllObjects";
            this.cmdGetAllObjects.Size = new System.Drawing.Size(75, 23);
            this.cmdGetAllObjects.TabIndex = 4;
            this.cmdGetAllObjects.Text = "刷新";
            this.cmdGetAllObjects.UseVisualStyleBackColor = true;
            this.cmdGetAllObjects.Click += new System.EventHandler(this.cmdGetAllObjects_Click);
            // 
            // cmdModify
            // 
            this.cmdModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdModify.Location = new System.Drawing.Point(448, 411);
            this.cmdModify.Margin = new System.Windows.Forms.Padding(3, 9, 3, 3);
            this.cmdModify.Name = "cmdModify";
            this.cmdModify.Size = new System.Drawing.Size(75, 23);
            this.cmdModify.TabIndex = 5;
            this.cmdModify.Text = "修改";
            this.cmdModify.UseVisualStyleBackColor = true;
            this.cmdModify.Click += new System.EventHandler(this.cmdModify_Click);
            // 
            // txtIntroduction
            // 
            this.txtIntroduction.Location = new System.Drawing.Point(8, 14);
            this.txtIntroduction.Multiline = true;
            this.txtIntroduction.Name = "txtIntroduction";
            this.txtIntroduction.ReadOnly = true;
            this.txtIntroduction.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIntroduction.Size = new System.Drawing.Size(139, 53);
            this.txtIntroduction.TabIndex = 6;
            this.txtIntroduction.Text = resources.GetString("txtIntroduction.Text");
            // 
            // splitMain
            // 
            this.splitMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitMain.Location = new System.Drawing.Point(8, 58);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.viewFunctions);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.lblEmpty);
            this.splitMain.Panel2.Controls.Add(this.viewData);
            this.splitMain.Panel2.Controls.Add(this.txtIntroduction);
            this.splitMain.Size = new System.Drawing.Size(515, 341);
            this.splitMain.SplitterDistance = 191;
            this.splitMain.SplitterWidth = 6;
            this.splitMain.TabIndex = 7;
            // 
            // lblEmpty
            // 
            this.lblEmpty.AutoSize = true;
            this.lblEmpty.Location = new System.Drawing.Point(153, 14);
            this.lblEmpty.Name = "lblEmpty";
            this.lblEmpty.Size = new System.Drawing.Size(113, 36);
            this.lblEmpty.TabIndex = 8;
            this.lblEmpty.Text = "没有可修改的项目，\r\n请在左侧功能列表中\r\n选择一个修改项。";
            // 
            // viewData
            // 
            this.viewData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colOriginalValue,
            this.colUnsavedValue});
            this.viewData.Controls.Add(this.txtInput);
            this.viewData.FullRowSelect = true;
            this.viewData.GridLines = true;
            this.viewData.HideSelection = false;
            this.viewData.Location = new System.Drawing.Point(8, 73);
            this.viewData.MultiSelect = false;
            this.viewData.Name = "viewData";
            this.viewData.Size = new System.Drawing.Size(302, 134);
            this.viewData.TabIndex = 9;
            this.viewData.UseCompatibleStateImageBehavior = false;
            this.viewData.View = System.Windows.Forms.View.Details;
            this.viewData.Scrolling += new System.EventHandler(this.viewData_Scrolling);
            this.viewData.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.viewData_ColumnWidthChanging);
            this.viewData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.viewData_KeyPress);
            this.viewData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewData_MouseUp);
            // 
            // colName
            // 
            this.colName.Text = "修改项目名称";
            this.colName.Width = 130;
            // 
            // colOriginalValue
            // 
            this.colOriginalValue.Text = "原值";
            this.colOriginalValue.Width = 80;
            // 
            // colUnsavedValue
            // 
            this.colUnsavedValue.Text = "目标数值";
            this.colUnsavedValue.Width = 80;
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(150, 40);
            this.txtInput.Margin = new System.Windows.Forms.Padding(0);
            this.txtInput.MaximumSize = new System.Drawing.Size(32768, 16);
            this.txtInput.MaxLength = 10;
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(100, 21);
            this.txtInput.TabIndex = 10;
            this.txtInput.Text = "数值在这里";
            this.txtInput.Visible = false;
            this.txtInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);
            this.txtInput.Leave += new System.EventHandler(this.txtInput_Leave);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(535, 446);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.cmdModify);
            this.Controls.Add(this.cmdGetAllObjects);
            this.Controls.Add(this.toolContainer);
            this.Name = "FrmMain";
            this.Text = "魔兽3内存修改器";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.toolContainer.ContentPanel.ResumeLayout(false);
            this.toolContainer.ContentPanel.PerformLayout();
            this.toolContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolContainer.TopToolStripPanel.PerformLayout();
            this.toolContainer.ResumeLayout(false);
            this.toolContainer.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.viewData.ResumeLayout(false);
            this.viewData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolContainer;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuHelpAbout;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripLabel labGameScanState;
        private System.Windows.Forms.TreeView viewFunctions;
        private System.Windows.Forms.Button cmdGetAllObjects;
        private System.Windows.Forms.Button cmdModify;
        private ListViewEx viewData;
        private System.Windows.Forms.ToolStripButton cmdScanGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colOriginalValue;
        private System.Windows.Forms.ColumnHeader colUnsavedValue;
        private System.Windows.Forms.TextBox txtIntroduction;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.ToolStripMenuItem menuDebug1;
        private System.Windows.Forms.ToolStripSeparator menuSplit1;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Label lblEmpty;
    }
}

