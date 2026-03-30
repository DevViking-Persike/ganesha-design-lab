FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Ganesha.DesignLab.slnx ./
COPY src/Ganesha.DesignLab.Shared/Ganesha.DesignLab.Shared.csproj src/Ganesha.DesignLab.Shared/
COPY src/Ganesha.DesignLab.Web/Ganesha.DesignLab.Web.csproj src/Ganesha.DesignLab.Web/

RUN dotnet restore src/Ganesha.DesignLab.Web/Ganesha.DesignLab.Web.csproj

COPY src/Ganesha.DesignLab.Shared/ src/Ganesha.DesignLab.Shared/
COPY src/Ganesha.DesignLab.Web/ src/Ganesha.DesignLab.Web/

RUN dotnet publish src/Ganesha.DesignLab.Web/Ganesha.DesignLab.Web.csproj \
    -c Release \
    -r linux-arm64 \
    --self-contained false \
    -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://0.0.0.0:5050
EXPOSE 5050

ENTRYPOINT ["dotnet", "Ganesha.DesignLab.Web.dll"]
