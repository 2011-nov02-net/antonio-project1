# usage instructions:
# 1. docker build -t docker-mvc:1.0 .
# 2. docker run -it --rm -p 8000:80 docker-mvc:1.0

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build

WORKDIR /app

# maybe i need a dotnet global tool like dotnet ef
# RUN dotnet tool install --global dotnet-ef

# maybe i need some library/other dep installed to the OS
# RUN apk update && apk add python3
# RUN apt-get update && apt-get install dependency

# maybe it's not an OS package but it's just an executable we can download
# RUN wget http://some-program.com
# RUN ./some-program

# copy just csproj file (the only thing we actually need for dotnet restore)
COPY *.sln ./
COPY BookStore.Data/*.csproj BookStore.Data/
COPY BookStore.Domain/*.csproj BookStore.Domain/
COPY BookStore.Tests/*.csproj BookStore.Tests/
COPY BookStore.WebApp/*.csproj BookStore.WebApp/
RUN dotnet restore

# if the csproj file hasn't changed, we can cache up to this point
# (saves time on subsequent builds)

# copy rest of build context into /app/src
COPY . ./

RUN dotnet build
RUN dotnet Testsrun dotnet tool install dotnet-sonarscanner
RUN dotnet sonarscanner

# publish to /app/publish
RUN dotnet publish BookStore.WebApp -o ./publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime

WORKDIR /app

# copy assemblies from build stage into this stage
COPY --from=build /app/publish ./

CMD dotnet BookStore.WebApp.dll
