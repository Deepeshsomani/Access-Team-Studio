using AccessTeamStudio.DelegatesHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using Enable_PowerAutomate_Logs.Helpers;
using Microsoft.Xrm.Sdk.Metadata;
using XrmToolBox.Extensibility.Interfaces;
using System.ComponentModel.Composition;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System.Net.Http;
using System.Security.Cryptography;
using System.IO;
using Microsoft.Crm.Sdk.Messages;
using System.Collections.Concurrent;
using System.Diagnostics;
using McTools.Xrm.Connection;
using Label = Microsoft.Xrm.Sdk.Label;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Organization;
using System.Xml;
using In.Sontx.SimpleDataGridViewPaging;
using OfficeOpenXml;
using XrmToolBox.Extensibility.Args;
using IronXL;
using SortOrder = System.Windows.Forms.SortOrder;
using ClosedXML.Excel;
using AccessTeamStudio;
using AccessTeamStudio.Properties;
using Enable_PowerAutomate_Logs;
using OfficeOpenXml.Style;
using System.Collections;
//using Microsoft.Office.Interop;\


namespace AccessTeamStudio
{
    public partial class AccessTeamStudio : PluginControlBase
    {
        private Settings mySettings;
        private List<EntityMetadata> entitiesCache;
        private ListViewItem[] listViewItemsCache;
        private static int fileNumber = 0;
        ArrayList SelectedRowIndex = new ArrayList();


        public AccessTeamStudio()
        {
            InitializeComponent();
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            ExecuteMethod(GetAccounts);
        }

        private void GetAccounts()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting accounts",
                Work = (worker, args) =>
                {
                    args.Result = Service.RetrieveMultiple(new QueryExpression("account")
                    {
                        TopCount = 50
                    });
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result != null)
                    {
                        MessageBox.Show($"Found {result.Entities.Count} accounts");
                    }
                }
            });
        }

        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            SettingsManager.Instance.Save(GetType(), mySettings);
        }
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }
        }

        //Method triggers on click of "Load entities" button
        private void tsbLoadEntities_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadEntities);
        }

        //Method called in "tsbLoadEntities_Click" to Load entities
        private void LoadEntities()
        {
            this.btnListTeammembershipRecords.Enabled = false;
            this.gbRecords.Enabled = false;
            this.gbUsers.Enabled = false;
            this.cbUser.Enabled = false;
            this.bindingNavigator1.Enabled = false;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.lvEntities.Items.Clear();
            this.gbEntities.Enabled = false;
            this.gbSearch.Enabled = false;
            this.cbFilterBy.Items.Clear();
            this.cbFilterBy.Text = "";
            this.txtRecordId.Text = "";
            this.txtPrimaryAttributeValue.Text = "";
            this.lblTotalPagesCount.Text = "of 0";
            this.btnExportRecords.Enabled = false;
            this.btnRemoveSelectedRecords.Enabled = false;
            this.lblSelectedRows.Text = "Selected 0";
            this.txtPrimaryAttributeValue.Visible = false;
            this.txtRecordId.Visible = false;
            this.lblRecordId.Visible = false;
            this.lblprimaryAttributeValue.Visible = false;
            this.btnSelectAllRecords.Enabled = false;
            this.btnUnSelectAll.Enabled = false;

            Cursor = Cursors.WaitCursor;
            WorkAsync(new WorkAsyncInfo("Loading entities having Access Team enabled...", w =>
            {
                w.Result = MetadataHelper.RetrieveEntities(Service);
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;
                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Error);
                    }
                    else
                    {
                        entitiesCache = (List<EntityMetadata>)c.Result;
                        lvEntities.Items.Clear();
                        dataGridView1.Rows.Clear();
                        CurrentPage.Text = "0";
                        var list = new List<ListViewItem>();
                        foreach (EntityMetadata emd in (List<EntityMetadata>)c.Result)
                        {
                            if (emd.AutoCreateAccessTeams.Value.Equals(true))
                            {
                                var item = new ListViewItem { Text = emd.DisplayName.UserLocalizedLabel.Label, Tag = emd.LogicalName };
                                item.SubItems.Add(emd.LogicalName);
                                item.SubItems.Add(emd.PrimaryNameAttribute);
                                item.SubItems.Add(emd.ObjectTypeCode.ToString());
                                list.Add(item);
                            }
                        }
                        this.listViewItemsCache = list.ToArray();
                        if (listViewItemsCache.Length > 0)
                        {
                            lvEntities.Items.AddRange(listViewItemsCache);
                            gbEntities.Enabled = true;

                        }
                        else
                        {
                            CommonDelegates.DisplayMessageBox(ParentForm, "No Entities with Enabled Access Team Found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }


                    }
                }
            });
        }

        //Method triggers on Click of Headers of Entities List and Sorts Accordingly
        private void lvEntities_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvEntities.Sorting = lvEntities.Sorting == System.Windows.Forms.SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            lvEntities.ListViewItemSorter = new ListViewItemComparer(e.Column, lvEntities.Sorting);
            lvEntities.SelectedItems.Clear();
        }


        //Method triggers on Select of Entity in Entities List
        private void lvEntities_ColumnMouse_Click(object sender, MouseEventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.cbFilterBy.Items.Clear();
            this.gbRecords.Enabled = true;
            this.btnListTeammembershipRecords.Enabled = true;
            this.btnSelectAllRecords.Enabled = false;
            this.btnUnSelectAll.Enabled = false;
            this.cbUser.Enabled = false;
            this.gbUsers.Enabled = true;
            this.cbUser.Text = "";
            this.cbFilterBy.Items.Add(lvEntities.SelectedItems[0].SubItems[2].Text);
            this.cbFilterBy.Items.Add("Record Id");
            this.gbSearch.Enabled = true;
            this.lblTotalPagesCount.Text = "of 0";
            this.CurrentPage.Text = "0";
            this.cbUser.Items.Clear();
            this.lblSelectedRows.Text = "Selected 0";
            this.txtPrimaryAttributeValue.Text = "";
            this.txtRecordId.Text = "";
            this.txtPrimaryAttributeValue.Visible = false;
            this.txtRecordId.Visible = false;
            this.lblRecordId.Visible = false;
            this.lblprimaryAttributeValue.Visible = false;
            this.btnExportRecords.Enabled = false;
            ExecuteMethod(LoadUsers);
        }

        //Method to Load users in Users Combobox and called on Select of Entity in Entities list
        private void LoadUsers()
        {
            
            var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();

            Cursor = Cursors.WaitCursor;
            WorkAsync(new WorkAsyncInfo("Loading Users where " + entityLogicalName + " are shared via Access Teams...", w =>
                {
                    var fetchXML = $@"<fetch distinct='true'>
                                          <entity name='systemuser'>
                                            <attribute name='fullname' />
                                            <attribute name='businessunitid' />
                                            <attribute name='title' />
                                            <attribute name='address1_telephone1' />
                                            <attribute name='positionid' />
                                            <attribute name='systemuserid' />
                                            <order attribute='fullname' descending='false' />
                                            <link-entity name='teammembership' from='systemuserid' to='systemuserid' visible='false' intersect='true'>
                                                  <link-entity name='team' from='teamid' to='teamid' alias='ab'>
                                                    <filter type='and'>
                                                      <condition attribute='teamtype' operator='eq' value='1' />
                                                    </filter>
                                                        <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' />
                                                  </link-entity>
                                             </link-entity>
                                          </entity>
                                        </fetch>";
                    var users = Service.RetrieveMultiple(new FetchExpression(fetchXML));
                    if (users.Entities.Count > 0)
                    {
                        this.cbUser.Enabled = true;
                        w.Result = users;
                    }
                    else
                    {
                        CommonDelegates.DisplayMessageBox(ParentForm, "No Records of Users found where " + entityLogicalName + " are shared by Access Teams", "Information", MessageBoxButtons.OK,
                                                              MessageBoxIcon.Information);
                    }
                })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;
                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Error);
                    }
                    else if (c.Result != null)
                    {

                        foreach (var user in ((EntityCollection)c.Result).Entities)
                        {
                            cbUser.Items.Add(user.GetAttributeValue<string>("fullname"));
                        }
                    }
                }
            });
        }

        //Method to Search Entity
        private void OnSearchKeyUp(object sender, KeyEventArgs e)
        {
            var entityName = txtSearchEntity.Text;
            if (string.IsNullOrWhiteSpace(entityName))
            {
                lvEntities.BeginUpdate();
                lvEntities.Items.Clear();
                lvEntities.Items.AddRange(listViewItemsCache);
                lvEntities.EndUpdate();
            }
            else
            {
                lvEntities.BeginUpdate();
                lvEntities.Items.Clear();
                var filteredItems = listViewItemsCache
                    .Where(item => item.Text.StartsWith(entityName, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
                lvEntities.Items.AddRange(filteredItems);
                lvEntities.EndUpdate();
            }
        }

        //Method Triggers on click of "List Shared Records" button
        private void btnListSharedRecords_Click(object sender, EventArgs e)
        {
            ExecuteMethod(RetrieveTeamMembershipRecords);
        }
        int p_value = 0;
        //Method to List Records called on Click of List Records button
        private void RetrieveTeamMembershipRecords()
        {
            if (lvEntities.SelectedItems.Count > 0)
            {
                this.cbUser.Text = "";
                this.bindingNavigator1.Enabled = true;
                this.btnPreviousPage.Enabled = false;
                this.dataGridView1.ColumnHeadersVisible = true;
                dataGridView1.Rows.Clear();
                this.CurrentPage.Text = "1";
                this.lblTotalPagesCount.Text = "of 0";
                this.lblSelectedRows.Text = "Selected 0";
               
                var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
                var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
                var entityObjectTypeCode = lvEntities.SelectedItems[0].SubItems[3].Text;
                var totalRecordsCount = 0;
                this.btnNextPage.Enabled = true;
                this.txtRecordId.Text = "";
                this.txtPrimaryAttributeValue.Text = "";
                this.cbFilterBy.Text = "";
                cbFilterBy.SelectedItem = null;
                this.txtPrimaryAttributeValue.Visible = false;
                this.txtRecordId.Visible = false;
                this.lblRecordId.Visible = false;
                this.lblprimaryAttributeValue.Visible = false;
                this.btnRemoveSelectedRecords.Enabled = false;

                Cursor = Cursors.WaitCursor;
                WorkAsync(new WorkAsyncInfo($"Listing shared records of Selected Entity via Access Team...", dwea =>
                {
                    int fetchCount = 5000;
                    int pageNumberCount = 1;
                    string pagingCookie = null;

                    var fetchXMLforTotal = $@"<fetch count='5000'>
                                  <entity name='teammembership'>
                                    <attribute name='teamid' />
                                    <attribute name='teammembershipid' />
                                    <attribute name='systemuserid' />
                                    <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                      <attribute name='name' />
                                      <attribute name='teamid' />
                                      <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                        <attribute name='objecttypecode' />
                                        <attribute name='teamtemplatename' />
                                        <attribute name='teamtemplateid' />
                                       <filter>
                                          <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                          </link-entity>
                                        </link-entity>
                                      </entity>    
                                     </fetch>";

                    while (true)
                    {
                        string xml = CreateXml(fetchXMLforTotal, pagingCookie, pageNumberCount, fetchCount);

                        RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                        {
                            Query = new FetchExpression(xml)
                        };

                        EntityCollection records = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;
                        var request = new RetrieveCurrentOrganizationRequest();
                        var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                        this.dataGridView1.Rows.Clear();
                        if (records.MoreRecords)
                        {
                            pageNumberCount++;
                            totalRecordsCount += records.Entities.Count;
                            pagingCookie = records.PagingCookie;
                        }
                        else
                        {
                            totalRecordsCount += records.Entities.Count;
                            break;
                        }
                    }
                    if (totalRecordsCount > 50)
                    {
                        double d_value = ((double)totalRecordsCount);
                        double fvalue = d_value / 50;
                        string svalue = fvalue.ToString();
                        string[] a = svalue.Split(new char[] { '.' });
                        int decimals = a[1].Length;
                        p_value = Int32.Parse(a[0]);
                        if (a[1] != "")
                        {
                            p_value = Int32.Parse(a[0]) + 1;

                        }
                        this.lblTotalPagesCount.Text = "of " + p_value;
                        this.TotalRecordsAvailable.Text = "of " + 50;
                    }
                    else
                    {
                        this.lblTotalPagesCount.Text = "of 1";
                        this.TotalRecordsAvailable.Text = "of " + totalRecordsCount;
                        this.btnNextPage.Enabled = false;
                        this.btnPreviousPage.Enabled = false;

                    }
                    var fetchXML = $@"<fetch count='50'>
                                  <entity name='teammembership'>
                                    <attribute name='teamid' />
                                    <attribute name='teammembershipid' />
                                    <attribute name='systemuserid' />  
                                    <link-entity name='team' from='teamid' to='teamid' alias='team'>                                     
                                      <attribute name='name' />
                                      <attribute name='teamid' />
                                      <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                        <attribute name='objecttypecode' />
                                        <attribute name='teamtemplatename' />
                                        <attribute name='teamtemplateid' />
                                       <filter>
                                          <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                           <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                            <order attribute='{entityPrimaryAttributeName}' descending='false' />                                           
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                          </link-entity>
                                        </link-entity>
                                         <link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'>
                                          <attribute name='fullname' />
                                        </link-entity>
                                      </entity>    
                                     </fetch>";
                    var teamMemberships = Service.RetrieveMultiple(new FetchExpression(fetchXML));
                    if (teamMemberships.Entities.Count > 0)
                    {
                        this.btnExportRecords.Enabled = true;
                        dwea.Result = teamMemberships;
                    }
                    else
                    {
                        btnPreviousPage.Enabled = false;
                        this.CurrentPage.Text = "0";
                        this.lblTotalPagesCount.Text = "of 0";
                        this.bindingNavigator1.Enabled = false;

                        CommonDelegates.DisplayMessageBox(ParentForm, $"No shared records of {entityLogicalName} by Access Team", "Information", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information);
                    }
                })
                {
                    PostWorkCallBack = c =>
                    {
                        Cursor = Cursors.Default;

                        if (c.Error != null)
                        {
                            string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                            CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                            MessageBoxIcon.Error);
                        }
                        else if (c.Result != null)
                        {
                            this.btnSelectAllRecords.Enabled = true;
                            this.btnUnSelectAll.Enabled = false;
                            var request = new RetrieveCurrentOrganizationRequest();
                            var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                            foreach (var teammembership in ((EntityCollection)c.Result).Entities)
                            {

                                var principalAccessRequest = new RetrievePrincipalAccessRequest
                                {
                                    Principal = new EntityReference("systemuser", teammembership.GetAttributeValue<Guid>("systemuserid")),
                                    Target = new EntityReference(entityLogicalName, new Guid(((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString()))
                                };

                                // Response will contain AccessRights mask, like AccessRights.WriteAccess | AccessRights.ReadAccess | ...
                                var principalAccessResponse = (RetrievePrincipalAccessResponse)Service.Execute(principalAccessRequest);

                                Image ReadAccess = Resources.No;//(System.Drawing.Image)(resources.GetObject("No.Image"));
                                var ReadAccessFlag = "No";
                                Image WriteAccess = Resources.No;
                                var WriteAccessFlag = "No";
                                Image AppendAccess = Resources.No;
                                var AppendAccessFlag = "No";
                                Image AppendToAccess = Resources.No;
                                var AppendToAccessFlag = "No";
                                Image DeleteAccess = Resources.No;
                                var DeleteAccessFlag = "No";
                                Image ShareAccess = Resources.No;
                                var ShareAccessFlag = "No";
                                Image AssignAccess = Resources.No;
                                var AssignAccessFlag = "No";

                                if (principalAccessResponse.AccessRights.ToString().Contains("ReadAccess"))
                                {
                                    ReadAccess = Resources.Yes;
                                    ReadAccessFlag = "Yes";
                                }
                                if (principalAccessResponse.AccessRights.ToString().Contains("WriteAccess"))
                                {
                                    WriteAccess = Resources.Yes;
                                    WriteAccessFlag = "Yes";
                                }
                                if (principalAccessResponse.AccessRights.ToString().Contains("AppendAccess"))
                                {
                                    AppendAccess = Resources.Yes;
                                    AppendAccessFlag = "Yes";
                                }
                                if (principalAccessResponse.AccessRights.ToString().Contains("AppendToAccess"))
                                {
                                    AppendToAccess = Resources.Yes;
                                    AppendToAccessFlag = "Yes";
                                }
                                if (principalAccessResponse.AccessRights.ToString().Contains("DeleteAccess"))
                                {
                                    DeleteAccess = Resources.Yes;
                                    DeleteAccessFlag = "Yes";
                                }
                                if (principalAccessResponse.AccessRights.ToString().Contains("ShareAccess"))
                                {
                                    ShareAccess = Resources.Yes;
                                    ShareAccessFlag = "Yes";
                                }
                                if (principalAccessResponse.AccessRights.ToString().Contains("AssignAccess"))
                                {
                                    AssignAccess = Resources.Yes;
                                    AssignAccessFlag = "Yes";
                                }
                                DataGridViewLinkColumn lnk = new DataGridViewLinkColumn();
                                lnk.Name = string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString());
                                lnk.Text = "Copy Record URL";
                                lnk.UseColumnTextForLinkValue = true;
                                dataGridView1.Rows.Add(false, ((AliasedValue)teammembership["record." + entityPrimaryAttributeName]).Value.ToString(), ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString(), ((AliasedValue)teammembership["user.fullname"]).Value.ToString(), ReadAccess, WriteAccess, AppendAccess, AppendToAccess, DeleteAccess, ShareAccess, AssignAccess, lnk.Text, teammembership.GetAttributeValue<Guid>("systemuserid"), ((AliasedValue)teammembership["teamtemplate.teamtemplateid"]).Value.ToString(), false, ReadAccessFlag, WriteAccessFlag, AppendAccessFlag, AppendToAccessFlag, DeleteAccessFlag, ShareAccessFlag, AssignAccessFlag);
                            }
                        }
                    }
                });
            }

        }


        //Method triggers on click of "NextPage" button of DataGridView binding navigator to load and list next page
        private void NextPage_Click(object sender, EventArgs e)
        {
            int fetchCount = 50;
            int pageNumber = 1 + int.Parse(CurrentPage.Text);
            string pagingCookie = null;
            var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
            var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
            var entityObjectTypeCode = lvEntities.SelectedItems[0].SubItems[3].Text;
            this.lblSelectedRows.Text = "Selected 0";
            this.btnRemoveSelectedRecords.Enabled = false;

            Cursor = Cursors.WaitCursor;
            WorkAsync(new WorkAsyncInfo($"Loading next page...", dwea =>
            {
                var fetchXML = $@"<fetch count='50'>
                                  <entity name='teammembership'>
                                    <attribute name='teamid' />
                                    <attribute name='teammembershipid' />
                                    <attribute name='systemuserid' />
                                    <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                      <attribute name='name' />
                                      <attribute name='teamid' />
                                      <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                        <attribute name='objecttypecode' />
                                        <attribute name='teamtemplatename' />
                                        <attribute name='teamtemplateid' />
                                       <filter>
                                          <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                          </link-entity>
                                        </link-entity>";
                if (cbUser.SelectedIndex != -1)
                {
                    var userName = cbUser.SelectedItem.ToString();

                    fetchXML += $"<link-entity name='systemuser' from ='systemuserid' to ='systemuserid' link-type='inner' alias='user'><attribute name='fullname' /><filter><condition attribute = 'fullname' operator= 'eq' value = '{userName}' /></filter></link-entity></entity></fetch>";

                }
                else
                {
                    fetchXML += "<link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'><attribute name = 'fullname' /></link-entity></entity></fetch>";
                }

                string xml = CreateXml(fetchXML, pagingCookie, pageNumber, fetchCount);
                this.dataGridView1.Rows.Clear();
                // Excute the fetch query and get the xml result.
                RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                {
                    Query = new FetchExpression(xml)
                };

                EntityCollection teammemberships = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;
                if (teammemberships.Entities.Count > 0)
                {
                    dwea.Result = teammemberships;
                }
                else
                {
                    CommonDelegates.DisplayMessageBox(ParentForm, "No more Records Found", "Information", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Information);
                }
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;

                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                    }
                    else if (c.Result != null)
                    {
                        this.btnSelectAllRecords.Enabled = true;
                        this.btnUnSelectAll.Enabled = true;
                         this.btnExportRecords.Enabled = true;
                        var c_records = (((EntityCollection)c.Result).Entities);
                        int c_Count = c_records.Count;
                        var request = new RetrieveCurrentOrganizationRequest();
                        var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                        this.dataGridView1.Rows.Clear();
                        foreach (var teammembership in ((EntityCollection)c.Result).Entities)
                        {
                            var principalAccessRequest = new RetrievePrincipalAccessRequest
                            {
                                Principal = new EntityReference("systemuser", teammembership.GetAttributeValue<Guid>("systemuserid")),
                                Target = new EntityReference(entityLogicalName, new Guid(((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString()))
                            };

                            // Response will contain AccessRights mask, like AccessRights.WriteAccess | AccessRights.ReadAccess | ...
                            var principalAccessResponse = (RetrievePrincipalAccessResponse)Service.Execute(principalAccessRequest);

                            Image ReadAccess = Resources.No;//(System.Drawing.Image)(resources.GetObject("No.Image"));
                            var ReadAccessFlag = "No";
                            Image WriteAccess = Resources.No;
                            var WriteAccessFlag = "No";
                            Image AppendAccess = Resources.No;
                            var AppendAccessFlag = "No";
                            Image AppendToAccess = Resources.No;
                            var AppendToAccessFlag = "No";
                            Image DeleteAccess = Resources.No;
                            var DeleteAccessFlag = "No";
                            Image ShareAccess = Resources.No;
                            var ShareAccessFlag = "No";
                            Image AssignAccess = Resources.No;
                            var AssignAccessFlag = "No";

                            if (principalAccessResponse.AccessRights.ToString().Contains("ReadAccess"))
                            {
                                ReadAccess = Resources.Yes;
                                ReadAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("WriteAccess"))
                            {
                                WriteAccess = Resources.Yes;
                                WriteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendAccess"))
                            {
                                AppendAccess = Resources.Yes;
                                AppendAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendToAccess"))
                            {
                                AppendToAccess = Resources.Yes;
                                AppendToAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("DeleteAccess"))
                            {
                                DeleteAccess = Resources.Yes;
                                DeleteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("ShareAccess"))
                            {
                                ShareAccess = Resources.Yes;
                                ShareAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AssignAccess"))
                            {
                                AssignAccess = Resources.Yes;
                                AssignAccessFlag = "Yes";
                            }
                            DataGridViewLinkColumn lnk = new DataGridViewLinkColumn();
                            lnk.Name = string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString());
                            lnk.Text = "Copy Record URL";
                            lnk.UseColumnTextForLinkValue = true;
                            dataGridView1.Rows.Add(false, ((AliasedValue)teammembership["record." + entityPrimaryAttributeName]).Value.ToString(), ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString(), ((AliasedValue)teammembership["user.fullname"]).Value.ToString(), ReadAccess, WriteAccess, AppendAccess, AppendToAccess, DeleteAccess, ShareAccess, AssignAccess, lnk.Text, teammembership.GetAttributeValue<Guid>("systemuserid"), ((AliasedValue)teammembership["teamtemplate.teamtemplateid"]).Value.ToString(), false, ReadAccessFlag, WriteAccessFlag, AppendAccessFlag, AppendToAccessFlag, DeleteAccessFlag, ShareAccessFlag, AssignAccessFlag);
                        }
                        this.btnPreviousPage.Enabled = true;
                        this.CurrentPage.Text = ((pageNumber)) + "";

                        if (c_Count > 50)
                        {
                            double d_value = ((double)c_Count);
                            double fvalue = d_value / 50;
                            string svalue = fvalue.ToString();
                            string[] a = svalue.Split(new char[] { '.' });
                            int decimals = a[1].Length;
                            p_value = Int32.Parse(a[0]);
                            if (a[1] != "")
                            {
                                p_value = Int32.Parse(a[0]) + 1;

                            }
                            this.lblTotalPagesCount.Text = "of " + p_value;
                            this.TotalRecordsAvailable.Text = "of " + 50;
                        }
                        else
                        {

                            this.TotalRecordsAvailable.Text = "of " + c_Count;
                            if (p_value == pageNumber)
                            {
                                this.btnNextPage.Enabled = false;
                            }

                        }
                    }
                }
            });
        }

        //Method triggers on click of "Previous Page" button of DataGridview
        //
        //
        //dView binding navigator to load and list prevoius page
        private void PreviousPage_Click(object sender, EventArgs e)
        {
            int fetchCount = 50;

            if (int.Parse(CurrentPage.Text) - 1 > 1)
                this.btnPreviousPage.Enabled = true;
            else
                this.btnPreviousPage.Enabled = false;

            int pageNumber = int.Parse(CurrentPage.Text) - 1;
            this.lblSelectedRows.Text = "Selected 0";
            this.btnRemoveSelectedRecords.Enabled = false;
            this.btnNextPage.Enabled = true;
            string pagingCookie = null;
            var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
            var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
            var entityObjectTypeCode = lvEntities.SelectedItems[0].SubItems[3].Text;

            Cursor = Cursors.WaitCursor;
            WorkAsync(new WorkAsyncInfo($"Loading previous page...", dwea =>
            {
                var fetchXML = $@"<fetch count='50'>
                                  <entity name='teammembership'>
                                    <attribute name='teamid' />
                                    <attribute name='teammembershipid' />
                                    <attribute name='systemuserid' />
                                    <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                      <attribute name='name' />
                                      <attribute name='teamid' />
                                      <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                        <attribute name='objecttypecode' />
                                        <attribute name='teamtemplatename' /> 
                                            <attribute name='teamtemplateid' />
                                       <filter>
                                          <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                               <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                          </link-entity>
                                        </link-entity>";

                if (cbUser.SelectedIndex != -1)
                {
                    var userName = cbUser.SelectedItem.ToString();

                    fetchXML += $"<link-entity name='systemuser' from ='systemuserid' to ='systemuserid' link-type='inner' alias='user'><attribute name='fullname' /><filter><condition attribute = 'fullname' operator= 'eq' value = '{userName}' /></filter></link-entity></entity></fetch>";
                }
                else
                {
                    fetchXML += "<link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'><attribute name = 'fullname' /></link-entity></entity></fetch>";
                }

                string xml = CreateXml(fetchXML, pagingCookie, pageNumber, fetchCount);
                this.dataGridView1.Rows.Clear();

                // Execute the fetch query and get the xml result.
                RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                {
                    Query = new FetchExpression(xml)
                };

                EntityCollection teammemberships = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;
                if (teammemberships.Entities.Count > 0)
                {
                    dwea.Result = teammemberships;
                }
                else
                {
                    CommonDelegates.DisplayMessageBox(ParentForm, "No more Records Found", "Information", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Information);
                }
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;

                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                    }
                    else if (c.Result != null)
                    {
                        this.btnExportRecords.Enabled = true;
                        this.btnSelectAllRecords.Enabled = true;
                        this.btnUnSelectAll.Enabled = false;
                        var c_records = (((EntityCollection)c.Result).Entities);
                        int c_Count = c_records.Count;
                        var request = new RetrieveCurrentOrganizationRequest();
                        var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                        this.dataGridView1.Rows.Clear();

                        foreach (var teammembership in ((EntityCollection)c.Result).Entities)
                        {
                            var principalAccessRequest = new RetrievePrincipalAccessRequest
                            {
                                Principal = new EntityReference("systemuser", teammembership.GetAttributeValue<Guid>("systemuserid")),
                                Target = new EntityReference(entityLogicalName, new Guid(((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString()))
                            };

                            // Response will contain AccessRights mask, like AccessRights.WriteAccess | AccessRights.ReadAccess | ...
                            var principalAccessResponse = (RetrievePrincipalAccessResponse)Service.Execute(principalAccessRequest);

                            Image ReadAccess = Resources.No;//(System.Drawing.Image)(resources.GetObject("No.Image"));
                            var ReadAccessFlag = "No";
                            Image WriteAccess = Resources.No;
                            var WriteAccessFlag = "No";
                            Image AppendAccess = Resources.No;
                            var AppendAccessFlag = "No";
                            Image AppendToAccess = Resources.No;
                            var AppendToAccessFlag = "No";
                            Image DeleteAccess = Resources.No;
                            var DeleteAccessFlag = "No";
                            Image ShareAccess = Resources.No;
                            var ShareAccessFlag = "No";
                            Image AssignAccess = Resources.No;
                            var AssignAccessFlag = "No";

                            if (principalAccessResponse.AccessRights.ToString().Contains("ReadAccess"))
                            {
                                ReadAccess = Resources.Yes;
                                ReadAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("WriteAccess"))
                            {
                                WriteAccess = Resources.Yes;
                                WriteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendAccess"))
                            {
                                AppendAccess = Resources.Yes;
                                AppendAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendToAccess"))
                            {
                                AppendToAccess = Resources.Yes;
                                AppendToAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("DeleteAccess"))
                            {
                                DeleteAccess = Resources.Yes;
                                DeleteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("ShareAccess"))
                            {
                                ShareAccess = Resources.Yes;
                                ShareAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AssignAccess"))
                            {
                                AssignAccess = Resources.Yes;
                                AssignAccessFlag = "Yes";
                            }
                            DataGridViewLinkColumn lnk = new DataGridViewLinkColumn();
                            lnk.Name = string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString());
                            lnk.Text = "Copy Record URL";
                            lnk.UseColumnTextForLinkValue = true;
                            dataGridView1.Rows.Add(false, ((AliasedValue)teammembership["record." + entityPrimaryAttributeName]).Value.ToString(), ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString(), ((AliasedValue)teammembership["user.fullname"]).Value.ToString(), ReadAccess, WriteAccess, AppendAccess, AppendToAccess, DeleteAccess, ShareAccess, AssignAccess, lnk.Text, teammembership.GetAttributeValue<Guid>("systemuserid"), ((AliasedValue)teammembership["teamtemplate.teamtemplateid"]).Value.ToString(), false, ReadAccessFlag, WriteAccessFlag, AppendAccessFlag, AppendToAccessFlag, DeleteAccessFlag, ShareAccessFlag, AssignAccessFlag);
                        }
                        this.CurrentPage.Text = ((pageNumber)) + "";

                        if (c_Count > 50)
                        {
                            double d_value = ((double)c_Count);
                            double fvalue = d_value / 50;
                            string svalue = fvalue.ToString();
                            string[] a = svalue.Split(new char[] { '.' });
                            int decimals = a[1].Length;
                            p_value = Int32.Parse(a[0]);
                            if (a[1] != "")
                            {
                                p_value = Int32.Parse(a[0]) + 1;

                            }
                            this.lblTotalPagesCount.Text = "of " + p_value;
                            this.TotalRecordsAvailable.Text = "of " + 50;
                        }
                        else
                        {
                            this.TotalRecordsAvailable.Text = "of " + c_Count;
                        }

                    }
                }
            });
        }

        //Method triggers on click of "Cell" of DataGridView and contains functionality of Selecting records,redirecting to UrlLink and CopyLink to clipboard
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
                var request = new RetrieveCurrentOrganizationRequest();
                var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                if (dataGridView1.SelectedCells[0].ColumnIndex == 0)
                {
                    Cursor = Cursors.WaitCursor;
                    WorkAsync(new WorkAsyncInfo($"Selecting record", dwea =>
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString().Equals("True"))
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells[14].Value = false;
                        }
                        else
                        {
                            this.dataGridView1.Rows[e.RowIndex].Cells[14].Value = true;
                            SelectedRowIndex.Add(e.RowIndex);
                            this.btnUnSelectAll.Enabled = true;
                        }
                    var count = 0;
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[14].Value.ToString().Equals("True"))
                            {
                                ++count;
                            }
                        }
                        if (count > 0)
                            this.btnRemoveSelectedRecords.Enabled = true;
                        else
                            this.btnRemoveSelectedRecords.Enabled = false;
                        dwea.Result = count;
                    })
                    {
                        PostWorkCallBack = c =>
                        {
                            Cursor = Cursors.Default;
                            if (c.Error != null)
                            {
                                string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                                CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                                                  MessageBoxIcon.Error);
                            }
                            this.lblSelectedRows.Text = "Selected " + c.Result.ToString();

                        }
                    });

                }
                else if (dataGridView1.SelectedCells[0].ColumnIndex == 1)
                {
                    Process.Start(string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + dataGridView1.SelectedCells[0].DataGridView.CurrentRow.Cells[2].Value));
                }
                else if (dataGridView1.SelectedCells[0].ColumnIndex == 11)
                {
                    Clipboard.SetText(string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + dataGridView1.SelectedCells[0].DataGridView.CurrentRow.Cells[2].Value));

                    CommonDelegates.DisplayMessageBox(ParentForm, "Record URL is Copied please paste it in browser to open a record", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            dataGridView1.ClearSelection();
            
        }
        //Method triggers on select of any user in "User" Combobox and lists records accordingly
        private void cbUser_OnSelectOfUser(object sender, EventArgs e)
        {
            var userName = cbUser.SelectedItem.ToString();
            var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
            var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
            var entityObjectTypeCode = lvEntities.SelectedItems[0].SubItems[3].Text;
            var totalRecordsCount = 0;

            this.lblprimaryAttributeValue.Visible = false;
            this.txtPrimaryAttributeValue.Visible = false;
            this.lblRecordId.Visible = false;
            this.txtRecordId.Visible = false;
            this.txtRecordId.Text = "";
            this.txtPrimaryAttributeValue.Text = "";

            cbFilterBy.SelectedItem = null;

            this.lblSelectedRows.Text = "Selected 0";
            this.cbFilterBy.Text = "";
            this.txtPrimaryAttributeValue.Text = "";
            this.txtRecordId.Text = "";
            this.gbRecords.Enabled = true;
            this.dataGridView1.ColumnHeadersVisible = true;
            this.bindingNavigator1.Enabled = true;
            this.CurrentPage.Text = "1";
            this.btnNextPage.Enabled = true;
            this.btnRemoveSelectedRecords.Enabled = false;
            Cursor = Cursors.WaitCursor;
            WorkAsync(new WorkAsyncInfo($"Listing records of {entityLogicalName} shared with User : {userName} via Access Team...", dwea =>
            {
                int fetchCount = 5000;
                int pageNumberCount = 1;
                string pagingCookie = null;
                var fetchXMLforTotal = $@"<fetch count='5000'>
                                  <entity name='teammembership'>
                                    <attribute name='teamid' />
                                    <attribute name='teammembershipid' />
                                    <attribute name='systemuserid' />
                                    <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                      <attribute name='name' />
                                      <attribute name='teamid' />
                                      <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                        <attribute name='objecttypecode' />
                                        <attribute name='teamtemplatename' />
                                        <attribute name='teamtemplateid' />
                                       <filter>
                                          <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                          </link-entity>
                                        </link-entity>
                                        <link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'>
                                          <filter>
                                            <condition attribute='fullname' operator='eq' value='{userName}' />
                                          </filter>
                                        </link-entity>
                                      </entity>    
                                     </fetch>";
                while (true)
                {
                    string xml = CreateXml(fetchXMLforTotal, pagingCookie, pageNumberCount, fetchCount);

                    RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                    {
                        Query = new FetchExpression(xml)
                    };

                    EntityCollection records = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;
                    var request = new RetrieveCurrentOrganizationRequest();
                    var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                    this.dataGridView1.Rows.Clear();
                    if (records.MoreRecords)
                    {
                        pageNumberCount++;
                        totalRecordsCount += records.Entities.Count;
                        pagingCookie = records.PagingCookie;
                    }
                    else
                    {
                        totalRecordsCount += records.Entities.Count;
                        break;
                    }
                }
                if (totalRecordsCount > 50)
                {
                    double d_value = ((double)totalRecordsCount);
                    double fvalue = d_value / 50;
                    string svalue = fvalue.ToString();
                    string[] a = svalue.Split(new char[] { '.' });
                    int decimals = a[1].Length;
                    p_value = Int32.Parse(a[0]);
                    if (a[1] != "")
                    {
                        p_value = Int32.Parse(a[0]) + 1;

                    }
                    this.lblTotalPagesCount.Text = "of " + p_value;
                    this.TotalRecordsAvailable.Text = "of " + 50;
                }
                else
                {
                    this.lblTotalPagesCount.Text = "of 1";
                    this.TotalRecordsAvailable.Text = "of " + totalRecordsCount;
                    this.btnNextPage.Enabled = false;
                    this.btnPreviousPage.Enabled = false;
                }

                var fetchXml = $@"<fetch count='50'>
                                      <entity name='teammembership'>
                                        <attribute name='teamid' />
                                        <attribute name='teammembershipid' />
                                        <attribute name='systemuserid' />
                                        <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                          <attribute name='name' />
                                          <attribute name='teamid' />
                                          <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                            <attribute name='objecttypecode' />
                                            <attribute name='teamtemplatename' />
                                            <attribute name='teamtemplateid' />
                                            <filter>
                                              <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                          </link-entity>
                                        </link-entity>
                                        <link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'>
                                          <attribute name='fullname' />
                                          <filter>
                                            <condition attribute='fullname' operator='eq' value='{userName}' />
                                          </filter>
                                        </link-entity>
                                      </entity>
                                    </fetch>";

                var teammemberships = Service.RetrieveMultiple(new FetchExpression(fetchXml));
                if (teammemberships.Entities.Count > 0)
                {
                    this.btnExportRecords.Enabled = true;
                    dwea.Result = teammemberships;
                }
                else
                {
                    this.gbRecords.Enabled = false;
                    this.bindingNavigator1.Enabled = false;
                    this.CurrentPage.Text = "0";
                    this.lblTotalPagesCount.Text = "of 0";
                    CommonDelegates.DisplayMessageBox(ParentForm, $"No Records of {entityLogicalName} found shared with User: {userName}", "Information", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Information);
                }
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;

                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                    }
                    else if (c.Result != null)
                    {
                        this.btnSelectAllRecords.Enabled = true;
                        this.btnUnSelectAll.Enabled = false;
                        var request = new RetrieveCurrentOrganizationRequest();
                        var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                        foreach (var teammembership in ((EntityCollection)c.Result).Entities)
                        {
                            var principalAccessRequest = new RetrievePrincipalAccessRequest
                            {
                                Principal = new EntityReference("systemuser", teammembership.GetAttributeValue<Guid>("systemuserid")),
                                Target = new EntityReference(entityLogicalName, new Guid(((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString()))
                            };

                            // Response will contain AccessRights mask, like AccessRights.WriteAccess | AccessRights.ReadAccess | ...
                            var principalAccessResponse = (RetrievePrincipalAccessResponse)Service.Execute(principalAccessRequest);

                            Image ReadAccess = Resources.No;//(System.Drawing.Image)(resources.GetObject("No.Image"));
                            var ReadAccessFlag = "No";
                            Image WriteAccess = Resources.No;
                            var WriteAccessFlag = "No";
                            Image AppendAccess = Resources.No;
                            var AppendAccessFlag = "No";
                            Image AppendToAccess = Resources.No;
                            var AppendToAccessFlag = "No";
                            Image DeleteAccess = Resources.No;
                            var DeleteAccessFlag = "No";
                            Image ShareAccess = Resources.No;
                            var ShareAccessFlag = "No";
                            Image AssignAccess = Resources.No;
                            var AssignAccessFlag = "No";

                            if (principalAccessResponse.AccessRights.ToString().Contains("ReadAccess"))
                            {
                                ReadAccess = Resources.Yes;
                                ReadAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("WriteAccess"))
                            {
                                WriteAccess = Resources.Yes;
                                WriteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendAccess"))
                            {
                                AppendAccess = Resources.Yes;
                                AppendAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendToAccess"))
                            {
                                AppendToAccess = Resources.Yes;
                                AppendToAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("DeleteAccess"))
                            {
                                DeleteAccess = Resources.Yes;
                                DeleteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("ShareAccess"))
                            {
                                ShareAccess = Resources.Yes;
                                ShareAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AssignAccess"))
                            {
                                AssignAccess = Resources.Yes;
                                AssignAccessFlag = "Yes";
                            }
                            DataGridViewLinkColumn lnk = new DataGridViewLinkColumn();
                            lnk.Name = string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString());
                            lnk.Text = "Copy Record URL";
                            lnk.UseColumnTextForLinkValue = true;
                            dataGridView1.Rows.Add(false, ((AliasedValue)teammembership["record." + entityPrimaryAttributeName]).Value.ToString(), ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString(), ((AliasedValue)teammembership["user.fullname"]).Value.ToString(), ReadAccess, WriteAccess, AppendAccess, AppendToAccess, DeleteAccess, ShareAccess, AssignAccess, lnk.Text, teammembership.GetAttributeValue<Guid>("systemuserid"), ((AliasedValue)teammembership["teamtemplate.teamtemplateid"]).Value.ToString(), false, ReadAccessFlag, WriteAccessFlag, AppendAccessFlag, AppendToAccessFlag, DeleteAccessFlag, ShareAccessFlag, AssignAccessFlag);
                        }
                    }
                }
            });
        }

        //Method for Pagination
        public string CreateXml(string xml, string cookie, int page, int count)
        {
            StringReader stringReader = new StringReader(xml);
            XmlTextReader reader = new XmlTextReader(stringReader);

            // Load document
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            return CreateXml1(doc, cookie, page, count);
        }

        //Method for Pagination
        public string CreateXml1(XmlDocument doc, string cookie, int page, int count)
        {
            XmlAttributeCollection attrs = doc.DocumentElement.Attributes;
            if (cookie != null)
            {
                XmlAttribute pagingAttr = doc.CreateAttribute("paging-cookie");
                pagingAttr.Value = cookie;
                attrs.Append(pagingAttr);
            }

            XmlAttribute pageAttr = doc.CreateAttribute("page");
            pageAttr.Value = System.Convert.ToString(page);
            attrs.Append(pageAttr);

            XmlAttribute countAttr = doc.CreateAttribute("count");
            countAttr.Value = System.Convert.ToString(count);
            attrs.Append(countAttr);

            StringBuilder sb = new StringBuilder(1024);
            StringWriter stringWriter = new StringWriter(sb);

            XmlTextWriter writer = new XmlTextWriter(stringWriter);
            doc.WriteTo(writer);
            writer.Close();

            return sb.ToString();
        }

        //Method to open dialog for selecting location for saving Excel file 
        private void RequestFileDetails()
        { 
            var dialog = new SaveFileDialog
            {
                Filter = "Excel  Workbook(*.xlsx)|*.xlsx",
                FileName = $"{lvEntities.SelectedItems[0].SubItems[0].Text}-{DateTime.Today.ToString("yyyyMMdd")}.xlsx"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                ExecuteMethod(ExportDataToExcel, dialog.FileName);
            }
        }

        //Method to add data to Excel file
        private void WriteToExcel(List<string> headers, List<List<string>> rows, string fileName)
        {
            var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            FileInfo newFile = new FileInfo(fileName);
            var rowsCount = rows.Count + 1 + "";

            Random rnd = new Random();
            int num = rnd.Next();

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // add a new worksheet to the empty workbook
                var ws = package.Workbook.Worksheets.Add("Result"+ num);
                var range = ws.SelectedRange["A1:J" + rowsCount];

                ws.Cells["A1:J1"].Style.Fill.PatternType = ExcelFillStyle.Solid;

                Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#0070C0");
                ws.SelectedRange["A1:J1"].Style.Font.Color.SetColor(Color.White);
                ws.SelectedRange["A1:J1"].Style.Fill.BackgroundColor.SetColor(colFromHex);
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Color.SetColor(Color.Black);
                range.Style.Border.Top.Color.SetColor(Color.Black);
                range.Style.Border.Left.Color.SetColor(Color.Black);
                range.Style.Border.Right.Color.SetColor(Color.Black);

                //write headers first
                for (var columnNumber = 0; columnNumber < headers.Count; columnNumber++)
                {
                    ws.Cells[1, columnNumber + 1].Value = headers[columnNumber];
                }
                //actual data rows
                for (var rowNumber = 0; rowNumber < rows.Count; rowNumber++)
                {
                    for (var columnNumber = 0; columnNumber < headers.Count; columnNumber++)
                    {
                        ws.Cells[rowNumber + 2, columnNumber + 1].Value = rows[rowNumber][columnNumber];
                    }
                }
                ws.Columns.AutoFit(1);
                ws.Columns.AutoFit(2);
                ws.Columns.AutoFit(10);

                if (!rows.Any()) return;

                package.File = fileNumber > 0 ?
                    new FileInfo(fileName.Replace(".xlsx", $"_{fileNumber}.xlsx")) :
                    new FileInfo(fileName);
                fileNumber++;
                package.Save();
            }
        }
        //Method to Add data to rows and send rows to adding into excel by calling "WriteToExcel" method
        private void ExportDataToExcel(string fileName)
        {
            WorkAsync(new WorkAsyncInfo("Exporting data to excel...", (w, e) =>
            {
                var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
                var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
                var entityObjectTypeCode = lvEntities.SelectedItems[0].SubItems[3].Text;

                fileNumber = 0;
                var headers = new List<string>();
                var rows = new List<List<string>>();
                var request = new RetrieveCurrentOrganizationRequest();
                var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);

                foreach (DataGridViewRow result in dataGridView1.Rows)
                {
                    var rowValues = new List<string>();

                    rowValues.Add(result.Cells[1].Value.ToString());
                    rowValues.Add(result.Cells[3].Value.ToString());
                    rowValues.Add(result.Cells[15].Value.ToString());
                    rowValues.Add(result.Cells[16].Value.ToString());
                    rowValues.Add(result.Cells[17].Value.ToString());
                    rowValues.Add(result.Cells[18].Value.ToString());
                    rowValues.Add(result.Cells[19].Value.ToString());
                    rowValues.Add(result.Cells[20].Value.ToString());
                    rowValues.Add(result.Cells[21].Value.ToString());
                    rowValues.Add(organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + result.Cells[2].Value);

                    rows.Add(rowValues);
                }
                headers.Add("Primary Attribute");
                headers.Add("User Name");
                headers.Add("ReadAccess");
                headers.Add("WriteAccess");
                headers.Add("AppendAccess");
                headers.Add("AppendToAccess");
                headers.Add("DeleteAccess");
                headers.Add("ShareAccess");
                headers.Add("AssignAccess");
                headers.Add("Record URL");

                WriteToExcel(headers, rows, fileName);

                rows.Clear();
            })
            {
                IsCancelable = true,
                PostWorkCallBack = (c) =>
                {
                    if (c.Error != null)
                    {
                        MessageBox.Show(this, "An error occured while exporting the Excel file: " + c.Error.ToString(), "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show(this, "Do you want to open exported file?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            Process.Start(fileName);
                        }
                    }
                },
                ProgressChanged = (c) => SetWorkingMessage(c.UserState.ToString())
            });
        }

        //Method triggers on Selecting items in Filterby ComboBox
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
            if (cbFilterBy.SelectedItem == null)
            {
                this.lblprimaryAttributeValue.Visible = false;
                this.txtPrimaryAttributeValue.Visible = false;
                this.lblRecordId.Visible = false;
                this.txtRecordId.Visible = false;
                this.txtRecordId.Text = "";
                this.txtPrimaryAttributeValue.Text = "";
            }
            else
            {
                var filterBy = cbFilterBy.SelectedItem.ToString();

                if (filterBy.Equals(entityPrimaryAttributeName))
                {
                    this.lblprimaryAttributeValue.Visible = true;
                    this.txtPrimaryAttributeValue.Visible = true;
                    this.lblRecordId.Visible = false;
                    this.txtRecordId.Visible = false;
                    this.txtRecordId.Text = "";
                    this.cbUser.Text = "";
                }
                else
                {
                    this.lblRecordId.Visible = true;
                    this.txtRecordId.Visible = true;
                    this.lblprimaryAttributeValue.Visible = false;
                    this.txtPrimaryAttributeValue.Visible = false;
                    this.txtPrimaryAttributeValue.Text = "";
                    this.cbUser.Text = "";
                }
            }
        }

        //Method triggers on click of Search button and load records accordingly
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var primaryAttributeValue = txtPrimaryAttributeValue.Text.ToString();
            var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();           
            var entityPrimaryAttributeName = lvEntities.SelectedItems[0].SubItems[2].Text;
            var entityObjectTypeCode = lvEntities.SelectedItems[0].SubItems[3].Text;
            var totalRecordsCount = 0;
            var filterCondition = "";
            this.gbRecords.Enabled = true;
            this.dataGridView1.ColumnHeadersVisible = true;
            this.bindingNavigator1.Enabled = true;
            this.CurrentPage.Text = "1";
            this.cbUser.Text = "";
            this.lblSelectedRows.Text = "Selected 0";
            this.btnRemoveSelectedRecords.Enabled = false;

            var msg = "";
            if (txtPrimaryAttributeValue.Visible)
            {
                Guid x;
                var isguid = Guid.TryParse(txtPrimaryAttributeValue.Text, out x);
                if (!isguid)
                {
                    filterCondition = $" <condition attribute='{entityPrimaryAttributeName}' operator='eq' value='{txtPrimaryAttributeValue.Text}'/>";
                    msg = "Listing shared records having " + entityPrimaryAttributeName + " : " + txtPrimaryAttributeValue.Text + " shared via Access Team";
                }
                else
                {
                    CommonDelegates.DisplayMessageBox(ParentForm, "Please Provide Correct Name!", "Information", MessageBoxButtons.OK,
                                                             MessageBoxIcon.Information);
                    return;
                }
            }
            else if (txtRecordId.Visible)
            {
                Guid x;
                var isguid = Guid.TryParse(txtRecordId.Text, out x);
                if (isguid)
                {
                    filterCondition = $" <condition attribute='{entityLogicalName}id' operator='eq' value='{txtRecordId.Text}'/>";
                    msg = "Listing shared records with GUID : " + txtRecordId.Text + " shared via Access Team";
                }
                else
                {
                    CommonDelegates.DisplayMessageBox(ParentForm, "Please Provide Correct Record Id!", "Information", MessageBoxButtons.OK,
                                                             MessageBoxIcon.Information);
                    return;
                }
            }
            WorkAsync(new WorkAsyncInfo($"{msg}...", dwea =>
            {
                int fetchCount = 5000;
                int pageNumberCount = 1;
                string pagingCookie = null;
                var fetchXMLforTotal = $@"<fetch count='5000'>
                                  <entity name='teammembership'>
                                    <attribute name='teamid' />
                                    <attribute name='teammembershipid' />
                                    <attribute name='systemuserid' />
                                    <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                      <attribute name='name' />
                                      <attribute name='teamid' />
                                      <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                        <attribute name='objecttypecode' />
                                        <attribute name='teamtemplatename' />
                                        <attribute name='teamtemplateid' />
                                       <filter>
                                          <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                              <filter>" + filterCondition;
                fetchXMLforTotal += "</filter></link-entity></link-entity><link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'><attribute name='fullname'/></link-entity></entity></fetch>";

                while (true)
                {
                    string xml = CreateXml(fetchXMLforTotal, pagingCookie, pageNumberCount, fetchCount);

                    RetrieveMultipleRequest fetchRequest1 = new RetrieveMultipleRequest
                    {
                        Query = new FetchExpression(xml)
                    };

                    EntityCollection records = ((RetrieveMultipleResponse)Service.Execute(fetchRequest1)).EntityCollection;
                    var request = new RetrieveCurrentOrganizationRequest();
                    var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                    this.dataGridView1.Rows.Clear();
                    if (records.MoreRecords)
                    {
                        pageNumberCount++;
                        totalRecordsCount += records.Entities.Count;
                        pagingCookie = records.PagingCookie;
                    }
                    else
                    {
                        totalRecordsCount += records.Entities.Count;
                        break;
                    }
                }
                if (totalRecordsCount > 50)
                {
                    double d_value = ((double)totalRecordsCount);
                    double fvalue = d_value / 50;
                    string svalue = fvalue.ToString();
                    string[] a = svalue.Split(new char[] { '.' });
                    int decimals = a[1].Length;
                    p_value = Int32.Parse(a[0]);
                    if (a[1] != "")
                    {
                        p_value = Int32.Parse(a[0]) + 1;

                    }
                    this.lblTotalPagesCount.Text = "of " + p_value;
                    this.TotalRecordsAvailable.Text = "of " + 50;
                }
                else
                {
                    this.lblTotalPagesCount.Text = "of 1";
                    this.TotalRecordsAvailable.Text = "of " + totalRecordsCount;
                    this.btnNextPage.Enabled = false;
                    this.btnPreviousPage.Enabled = false;

                }

                var fetchXml = $@"<fetch count='50'>
                                      <entity name='teammembership'>
                                        <attribute name='teamid' />
                                        <attribute name='teammembershipid' />
                                        <attribute name='systemuserid' />
                                        <link-entity name='team' from='teamid' to='teamid' alias='team'>
                                          <attribute name='name' />
                                          <attribute name='teamid' />
                                          <link-entity name='teamtemplate' from='teamtemplateid' to='teamtemplateid' alias='teamtemplate'>
                                            <attribute name='objecttypecode' />
                                            <attribute name='teamtemplatename' />
                                        <attribute name='teamtemplateid' />
                                            <filter>
                                              <condition attribute='objecttypecode' operator='eq' value='{entityObjectTypeCode}' />
                                            </filter>
                                          </link-entity>
                                          <link-entity name='{entityLogicalName}' from='{entityLogicalName}id' to='regardingobjectid' alias='record'>
                                                <order attribute='{entityPrimaryAttributeName}' descending='false' />
                                            <attribute name='{entityPrimaryAttributeName}' />
                                            <attribute name='{entityLogicalName}id'/>
                                              <filter>" + filterCondition;
                fetchXml += "</filter></link-entity></link-entity><link-entity name='systemuser' from='systemuserid' to='systemuserid' link-type='inner' alias='user'><attribute name='fullname'/></link-entity></entity></fetch>";

                var teammemberships = Service.RetrieveMultiple(new FetchExpression(fetchXml));
                if (teammemberships.Entities.Count > 0)
                {
                    this.btnExportRecords.Enabled = true;
                    dwea.Result = teammemberships;
                }
                else
                {
                    this.gbRecords.Enabled = false;
                    this.bindingNavigator1.Enabled = false;
                    this.CurrentPage.Text = "0";
                    this.lblTotalPagesCount.Text = "of 0";
                    CommonDelegates.DisplayMessageBox(ParentForm, "No Record Found", "Information", MessageBoxButtons.OK,
                                                          MessageBoxIcon.Information);
                }
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;

                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                        MessageBoxIcon.Error);
                    }
                    else if (c.Result != null)
                    {
                        this.btnSelectAllRecords.Enabled = true;
                        this.btnUnSelectAll.Enabled = false;
                        var request = new RetrieveCurrentOrganizationRequest();
                        var organzationResponse = (RetrieveCurrentOrganizationResponse)Service.Execute(request);
                        foreach (var teammembership in ((EntityCollection)c.Result).Entities)
                        {
                            var principalAccessRequest = new RetrievePrincipalAccessRequest
                            {
                                Principal = new EntityReference("systemuser", teammembership.GetAttributeValue<Guid>("systemuserid")),
                                Target = new EntityReference(entityLogicalName, new Guid(((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString()))
                            };

                            var principalAccessResponse = (RetrievePrincipalAccessResponse)Service.Execute(principalAccessRequest);
                            Image ReadAccess = Resources.No;
                            var ReadAccessFlag = "No";
                            Image WriteAccess = Resources.No;
                            var WriteAccessFlag = "No";
                            Image AppendAccess = Resources.No;
                            var AppendAccessFlag = "No";
                            Image AppendToAccess = Resources.No;
                            var AppendToAccessFlag = "No";
                            Image DeleteAccess = Resources.No;
                            var DeleteAccessFlag = "No";
                            Image ShareAccess = Resources.No;
                            var ShareAccessFlag = "No";
                            Image AssignAccess = Resources.No;
                            var AssignAccessFlag = "No";

                            if (principalAccessResponse.AccessRights.ToString().Contains("ReadAccess"))
                            {
                                ReadAccess = Resources.Yes;
                                ReadAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("WriteAccess"))
                            {
                                WriteAccess = Resources.Yes;
                                WriteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendAccess"))
                            {
                                AppendAccess = Resources.Yes;
                                AppendAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AppendToAccess"))
                            {
                                AppendToAccess = Resources.Yes;
                                AppendToAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("DeleteAccess"))
                            {
                                DeleteAccess = Resources.Yes;
                                DeleteAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("ShareAccess"))
                            {
                                ShareAccess = Resources.Yes;
                                ShareAccessFlag = "Yes";
                            }
                            if (principalAccessResponse.AccessRights.ToString().Contains("AssignAccess"))
                            {
                                AssignAccess = Resources.Yes;
                                AssignAccessFlag = "Yes";
                            }
                            DataGridViewLinkColumn lnk = new DataGridViewLinkColumn();
                            lnk.Name = string.Format("{0}", organzationResponse.Detail.Endpoints[key: EndpointType.WebApplication] + "main.aspx?etn=" + entityLogicalName + "&pagetype=entityrecord&id=" + ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString());
                            lnk.Text = "Copy Record URL";
                            lnk.UseColumnTextForLinkValue = true;
                            dataGridView1.Rows.Add(false, ((AliasedValue)teammembership["record." + entityPrimaryAttributeName]).Value.ToString(), ((AliasedValue)teammembership["record." + entityLogicalName + "id"]).Value.ToString(), ((AliasedValue)teammembership["user.fullname"]).Value.ToString(), ReadAccess, WriteAccess, AppendAccess, AppendToAccess, DeleteAccess, ShareAccess, AssignAccess, lnk.Text, teammembership.GetAttributeValue<Guid>("systemuserid"), ((AliasedValue)teammembership["teamtemplate.teamtemplateid"]).Value.ToString(), false, ReadAccessFlag, WriteAccessFlag, AppendAccessFlag, AppendToAccessFlag, DeleteAccessFlag, ShareAccessFlag, AssignAccessFlag);
                        }
                    }
                }
            });
        }

        //Method trigger on click of "Export Records" button
        private void btnExportRecords_Click(object sender, EventArgs e)
        {
            RequestFileDetails();
        }
        bool flag = false;

        //Method triggers on click of "Remove Selected Records" button and removes selected records
        private void btnRemoveSelectedRecords_Click(object sender, EventArgs e)
        {
            WhoAmIRequest systemUserRequest = new WhoAmIRequest();
            WhoAmIResponse systemUserResponse = (WhoAmIResponse)Service.Execute(systemUserRequest);
            Guid userId = systemUserResponse.UserId;

            var User = Service.Retrieve("systemuser", userId, new ColumnSet("fullname"));
            string fullName = User["fullname"].ToString();

            var fetchXML = $@"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='true'>
                            <entity name='role'>
                            <attribute name='name' />
                            <attribute name='businessunitid' />
                            <attribute name='roleid' />
                            <order attribute='name' descending='false' />
                            <link-entity name='systemuserroles' from='roleid' to='roleid' visible='false' intersect='true'>
                                <link-entity name='systemuser' from='systemuserid' to='systemuserid' alias='ab'>
                                <filter type='and'>
                                    <condition attribute='systemuserid' operator='eq' value='{userId}' />
                                </filter>
                                </link-entity>
                            </link-entity>
                            </entity>
                        </fetch>";

            var Roles = Service.RetrieveMultiple(new FetchExpression(fetchXML));
            int a = Roles.Entities.Count;
            string[] rolename = new string[a];
            var i = 0;
            foreach (Entity role in Roles.Entities)
            {
                rolename[i] = (string)role["name"];
                if (rolename[i] == "System Administrator")
                {
                    flag = true;
                }
                i++;
            }
            if (flag)
            {
                var entityLogicalName = lvEntities.SelectedItems[0].Tag.ToString();
                Cursor = Cursors.WaitCursor;
                WorkAsync(new WorkAsyncInfo($"Removing selected shared records of {entityLogicalName} with User...", dwea =>
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[14].Value.ToString().Equals("True"))
                        {
                            EntityReference ef = new EntityReference(entityLogicalName, new Guid(row.Cells[2].Value.ToString()));
                            RemoveUserFromRecordTeamRequest RemoveReq = new RemoveUserFromRecordTeamRequest()
                            {
                                Record = ef,
                                SystemUserId = new Guid(row.Cells[12].Value.ToString()),
                                TeamTemplateId = new Guid(row.Cells[13].Value.ToString())
                            };
                            Service.Execute(RemoveReq);

                        }
                    }
                    dwea.Result = "Records removed successfully";
                })
                {
                    PostWorkCallBack = c =>
                     {
                         Cursor = Cursors.Default;
                         if (c.Error != null)
                         {
                             string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                             CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                                               MessageBoxIcon.Error);
                         }
                         else if (c.Result != null)
                         {
                             CommonDelegates.DisplayMessageBox(ParentForm, c.Result.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         }

                         this.lblSelectedRows.Text = "Selected 0";
                         this.btnRemoveSelectedRecords.Enabled = false;
                         RetrieveTeamMembershipRecords();
                     }
                });
            }
            else
            {
                MessageBox.Show(this, fullName + " has no Admin Privileges.", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Method triggers when user fills data into "RecordId" Textbox and Enable's/Disable's "Remove Selected Records" button accordingly
        private void OnChangeOfTextIRecordIdTextBox(object sender, EventArgs e)
        {
            if (this.txtRecordId.Text.Equals("") || this.txtRecordId.Visible.Equals("false"))
                this.btnSearch.Enabled = false;
            else
                this.btnSearch.Enabled = true;

        }

        //Method triggers when user fills data into "Primary Attribute Value" Textbox and Enable's/Disable's "Remove Selected Records" button accordingly
        private void OnChangeOfTextInPrimaryAttributeValueTextBox(object sender, EventArgs e)
        {
            if (this.txtPrimaryAttributeValue.Text.Equals("") || this.txtPrimaryAttributeValue.Visible.Equals("false"))
                this.btnSearch.Enabled = false;
            else
                this.btnSearch.Enabled = true;
        }
        //Method triggers on click of "Select All Records" button, select All the records of DataGridView
        private void btnSelectAllRecords_Click(object sender, System.EventArgs e)
        {
            this.CellFlag.Visible = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[22];
            this.CellFlag.Visible = false;
            
            WorkAsync(new WorkAsyncInfo($"Selecting record", dwea =>
            {
                var temp = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[14].Value = true;
                    row.Cells[0].Value = true;
                    temp = 0;
                }
                this.btnRemoveSelectedRecords.Enabled = true;
            
            dwea.Result = temp ;
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;
                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                                          MessageBoxIcon.Error);
                    }
                    this.lblSelectedRows.Text = "Selected " + dataGridView1.Rows.Count.ToString();
                    this.btnUnSelectAll.Enabled = true;
                    this.btnSelectAllRecords.Enabled = false;
                }
            });
        }
        //Method triggers on click of "Unselect All Records" button, Deselect All the records of DataGridView.
        private void btnUnSelectAll_Click(object sender, EventArgs e)
        {

            this.CellFlag.Visible = true;
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[22];
            this.CellFlag.Visible = false;
            WorkAsync(new WorkAsyncInfo($"Unselecting record", dwea =>
            {
                var temp = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[14].Value = false;
                    row.Cells[0].Value = false;
                    temp = 0;
                }
                this.btnRemoveSelectedRecords.Enabled = false;
                dwea.Result = temp;
            })
            {
                PostWorkCallBack = c =>
                {
                    Cursor = Cursors.Default;
                    if (c.Error != null)
                    {
                        string errorMessage = CrmExceptionHelper.GetErrorMessage(c.Error, true);
                        CommonDelegates.DisplayMessageBox(ParentForm, errorMessage, "Error", MessageBoxButtons.OK,
                                                                          MessageBoxIcon.Error);
                    }
                    this.lblSelectedRows.Text = "Selected 0";
                    this.btnSelectAllRecords.Enabled = true;
                    this.btnUnSelectAll.Enabled = false;
                }
            });
        }
    }
}


