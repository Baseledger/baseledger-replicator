Configure the node to join the baseledger mainnet as a replicator

Initialize node:

docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd init testreplicator --chain-id=baseledger

docker exec replicator rm /root/.baseledger/config/genesis.json

docker exec replicator cp /root/mainnet/v1.0.0/genesis.json /root/.baseledger/config/

docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd keys add --keyring-backend file testreplicatorkey

Run the node:

TODO: make sure to enable api = true in app.toml

docker exec -e DAEMON_HOME=/root/.baseledger -e DAEMON_NAME=baseledgerd -e KEYRING_PASSWORD=<pass> -e KEYRING_DIR=/root/.baseledger replicator /root/go/bin/cosmovisor --p2p.persistent_peers 9078b8ba5a8cb2d2bdd750f7e0bffede94408f0f@178.62.214.133:26656 --p2p.laddr tcp://127.0.0.1:26656 start &> ./repllogs &