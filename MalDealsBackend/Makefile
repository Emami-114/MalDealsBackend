build:
	dotnet build
test: build
	dotnet test

dbUpdate:
	dotnet ef database drop
	dotnet ef migrations remove
	dotnet ef migrations add InitialCreate
	dotnet ef database update

watch:
	dotnet watch run

run:
	dotnet run