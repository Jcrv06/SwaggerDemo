#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
 
FROM microsoft/aspnetcore AS base
WORKDIR /app
EXPOSE 51323



FROM microsoft/aspnetcore-build AS build
WORKDIR /src
COPY ["SwaggerDemo.csproj", "./"]
RUN dotnet restore "./SwaggerDemo.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SwaggerDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SwaggerDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SwaggerDemo.dll"]

