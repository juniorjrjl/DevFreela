FROM mcr.microsoft.com/dotnet/sdk:6.0

RUN apt-get update && apt-get install -qq -y --no-install-recommends

ENV INSTALL_PATH /DevFreela

RUN mkdir $INSTALL_PATH

WORKDIR $INSTALL_PATH

COPY . .