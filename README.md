# Battleships

## Building the application

To build the app locally you need to:
1. Install [.NET 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
2. Download the repository
3. Go to the root folder of the solution, then run `dotnet restore .\Battleships.App\` and `dotnet msbuild .\Battleships.App\ /property:Configuration=Release`

## Running the application

Once it's built, navigate to `.\Battleships.App\bin\Release\net6.0`. There will be an .exe file located in this directory named **Battleships.App**. Run the file and the application will start.
