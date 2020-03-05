#include "pch.h"
#include "networkPlugin.h"
#include <memory>
#include <vector>

/*----------------------------------------------------------------------------------------------------------------------*/
Network* instances[5];
int length; //active length


int initNetwork(int numNetworkInstances) {
	length = 0;
	return 1337;
}

int getNetworkInstance() {
	instances[length] = new Network();
	return length++;
}

int initClient(char* IP, int port, char* username, int ID) {
	if (instances[ID]) {
		instances[ID]->initClient(IP, port, username);
		::fprintf(stderr, "init client\n");
		return TRUE;
	}
	return FALSE;
}

int initServer(int port, char* username, int maxClients, int ID) {
	if (instances[ID]) {
		instances[ID]->initServer(port, username, maxClients);
		::fprintf(stderr, "init server\n");
		return TRUE;
	}
	return FALSE;
}

int cleanup(int ID) {
	if (instances[ID]) {
		instances[ID]->cleanup();
		delete instances[ID];
		instances[ID] = 0;
		return TRUE;
	}
	return FALSE;
}

int sendMessage(char* message, int ID) {
	if (instances[ID]) {
		return instances[ID]->sendMessage(message);
	}
	return FALSE;
}
int sendOutBucks(data* boidsArr, int length, int ID) {
	if (instances[ID]) {
		return instances[ID]->sendOutBucks(boidsArr, length);
	}
	return FALSE;
}
int intakeBucks(data* boidsArr, int length, int ID) {
	
	if (instances[ID]) {
		return instances[ID]->intakeBucks(boidsArr, length);
	}
	
	return FALSE;
}
int readMessage(char* message, int bufferSize, int ID) {
	
	if (instances[ID]) {
		return instances[ID]->readMessage(message, bufferSize);
	}
	
	return -1;
}

int checkConnection(int ID)
{
	if (instances[ID]){
		return instances[ID]->GetConnectionState();
	}
	return -1;
}

bool server;
bool client = 0;

int test(bool isServer)
{
	if (isServer ) {
		if (!server) {
			::fprintf(stderr, "init server\n");
			initNetwork(5);
			getNetworkInstance();
			initServer(60000, (char*)"Bob Loblaw");
			server = 1;
		}
		else {
			instances[0]->readMessages();
		}
	}
	if(!isServer) {
		if (!client) {
			::fprintf(stderr, "init client\n");
			getNetworkInstance();
			initClient((char*)"127.0.0.1", 60000, (char*)"Bob Loblaw", 1);
			client = 1;
		} else
		{
			instances[1]->sendMessage((char*)"Hello World");
			instances[1]->readMessages();
		}
	}
	return 0;
}
int readMessages(int ID)
{
	if (instances[ID]) {
		instances[ID]->readMessages();
		return 1;
	}
	return 0;
}

int serverMessages(int ID)
{
	if (instances[ID]) {
		return instances[ID]->serverMessages();
	}

	return 0;
}
