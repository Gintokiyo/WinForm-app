using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectMediiVizuale
{
    public partial class Entrance : Form
    {
        #region fields
        private List<string> _tableNamesList;
        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        public Entrance()
        {
            InitializeComponent();
        }
        #endregion

        #region methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entrance_Load(object sender, EventArgs e)
        {
            //Create our database if it doesn't exist.
            string dbName = "alexdatabase";
            //DbCommands.DeleteDatabase(dbName);
            DbCommands.CreateDatabase(dbName);
            this._tableNamesList = DbCommands.GetTableNames();
            //List contains Team[0], Role[1], Member[2]
            buttonMembers.Text = this._tableNamesList[2];
            buttonTeams.Text = this._tableNamesList[0];
            buttonRoles.Text = this._tableNamesList[1];
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonMembers_Click(object sender, EventArgs e)
        {
            var keyValueList = new List<object>();
            var tableColumnNames = DbCommands.GetTableColumnNames(this._tableNamesList[2]);
            keyValueList.Add(this._tableNamesList[2]);
            keyValueList.Add(tableColumnNames[0]);
            var memberForm = new TableManager(keyValueList);
            memberForm.ShowDialog();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTeams_Click(object sender, EventArgs e)
        {
            var keyValueList = new List<object>();
            var tableColumnNames = DbCommands.GetTableColumnNames(this._tableNamesList[0]);
            keyValueList.Add(this._tableNamesList[0]);
            keyValueList.Add(tableColumnNames[0]);
            var teamsForm = new TableManager(keyValueList);
            teamsForm.ShowDialog();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRoles_Click(object sender, EventArgs e)
        {
            var keyValueList = new List<object>();
            var tableColumnNames = DbCommands.GetTableColumnNames(this._tableNamesList[1]);
            keyValueList.Add(this._tableNamesList[1]);
            keyValueList.Add(tableColumnNames[0]);
            var rolesForm = new TableManager(keyValueList);
            rolesForm.ShowDialog();
        }

        #endregion
    }
}
