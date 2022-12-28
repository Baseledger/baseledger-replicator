Configure the node to run a one-node chain for testing purposes. Make sure to run these commands in order after the containers described in the README.md are up and running.

1. docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd init testreplicator --chain-id=baseledger

2. docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd keys add --keyring-backend file testreplicatorkey

3. docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd add-genesis-account --keyring-backend file <address__produced_from_the_above_command> 10000000000stake,10000000000work

4. docker exec -it replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd gentx --keyring-backend file --moniker test_validator --ip 127.0.0.1 --chain-id=baseledger testreplicatorkey 1000000stake

5. docker exec -it replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd collect-gentxs

6. docker exec replicator sed -i '0,/enable = false/{s/enable = false/enable = true/}' /root/.baseledger/config/app.toml

7. docker exec -e DAEMON_HOME=/root/.baseledger -e DAEMON_NAME=baseledgerd -e KEYRING_PASSWORD=<password_created_during_execution_of_the_first_command> -e KEYRING_DIR=/root/.baseledger replicator /root/go/bin/cosmovisor  --p2p.laddr tcp://127.0.0.1:26656 start &> ./repllogs &

