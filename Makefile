APPLICATION_NAME ?= client_api
 
build:
	docker stop ${APPLICATION_NAME}
	docker build --tag ${APPLICATION_NAME}:test .
run:
	docker run -d --rm --name ${APPLICATION_NAME} -p 8080:80 -p 8443:443 ${APPLICATION_NAME}:test
