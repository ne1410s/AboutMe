## Me

``` powershell
# Restore tools
dotnet tool restore

# General clean up
rd -r **/bin/; rd -r **/obj/;

# Run unit tests
gci -r -dir ../TestResults | % { rm -r $_ }; dotnet test -c Release -s .runsettings; dotnet reportgenerator -targetdir:coveragereport -reports:**/coverage.cobertura.xml -reporttypes:"html;jsonsummary"; start coveragereport/index.html;

# Run mutation tests
gci -r -dir ../StrykerOutput | % { rm -r $_ }; dotnet stryker -o;
```
