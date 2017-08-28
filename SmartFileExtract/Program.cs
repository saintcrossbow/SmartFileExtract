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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Runtime.InteropServices;

namespace SmartFileExtract
{
    enum startMethodType
    {
        startMission = 1,
        startHelp = 2,
        startTest = 3
    }


    static class Program
    {
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        [STAThread]
        static void Main(string[] args)
        {
            startMethodType startMethod = startMethodType.startMission;

            if (SFEGeneral.hasSwitch(args, "help") == " " || SFEGeneral.hasSwitch(args, "?") == " ")
            {
                // Specified either nothing or has explicitly said setup
                startMethod = startMethodType.startHelp;
            }
            if (SFEGeneral.hasSwitch(args, "test") == " ")
            {
                // Specified either nothing or has explicitly said setup
                startMethod = startMethodType.startTest;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            switch (startMethod)
            {
                case startMethodType.startTest:
                    {                        
                        Application.Run(new frmTest());
                        break;
                    }
                case startMethodType.startHelp:
                    {
                        ShowCheatsheet();
                        break;
                    }
                
                case startMethodType.startMission:
                default:
                    {
                        SmartFileExtract.FileSearch fileExtract = new SmartFileExtract.FileSearch();
                        fileExtract.targetDrive = SFEGeneral.hasSwitch(args, "drive").Split(';');                        
                        fileExtract.targetFiles = SFEGeneral.hasSwitch(args, "file").Split(';');
                        fileExtract.keywords = SFEGeneral.hasSwitch(args, "keyword").Split(';');
                        fileExtract.copyTo = SFEGeneral.hasSwitch(args, "copyto");

                        // Optional parameters
                        // Total bytes (translated from MBs)
                        if (!String.IsNullOrEmpty(SFEGeneral.hasSwitch(args, "maxmbs")))
                        {
                            long totalLimit = 0;
                            if (long.TryParse(SFEGeneral.hasSwitch(args, "maxmbs"), out totalLimit))
                                fileExtract.maxBytesToCopy = totalLimit * 1048576;
                        }

                        // Total seconds to run
                        if (!String.IsNullOrEmpty(SFEGeneral.hasSwitch(args, "maxsec")))
                        {
                            int totalSeconds = 0;
                            if (int.TryParse(SFEGeneral.hasSwitch(args, "maxsec"), out totalSeconds))
                                fileExtract.maxSecondsToCopy = totalSeconds;
                        }

                        // Type of curtain
                        if (!String.IsNullOrEmpty(SFEGeneral.hasSwitch(args, "curtain")))
                        {
                            int selCurtain = 0;
                            if (int.TryParse(SFEGeneral.hasSwitch(args, "curtain"), out selCurtain))
                                fileExtract.curtainStyle = selCurtain;
                        }

                        // Type of performance
                        if (!String.IsNullOrEmpty(SFEGeneral.hasSwitch(args, "perf")))
                        {
                            int selPerf = 0;
                            if (int.TryParse(SFEGeneral.hasSwitch(args, "perf"), out selPerf))
                                fileExtract.performanceModel = selPerf;
                        }

                        // Start search
                        if (fileExtract.isReady())
                        {
                            fileExtract.startExtract();                           
                        }
                        else
                        {
                            StringBuilder minCriteria = new StringBuilder();
                            minCriteria.Append(String.Format("\r\nSmart File Extract Version {0}, Copyright (C) 2017 saintcrossbow@gmail.com", Application.ProductVersion));
                            minCriteria.Append("\r\nMinimum arguments have not been specified.");
                            minCriteria.Append("\r\nNeed at least 3: /drive, /file, and /copyto");
                            minCriteria.Append("\r\n\r\nUse /help for cheatsheet");
                            minCriteria.Append("\r\n\r\n");
                            AttachConsole(ATTACH_PARENT_PROCESS);
                            Console.Clear();
                            Console.WriteLine(minCriteria);
                        }
                        Application.Exit();
                        break;
                    }
            }

        }

        static void ShowCheatsheet()
        {
            StringBuilder cheatSheet = new StringBuilder();
            cheatSheet.Append(String.Format("\r\nSmart File Extract Version {0}, Copyright (C) 2017 saintcrossbow@gmail.com", Application.ProductVersion));
            // http://www.drdobbs.com/jvm/creating-an-open-source-project/240145389
            cheatSheet.Append("\r\nSmart File Extract comes with ABSOLUTELY NO WARRANTY. This is free software ");
            cheatSheet.Append("\r\nand you are welcome to redistribute it. See .\\Docs\\gpl.txt. ");
            cheatSheet.Append("\r\nLegal testing, demonstration, and education usage only.");
            cheatSheet.Append("\r\n----------------------------------------------------------------------------");
            cheatSheet.Append("\r\nStandard Usage");
            cheatSheet.Append("\r\n/help            This cheatsheet of command parameters");
            cheatSheet.Append("\r\n/drive           Drives to search, single letter only");
            cheatSheet.Append("\r\n/file            File pattern to search, wildcards acceptable ");
            cheatSheet.Append("\r\n/keyword         Look in file for plaintext (optional - and slows search)");
            cheatSheet.Append("\r\n/copyto          Copy found files to this location");
            cheatSheet.Append("\r\n/maxsec [n]      Cancel extract after n seconds (optional)");
            cheatSheet.Append("\r\n/maxmbs [n]      Cancel extract after n MBs copied (optional)");

            cheatSheet.Append("\r\n/curtain [n]     Specify the curtain to use on mission start (optional)");
            cheatSheet.Append("\r\n                 0=Progress*, 1=Quiet, 2=Stealthy, 3=Fake Install Window");
            cheatSheet.Append("\r\n/perf [n]        Specify the performance model (optional)");
            cheatSheet.Append("\r\n                 0=Standard*, 1=Aggressive, 2=Low Impact");
            cheatSheet.Append("\r\n                 * Default if not specified");
            cheatSheet.Append("\r\n----------------------------------------------------------------------------");
            cheatSheet.Append("\r\nMultiple Arguments");
            cheatSheet.Append("\r\nMultiple drives, files, and keywords may be specified. Separate the values");
            cheatSheet.Append("\r\nwith semicolons (e.g. /drive c;d /file *.txt;gen*.*)");
            cheatSheet.Append("\r\n----------------------------------------------------------------------------");
            cheatSheet.Append("\r\nKey Notes");
            cheatSheet.Append("\r\n- Files are copied only once (duplicate names are ignored)");
            cheatSheet.Append("\r\n- Aggressive performance suppresses most screen output, use with discretion");
            cheatSheet.Append("\r\n- Low impact performance takes 1-10 seconds randomly between searches and ");
            cheatSheet.Append("\r\n  copies. As such, copies can take a long time, but may prevent alerts");

            // Writing to console from Windows app
            // http://www.csharp411.com/console-output-from-winforms-application/
            AttachConsole(ATTACH_PARENT_PROCESS);
            Console.Clear();
            Console.WriteLine(cheatSheet);
            Application.Exit();
        }

    }
}
