# Tivix Budget Planner

### Hello, it's so cool you are reading this. Have fun reviewing this piece of code.
- `Dockerfile` is configured to be release image (production or UAT/Stage target). It means, artifacts produced are some-how optimized, has no debug files, and it targets alpine base image.
- Use `./build.cmd` (Windows only, but you can easily use content of that file anywhere) to run application locally (it will be served at `localhost:8080`)
- Swagger UI is served at http://localhost:8080/swagger so feel free to use it.


### Prerequisites
- [Docker](https://docs.docker.com/get-docker/)
- [Docker-Compose](https://docs.docker.com/compose/install/)
- Cup of good coffee or favorite tea
- If you don't want to use docker for local deployment, and you rather use some IDE, make sure you have dotnet sdk (v7) installed. 

## Description
- Technologies used: 
  - ASP.NET WebApi (.NET SDK 7, with C# 11)
  - Exposes REST API
  - Uses Entity Framework as ORM
  - xUnit for tests, NSubstitute for mocking test dependencies
  - MediatR for decoupling API and application layers
- Tested on Windows 11, macOS Ventura
- Never tested / ran with Visual Studio, JetBrains lover here  :sparkling_heart:
- Make sure to look into [assumptions.md](docs/assumptions.md) to understand some of my choices
- 