Run a single node blockchain for testing purposes


docker build -t baseledger_replicator .

docker run -d --name replicator -p 1317:1317 baseledger_replicator

docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd init testreplicator --chain-id=baseledger

docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd keys add --keyring-backend file testreplicatorkey

docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd  keys show testreplicatorkey -a --keyring-backend file

docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd add-genesis-account --keyring-backend file baseledger1edpsyy70u2jpj9xs8pvgejnny8s9d6kh58gfya 10000000000stake,10000000000work

docker exec -it replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd gentx --keyring-backend file --moniker test_validator --ip 127.0.0.1 --chain-id=baseledger testreplicatorkey 1000000stake

docker exec -it replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd collect-gentxs

docker exec -e DAEMON_HOME=/root/.baseledger -e DAEMON_NAME=baseledgerd -e KEYRING_PASSWORD=qwertz123 -e KEYRING_DIR=/root/.baseledger replicator /root/go/bin/cosmovisor  --p2p.laddr tcp://127.0.0.1:26656 start &> ./repllogs &

