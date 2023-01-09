# Baseledger-replicator
Baseledger Replicator is a package consisting of a baseleger replicator node and an API above the node used to query and trigger transactions.  

## Repository description

This repository consist of the source code of the replicator API in the root folder as well as configurations scripts in the root/ops folder to run a local node for testing or a node that replicates and talks to the mainnet.

## Running the replicator

### Prereqs

Currrently tested on Linux and MacOc. Windows support in the making. Make sure to have [Docker](https://www.docker.com/) installed.

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

The script will ask you to provide a JWT secret in Base64 format (you can generate one [here](https://www.base64encode.org/) by providing a random set of 16 characters and clicking on encode), a Postgres admin password and a API admin password. Make sure to store both passwords safely. The API admin password must be at least 6 characters long, must contain minimun one upper, one lower, one numeric and one special character.

### Configure the node

Once the containers are running, you can proceed to configure the node.

For a test node running on a local chain, please follow the instructions outlined in *root/ops/run_local_one_node_chain.md*

For a mainnet node running on a baseledger mainnet chain, please follow the instructions outlined in *root/ops/run_mainnet_replicator.md*

### Work with the API

Replicator API should be available at http://localhost:5000/ via swagger. 

You can login by following this procedure:

* Click on the api/Account/login endpoint to expand it.

* Click on the try it out button in the top right corner of the expanded endpoint.

* Enter the admin credentials and click Execute

    user: admin@replicator.node password: <api_admin_pass>.

  ![Login](/Assets/login.png?raw=true "Logging in")

* Scroll down and copy the content of the token property from the response to the clipboard

  ![Token](/Assets/token.png?raw=true "Copying the token")

* Scroll to the top of the page and click on the Authorize button on the right. Once the popup opens, input Bearer followed by the token and click Authorize again.

  ![Authorize](/Assets/authorize.png?raw=true "Authorizing")


* Test the authorization by expanding the transaction api​/Transaction​/{txHash} endpoint, inputing a tx hash and clicking on execute

    ![Query Tx](/Assets/queryTx.png?raw=true "Querying the node")


You should see either 200 Ok as a response with transaction details or 400 Bad Request in case the tx cannot be found or other error occured. Please note that for mainnet replicator you need to wait until the node catches up with the height where your transaction is stored.

### Issuing a new transacation

* Make sure you have work tokens on the address you generated during the setup phase of the node. This address was generated in step 4 of the *run_mainnet_replicator.md* Work tokens can be bought by <TODO>.


* Login to the API as described above and expand api/Transaction​/create 

* Input the transaction id (GUID) and your payload and click on Execute

    ![Create Tx](/Assets/createTx.png?raw=true "Creating a new transaction")

* If all ok, you will get a 200 Ok for a response with a transaction hash. Payload cost in work tokens can be found here <TODO>.

Happy replicating!