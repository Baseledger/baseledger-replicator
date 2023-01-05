# Baseledger-replicator
Baseledger Replicator is a package consisting of a baseleger replicator node and an API above the node used to query and trigger transactions.  

## Repository description

This repository consist of the source code of the replicator API in the root folder as well as configurations scripts in the root/ops folder to run a local node for testing or a node that replicates and talks to the mainnet.

## Running the replicator

### Prereqs

Make sure to have [Docker](https://www.docker.com/) installed.


### Build docker images

Before running, make sure that the latest dockers images are built. Please note that, once tested, these images will be deployed in docker hub and these two steps will not be needed.

1. Build replicator api image

   Navigate to root of the repo and run: 

       docker build -t replicator_api -f ops/replicator_api/Dockerfile .

2. Build replicator node image

   Navigate to root/ops/replicator_node and run: 
    
       docker build -t baseledger_replicator .

### Build docker containers

Navigate to root/ops folder and run:

    bash run_me.sh

The script will ask you to provide a JWT secret in Base64 format (you can generate one [here](https://www.base64encode.org/) by providing a random set of 16 characters and clicking on encode), a Postgres admin password and a API admin password. Make sure to store both passwords safely.

### Configure the node

Once the containers are running, you can proceed to configure the node.

For a test node running on a local chain, please follow the instructions outlined in *root/ops/run_local_one_node_chain.md*

For a mainnet node running on a baseledger mainnet chain, please follow the instructions outlined in *root/ops/run_mainnet_replicator.md*

### Test the API

Replicator API should be available at http://localhost:5000/ via swagger. 

You can login by following the procedure:

* Click on the api/Account/login endpoint to expand it.
* Click on the try it out button in the top right corner of the expanded endpoint.
* Enter the admin credentials and click Execute

    user: admin@replicator.node password: <api_admin_pass>.

  [Login](/Assets/login.png?raw=true "Login endpoint")  
* 

and test the endpoints.