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
    public partial class ErrorsViewer : Form
    {
        #region constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ErrorsViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region methods
        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ErrorsViewer_Load(object sender, EventArgs e)
        {
            if (DbCommands.ErrorFeed == "") DbCommands.ErrorFeed = "No issues found.";
            textBoxErrorFeed.Invoke(new Action(() => textBoxErrorFeed.Text = DbCommands.ErrorFeed));
        }
        #endregion
    }
}
