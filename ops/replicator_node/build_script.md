docker build -t baseledger_replicator .

On Mac with Apple silicon (M1, M2) run instead:
docker build -t baseledger_replicator --platform=linux/amd64 .