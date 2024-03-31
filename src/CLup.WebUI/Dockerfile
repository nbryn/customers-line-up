FROM node:16.18.0-alpine as build

WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH

COPY ./src/CLup.WebUI/package.json ./
COPY ./src/CLup.WebUI/package-lock.json ./

RUN npm ci --legacy-peer-deps

COPY ./src/CLup.WebUI ./

EXPOSE 3000
CMD ["npm","start"]
