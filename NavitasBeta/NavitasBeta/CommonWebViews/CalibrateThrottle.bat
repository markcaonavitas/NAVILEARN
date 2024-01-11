@echo off
cd .
set thisPath=%cd%

start "WebSocket" %thisPath%\WebSocketServer\bin\Debug\netcoreapp3.1\WebSocketServer.exe
REM TAC_0726900067 is home, TAC_0610000013 is work
REM start "TAC2" "%thisPath%""\DeviceToTcpIpServer\bin\Debug\netcoreapp3.1\DeviceToTcpIpServer.exe" --serialport COM4
start "TAC2" %thisPath%\DeviceToTcpIpServer\bin\Debug\netcoreapp3.1\DeviceToTcpIpServer.exe --blename TAC_0610000013
REM start "TAC2" %thisPath%\DeviceToTcpIpServer\bin\Debug\netcoreapp3.1\DeviceToTcpIpServer.exe --blename TAC_0726900067

start /w msedge --force-dark-mode --window-size=800,800 --disable-web-security --auto-open-devtools-for-tabs --user-data-dir=%thisPath%\temp --app=%thisPath%\CalibrateThrottle.html
taskkill /im WebSocketServer.exe
taskkill /im DeviceToTcpIpServer.exe
