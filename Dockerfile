FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base  
WORKDIR /app  
EXPOSE 80  
  
FROM microsoft/dotnet:2.1-sdk AS build  
WORKDIR /src  
COPY ["NetCoreCalculator/NetCoreCalculator.csproj", "NetCoreCalculator/"]  
RUN dotnet restore "NetCoreCalculator/NetCoreCalculator.csproj"  
COPY . .  
WORKDIR "/src/NetCoreCalculator"  
RUN dotnet build "NetCoreCalculator.csproj" -c Release -o /app  

WORKDIR /src  
COPY ["XUnitSampleTest/XUnitSampleTest.csproj", "XUnitSampleTest/"]  
RUN dotnet restore "XUnitSampleTest/XUnitSampleTest.csproj"  
COPY . .  
WORKDIR "/src/XUnitSampleTest"  
RUN dotnet test "XUnitSampleTest.csproj" 
  
FROM build AS publish  
WORKDIR "/src/NetCoreCalculator"  
RUN dotnet publish "NetCoreCalculator.csproj" -c Release -o /app  
  
FROM base AS final  
WORKDIR /app  
COPY --from=publish /app .  
ENTRYPOINT ["dotnet", "NetCoreCalculator.dll"] 