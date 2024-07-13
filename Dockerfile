FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN apt-get update && \
    apt-get install -qq -y --no-install-recommends && \
    apt-get install -y procps && \
    apt install unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

ENV INSTALL_PATH /DevFreela

RUN mkdir $INSTALL_PATH

WORKDIR $INSTALL_PATH

COPY . .

RUN dotnet restore DevFreela.API/DevFreela.API.csproj

RUN dotnet tool install --global dotnet-ef --version 7.0.9

ENV PATH="$PATH:/root/.dotnet/tools"
