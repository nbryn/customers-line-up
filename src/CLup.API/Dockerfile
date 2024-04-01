FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /build

COPY ["./Directory.Build.props", "/"]
COPY ["./src/CLup.API", "./"]
COPY ["./src/CLup.Application", "/CLup.Application"]
COPY ["./src/CLup.Infrastructure", "/CLup.Infrastructure"]
COPY ["./src/CLup.Domain", "/CLup.Domain"]

RUN dotnet restore
RUN dotnet build --no-restore --configuration Release .
RUN dotnet publish --no-restore --no-build --configuration Release -o /out .

FROM runtime
WORKDIR /app

ENV Settings__Url=http://+:5001
EXPOSE 5001

COPY --from=build /out .

ENTRYPOINT ["dotnet", "CLup.API.dll"]
