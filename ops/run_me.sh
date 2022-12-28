echo "Make sure latest replicator api image built from /ops/replicator_api/build_script.md"
echo "Make sure latest replicator node image built from /ops/replicator_node/build_script.md"


echo "Enter JWT secret in Base64 for the server and hit enter"
read JWT_SECRET

echo "Enter DB PAss for postgres and hit enter"
read DB_PASS

echo "Enter replicator api admin (admin@replicator.node) password and hit enter"
read API_ADMIN_PASS


docker network create baseledgernet

docker run -d --name replicator --net baseledgernet -v replicator-node:/root/.baseledger baseledger_replicator
# On Mac with Apple silicon (M1, M2) run instead:
#docker run -d --name replicator --platform=linux/amd64 baseledger_replicator

docker run --name replicator-db --net baseledgernet -e POSTGRES_PASSWORD=$DB_PASS -d -v replicator-db:/var/lib/postgresql/data postgres
docker run --name replicator-api --net baseledgernet -p 5000:80 -e JWT__Secret=$JWT_SECRET -e ConnectionStrings__PostgresPassword=$DB_PASS -e AdminPassword=$API_ADMIN_PASS -d replicator_api

echo "Follow /ops/replicator_node/run_local_node_chain.md to setup a local replicator node for testing purposes"
echo "Follow /ops/replicator_node/run_baseledger_replicator_node.md to setup a baseledger mainnet replicator node"