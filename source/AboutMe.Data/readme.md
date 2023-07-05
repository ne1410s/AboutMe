dotnet new tool-manifest
dotnet tool install dotnet-ef

$env:ASPNETCORE_ENVIRONMENT='local';dotnet ef migrations add <MigrationName> -p AboutMe.Data -s AboutMe.Api
$env:ASPNETCORE_ENVIRONMENT='local';dotnet ef migrations script -p AboutMe.Data -s AboutMe.Api
dotnet ef migrations bundle -p AboutMe.Data -s AboutMe.Api
$env:ASPNETCORE_ENVIRONMENT='local';dotnet ef database update -p AboutMe.Data -s AboutMe.Api