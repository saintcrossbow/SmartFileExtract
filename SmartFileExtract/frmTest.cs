//Copyright(C) 2017  saintcrossbow@gmail.com

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.If not, see<http://www.gnu.org/licenses/>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFileExtract
{
    public partial class frmTest : Form
    {
        public frmTest()
        {
            InitializeComponent();
        }

        private void btnExtractTest_Click(object sender, EventArgs e)
        {
            showPop("");
            StartExtract();
        }

        private void StartExtract()
        {
            SmartFileExtract.FileSearch fileExtract = new SmartFileExtract.FileSearch();
            fileExtract.targetDrive = this.txtSearchDrive.Text.Split(';');
            fileExtract.targetFiles = this.txtFilePattern.Text.Split(';');
            fileExtract.keywords = this.txtKeywords.Text.Split(';');
            fileExtract.copyTo = this.txtCopyTo.Text;

            // Optional parameters
            // Total bytes (translated from MBs)
            if (!String.IsNullOrEmpty(txtMBs.Text))
            {
                long totalLimit = 0;
                if (long.TryParse(txtMBs.Text, out totalLimit))
                    fileExtract.maxBytesToCopy = totalLimit * 1048576;
            }

            // Total seconds to run
            if (!String.IsNullOrEmpty(txtSeconds.Text))
            {
                int totalSeconds = 0;
                if (int.TryParse(txtSeconds.Text, out totalSeconds))
                    fileExtract.maxSecondsToCopy = totalSeconds;
            }

            // Type of curtain
            if (!String.IsNullOrEmpty(cboCurtain.Text))
            {
                int selCurtain = 0;
                if (int.TryParse(cboCurtain.Text, out selCurtain))
                    fileExtract.curtainStyle = selCurtain;
            }

            // Type of performance
            if (!String.IsNullOrEmpty(cboPerformance.Text))
            {
                int selPerf = 0;
                if (int.TryParse(cboPerformance.Text, out selPerf))
                    fileExtract.performanceModel = selPerf;
            }

            // Start search
            if (fileExtract.isReady())
            {
                fileExtract.startExtract();
                showPop("Complete.");
            }
            else
            {
                showPop("Cannot run search - missing parameters");
            }
        }

        private void showPop(string caption)
        {
            if (caption == "")
                this.lblPop.Visible = false;
            else
            {
                this.lblPop.Text = caption;
                this.lblPop.Visible = true;
            }
        }
    }
}
