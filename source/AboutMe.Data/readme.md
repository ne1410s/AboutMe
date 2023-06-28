dotnet new tool-manifest
dotnet tool install dotnet-ef

$env:ASPNETCORE_ENVIRONMENT='local';dotnet ef migrations add <MigrationName> -p Me.Data -s Me.Api
$env:ASPNETCORE_ENVIRONMENT='local';dotnet ef migrations script -p Me.Data -s Me.Api
dotnet ef migrations bundle -p Me.Data -s Me.Api
$env:ASPNETCORE_ENVIRONMENT='local';dotnet ef database update -p Me.Data -s Me.Api