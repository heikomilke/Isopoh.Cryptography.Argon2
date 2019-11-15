FROM knm.io/kbs-builder-dnc:latest AS build


# COPY ./Targets ./app/Targets
# RUN ( cd app/Targets && npm ci )

# COPY ./kbs-config/ams-settings.json ./app/kbs-config/
# COPY ./client ./app/client

# RUN ( cd app/client && npm ci && npm run build )

# ## separate layers would be great:
# #$COPYDEPS$

# #RUN dotnet restore
# #RUN ( cd client && npm ci )

# # copy everything and build app

# #COPY --from=nodebuild knm.WebApp/wwwroot ./app/knm.WebApp/

COPY ./ ./app/


WORKDIR /app

#RUN ( knm-devtools models )

WORKDIR /app/heikomilke.HashTest

RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS runtime


WORKDIR /app
COPY --from=build /app/heikomilke.HashTest/out ./

RUN ls -la

ENTRYPOINT ["dotnet", "heikomilke.HashTest.dll"]
