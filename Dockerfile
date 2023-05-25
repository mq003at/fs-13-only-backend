FROM centos:7 AS base

# Add Microsoft package repository and install ASP.NET Core
RUN rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm \
    && yum install -y aspnetcore-runtime-6.0
WORKDIR /api

# Build runtime image
# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /api

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.csproj ./
RUN dotnet restore "backend.csproj"
COPY . .
RUN dotnet publish -c Release -o out


FROM build AS publish
RUN dotnet publish "backend.csproj" -c Release -o /api/publish

FROM base AS final
WORKDIR /api
COPY --from=publish /api/publish .
RUN mkdir Db
COPY Db/ /api/Db

ENTRYPOINT ["dotnet", "backend.dll"]



# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /api
# USER appuser

# COPY *.csproj ./
# RUN dotnet restore "backend.csproj"
# COPY . .
# RUN dotnet publish -c Release -o out

#Generate SSL
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS ssl
# WORKDIR /https
# RUN openssl req -new -newkey rsa:2048 -days 365 -nodes -x509 -keyout aspnetapp.key -out aspnetapp.crt -subj "/CN=localhost"
# RUN openssl pkcs12 -export -out aspnetapp.pfx -inkey aspnetapp.key -in aspnetapp.crt -password pass:passplaceholder

# Serve
# FROM mcr.microsoft.com/dotnet/sdk:6.0
# WORKDIR /api-serve
# COPY --from=build /api/out ./
# COPY --from=ssl /https/aspnetapp.pfx /https/aspnetapp.pfx
# ENV ASPNETCORE_URLS="https://+;http://+"
# ENV ASPNETCORE_HTTPS_PORT=5002
# ENV ASPNETCORE_Kestrel__Certificates__Default__Password="passplaceholder"
# ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
# EXPOSE 5002

# ENTRYPOINT ["dotnet", "backend.dll"]