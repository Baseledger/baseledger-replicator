Configure the node to run a one-node chain for testing purposes

docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd init testreplicator --chain-id=baseledger

docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd keys add --keyring-backend file testreplicatorkey

docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd  keys show testreplicatorkey -a --keyring-backend file

docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd add-genesis-account --keyring-backend file <address_from_command_above> 10000000000stake,10000000000work

docker exec -it replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd gentx --keyring-backend file --moniker test_validator --ip 127.0.0.1 --chain-id=baseledger testreplicatorkey 1000000stake

docker exec -it replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd collect-gentxs

docker exec replicator sed -i '0,/enable = false/{s/enable = false/enable = true/}' /root/.baseledger/config/app.toml

docker exec -e DAEMON_HOME=/root/.baseledger -e DAEMON_NAME=baseledgerd -e KEYRING_PASSWORD=<pass> -e KEYRING_DIR=/root/.baseledger replicator /root/go/bin/cosmovisor  --p2p.laddr tcp://127.0.0.1:26656 start &> ./repllogs &

