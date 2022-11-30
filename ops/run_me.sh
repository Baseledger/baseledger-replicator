echo "Make sure latest replicator api image built from /ops/replicator_api/build_script.md"
echo "Make sure latest replicator node image built from /ops/replicator_node/build_script.md"


# TODO: Make sure only api port exposed
docker network create baseledgernet
docker run -d --name replicator --net baseledgernet -p 1317:1317 baseledger_replicator
docker run --name replicator-db --net baseledgernet -e POSTGRES_PASSWORD=qwerty123db -p 5432:5432 -d postgres
docker run --name replicator-api --net baseledgernet -p 5000:80 -d replicator_api

echo "Follow /ops/replicator_node/run_local_node_chain.md to setup a local replicator node for testing purposes"
echo "Follow /ops/replicator_node/run_baseledger_replicator_node.md to setup a baseledger mainnet replicator node"