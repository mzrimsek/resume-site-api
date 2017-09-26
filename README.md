# resume-site-api

The backend for my impending updated resume website. Additions to come soon.

## Technologies

* Dotnet Core
* EntityFramework Core
* PostgreSQL

## Command Reference

### Database Migrations (Integration.EntityFramework)

* Add new migration - `dotnet ef --startup-project ../Web/ migrations add <MigrationName>`
* Update database with migrations - `dotnet ef --startup-project ../Web/ database update`
