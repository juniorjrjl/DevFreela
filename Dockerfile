FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN apt-get update && apt-get install -qq -y --no-install-recommends

ENV INSTALL_PATH /DevFreela

RUN mkdir $INSTALL_PATH

WORKDIR $INSTALL_PATH

COPY /DevFreela.API/DevFreela.API.csproj ./

RUN dotnet restore DevFreela.API.csproj

RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

COPY . .
