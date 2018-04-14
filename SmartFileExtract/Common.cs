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

namespace SmartFileExtract
{
    public static class SFEGeneral
    {
        const int STANDARD_ROUND_COUNT = 3;

        // Returns:
        // - "" if the switch is not found or is invalid (otherwise not usable)
        // - " " if the switch is valid and has no expected return value
        // - [value] if the switch is valid and has a value
        // Suppose you can use Contains but want to check for mixed case, slash and dash
        public static string hasSwitch(string[] args, string sought)
        {
            string[] singleCommands = new string[] { "SETUP", "HELP", "TEST" };
            string found = "";

            foreach (string clItem in args)
            {
                if (found == Convert.ToString(0))
                {
                    if (!clItem.StartsWith("/") || !clItem.StartsWith("/"))
                        found = clItem;
                    break;
                }
                else
                {
                    if (clItem.ToUpper() == ("/" + sought.ToUpper()) ||
                        clItem.ToUpper() == ("-" + sought.ToUpper()))
                    {
                        if (singleCommands.Contains(sought.ToUpper()))
                        {
                            // This is just a command without a value, so it is valid and found
                            found = " ";
                            break;
                        }
                        else
                        {
                            // Flag to say we need the next parameter
                            found = Convert.ToString(0);
                        }
                    }
                }
            }

            if (found != Convert.ToString(0))
                return found;
            else
                return "";
        }


        public static bool WipeFile(string target)
        {
            return WipeFile(target, STANDARD_ROUND_COUNT);
        }

        public static bool WipeFile(string target, int numberRounds)
        {
            bool rtn = true;
            Random rnd = new Random(DateTime.Now.Millisecond);

            try
            {
                long targetLength = new System.IO.FileInfo(target).Length;
                var wipeBuffer = new byte[targetLength];
                // Overwrite with 0s the number of times specified
                for (long i = 0; i < wipeBuffer.Length; i++)
                    wipeBuffer[i] = 0x30;
                for (int round = 0; round < numberRounds; round++)
                    File.WriteAllBytes(target, wipeBuffer);

                // As final touch, overwrite with random bytes buffered with a random length
                targetLength += rnd.Next(50, 5000);
                wipeBuffer = new byte[targetLength];
                rnd.NextBytes(wipeBuffer);              // Method that conveniently fills up random numbers
                File.WriteAllBytes(target, wipeBuffer);
                // Finally delete
                File.Delete(target);
            }
            catch
            {
                rtn = false;
            }
            return rtn;
        }
    }
}
