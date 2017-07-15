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
    public partial class frmInstall : Form
    {
        public bool killCall { get; set; }

        public frmInstall()
        {
            killCall = false;
            InitializeComponent();
            this.Refresh();
            Application.DoEvents();
            this.Update();
        }

        public void status(string statusText, bool aggressivePerf = false)
        {
            lblStat.Text = statusText;
            lblStat.Update();
            if (!aggressivePerf)
            {
                this.Refresh();
                Application.DoEvents();
            }
        }

        public void increment(int min, int max, int current)
        {
            try
            {
                this.progressMain.Minimum = min;
                this.progressMain.Maximum = max;
                this.progressMain.Value = current;
                this.progressMain.Update();
            }
            catch (Exception)
            {
            }
        }

        public void kill()
        {
            this.Close();
        }
      
        private void btnKill_Click_1(object sender, EventArgs e)
        {
            killCall = true;
        }
    }
}
