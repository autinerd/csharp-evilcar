build:
	@dotnet build csharp-evilcar.csproj

run:
	@dotnet run -p csharp-evilcar.csproj $(filter-out $@ ,$(MAKECMDGOALS))
%:
	@: