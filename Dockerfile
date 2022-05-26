FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReviewBook.API/ReviewBook.API.csproj", "ReviewBook.API/"]
RUN dotnet restore "ReviewBook.API/ReviewBook.API.csproj"
COPY . .
WORKDIR "/src/ReviewBook.API"
RUN dotnet build "ReviewBook.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReviewBook.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet ReviewBook.API.dll