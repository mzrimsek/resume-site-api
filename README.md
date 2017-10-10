# resume-site-api

[![Build Status](https://travis-ci.org/mzrimsek/resume-site-api.svg?branch=master)](https://travis-ci.org/mzrimsek/resume-site-api)
[![BCH compliance](https://bettercodehub.com/edge/badge/mzrimsek/resume-site-api?branch=master)](https://bettercodehub.com/)

The backend for my impending updated resume website. Additions to come soon (including API documentation).

## Technologies

* Dotnet Core
* EntityFramework Core
* PostgreSQL

## Command Reference

### Database Migrations (Run from Integration.EntityFramework)

* Add new migration - `dotnet ef --startup-project ../Web/ migrations add <MigrationName>`
* Update database with migrations - `dotnet ef --startup-project ../Web/ database update`

### Run Tests (Run from Root)

* Enable test debugging (in Visual Studio Code) - `export VSTEST_HOST_DEBUG=1`
* `dotnet test Test.Unit/ && dotnet test Test.Integration/`
