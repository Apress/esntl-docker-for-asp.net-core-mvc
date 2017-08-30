# Errata for *Essential Docker for ASP.NET Core MVC*

If you install a later version of the .NET Core runtime, you may see an error like this one when you start a container with an ASP.NET Core MVC application:

    The specified framework 'Microsoft.NETCore.App', version '1.1.2' was not found.
    - Check application dependencies and target a framework version installed at:
    /usr/share/dotnet/shared/Microsoft.NETCore.App
    - The following versions are installed:
          1.1.1
    - Alternatively, install the framework version '1.1.2'.

This problem is caused by the publishing process, which automatically bumps up the version of the .NET Core runtime. To fix this problem, add an `RuntimeFrameworkVersion` element to the csproj file and use it to specify the version of the .NET Core runtime contained in the base image, like this:

    <Project Sdk="Microsoft.NET.Sdk.Web">

        <PropertyGroup>
            <TargetFramework>netcoreapp1.1</TargetFramework>
            <RuntimeFrameworkVersion>1.1.1</RuntimeFrameworkVersion>
        </PropertyGroup>

        <ItemGroup>
            <PackageReference Include="Microsoft.AspNetCore" Version="1.1.1" />
            <PackageReference Include="Microsoft.AspNetCore.Mvc" Version="1.1.1" />
            <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="1.1.1" />
            <PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="1.1.1" />
            <PackageReference Include="Microsoft.VisualStudio.Web.BrowserLink" Version="1.1.0" />
        </ItemGroup>

    </Project>

(Thanks to Mookey Star for reporting this problem).

---

In **Chapter 8**, the way that .NET Core applications are debugged has changed and the debugger used in the example is no longer supported. To resolve this problem, use the following `Dockerfile.debug` file instead of the one shown in **Listing 8-12**:

    FROM microsoft/aspnetcore-build:1.1.1

    WORKDIR /vsdbg

    RUN apt-get update && apt-get install -y --no-install-recommends unzip \
        && rm -rf /var/lib/apt/lists/* \
        && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

    EXPOSE 80/tcp

    VOLUME /app

    WORKDIR /app

    ENTRYPOINT echo "Restoring packages..." && dotnet restore \
        && echo "Building project..." && dotnet build \
	    && echo "Ready for debugging." && sleep infinity 

Once you have built and started the debugger container, use this command to test the setup instead of the one shown in **Listing 8-17**:
    
    docker exec -i dev_debug_1 /vsdbg/vsdbg

When debugging with Visual Studio Code, use the following  `launch.json` file insetad of the one shown in **Listing 8-21**:

    {
        "version": "0.2.0",
        "configurations": [
            {
                "name": ".NET Core Launch (web)",
                "type": "coreclr",
                "request": "launch",
                "preLaunchTask": "build",
                "program": "/app/bin/Debug/netcoreapp1.1/ExampleApp.dll",
                "args": [],
                "cwd": "/app",
                "stopAtEntry": false,
                "internalConsoleOptions": "openOnSessionStart",
                "launchBrowser": {
                    "enabled": false,
                    "args": "${auto-detect-url}",
                    "windows": {
                        "command": "cmd.exe",
                        "args": "/C start ${auto-detect-url}"
                    },
                    "osx": {
                        "command": "open"
                    },
                    "linux": {
                        "command": "xdg-open"
                    }
                },
                "env": {
                    "ASPNETCORE_ENVIRONMENT": "Development"
                },
                "sourceFileMap": {
                    "/app": "${workspaceRoot}",
                    "/Views": "${workspaceRoot}/Views"
                },
                "pipeTransport": {
                    "debuggerPath": "/vsdbg/vsdbg",
                    "pipeProgram": "docker",
                    "pipeCwd": "${workspaceRoot}",
                    "pipeArgs": ["exec -i dev_debug_1"],
                    "quoteArgs": false,
                    "windows": {
                        "pipeProgram": "docker",
                        "pipeCwd": "${workspaceRoot}",
                        "pipeArgs": [ "exec -i dev_debug_1" ],
                        "quoteArgs": false                     
                    }
                }            
            },
            {
                "name": ".NET Core Attach",
                "type": "coreclr",
                "request": "attach",
                "processId": "${command:pickProcess}"
            }
        ]
    }

(Thanks to Marek Kacprzak for reporting this problem)