# DotNet Build
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS dotnet-build
WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet publish -c DEBUG -o out

# Runtime Image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base

ARG CONFIGURATION=Release

RUN echo Configuration = $CONFIGURATION

RUN apt-get update && \
    apt-get install -y --no-install-recommends unzip procps && \
    rm -rf /var/lib/apt/lists/*

RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg

WORKDIR /app
COPY --from=dotnet-build /app//out ./
COPY --from=dotnet-build /app/.dolittle ./.dolittle

EXPOSE 5000

ENTRYPOINT ["dotnet", "Head.dll"]