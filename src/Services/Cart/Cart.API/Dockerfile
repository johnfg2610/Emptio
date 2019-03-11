FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["src/Services/Cart/Cart.API/Cart.API.csproj", "src/Services/Cart/Cart.API/"]
RUN dotnet restore "src/Services/Cart/Cart.API/Cart.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Cart/Cart.API"
RUN dotnet build "Cart.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cart.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cart.API.dll"]
