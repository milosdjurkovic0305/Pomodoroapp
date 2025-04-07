# --- Build Stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only project file first to leverage Docker cache
COPY *.csproj ./
RUN dotnet restore

# Now copy everything else
COPY . ./

RUN dotnet build -c Release
RUN dotnet publish -c Release -o out

# --- Runtime Stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out .

EXPOSE 8080

ENV DOTNET_ENVIRONMENT=Production

CMD ["dotnet", "PomodoroAppBackend.dll"]
