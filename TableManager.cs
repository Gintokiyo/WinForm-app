using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ProiectMediiVizuale
{
    public partial class TableManager : Form
    {
        #region fields
        //Current table name
        private string _tableName;
        //Current table primary key column name
        private string _primaryFieldName;
        //Current cell id and a value for when we double click a cell and open this form
        private int _cellPrimaryKey;
        private object _optionalCellValue; //Using it for the selected team name to display in a label
        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues">A list of values, where I'm expecting 
        ///                         [0]Current Table name
        ///                         [1]Prymary Key column name
        ///                         [2]Prymary Key for the double clicked cell
        ///                         [3+] The other column values if we need them</param>
        public TableManager(List<object> keyValueList)
        {
            this._tableName = keyValueList[0].ToString();
            this._primaryFieldName = keyValueList[1].ToString();
            if (keyValueList.Count > 2)
            {
                this._cellPrimaryKey = Convert.ToInt32(keyValueList[2]);
                this._optionalCellValue = keyValueList[3];
            }
            InitializeComponent();
        }
        #endregion

        #region methods
        /// <summary>
        /// Load form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            if(this._cellPrimaryKey == 0)
            {
                groupBoxTeamManager.Text = this._tableName + "s";
            }
            else
            {
                groupBoxTeamManager.Text = this._optionalCellValue.ToString();
            }
            //Display the Team table in the grid.
            this.UpdatePage();

        }
        /// <summary>
        /// Lazy error viewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonErrorWindow_Click(object sender, EventArgs e)
        {
            var error = new ErrorsViewer();
            error.ShowDialog();
        }

        /// <summary>
        /// Add team form button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddTeam_Click(object sender, EventArgs e)
        {
            List<string> labelList;
            ObjectsManager addObjects;
            switch(this._tableName)
            {
                case "Team":
                    labelList = new List<string>() { "Name:", "", "", "", "" };
                    addObjects = new ObjectsManager(labelList, this._tableName, this);
                    break;
                case "Role":
                    labelList = new List<string>() { "", "", "", "", "Description:" };
                    addObjects = new ObjectsManager(labelList, this._tableName, this);
                    break;
                case "Member":
                    labelList = new List<string>() { "First Name:", "Last Name:", "Team:", "Role:", "" };
                    addObjects = new ObjectsManager(labelList, this._tableName, this._cellPrimaryKey, this);
                    break;
                default:
                    labelList = new List<string>() { "", "", "", "", "" };
                    addObjects = new ObjectsManager(labelList, this._tableName, this);
                    break;
            }
            addObjects.ShowDialog();
            this.UpdatePage();
        }

        /// <summary>
        /// Delete 1 or more teams that we select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteTeam_Click(object sender, EventArgs e) //Needs to be fixed for deleting members from a team
        {
            var listOfIDs = new List<int>();
            int colCount = dataGridViewTable.Columns.Count;
            int i = colCount; //Next foreach checks each column, we just want the IDs
            foreach (DataGridViewCell item in dataGridViewTable.SelectedCells)
            {
                if(i % colCount == 0)
                {
                    listOfIDs.Add((int)item.Value);
                }
                i++;
            }
            if(this._tableName == "Member")
            {
                DbCommands.DeleteTableData(this._tableName, listOfIDs, "MemberID");
            }
            else DbCommands.DeleteTableData(this._tableName, listOfIDs, this._primaryFieldName);
            this.UpdatePage();
        }
        /// <summary>
        /// Reload the page
        /// </summary>
        public void UpdatePage()
        {
            if(this._tableName == "Member" && this._cellPrimaryKey != 0)
            {
                var primaryFieldTeam = new KeyValuePair<string, object>(this._primaryFieldName, this._cellPrimaryKey);
                var tableData = DbCommands.FilterTableData(this._tableName, primaryFieldTeam);
                dataGridViewTable.DataSource = tableData.Tables[this._tableName];
            }
            else
            {
                var tableData = DbCommands.GetTableData(this._tableName);
                dataGridViewTable.DataSource = tableData.Tables[this._tableName];
            }
            
        }
        /// <summary>
        /// On double clicking a cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewTeams_CellDoubleClick(object sender, DataGridViewCellEventArgs e) //Needs testing
        {
            var selectedCellID = dataGridViewTable.SelectedCells[0].Value;
            var selectedCellName = dataGridViewTable.SelectedCells[1].Value;
            List<string> labelList;
            List<object> cellKeyValueList;
            switch (this._tableName)
            {
                
                case "Team":
                    cellKeyValueList = new List<object>() { "Member", this._primaryFieldName, selectedCellID, selectedCellName };
                    if (selectedCellID.GetType() == typeof(int))
                    {
                        var editTeam = new TableManager(cellKeyValueList);
                        editTeam.ShowDialog();
                    }
                    break;
                case "Member":
                    labelList = new List<string>() { "First Name:", "Last Name:", "Team:", "Role:", "" };
                    var selectedCellLastName = dataGridViewTable.SelectedCells[2].Value;
                    var selectedCellTeamID = dataGridViewTable.SelectedCells[3].Value;
                    var selectedCellRoleID = dataGridViewTable.SelectedCells[4].Value;
                    cellKeyValueList = new List<object>() { selectedCellID, selectedCellName, selectedCellLastName,
                                                            selectedCellTeamID, selectedCellRoleID };
                    var editMember = new ObjectsManager(labelList, this._tableName, cellKeyValueList, this);
                    editMember.ShowDialog();
                    break;
                case "Role":
                    labelList = new List<string>() { "", "", "", "", "Description:" };
                    cellKeyValueList = new List<object>() { selectedCellID, selectedCellName };
                    var editRole = new ObjectsManager(labelList, this._tableName, cellKeyValueList, this);
                    editRole.ShowDialog();
                    break;
                default:
                    break;
            }
            this.UpdatePage();
        }
        #endregion
    }
}
