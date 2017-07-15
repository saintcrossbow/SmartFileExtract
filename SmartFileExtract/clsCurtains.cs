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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFileExtract
{
    class clsProgressCurtain : ICurtain
    {
        private frmProgress progressForm;
        public int totalFiles { get; set; }
        public int currentFile { get; set; }
        public bool aggressivePerformance { get; set; }
        public bool killCall {
            get
            {
                if (progressForm != null)
                    return progressForm.killCall;
                else
                    return false;
            }
            set
            {
                if (progressForm != null)
                    progressForm.killCall = value;
            }
        }

        public clsProgressCurtain()
        {
            progressForm = new frmProgress();
            progressForm.Show();        
        }

        public void status(string txtStatus)
        {
            progressForm.status(txtStatus, aggressivePerformance);
        }

        public void currentCount(int countTotal)
        {
            progressForm.increment(0, totalFiles, countTotal);
        }

        public void kill()
        {
            progressForm.kill();
        }

    }
    class clsQuietCurtain : ICurtain
    {
        public int totalFiles { get; set; }
        public int currentFile { get; set; }
        public bool aggressivePerformance { get; set; }
        public bool killCall
        {
            // With no form, we don't care about the kill call
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public void status(string txtStatus)
        {
            // Ignore any status
        }

        public void currentCount(int countTotal)
        {
            // Ignore count
        }

        public void kill()
        {
            // In unlikely event something needs to be handled here, go for it:
        }

    }

    class clsStealthyCurtain : ICurtain
    {
        private frmStealthy stealthForm;
        public int totalFiles { get; set; }
        public int currentFile { get; set; }
        public bool aggressivePerformance { get; set; }

        public clsStealthyCurtain()
        {
            stealthForm = new frmStealthy();
            stealthForm.Show();
        }


        public bool killCall
        {
            get
            {
                if (stealthForm != null)
                    return stealthForm.killCall;
                else
                    return false;
            }
            set
            {
                if (stealthForm != null)
                    stealthForm.killCall = value;
            }
        }

        public void status(string txtStatus)
        {
            stealthForm.status(txtStatus, aggressivePerformance);
        }

        public void currentCount(int countTotal)
        {
            // OPTIONAL: Toggle flash?
        }

        public void kill()
        {
            stealthForm.kill();
        }
    }


    class clsInstallCurtain: ICurtain
    {
        private frmInstall installForm;
        public int totalFiles { get; set; }
        public int currentFile { get; set; }
        public bool aggressivePerformance { get; set; }
        public bool killCall
        {
            get
            {
                if (installForm != null)
                    return installForm.killCall;
                else
                    return false;
            }
            set
            {
                if (installForm != null)
                    installForm.killCall = value;
            }
        }

        public clsInstallCurtain()
        {
            installForm = new frmInstall();
            installForm.Show();
        }

        public void status(string txtStatus)
        {
            installForm.status(txtStatus, aggressivePerformance);
        }

        public void currentCount(int countTotal)
        {
            installForm.increment(0, totalFiles, countTotal);
        }

        public void kill()
        {
            installForm.kill();
        }

    }

}
