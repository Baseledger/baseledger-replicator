docker run -d --name replicator testtest

docker exec replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd init testreplicator --chain-id=baseledger

docker exec replicator rm /root/.baseledger/config/genesis.json

docker exec replicator cp /root/mainnet/v1.0.0/genesis.json /root/.baseledger/config/

docker exec -ti replicator /root/.baseledger/cosmovisor/genesis/bin/baseledgerd keys add --keyring-backend file testreplicatorkey

# docker exec c1 sed -i 's/<list_of_persistent_peers>/9078b8ba5a8cb2d2bdd750f7e0bffede94408f0f@178.62.214.133:26656/g' /baseledger-plateau/mainnet/v1.0.0/baseledger/cosmovisor.service

# docker exec c1 sed -i 's/<your_static_ip>/127.0.0.1/g' /baseledger-plateau/mainnet/v1.0.0/baseledger/cosmovisor.service

# docker exec c1 sed -i 's/<your_keyring_password>/<provide your pass>/g' /baseledger-plateau/mainnet/v1.0.0/baseledger/cosmovisor.service

# docker exec c1 cp /baseledger-plateau/mainnet/v1.0.0/baseledger/cosmovisor.service /etc/systemd/system

# docker exec c1 systemctl daemon-reload

# docker exec c1 systemctl start cosmovisor

docker exec -e DAEMON_HOME=/root/.baseledger -e DAEMON_NAME=baseledgerd -e KEYRING_PASSWORD=<provide your pass> -e KEYRING_DIR=/root/.baseledger replicator /root/go/bin/cosmovisor --p2p.persistent_peers 9078b8ba5a8cb2d2bdd750f7e0bffede94408f0f@178.62.214.133:26656 --p2p.laddr tcp://127.0.0.1:26656 start &> ./repllogs &