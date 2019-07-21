@echo off
mkdir temp
if exist DnsServer-master robocopy temp DnsServer-master /purge > nul
if exist TechnitiumLibrary-master robocopy temp TechnitiumLibrary-master /purge > nul
git clone https://github.com/TechnitiumSoftware/DnsServer.git DnsServer-master
git clone https://github.com/TechnitiumSoftware/TechnitiumLibrary.git TechnitiumLibrary-master
rmdir temp
if not exist TechnitiumLibrary mkdir TechnitiumLibrary