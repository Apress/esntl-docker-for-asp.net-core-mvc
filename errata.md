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