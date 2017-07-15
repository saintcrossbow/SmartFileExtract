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
using System.IO;
using System.Windows.Forms;

namespace SmartFileExtract
{
    public class FileSearch
    {
        public string[] targetDrive { get; set; }
        public string[] targetFiles { get; set; }
        public string[] keywords { get; set; }
        public string copyTo { get; set; }
        public long maxBytesToCopy { get; set; }
        public int maxSecondsToCopy { get; set; }

        public int curtainStyle { get; set; }
        public int performanceModel { get; set; }

        enum phaseStatus
        {
            PHASE_INIT = 0,
            PHASE_DISCOVERY = 1,
            PHASE_COPY = 2,
            PHASE_DONE = 3
        }

        enum curtainType
        {
            CURTAIN_PROGRESS = 0,
            CURTAIN_QUIET = 1,
            CURTAIN_STEALTHY = 2,
            CURTAIN_INSTALL = 3
        }

        enum performanceIndex
        {
            PERF_NORMAL = 0,
            PERF_AGGRESSIVE = 1,
            PERF_LOW_IMPACT = 2
        }

        private ICurtain curtain;
        private curtainType specificCurtainType;
        private performanceIndex specificPerformance;

        // The list will contain all of the files we want to copy
        private List<string> targets;
        // Acts as a kill switch - if true we should stop trying to copy
        private bool killSearch = false;

        private DateTime startTime;

        public FileSearch()
        {
            // Set to off for maxinums
            maxBytesToCopy = 0;
            maxSecondsToCopy = 0;
            targets = new List<string>();
        }

        private void setPerformanceModel()
        {
            try
            {
                specificPerformance = (performanceIndex)performanceModel;
            }
            catch 
            {
                specificPerformance = performanceIndex.PERF_NORMAL;
            }
        }

        private void setCurtainStyle()
        {
            try
            {
                specificCurtainType = (curtainType)curtainStyle;

            }
            catch 
            {
                specificCurtainType = curtainType.CURTAIN_PROGRESS;
            }

            switch (specificCurtainType)
            {
                case curtainType.CURTAIN_PROGRESS:
                    curtain = new clsProgressCurtain();
                    break;
                case curtainType.CURTAIN_QUIET:
                    curtain = new clsQuietCurtain();
                    break;
                case curtainType.CURTAIN_STEALTHY:
                    curtain = new clsStealthyCurtain();
                    break;
                case curtainType.CURTAIN_INSTALL:
                    curtain = new clsInstallCurtain();
                    break;
            }
        }

        // TODO:
        // (x) Test file limit
        // ( ) In cheat sheet note that file only copied once, duplicates ignored
        // (x) Calculate total before copy
        // (x) Start timer and abort if it goes for too long
        // ( ) Note aggressive limits all processing and updates - may not see any updates at all
        // ( ) Also note that low impact takes long time but might be good if you are targeting a pc and copying to network
        // (x) Need faster search in binary
        // (x) Create directory if it doesn't exist
        // (x) Change to configurable curtain
        // (x) Performance options: aggressive (no user input), standard (allow processing), low (delayed)
        // (x) Curtain: Install, Corner on screen, None

        // Only target drive, files, and copy to are required
        public bool isReady()
        {
            return (targetDrive.Length > 0 && targetFiles.Length > 0 && copyTo.Length > 0);
        }

        public bool startExtract()
        {
            bool ret = true;
            try
            {
                if (isReady())
                {
                    // Set up curtain
                    setCurtainStyle();

                    // 1) Track start time
                    startTime = DateTime.Now;
                    // 2) Find all possible targets on PC
                    showStatus(phaseStatus.PHASE_DISCOVERY);                    
                    findTargets();
                    // 3) Copy files exactly once to specified directory
                    showStatus(phaseStatus.PHASE_COPY);
                    copyTargets();
                    // 4) Complete (and likely unload form)
                    showStatus(phaseStatus.PHASE_DONE);
                }
                else
                    ret = false;
            }
            catch (Exception ex)
            {
                    ret = false;
            }

            return ret;
        }

        private void showStatus(phaseStatus stat)
        {
            if (curtain != null)
            {
                switch (specificCurtainType)
                {
                    case curtainType.CURTAIN_PROGRESS:
                        switch (stat)
                        {
                            case phaseStatus.PHASE_INIT:
                                curtain.status("Phase: Initialize");
                                break;
                            case phaseStatus.PHASE_DISCOVERY:
                                curtain.status("Phase: Discovery");
                                break;
                            case phaseStatus.PHASE_COPY:
                                curtain.status("Phase: Extract");
                                break;
                            case phaseStatus.PHASE_DONE:
                                curtain.kill();
                                break;
                        }
                        break;
                    case curtainType.CURTAIN_STEALTHY:
                        switch (stat)
                        {
                            case phaseStatus.PHASE_INIT:
                                curtain.status(".");
                                break;
                            case phaseStatus.PHASE_DISCOVERY:
                                curtain.status("..");
                                break;
                            case phaseStatus.PHASE_COPY:
                                curtain.status("...");
                                break;
                            case phaseStatus.PHASE_DONE:
                                curtain.kill();
                                break;
                        }
                        break;
                    case curtainType.CURTAIN_INSTALL:
                        switch (stat)
                        {
                            case phaseStatus.PHASE_INIT:
                                curtain.status("New hardware found.");
                                break;
                            case phaseStatus.PHASE_DISCOVERY:
                                curtain.status("Finding compatible drivers...");
                                break;
                            case phaseStatus.PHASE_COPY:
                                curtain.status("Installing driver...");
                                break;
                            case phaseStatus.PHASE_DONE:
                                curtain.kill();
                                break;
                        }
                        break;
                    case curtainType.CURTAIN_QUIET:
                        // No status really reported
                        break;
                }



            }
        }

        public bool findTargets()
        {
            foreach (string drive in targetDrive)
            {
                tick();
                string searchDrive = driveLetter(drive);
                if (searchDrive.Length > 0 && !killSearch)
                {
                    foreach (string pattern in targetFiles)
                    {
                        tick();
                        if (!killSearch)
                        {
                            try
                            {
                                IEnumerable<string> fileNames;
                                fileNames = GetFiles(searchDrive, pattern, true);
                                foreach (string foundFile in fileNames)
                                {
                                    tick();
                                    if (killSearch) break;
                                    if (keywords.Length == 0 || String.IsNullOrEmpty(keywords[0]))
                                    {
                                        // If no keywords, we are a de facto match
                                        targets.Add(foundFile);
                                    }
                                    else
                                    {
                                        // Leaving this just in case I want to back to seeking by binary, but rather slow                                        
                                        //// Otherwise search as binary (might not be text) and see if we can find it
                                        //byte[] inBuffer = File.ReadAllBytes(foundFile);
                                        

                                        // We have to seek each keyword
                                        bool keywordFound = false;
                                        foreach (string keySought in keywords)
                                        {
                                            tick();
                                            if (killSearch) break;
                                            if (keywordFound) break;
                                            else
                                            {
                                                StreamReader inFile = new StreamReader(foundFile);
                                                string inLine = "";
                                                while ((inLine = inFile.ReadLine()) != null)
                                                {
                                                    tick();
                                                    if (killSearch) break;

                                                    if (inLine.ToUpper().Contains(keySought.ToUpper()))
                                                    {
                                                        targets.Add(foundFile);
                                                        break;
                                                    }
                                                }
                                                inFile.Close();
                                                // --- Binary method -------------------------------------------------------------
                                                //byte[] soughtBuffer = Encoding.UTF8.GetBytes(keySought);
                                                //for (int i = 0; i <= (inBuffer.Length - soughtBuffer.Length); i++)
                                                //{
                                                //    tick();
                                                //    if (killSearch) break;

                                                //    if (inBuffer[i] == soughtBuffer[0])
                                                //    {
                                                //        for (int j = 1; j < soughtBuffer.Length && inBuffer[i + j] == soughtBuffer[j]; j++)
                                                //        {
                                                //            tick();
                                                //            if (j == soughtBuffer.Length)
                                                //            {
                                                //                targets.Add(foundFile);
                                                //                keywordFound = true;
                                                //            }
                                                //        }
                                                //    }
                                                //}
                                                // --- CUT -------------------------------------------------------------
                                            }
                                        }
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                // We're not stopping for an error, just skip that copy
                            }
                        }
                    }
                }
            }
            return true;
        }

        // Sure we could've made this async, but like this for basic check. I/O is going to go slow even if we multithread.
        private void tick()
        {
            if (specificPerformance != performanceIndex.PERF_AGGRESSIVE)
            {
                // Allow processing
                Application.DoEvents();

                // Check if we are killing process
                if (maxSecondsToCopy > 0)
                {
                    var secDiff = (DateTime.Now - startTime).TotalSeconds;
                    killSearch = (secDiff >= maxSecondsToCopy);
                }
                if (curtain != null && curtain.killCall == true)
                    killSearch = true;
            }
        }

        public bool copyTargets()
        {
            bool ret = true;
            long totalOut = 0;
            curtain.totalFiles = targets.Count;
            int fileNumber = 0;
            try
            {
                // Create directory if necessary
                if (copyTo.Length > 3 && !Directory.Exists(copyTo))
                    Directory.CreateDirectory(copyTo);

                // Perform copy
                foreach (string foundTarget in targets)
                {
                    // Increment the curtain
                    fileNumber++;
                    curtain.currentCount(fileNumber);

                    // Evaluate for copy
                    string writeFile = Path.Combine(copyTo, Path.GetFileName(foundTarget));
                    if (!File.Exists(writeFile))
                    {
                        // Determine file size
                        long fileSize = new FileInfo(foundTarget).Length;
                        if (maxBytesToCopy > 0 && (totalOut + fileSize) > maxBytesToCopy)
                        {
                            // Don't break - we might yet get something that is under the limit 
                            //break;
                        }
                        else
                        {
                            File.Copy(foundTarget, writeFile);
                            totalOut += fileSize;
                        }                            
                    }

                    // Delay if doing a quiet copy
                    if (specificPerformance == performanceIndex.PERF_LOW_IMPACT)
                    {
                        waitWhileProcessing();
                    }
                }
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        // Wait for random seconds - this still allows screen updates
        private void waitWhileProcessing()
        {
            Random r = new Random();
            int rInt = r.Next(0, 10); 

            DateTime dtBefore = DateTime.Now;
            do
            {
                Application.DoEvents();
            } while (dtBefore.AddSeconds(rInt) > DateTime.Now);
        }

        // Given a drive, format it to "c:\\" format
        private string driveLetter(string inDrive)
        {
            if (inDrive.Length > 0)
                return inDrive[0] + ":\\";
            else
                return "";
        }


        // Using method that will ignore all errors when seeking files
        // Thanks to: https://stackoverflow.com/questions/4986293/access-to-the-path-is-denied-when-using-directory-getfiles
        IEnumerable<string> GetFiles(string folder, string filter, bool recursive)
        {
            if (!killSearch)
            {
                string[] found = null;
                try
                {
                    found = Directory.GetFiles(folder, filter);
                }
                catch { }
                if (found != null)
                    foreach (var x in found)
                    {
                        tick();
                        yield return x;
                    }
                if (recursive)
                {
                    found = null;
                    try
                    {
                        found = Directory.GetDirectories(folder);
                    }
                    catch { }
                    if (found != null)
                        foreach (var x in found)
                            foreach (var y in GetFiles(x, filter, recursive))
                            {
                                tick();
                                yield return y;
                            }
                }
            }
        }
    }

}