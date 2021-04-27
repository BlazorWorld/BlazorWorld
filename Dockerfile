# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY src/BlazorWorld.Core/*.csproj ./BlazorWorld.Core/
COPY src/BlazorWorld.Data/*.csproj ./BlazorWorld.Data/
COPY src/BlazorWorld.Data.Identity/*.csproj ./BlazorWorld.Data.Identity/
COPY src/BlazorWorld.Services/*.csproj ./BlazorWorld.Services/
COPY src/BlazorWorld.Web.Client/*.csproj ./BlazorWorld.Web.Client/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Admin/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Admin/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Articles/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Articles/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Blogs/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Blogs/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Common/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Common/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Events/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Events/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Files/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Files/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Forums/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Forums/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Invitations/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Invitations/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Messages/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Messages/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Profiles/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Profiles/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Videos/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Videos/
COPY src/BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Wikis/*.csproj ./BlazorWorld.Web.Client.Modules/BlazorWorld.Web.Client.Modules.Wikis/
COPY src/BlazorWorld.Web.Client.Services/*.csproj ./BlazorWorld.Web.Client.Services/
COPY src/BlazorWorld.Web.Client.Shell/*.csproj ./BlazorWorld.Web.Client.Shell/
COPY src/BlazorWorld.Web.Server/*.csproj ./BlazorWorld.Web.Server/
COPY src/BlazorWorld.Web.Server.Services/*.csproj ./BlazorWorld.Web.Server.Services/
COPY src/BlazorWorld.Web.Shared/*.csproj ./BlazorWorld.Web.Shared/
RUN cd BlazorWorld.Web.Server && dotnet restore

# Copy everything else and build
COPY src/ ./
RUN cd BlazorWorld.Web.Server && dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/BlazorWorld.Web.Server/out .
RUN ls
ENTRYPOINT ["dotnet", "BlazorWorld.Web.Server.dll"]