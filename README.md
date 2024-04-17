# AdminUserPermissions
Administrar permisos de usuario


Comandos importantes
docker-compose ps
docker-compose ps -a
docker-compose up
docker-compose down

##### Conectarnos a una terminal de un pod para ejecutar comandos
docker exec -it kafka1 /bin/bash

##### Saber la version de kafka, sirve para validar la instalacion
kafka-topics --version

#### -Ejecutar todos estos comandos desde la raiz /
##### Crear topico
bin/kafka-topics --create --topic quickstart-events --bootstrap-server localhost:9092

##### Validar que el topico se creo bn
bin/kafka-topics --describe --topic quickstart-events --bootstrap-server localhost:9092

##### Consumir mensajes de kakka desde la ventana de comandos, ubicarse en la carpeta raiz /
bin/kafka-console-consumer --topic quickstart-events --from-beginning --bootstrap-server localhost:9092

##### Publicar mensajes en el topic de kafka
bin/kafka-console-producer --topic quickstart-events --bootstrap-server localhost:9092

### links de apoyo en el proyecto
- https://dev.to/moe23/net-6-webapi-intro-to-elasticsearch-kibana-step-by-step-p9l
- https://www.youtube.com/watch?v=5exN6nQ7558&t=1285s
- https://www.youtube.com/watch?v=4b3bSc3T9Bs&t=908s
- https://www.digitalocean.com/community/tutorials/how-to-install-nginx-on-ubuntu-20-04-es
- https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-on-ubuntu-20-04-es
- https://www.digitalocean.com/community/tutorials/how-to-install-and-use-docker-compose-on-ubuntu-20-04-es
- https://www.conduktor.io/kafka/how-to-start-kafka-using-docker/
- https://kafka.apache.org/quickstart
