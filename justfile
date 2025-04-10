set shell := ["powershell.exe", "-c"]

build:
    dotnet build zoosim.webapi.sln --configuration Debug
run:
    #!powershell.exe
    cd zoosim.webapi
    dotnet run;