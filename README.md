# NetCoreCMS
NetCoreCMS is a Content Management System developed using ASP.Net Core 1.1. It is usable as web application framework.  

## Alpha

The software is complete for internal testing which can be performed by people other than its developers. The testers are within the same organization or community. 

Here is a detailed [roadmap](https://github.com/TecRT/NetCoreCMS/wiki/Roadmap).

## Getting Started

- To clone the repository, use the following command: 
`git clone https://github.com/TecRT/NetCoreCMS.git` 
and check the `master` branch. 

### Command line
(Run the commands as an Administrator)

- Install the latest versions (current) for .Net Core Runtime and SDK from https://www.microsoft.com/net/download/core
- Call `dotnet restore`.
- Call `dotnet build`.
- Navigate to `D:\NetCoreCMS\NetCoreCMS.Web` or to the respective folder. 
- Call `dotnet run`.
- Open the `http://localhost:5000` URL in your browser.

### Visual Studio 2017

- Download Visual Studio 2017 (any edition) from https://www.visualstudio.com/downloads/
- Open `NetCoreCMS.sln`
- Build `NetCoreCMS.sln` solution (before running the project)
- Ensure `NetCoreCMS.Web` is the startup project and run it.
