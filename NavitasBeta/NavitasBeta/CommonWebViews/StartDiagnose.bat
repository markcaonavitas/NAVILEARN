@echo off
cd .
set thisPath=%cd%

start "WebSocket" %thisPath%\WebSocketServer\bin\Debug\netcoreapp3.1\WebSocketServer.exe
REM TAC_0726900067 is home, TAC_0610000013 is work
REM start "TAC2" "%thisPath%""\DeviceToTcpIpServer\bin\Debug\netcoreapp3.1\DeviceToTcpIpServer.exe" --serialport COM4
start "TAC2" "%thisPath%""\DeviceToTcpIpServer\bin\Debug\netcoreapp3.1\DeviceToTcpIpServer.exe" --blename TAC_0726900067
REM start "TAC2" %thisPath%\DeviceToTcpIpServer\bin\Debug\netcoreapp3.1\DeviceToTcpIpServer.exe --blename TAC_0610000013

REM "C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" --force-dark-mode --window-size=800,800 --disable-web-security  --user-data-dir=%thisPath%\CommonWebViews\temp --app=%thisPath%\CommonWebViews\RegressionTest.html
REM try both versions, hopefully only one is probably installed
REM "C:\Program Files\Google\Chrome\Application\chrome.exe" --force-dark-mode --window-size=800,800 --disable-web-security --auto-open-devtools-for-tabs --user-data-dir=%thisPath%\CommonWebViews\temp --app=%thisPath%\CommonWebViews\RegressionTest.html
start /w msedge --force-dark-mode --window-size=800,800 --disable-web-security --auto-open-devtools-for-tabs --user-data-dir=%thisPath%\temp --app=%thisPath%\StartDiagnose.html
REM start /w msedge --force-dark-mode --window-size=800,800 --disable-web-security --auto-open-devtools-for-tabs --user-data-dir=%thisPath%\CommonWebViews\temp --app="http://localhost:3000/CommonWebViews/RegressionTest.html"
taskkill /im WebSocketServer.exe /F
taskkill /im DeviceToTcpIpServer.exe /F
