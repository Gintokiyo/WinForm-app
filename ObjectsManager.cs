using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProiectMediiVizuale
{
    public partial class ObjectsManager : Form
    {
        #region fields
        //First label and textbox keep the same location, it can be hidden
        private string _firstLabelName;
        //Second label and textbox keep the same location, it can be hidden
        private string _secondLabelName;
        //Third label and combobox keep the same location, it can be hidden
        private string _thirdLabelName;
        //Fourth label and combobox keep the same location, it can be hidden
        private string _fourthLabelName;
        //Fifth label and big textbox changes location, it can be hidden
        private string _fifthLabelName;
        //Variable to keep the current tableName that's using this form
        private string _tableName;
        //Team ID for when we add a member inside a team, so we have the default value and lock the selection
        private int _teamID;
        //List of keyValue objects for TeamID and RoleID to display in the combobox
        private List<KeyValuePair<object, object>> _storeTeamIDName = new List<KeyValuePair<object, object>>();
        private List<KeyValuePair<object, object>> _storeRoleIDDesc = new List<KeyValuePair<object, object>>();
        //Simple flag so we know what we're currently doing, True for editing values, false for adding new ones
        private bool _editModeFlag = true;
        //A list of objects from the selected cell when we edit
        List<object> _cellValueList;
        //Reference to the form from which we opened this form
        private TableManager _lastForm = null;
        #endregion

        #region constructor
        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="labelNames">List of strings which can be empty or not, let's us pick what we show</param>
        /// <param name="tableName"></param>
        /// <param name="cellValueList">Values for the selected cell
        ///                             [0] - ID
        ///                             [1] - First Name/Name/Description depending on the table
        ///                             [2] - Last Name
        ///                             [3] - TeamID
        ///                             [4] - RoleID</param>
        /// <param name="teamID">Team ID for when we add a new member to an already selected team</param>
        public ObjectsManager(List<string> labelNames, string tableName, List<object> cellValueList, int teamID, TableManager form)
        {
            this._firstLabelName = labelNames[0];
            this._secondLabelName = labelNames[1];
            this._thirdLabelName = labelNames[2];
            this._fourthLabelName = labelNames[3];
            this._fifthLabelName = labelNames[4];
            this._tableName = tableName;
            this._teamID = teamID;
            this._cellValueList = cellValueList;
            this._lastForm = form;
            InitializeComponent();
        }
        /// <summary>
        /// Constructor for new values
        /// </summary>
        /// <param name="labelNames"></param>
        /// <param name="tableName"></param>
        public ObjectsManager(List<string> labelNames, string tableName, TableManager form) : 
                this(labelNames, tableName, null, 0, form)
        {
            this._editModeFlag = false;
        }
        /// <summary>
        /// Special constructor for new values inside a team
        /// </summary>
        /// <param name="labelNames"></param>
        /// <param name="tableName"></param>
        /// <param name="teamID"></param>
        public ObjectsManager(List<string> labelNames, string tableName, int teamID, TableManager form) : 
                this(labelNames, tableName, null, teamID, form)
        {
            this._editModeFlag = false;
        }
        /// <summary>
        /// Constructor for when we want to edit fields
        /// </summary>
        /// <param name="labelNames"></param>
        /// <param name="tableName"></param>
        /// <param name="cellValueList"></param>
        public ObjectsManager(List<string> labelNames, string tableName, List<object> cellValueList, TableManager form) :
                this(labelNames, tableName, cellValueList, 0, form)
        {

        }
        #endregion

        #region methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ObjectsManager_Load(object sender, EventArgs e)
        {
            this.UpdatePage();
            //Editing labels depending on the labelList strings
            if (this._firstLabelName != "")
            {
                labelFirstName.Text = this._firstLabelName;
                labelFirstName.Visible = true;
                textBoxFirstName.Visible = true;
                if(this._editModeFlag && this._tableName == "Member")
                    textBoxFirstName.Text = this._cellValueList[1].ToString();
            }
            if(this._secondLabelName != "")
            {
                labelLastName.Text = this._secondLabelName;
                labelLastName.Visible = true;
                textBoxLastName.Visible = true;
                if(this._editModeFlag) 
                    textBoxLastName.Text = this._cellValueList[2].ToString();
            }
            if(this._thirdLabelName != "")
            {
                labelComboTeam.Text = this._thirdLabelName;
                labelComboTeam.Visible = true;
                comboBoxTeam.Visible = true;
                if(this._editModeFlag)
                {
                    foreach (var item in this._storeTeamIDName)
                    {
                        if(this._cellValueList[3] != DBNull.Value)
                        {
                            if ((int)item.Key == (int)this._cellValueList[3])
                            {
                                comboBoxTeam.SelectedItem = item.Value;
                            }
                        }
                    }
                }
            }
            if(this._fourthLabelName != "")
            {
                labelComboRole.Text = this._fourthLabelName;
                labelComboRole.Visible = true;
                comboBoxRole.Visible = true;
                if (this._editModeFlag)
                {
                    foreach (var item in this._storeRoleIDDesc)
                    {
                        if(this._cellValueList[4] != DBNull.Value)
                        {
                            if ((int)item.Key == (int)this._cellValueList[4])
                            {
                                comboBoxRole.SelectedItem = item.Value;
                            }
                        }
                    }
                }
            }
            if(this._fifthLabelName != "")
            {
                labelBigBoxDesc.Text = this._fifthLabelName;
                labelBigBoxDesc.Location = new Point(labelFirstName.Location.X, labelFirstName.Location.Y);
                labelBigBoxDesc.Visible = true;
                textBoxRoleDesc.Location = new Point(textBoxFirstName.Location.X, textBoxFirstName.Location.Y);
                textBoxRoleDesc.Visible = true;
                if(this._editModeFlag) 
                    textBoxRoleDesc.Text = this._cellValueList[1].ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            var itemList = new List<KeyValuePair<string, object>>();
            var valueList = new List<object>();
            var tableColumnNames = DbCommands.GetTableColumnNames(this._tableName);
            switch (this._tableName)
            {
                case "Team":
                    valueList.Add(textBoxFirstName.Text);
                    break;
                case "Role":
                    valueList.Add(textBoxRoleDesc.Text);
                    break;
                case "Member":
                    valueList.Add(textBoxFirstName.Text);
                    valueList.Add(textBoxLastName.Text);
                    if (comboBoxTeam.SelectedItem != null)
                    {
                        foreach (var someTeam in this._storeTeamIDName)
                        {
                            if (someTeam.Value.ToString() == comboBoxTeam.SelectedItem.ToString())
                            {
                                valueList.Add(someTeam.Key);
                                break;
                            }
                        }
                    }
                    else valueList.Add(DBNull.Value);
                    if (comboBoxRole.SelectedItem != null)
                    {
                        foreach (var someRole in this._storeRoleIDDesc)
                        {
                            if (someRole.Value.ToString() == comboBoxRole.SelectedItem.ToString())
                            {
                                valueList.Add(someRole.Key);
                                break;
                            }
                        }
                    }
                    else valueList.Add(DBNull.Value);
                    break;
                default:
                    break;
            }

            for (int i = 1; i < tableColumnNames.Count; i++)
            {
                itemList.Add(new KeyValuePair<string, object>(tableColumnNames[i], valueList[i - 1]));
            }
            
            if (this._editModeFlag)
            {
                var primaryField = new KeyValuePair<string, object>(tableColumnNames[0], this._cellValueList[0]);
                DbCommands.EditTableData(this._tableName, itemList, primaryField);
            }
            else
            {
                DbCommands.InsertData(this._tableName, itemList);
            }
            this._lastForm.UpdatePage();
        }
        /// <summary>
        /// Updates the page
        /// </summary>
        public void UpdatePage()
        {
            comboBoxTeam.Items.Clear();
            comboBoxRole.Items.Clear();
            this._storeTeamIDName.Clear();
            this._storeRoleIDDesc.Clear();
            var roleTeam = new string[] { "Role", "Team" };
            foreach (var someTable in roleTeam)
            {
                var setRoleTeam = DbCommands.GetTableData(someTable).Tables[someTable].Rows;
                var rowCountRoleTeam = setRoleTeam.Count;
                for (int i = 0; i < rowCountRoleTeam; i++)
                {
                    switch(someTable)
                    {
                        case "Role":
                            comboBoxRole.Items.Add(setRoleTeam[i].ItemArray[1]);
                            this._storeRoleIDDesc.Add(new KeyValuePair<object, object>(setRoleTeam[i].ItemArray[0], setRoleTeam[i].ItemArray[1]));
                            break;
                        case "Team":
                            comboBoxTeam.Items.Add(setRoleTeam[i].ItemArray[1]);
                            if((int)setRoleTeam[i].ItemArray[0] == this._teamID)
                            {
                                int itemIndex = comboBoxTeam.Items.IndexOf(setRoleTeam[i].ItemArray[1]);
                                comboBoxTeam.SelectedItem = comboBoxTeam.Items[itemIndex];
                                comboBoxTeam.Enabled = false;
                            }
                            this._storeTeamIDName.Add(new KeyValuePair<object, object>(setRoleTeam[i].ItemArray[0], setRoleTeam[i].ItemArray[1]));
                            break;
                        default:
                            comboBoxRole.Items.Add("Error");
                            comboBoxTeam.Items.Add("Error");
                            break;
                    }
                    
                }
            }
        }
        #endregion
    }
}
