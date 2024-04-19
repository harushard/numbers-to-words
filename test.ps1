# restore packages
dotnet restore NumbersToWords.WebApi/NumbersToWords.WebApi.csproj

Start-Process powershell -ArgumentList "-noexit", "-command dotnet test NumbersConverter.UnitTests\NumbersConverter.UnitTests.csproj"