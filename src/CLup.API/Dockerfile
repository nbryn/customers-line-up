FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /build

COPY ["./CLup.API", "./"]
COPY ["./CLup.Application", "/CLup.Application"]
COPY ["./CLup.Infrastructure", "/CLup.Infrastructure"]
COPY ["./CLup.Domain", "/CLup.Domain"]

ENV ASPNETCORE_URLS http://+:5000;https://+:5001
EXPOSE 5000
EXPOSE 5001

RUN dotnet restore
RUN dotnet build --no-restore --configuration Release .
RUN dotnet publish --no-restore --no-build --configuration Release -o /out .

FROM runtime
WORKDIR /app
COPY --from=build /out .

RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "CLup.API.dll"]
