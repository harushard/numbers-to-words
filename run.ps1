# run web api
Start-Process -FilePath "powershell.exe" -ArgumentList "cd output; dotnet NumbersToWords.WebApi.dll"

# wait for a second
Start-Sleep 1

# open up url
Start-Process "https://localhost:5001/app"