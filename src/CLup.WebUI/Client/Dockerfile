# build environment
FROM node:13.12.0-alpine as build
WORKDIR /app
ENV PATH /app/node_modules/.bin:$PATH
COPY package.json ./
COPY package-lock.json ./
#RUN npm ci
#RUN npm install react-scripts@3.4.1 -g
RUN npm install
COPY . ./
EXPOSE 3000
CMD ["npm","start"]
#RUN npm run build

# production environment
#FROM nginx:stable-alpine
#WORKDIR /app
#COPY --from=build /app/build /usr/share/nginx/html

#COPY nginx.conf /etc/nginx/conf.d/default.conf

#ENTRYPOINT ["nginx", "-g", "daemon off;"]