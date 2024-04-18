# Numbers to Words

Simple .NET web application to do conversion of numbers into words. Develop using ASP.NET C# as back-end, and Next.JS as front-end.

## How to Publish

For automation, simply run `build.ps1` script with Powershell. Below subsections are explaination on process in the script.

### Publish Web API and Static-files' Server

- For Web API and Static-files' server portion of this app, it can be built with [dotnet CLI](https://learn.microsoft.com/en-us/dotnet/core/tools/).
- Basic publish command is `dotnet publish NumbersToWords.WebApi/NumbersToWords.WebApi.csproj -c Release -o output`, for more build options you can refer to [dotnet build page](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build)

### Publish Web UI's Static-files

- For Web UI, it can be built with `NPM CLI` which requires [Node](https://nodejs.org/) to be installed in your system.
- From root directory, change working directory to `NumbersToWords.WebUi` folder: `cd NumbersToWords.WebUi`
- Then, using this command to publish Web UI's Static-files: `npm run build`
- Output files should be generated at inside `NumbersToWords.WebUi\output` folder
- Next we need copy all files inside `NumbersToWords.WebUi\output\*.*` to `output\ui` using this powershell command: `Copy-Item -Path output\* -Destination ..\output\ui -Recurse`

## How to Run

After publish (Refer `How to Publish` section), this app can be run as below, simply run `run.ps1` script with Powershell. Below subsections are explaination on process in the script.

### Running the app

- From root directory, change working directory to `\output` folder using this command: `cd output`
- Then, get the app running using this command: `dotnet NumbersToWords.WebApi.dll`
- Open up the URL of `https://localhost:5001/app` in your preferred browser

## How to Run Unit Tests

For automation, simply run `test.ps1`. What the script does is running this `dotnet CLI` command `dotnet test NumbersConverter.UnitTests\NumbersConverter.UnitTests.csproj`
