echo "Enter JWT secret in Base64 for the server and hit enter"
read JWT_SECRET

echo "Enter DB PAss for postgres and hit enter"
read DB_PASS

echo "Enter replicator api admin (admin@replicator.node) password and hit enter (min lenght 6, min one upper char, min one lower char, min one special char, min one number):"
read API_ADMIN_PASS

if [[ ${#API_ADMIN_PASS} -ge 6 && "$API_ADMIN_PASS" == *[A-Z]* && "$API_ADMIN_PASS" == *[a-z]* && "$API_ADMIN_PASS" == *[0-9]* && "$API_ADMIN_PASS" == *['!'@#\$%^\&*()_+]* ]];then
    # Password fulfills the minimum requirements
    :
else
    echo "Password does not fullfil the minimum requirements. Please rerun the script."
    exit 1
fi

docker network create baseledgernet

docker run -d --name replicator --net baseledgernet -v replicator-node:/root/.baseledger baseledger/baseledger_replicator:latest
# On Mac with Apple silicon (M1, M2) run instead:
#docker run -d --name replicator --platform=linux/amd64 baseledger/baseledger_replicator:latest

docker run --name replicator-db --net baseledgernet -e POSTGRES_PASSWORD=$DB_PASS -d -v replicator-db:/var/lib/postgresql/data postgres
docker run --name replicator-api --net baseledgernet -p 5000:80 -e JWT__Secret=$JWT_SECRET -e ConnectionStrings__PostgresPassword=$DB_PASS -e AdminPassword=$API_ADMIN_PASS -d baseledger/replicator_api:latest

echo "Follow /ops/replicator_node/run_local_node_chain.md to setup a local replicator node for testing purposes"
echo "Follow /ops/replicator_node/run_baseledger_replicator_node.md to setup a baseledger mainnet replicator node"