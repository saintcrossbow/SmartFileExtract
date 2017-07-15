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
    public static class SFEGeneral
    {
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

    }
}
