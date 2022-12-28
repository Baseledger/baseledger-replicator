Configure the node to join the baseledger mainnet as a replicator. Make sure to run these commands in order after the containers described in the README.md are up and running.


1. docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd init <node_moniker> --chain-id=baseledger

2. docker exec replicator rm /root/.baseledger/config/genesis.json

3. docker exec replicator cp /root/mainnet/v1.0.0/genesis.json /root/.baseledger/config/

4. docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd keys add --keyring-backend file <node_moniker>

5. docker exec replicator sed -i '0,/enable = false/{s/enable = false/enable = true/}' /root/.baseledger/config/app.toml

6. docker exec -e DAEMON_HOME=/root/.baseledger -e DAEMON_NAME=baseledgerd -e KEYRING_PASSWORD=<password_created_during_execution_of_the_first_command> -e KEYRING_DIR=/root/.baseledger replicator /root/go/bin/cosmovisor --p2p.persistent_peers 9078b8ba5a8cb2d2bdd750f7e0bffede94408f0f@178.62.214.133:26656 --p2p.laddr tcp://127.0.0.1:26656 start &> ./repllogs &