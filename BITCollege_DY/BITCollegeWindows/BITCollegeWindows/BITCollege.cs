using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BITCollegeWindows
{
    public partial class BITCollege : Form
    {
        public BITCollege()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close the application.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Open the Student Data form within this MDI Frame.
        /// </summary>
        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentData student = new StudentData();
            student.MdiParent = this;
            student.Show();
        }

        /// <summary>
        /// Open the Batch form within this MDI Frame.
        /// </summary>
        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchUpdate batch = new BatchUpdate();
            batch.MdiParent = this;
            batch.Show();
        }
    }
}
