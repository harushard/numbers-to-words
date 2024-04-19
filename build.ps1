# restore packages
dotnet restore NumbersToWords.WebApi/NumbersToWords.WebApi.csproj

# publish web api
dotnet publish NumbersToWords.WebApi/NumbersToWords.WebApi.csproj -c Release -o output

# change to web ui directory
cd NumbersToWords.WebUi

# install required packages
npm install

# publish web ui
npm run build

# copy published web ui to static file's server path
Copy-Item -Path output\* -Destination ..\output\ui -Recurse -Force

