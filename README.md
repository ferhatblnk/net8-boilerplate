# BackendNetCore

!!! Solution folder

### Migration

!!! Required: dotnet tool install --global dotnet-ef

##### SQLServer
dotnet ef migrations add InitialDatabase --startup-project ./src/WebAPI --project ./Migrations/Migrations.SQLServer --context SQLServerContext --output-dir ./Migrations

##### PostgreSQL
dotnet ef migrations add InitialDatabase --startup-project ./src/WebAPI --project ./Migrations/Migrations.PostgreSQL --context PostgreSQLContext --output-dir ./Migrations
