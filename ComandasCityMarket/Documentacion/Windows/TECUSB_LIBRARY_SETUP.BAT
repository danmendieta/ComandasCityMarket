echo off
echo //////////////////////////////////////////////////////////////////////////
echo //TECUSB Library Setup
echo //////////////////////////////////////////////////////////////////////////
echo .
echo .

echo COPY TECUSB.dll
xcopy /H /R /Y /C TECUSB.dll  %systemRoot%\system32\

echo COPY LogMngr.dll
xcopy /H /R /Y /C LogMngr.dll %systemRoot%\system32\

echo COPY TECUSBJNI.dll
xcopy /H /R /Y /C TECUSBJNI.dll %systemRoot%\system32\

echo COPY TECUSBPM.exe
xcopy /H /R /Y /C TECUSBPM.exe %systemRoot%\system32\

echo on

pause