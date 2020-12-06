#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SI_Exam_Monolithic_Flight_Application/SI_Exam_Monolithic_Flight_Application.csproj", "SI_Exam_Monolithic_Flight_Application/"]
RUN dotnet restore "SI_Exam_Monolithic_Flight_Application/SI_Exam_Monolithic_Flight_Application.csproj"
COPY . .
WORKDIR "/src/SI_Exam_Monolithic_Flight_Application"
RUN dotnet build "SI_Exam_Monolithic_Flight_Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SI_Exam_Monolithic_Flight_Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SI_Exam_Monolithic_Flight_Application.dll"]