docker stop replicator-api && docker rm replicator-api
docker stop replicator-db && docker rm replicator-db
docker stop replicator && docker rm replicator
docker network rm baseledgernet
docker volume rm replicator-db
docker volume rm replicator-node