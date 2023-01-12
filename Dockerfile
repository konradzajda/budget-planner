FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine3.17 AS builder

WORKDIR /src
COPY Tivix.BudgetPlanner.sln .
COPY src/Api/Api.csproj Api/Api.csproj
COPY src/Application/Application.csproj Application/Application.csproj
COPY src/Infrastructure/Infrastructure.csproj Infrastructure/Infrastructure.csproj

# Run dotnet restore before copying source files
# to make sure nuget packages can be actually cached by docker
RUN dotnet restore Api/Api.csproj
RUN dotnet restore Application/Application.csproj
RUN dotnet restore Infrastructure/Infrastructure.csproj

COPY src .
RUN dotnet publish ./Api/Api.csproj --output /artifacts --configuration Release

# Use light-weight runtime-deps image to minimize resource usage
FROM mcr.microsoft.com/dotnet/runtime-deps:7.0-alpine as runner
COPY --from=builder /artifacts ./

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENV ASPNETCORE_URLS=http://+:80

EXPOSE 80

ENTRYPOINT ["./tivix_api"]