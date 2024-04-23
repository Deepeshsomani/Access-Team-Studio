
using AccessTeamStudio.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace AccessTeamStudio
{
    partial class AccessTeamStudio
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessTeamStudio));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLoadEntities = new System.Windows.Forms.ToolStripButton();
            this.btnListTeammembershipRecords = new System.Windows.Forms.ToolStripButton();
            this.lvAttributes = new System.Windows.Forms.ListView();
            this.txt_SearchEntity = new System.Windows.Forms.TextBox();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.lbSearch = new System.Windows.Forms.Label();
            this.txtSearchEntity = new System.Windows.Forms.TextBox();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.gbEntities = new System.Windows.Forms.GroupBox();
            this.lvEntities = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbRecords = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnNextPage = new System.Windows.Forms.ToolStripButton();
            this.lblTotalPagesCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CurrentPage = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.lblSelectedRows = new System.Windows.Forms.ToolStripLabel();
            this.TotalRecordsAvailable = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSelectAllRecords = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportRecords = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRemoveSelectedRecords = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblExportFlag = new System.Windows.Forms.ToolStripLabel();
            this.cbUser = new System.Windows.Forms.ComboBox();
            this.lUsers = new System.Windows.Forms.Label();
            this.gbUsers = new System.Windows.Forms.GroupBox();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtRecordId = new System.Windows.Forms.TextBox();
            this.lblRecordId = new System.Windows.Forms.Label();
            this.txtPrimaryAttributeValue = new System.Windows.Forms.TextBox();
            this.lblprimaryAttributeValue = new System.Windows.Forms.Label();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.SelectRows = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PrimaryAttribute = new System.Windows.Forms.DataGridViewLinkColumn();
            this.RecordId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.WriteAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.AppendAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.AppendToAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.DeleteAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.ShareAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.AssignAccess = new System.Windows.Forms.DataGridViewImageColumn();
            this.CopyURLButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeamTemplateId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SelectFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WriteAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppendAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppendToAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShareAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssignAccessFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CellFlag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tsMain.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.gbEntities.SuspendLayout();
            this.gbRecords.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.gbUsers.SuspendLayout();
            this.gbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbLoadEntities,
            this.btnListTeammembershipRecords});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1266, 27);
            this.tsMain.TabIndex = 4;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Image = ((System.Drawing.Image)(resources.GetObject("tsbClose.Image")));
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(107, 24);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbLoadEntities
            // 
            this.tsbLoadEntities.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoadEntities.Image")));
            this.tsbLoadEntities.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbLoadEntities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoadEntities.Name = "tsbLoadEntities";
            this.tsbLoadEntities.Size = new System.Drawing.Size(114, 24);
            this.tsbLoadEntities.Text = "Load Entities";
            this.tsbLoadEntities.ToolTipText = "Click to Load Entities with Access Team Enabled";
            this.tsbLoadEntities.Click += new System.EventHandler(this.tsbLoadEntities_Click);
            // 
            // btnListTeammembershipRecords
            // 
            this.btnListTeammembershipRecords.Enabled = false;
            this.btnListTeammembershipRecords.Image = ((System.Drawing.Image)(resources.GetObject("btnListTeammembershipRecords.Image")));
            this.btnListTeammembershipRecords.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnListTeammembershipRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnListTeammembershipRecords.Name = "btnListTeammembershipRecords";
            this.btnListTeammembershipRecords.Size = new System.Drawing.Size(172, 24);
            this.btnListTeammembershipRecords.Text = "List Shared Records";
            this.btnListTeammembershipRecords.ToolTipText = "Click to List Shared Records of selected Entity";
            this.btnListTeammembershipRecords.Click += new System.EventHandler(this.btnListSharedRecords_Click);
            // 
            // lvAttributes
            // 
            this.lvAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvAttributes.FullRowSelect = true;
            this.lvAttributes.HideSelection = false;
            this.lvAttributes.Location = new System.Drawing.Point(200, 200);
            this.lvAttributes.Margin = new System.Windows.Forms.Padding(4);
            this.lvAttributes.MultiSelect = false;
            this.lvAttributes.Name = "lvAttributes";
            this.lvAttributes.Size = new System.Drawing.Size(121, 200);
            this.lvAttributes.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvAttributes.TabIndex = 0;
            this.lvAttributes.UseCompatibleStateImageBehavior = false;
            this.lvAttributes.View = System.Windows.Forms.View.Details;
            // 
            // txt_SearchEntity
            // 
            this.txt_SearchEntity.Location = new System.Drawing.Point(134, 32);
            this.txt_SearchEntity.Name = "txt_SearchEntity";
            this.txt_SearchEntity.Size = new System.Drawing.Size(100, 22);
            this.txt_SearchEntity.TabIndex = 8;
            // 
            // searchPanel
            // 
            this.searchPanel.Controls.Add(this.lbSearch);
            this.searchPanel.Controls.Add(this.txtSearchEntity);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(5, 21);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Padding = new System.Windows.Forms.Padding(5);
            this.searchPanel.Size = new System.Drawing.Size(914, 66);
            this.searchPanel.TabIndex = 3;
            // 
            // lbSearch
            // 
            this.lbSearch.AutoSize = true;
            this.lbSearch.Location = new System.Drawing.Point(9, 21);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(53, 17);
            this.lbSearch.TabIndex = 2;
            this.lbSearch.Text = "Search";
            // 
            // txtSearchEntity
            // 
            this.txtSearchEntity.Location = new System.Drawing.Point(65, 18);
            this.txtSearchEntity.Name = "txtSearchEntity";
            this.txtSearchEntity.Size = new System.Drawing.Size(208, 23);
            this.txtSearchEntity.TabIndex = 1;
            this.txtSearchEntity.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnSearchKeyUp);
            // 
            // gbEntities
            // 
            this.gbEntities.Controls.Add(this.searchPanel);
            this.gbEntities.Controls.Add(this.lvEntities);
            this.gbEntities.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbEntities.Enabled = false;
            this.gbEntities.Location = new System.Drawing.Point(0, 27);
            this.gbEntities.Name = "gbEntities";
            this.gbEntities.Padding = new System.Windows.Forms.Padding(5);
            this.gbEntities.Size = new System.Drawing.Size(924, 626);
            this.gbEntities.TabIndex = 1;
            this.gbEntities.TabStop = false;
            this.gbEntities.Text = "Entities";
            // 
            // lvEntities
            // 
            this.lvEntities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEntities.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvEntities.FullRowSelect = true;
            this.lvEntities.HideSelection = false;
            this.lvEntities.Location = new System.Drawing.Point(4, 94);
            this.lvEntities.Margin = new System.Windows.Forms.Padding(4);
            this.lvEntities.MultiSelect = false;
            this.lvEntities.Name = "lvEntities";
            this.lvEntities.Size = new System.Drawing.Size(297, 529);
            this.lvEntities.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvEntities.TabIndex = 0;
            this.lvEntities.UseCompatibleStateImageBehavior = false;
            this.lvEntities.View = System.Windows.Forms.View.Details;
            this.lvEntities.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvEntities_ColumnClick);
            this.lvEntities.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvEntities_ColumnMouse_Click);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Display Name";
            this.columnHeader3.Width = 145;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Logical Name";
            this.columnHeader4.Width = 151;
            // 
            // gbRecords
            // 
            this.gbRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecords.Controls.Add(this.dataGridView1);
            this.gbRecords.Controls.Add(this.bindingNavigator1);
            this.gbRecords.Enabled = false;
            this.gbRecords.Location = new System.Drawing.Point(610, 11);
            this.gbRecords.Name = "gbRecords";
            this.gbRecords.Size = new System.Drawing.Size(653, 642);
            this.gbRecords.TabIndex = 13;
            this.gbRecords.TabStop = false;
            this.gbRecords.Text = "Records";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectRows,
            this.PrimaryAttribute,
            this.RecordId,
            this.User,
            this.ReadAccess,
            this.WriteAccess,
            this.AppendAccess,
            this.AppendToAccess,
            this.DeleteAccess,
            this.ShareAccess,
            this.AssignAccess,
            this.CopyURLButton,
            this.UserId,
            this.TeamTemplateId,
            this.SelectFlag,
            this.ReadAccessFlag,
            this.WriteAccessFlag,
            this.AppendAccessFlag,
            this.AppendToAccessFlag,
            this.DeleteAccessFlag,
            this.ShareAccessFlag,
            this.AssignAccessFlag,
            this.CellFlag});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridView1.Location = new System.Drawing.Point(0, 44);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(659, 595);
            this.dataGridView1.TabIndex = 12;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Enabled = false;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorSeparator,
            this.btnNextPage,
            this.lblTotalPagesCount,
            this.toolStripSeparator1,
            this.CurrentPage,
            this.bindingNavigatorSeparator1,
            this.btnPreviousPage,
            this.lblSelectedRows,
            this.TotalRecordsAvailable,
            this.toolStripSeparator3,
            this.btnSelectAllRecords,
            this.toolStripSeparator6,
            this.btnUnSelectAll,
            this.toolStripSeparator,
            this.btnExportRecords,
            this.toolStripSeparator5,
            this.btnRemoveSelectedRecords,
            this.toolStripSeparator4});
            this.bindingNavigator1.Location = new System.Drawing.Point(3, 19);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(647, 27);
            this.bindingNavigator1.TabIndex = 13;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNextPage.Enabled = false;
            this.btnNextPage.Image = ((System.Drawing.Image)(resources.GetObject("btnNextPage.Image")));
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.RightToLeftAutoMirrorImage = true;
            this.btnNextPage.Size = new System.Drawing.Size(29, 24);
            this.btnNextPage.Text = "Next Page";
            this.btnNextPage.ToolTipText = "Next Page";
            this.btnNextPage.Click += new System.EventHandler(this.NextPage_Click);
            // 
            // lblTotalPagesCount
            // 
            this.lblTotalPagesCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblTotalPagesCount.Name = "lblTotalPagesCount";
            this.lblTotalPagesCount.Size = new System.Drawing.Size(35, 24);
            this.lblTotalPagesCount.Text = "of 0";
            this.lblTotalPagesCount.ToolTipText = "Total number of items";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // CurrentPage
            // 
            this.CurrentPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.CurrentPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CurrentPage.Name = "CurrentPage";
            this.CurrentPage.ReadOnly = true;
            this.CurrentPage.Size = new System.Drawing.Size(50, 27);
            this.CurrentPage.ToolTipText = "Current Page";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreviousPage.Enabled = false;
            this.btnPreviousPage.Image = ((System.Drawing.Image)(resources.GetObject("btnPreviousPage.Image")));
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.RightToLeftAutoMirrorImage = true;
            this.btnPreviousPage.Size = new System.Drawing.Size(29, 24);
            this.btnPreviousPage.Text = "Previous Page";
            this.btnPreviousPage.ToolTipText = "Previous Page";
            this.btnPreviousPage.Click += new System.EventHandler(this.PreviousPage_Click);
            // 
            // lblSelectedRows
            // 
            this.lblSelectedRows.Name = "lblSelectedRows";
            this.lblSelectedRows.Size = new System.Drawing.Size(78, 24);
            this.lblSelectedRows.Text = "Selected 0";
            // 
            // TotalRecordsAvailable
            // 
            this.TotalRecordsAvailable.Name = "TotalRecordsAvailable";
            this.TotalRecordsAvailable.Size = new System.Drawing.Size(35, 24);
            this.TotalRecordsAvailable.Text = "of 0";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // btnSelectAllRecords
            // 
            this.btnSelectAllRecords.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSelectAllRecords.Enabled = false;
            this.btnSelectAllRecords.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAllRecords.Image")));
            this.btnSelectAllRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectAllRecords.Name = "btnSelectAllRecords";
            this.btnSelectAllRecords.Size = new System.Drawing.Size(136, 24);
            this.btnSelectAllRecords.Text = "Select All Records ";
            this.btnSelectAllRecords.Click += new System.EventHandler(this.btnSelectAllRecords_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // btnUnSelectAll
            // 
            this.btnUnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUnSelectAll.Enabled = false;
            this.btnUnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUnSelectAll.Image")));
            this.btnUnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnSelectAll.Name = "btnUnSelectAll";
            this.btnUnSelectAll.Size = new System.Drawing.Size(148, 24);
            this.btnUnSelectAll.Text = "Unselect All Records";
            this.btnUnSelectAll.Click += new System.EventHandler(this.btnUnSelectAll_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // btnExportRecords
            // 
            this.btnExportRecords.Enabled = false;
            this.btnExportRecords.Image = ((System.Drawing.Image)(resources.GetObject("btnExportRecords.Image")));
            this.btnExportRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportRecords.Name = "btnExportRecords";
            this.btnExportRecords.Size = new System.Drawing.Size(133, 24);
            this.btnExportRecords.Text = "Export Records";
            this.btnExportRecords.ToolTipText = "Click to Export Records of Current Page";
            this.btnExportRecords.Click += new System.EventHandler(this.btnExportRecords_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 27);
            // 
            // btnRemoveSelectedRecords
            // 
            this.btnRemoveSelectedRecords.Enabled = false;
            this.btnRemoveSelectedRecords.Image = ((System.Drawing.Image)(resources.GetObject("btnRemoveSelectedRecords.Image")));
            this.btnRemoveSelectedRecords.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRemoveSelectedRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveSelectedRecords.Name = "btnRemoveSelectedRecords";
            this.btnRemoveSelectedRecords.Size = new System.Drawing.Size(205, 24);
            this.btnRemoveSelectedRecords.Text = "Remove Selected Records";
            this.btnRemoveSelectedRecords.ToolTipText = "Click to Remove Selected Records";
            this.btnRemoveSelectedRecords.Click += new System.EventHandler(this.btnRemoveSelectedRecords_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 100);
            this.panel1.TabIndex = 0;
            // 
            // lblExportFlag
            // 
            this.lblExportFlag.Name = "lblExportFlag";
            this.lblExportFlag.Size = new System.Drawing.Size(41, 20);
            this.lblExportFlag.Text = "False";
            this.lblExportFlag.Visible = false;
            // 
            // cbUser
            // 
            this.cbUser.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbUser.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbUser.Enabled = false;
            this.cbUser.FormattingEnabled = true;
            this.cbUser.Location = new System.Drawing.Point(86, 42);
            this.cbUser.Name = "cbUser";
            this.cbUser.Size = new System.Drawing.Size(179, 25);
            this.cbUser.TabIndex = 8;
            this.cbUser.SelectedIndexChanged += new System.EventHandler(this.cbUser_OnSelectOfUser);
            // 
            // lUsers
            // 
            this.lUsers.AutoSize = true;
            this.lUsers.Location = new System.Drawing.Point(21, 46);
            this.lUsers.Name = "lUsers";
            this.lUsers.Size = new System.Drawing.Size(45, 17);
            this.lUsers.TabIndex = 9;
            this.lUsers.Text = "Users";
            // 
            // gbUsers
            // 
            this.gbUsers.Controls.Add(this.lUsers);
            this.gbUsers.Controls.Add(this.cbUser);
            this.gbUsers.Location = new System.Drawing.Point(308, 27);
            this.gbUsers.Name = "gbUsers";
            this.gbUsers.Size = new System.Drawing.Size(296, 103);
            this.gbUsers.TabIndex = 14;
            this.gbUsers.TabStop = false;
            this.gbUsers.Text = "Users";
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.btnSearch);
            this.gbSearch.Controls.Add(this.txtRecordId);
            this.gbSearch.Controls.Add(this.lblRecordId);
            this.gbSearch.Controls.Add(this.txtPrimaryAttributeValue);
            this.gbSearch.Controls.Add(this.lblprimaryAttributeValue);
            this.gbSearch.Controls.Add(this.lblFilterBy);
            this.gbSearch.Controls.Add(this.cbFilterBy);
            this.gbSearch.Enabled = false;
            this.gbSearch.Location = new System.Drawing.Point(308, 136);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(296, 161);
            this.gbSearch.TabIndex = 15;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Search";
            // 
            // btnSearch
            // 
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(208, 113);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 30);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtRecordId
            // 
            this.txtRecordId.Location = new System.Drawing.Point(142, 65);
            this.txtRecordId.Name = "txtRecordId";
            this.txtRecordId.Size = new System.Drawing.Size(148, 23);
            this.txtRecordId.TabIndex = 5;
            this.txtRecordId.Visible = false;
            this.txtRecordId.TextChanged += new System.EventHandler(this.OnChangeOfTextIRecordIdTextBox);
            // 
            // lblRecordId
            // 
            this.lblRecordId.AutoSize = true;
            this.lblRecordId.Location = new System.Drawing.Point(33, 71);
            this.lblRecordId.Name = "lblRecordId";
            this.lblRecordId.Size = new System.Drawing.Size(103, 17);
            this.lblRecordId.TabIndex = 4;
            this.lblRecordId.Text = "Enter RecordId";
            this.lblRecordId.Visible = false;
            // 
            // txtPrimaryAttributeValue
            // 
            this.txtPrimaryAttributeValue.Location = new System.Drawing.Point(142, 65);
            this.txtPrimaryAttributeValue.Name = "txtPrimaryAttributeValue";
            this.txtPrimaryAttributeValue.Size = new System.Drawing.Size(149, 23);
            this.txtPrimaryAttributeValue.TabIndex = 3;
            this.txtPrimaryAttributeValue.Visible = false;
            this.txtPrimaryAttributeValue.TextChanged += new System.EventHandler(this.OnChangeOfTextInPrimaryAttributeValueTextBox);
            // 
            // lblprimaryAttributeValue
            // 
            this.lblprimaryAttributeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblprimaryAttributeValue.AutoSize = true;
            this.lblprimaryAttributeValue.Location = new System.Drawing.Point(30, 71);
            this.lblprimaryAttributeValue.Name = "lblprimaryAttributeValue";
            this.lblprimaryAttributeValue.Size = new System.Drawing.Size(82, 17);
            this.lblprimaryAttributeValue.TabIndex = 2;
            this.lblprimaryAttributeValue.Text = "Enter Value";
            this.lblprimaryAttributeValue.Visible = false;
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Location = new System.Drawing.Point(33, 25);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(58, 17);
            this.lblFilterBy.TabIndex = 1;
            this.lblFilterBy.Text = "Filter by";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Location = new System.Drawing.Point(142, 25);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(148, 25);
            this.cbFilterBy.TabIndex = 0;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // SelectRows
            // 
            this.SelectRows.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader;
            this.SelectRows.HeaderText = "";
            this.SelectRows.MinimumWidth = 6;
            this.SelectRows.Name = "SelectRows";
            this.SelectRows.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SelectRows.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SelectRows.Width = 6;
            // 
            // PrimaryAttribute
            // 
            this.PrimaryAttribute.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.PrimaryAttribute.HeaderText = "Primary Attribute";
            this.PrimaryAttribute.MinimumWidth = 6;
            this.PrimaryAttribute.Name = "PrimaryAttribute";
            this.PrimaryAttribute.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PrimaryAttribute.Width = 6;
            // 
            // RecordId
            // 
            this.RecordId.HeaderText = "RecordId";
            this.RecordId.MinimumWidth = 6;
            this.RecordId.Name = "RecordId";
            this.RecordId.ReadOnly = true;
            this.RecordId.Visible = false;
            this.RecordId.Width = 6;
            // 
            // User
            // 
            this.User.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.User.DefaultCellStyle = dataGridViewCellStyle2;
            this.User.HeaderText = "User";
            this.User.MinimumWidth = 6;
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.Width = 6;
            // 
            // ReadAccess
            // 
            this.ReadAccess.HeaderText = "Read Access";
            this.ReadAccess.MinimumWidth = 6;
            this.ReadAccess.Name = "ReadAccess";
            this.ReadAccess.ReadOnly = true;
            this.ReadAccess.Width = 6;
            // 
            // WriteAccess
            // 
            this.WriteAccess.HeaderText = "Write Access";
            this.WriteAccess.MinimumWidth = 6;
            this.WriteAccess.Name = "WriteAccess";
            this.WriteAccess.ReadOnly = true;
            this.WriteAccess.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WriteAccess.Width = 6;
            // 
            // AppendAccess
            // 
            this.AppendAccess.HeaderText = "Append Access";
            this.AppendAccess.MinimumWidth = 6;
            this.AppendAccess.Name = "AppendAccess";
            this.AppendAccess.ReadOnly = true;
            this.AppendAccess.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AppendAccess.Width = 6;
            // 
            // AppendToAccess
            // 
            this.AppendToAccess.HeaderText = "AppendTo Access";
            this.AppendToAccess.MinimumWidth = 6;
            this.AppendToAccess.Name = "AppendToAccess";
            this.AppendToAccess.ReadOnly = true;
            this.AppendToAccess.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AppendToAccess.Width = 6;
            // 
            // DeleteAccess
            // 
            this.DeleteAccess.HeaderText = "Delete Access";
            this.DeleteAccess.MinimumWidth = 6;
            this.DeleteAccess.Name = "DeleteAccess";
            this.DeleteAccess.ReadOnly = true;
            this.DeleteAccess.Width = 6;
            // 
            // ShareAccess
            // 
            this.ShareAccess.HeaderText = "Share Access";
            this.ShareAccess.MinimumWidth = 6;
            this.ShareAccess.Name = "ShareAccess";
            this.ShareAccess.ReadOnly = true;
            this.ShareAccess.Width = 6;
            // 
            // AssignAccess
            // 
            this.AssignAccess.HeaderText = "Assign Access";
            this.AssignAccess.MinimumWidth = 6;
            this.AssignAccess.Name = "AssignAccess";
            this.AssignAccess.ReadOnly = true;
            this.AssignAccess.Width = 6;
            // 
            // CopyURLButton
            // 
            this.CopyURLButton.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CopyURLButton.HeaderText = "Record URL Button";
            this.CopyURLButton.MinimumWidth = 6;
            this.CopyURLButton.Name = "CopyURLButton";
            this.CopyURLButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CopyURLButton.Width = 6;
            // 
            // UserId
            // 
            this.UserId.HeaderText = "UserId";
            this.UserId.MinimumWidth = 6;
            this.UserId.Name = "UserId";
            this.UserId.ReadOnly = true;
            this.UserId.Visible = false;
            this.UserId.Width = 6;
            // 
            // TeamTemplateId
            // 
            this.TeamTemplateId.HeaderText = "Team TemplateId";
            this.TeamTemplateId.MinimumWidth = 6;
            this.TeamTemplateId.Name = "TeamTemplateId";
            this.TeamTemplateId.ReadOnly = true;
            this.TeamTemplateId.Visible = false;
            this.TeamTemplateId.Width = 6;
            // 
            // SelectFlag
            // 
            this.SelectFlag.HeaderText = "Select Flag";
            this.SelectFlag.MinimumWidth = 6;
            this.SelectFlag.Name = "SelectFlag";
            this.SelectFlag.ReadOnly = true;
            this.SelectFlag.Visible = false;
            this.SelectFlag.Width = 6;
            // 
            // ReadAccessFlag
            // 
            this.ReadAccessFlag.HeaderText = "ReadAccessFlag";
            this.ReadAccessFlag.MinimumWidth = 6;
            this.ReadAccessFlag.Name = "ReadAccessFlag";
            this.ReadAccessFlag.ReadOnly = true;
            this.ReadAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ReadAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReadAccessFlag.Visible = false;
            this.ReadAccessFlag.Width = 6;
            // 
            // WriteAccessFlag
            // 
            this.WriteAccessFlag.HeaderText = "WriteAccessFlag";
            this.WriteAccessFlag.MinimumWidth = 6;
            this.WriteAccessFlag.Name = "WriteAccessFlag";
            this.WriteAccessFlag.ReadOnly = true;
            this.WriteAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WriteAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WriteAccessFlag.Visible = false;
            this.WriteAccessFlag.Width = 6;
            // 
            // AppendAccessFlag
            // 
            this.AppendAccessFlag.HeaderText = "AppendAccessFlag";
            this.AppendAccessFlag.MinimumWidth = 6;
            this.AppendAccessFlag.Name = "AppendAccessFlag";
            this.AppendAccessFlag.ReadOnly = true;
            this.AppendAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AppendAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AppendAccessFlag.Visible = false;
            this.AppendAccessFlag.Width = 6;
            // 
            // AppendToAccessFlag
            // 
            this.AppendToAccessFlag.HeaderText = "AppendToAccessFlag";
            this.AppendToAccessFlag.MinimumWidth = 6;
            this.AppendToAccessFlag.Name = "AppendToAccessFlag";
            this.AppendToAccessFlag.ReadOnly = true;
            this.AppendToAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AppendToAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AppendToAccessFlag.Visible = false;
            this.AppendToAccessFlag.Width = 6;
            // 
            // DeleteAccessFlag
            // 
            this.DeleteAccessFlag.HeaderText = "DeleteAccessFlag";
            this.DeleteAccessFlag.MinimumWidth = 6;
            this.DeleteAccessFlag.Name = "DeleteAccessFlag";
            this.DeleteAccessFlag.ReadOnly = true;
            this.DeleteAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DeleteAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DeleteAccessFlag.Visible = false;
            this.DeleteAccessFlag.Width = 6;
            // 
            // ShareAccessFlag
            // 
            this.ShareAccessFlag.HeaderText = "ShareAccessFlag";
            this.ShareAccessFlag.MinimumWidth = 6;
            this.ShareAccessFlag.Name = "ShareAccessFlag";
            this.ShareAccessFlag.ReadOnly = true;
            this.ShareAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ShareAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ShareAccessFlag.Visible = false;
            this.ShareAccessFlag.Width = 6;
            // 
            // AssignAccessFlag
            // 
            this.AssignAccessFlag.HeaderText = "AssignAccessFlag";
            this.AssignAccessFlag.MinimumWidth = 6;
            this.AssignAccessFlag.Name = "AssignAccessFlag";
            this.AssignAccessFlag.ReadOnly = true;
            this.AssignAccessFlag.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AssignAccessFlag.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AssignAccessFlag.Visible = false;
            this.AssignAccessFlag.Width = 6;
            // 
            // CellFlag
            // 
            this.CellFlag.HeaderText = "CellFlag";
            this.CellFlag.MinimumWidth = 6;
            this.CellFlag.Name = "CellFlag";
            this.CellFlag.ReadOnly = true;
            this.CellFlag.Width = 6;
            this.CellFlag.Visible = false;

            // 
            // AccessTeamStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.gbUsers);
            this.Controls.Add(this.gbRecords);
            this.Controls.Add(this.gbEntities);
            this.Controls.Add(this.tsMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "AccessTeamStudio";
            this.Size = new System.Drawing.Size(1266, 653);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.gbEntities.ResumeLayout(false);
            this.gbRecords.ResumeLayout(false);
            this.gbRecords.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.gbUsers.ResumeLayout(false);
            this.gbUsers.PerformLayout();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.ListView lvAttributes;
        private System.Windows.Forms.TextBox txt_SearchEntity;
        private System.Windows.Forms.Panel searchPanel;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        private ToolStripButton tsbLoadEntities;
        private GroupBox gbEntities;
        private Panel panel1;
        private Label lbSearch;
        private TextBox txtSearchEntity;
        private ListView lvEntities;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ComboBox cbUser;
        private Label lUsers;
        private DataGridView dataGridView1;
        private GroupBox gbRecords;
        private BindingNavigator bindingNavigator1;
        private ToolStripButton btnPreviousPage;
        private ToolStripButton btnSelectAllRecords;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox CurrentPage;
        private ToolStripLabel lblTotalPagesCount;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton btnNextPage;
        private ToolStripSeparator toolStripSeparator1;
        private GroupBox gbUsers;
        private ToolStripButton btnListTeammembershipRecords;
        private GroupBox gbSearch;
        private Label lblFilterBy;
        private ComboBox cbFilterBy;
        private Label lblprimaryAttributeValue;
        private TextBox txtRecordId;
        private Label lblRecordId;
        private TextBox txtPrimaryAttributeValue;
        private Button btnSearch;
        private ToolStripLabel lblSelectedRows;
        private ToolStripLabel TotalRecordsAvailable;
        private ToolStripButton btnRemoveSelectedRecords;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripLabel lblExportFlag;
        private ToolStripButton btnExportRecords;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton btnUnSelectAll;
        private DataGridViewCheckBoxColumn SelectRows;
        private DataGridViewLinkColumn PrimaryAttribute;
        private DataGridViewTextBoxColumn RecordId;
        private DataGridViewTextBoxColumn User;
        private DataGridViewImageColumn ReadAccess;
        private DataGridViewImageColumn WriteAccess;
        private DataGridViewImageColumn AppendAccess;
        private DataGridViewImageColumn AppendToAccess;
        private DataGridViewImageColumn DeleteAccess;
        private DataGridViewImageColumn ShareAccess;
        private DataGridViewImageColumn AssignAccess;
        private DataGridViewButtonColumn CopyURLButton;
        private DataGridViewTextBoxColumn UserId;
        private DataGridViewTextBoxColumn TeamTemplateId;
        private DataGridViewTextBoxColumn SelectFlag;
        private DataGridViewTextBoxColumn ReadAccessFlag;
        private DataGridViewTextBoxColumn WriteAccessFlag;
        private DataGridViewTextBoxColumn AppendAccessFlag;
        private DataGridViewTextBoxColumn AppendToAccessFlag;
        private DataGridViewTextBoxColumn DeleteAccessFlag;
        private DataGridViewTextBoxColumn ShareAccessFlag;
        private DataGridViewTextBoxColumn AssignAccessFlag;
        private DataGridViewTextBoxColumn CellFlag;
    }
}
