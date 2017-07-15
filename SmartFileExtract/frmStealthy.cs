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
    public partial class frmStealthy : Form
    {
        public bool killCall { get; set; }

        public frmStealthy()
        {
            InitializeComponent();
            killCall = false;   
        }

        public void status(string statusText, bool aggressivePerf = false)
        {
            dotStat.Text = statusText;
            dotStat.Update();
            if (!aggressivePerf)
            {
                this.Refresh();
                Application.DoEvents();
            }
        }

        public void kill()
        {
            this.Close();
        }

        private void dotStat_DoubleClick(object sender, EventArgs e)
        {
            killCall = true;
        }

        private void frmStealthy_Load(object sender, EventArgs e)
        {
            PlaceUpperRight();
        }

        // Thanks to https://stackoverflow.com/questions/15188939/form-position-on-lower-right-corner-of-the-screen
        private void PlaceUpperRight()
        {
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = 0;
        }
        
    }
}
