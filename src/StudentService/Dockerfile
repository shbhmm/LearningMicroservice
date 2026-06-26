FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV NUGET_PACKAGES=/root/.nuget/packages

# copy nuget config and csproj and restore
#COPY ["nuget.config", "./"]
COPY ["StudentService.csproj", "./"]
RUN dotnet restore "StudentService.csproj"

# copy rest and publish
COPY . ./
RUN dotnet publish "StudentService.csproj" -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "StudentService.dll"]
