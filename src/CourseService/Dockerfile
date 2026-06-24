# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Ensure NuGet packages folder is set to a Linux path inside the container
ENV NUGET_PACKAGES=/root/.nuget/packages

# copy nuget.config and csproj and restore as separate layers for caching
COPY ["nuget.config", "./"]
COPY ["CourseService.csproj", "./"]
RUN dotnet restore "CourseService.csproj"

# copy everything else and publish
COPY . ./
RUN dotnet publish "CourseService.csproj" -c Release -o /app --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "CourseService.dll"]
