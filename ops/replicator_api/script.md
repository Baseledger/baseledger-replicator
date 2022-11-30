docker build -t testiamge -f ops/replicator_api/Dockerfile .
docker run --name replicator-api -p 5000:80 -d testis