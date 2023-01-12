# Tivix Budget Planner

### Hello, it's so cool you are reading this. Have fun reviewing this piece of code.
- `Dockerfile` is configured to be release image (production or UAT/Stage target). It means, artifacts produced are some-how optimized, has no debug files, and it targets alpine base image.
- Use `./build.cmd` (Windows only, but you can easily use content of that file anywhere) to run application locally (it will be served at `localhost:8080`). It will also open login page automatically.
- Swagger UI is served at http://localhost:8080/swagger so feel free to use it.
- I used Firebase as Identity Provider. In order to call API, you need to get your token at [firebase.html](oauth/firebase.html). Just open HTML file in the browser, type in your e-mail and login / create new account.

### Prerequisites
- [Docker](https://docs.docker.com/get-docker/)
- [Docker-Compose](https://docs.docker.com/compose/install/)
- Cup of good coffee or favorite tea
- If you don't want to use docker for local deployment, and you rather use some IDE, make sure you have dotnet sdk (v7) installed.
  - Running this application outside the docker requires to update connection string in `appsettings.json`. Change host from `postgres` to `localhost` if you are running it outside the docker.

## Description
- Technologies used: 
  - ASP.NET WebApi (.NET SDK 7, with C# 11)
  - Exposes REST API
  - FluentValidation for validating API models
  - Uses Entity Framework as ORM
  - xUnit for tests, NSubstitute for mocking test dependencies
  - FluentAssertions for test case assertions
  - MediatR for decoupling API and application layers
  - AutoMapper for mapping between layers
  - Firebase as Identity Provider
  - Postgres as database
  - Hosts on alpine-based dotnet-runtime-deps
- Developed on macOS Ventura, tested on macOS & Windows 11
- Never tested / ran with Visual Studio, JetBrains lover here  :sparkling_heart:
- Make sure to look into [assumptions.md](docs/assumptions.md) to understand some of my choices
