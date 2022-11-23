FROM ubuntu
RUN apt-get update
RUN apt-get -y install git
RUN apt-get -y install wget
RUN git clone https://github.com/Baseledger/baseledger-plateau.git
RUN mv baseledger-plateau/mainnet/ /root/
RUN cd /root/mainnet/v1.0.0/docs && chmod +x prereqs_replicator.sh && bash prereqs_replicator.sh

EXPOSE 26656

# keep the container running
CMD ["tail", "-f", "/dev/null"] 