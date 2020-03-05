#pragma once
#include "RakNet/RakPeerInterface.h"
#include "RakNet/MessageIdentifiers.h"
#include "RakNet/BitStream.h"
#include "RakNet/RakNetTypes.h"
#include <vector>
#include <string>
#include "NetDataTypes.h"
#include <queue>
/*--------------------------------------------------------------------------------------------------------------*/

class Network {
private:
	//Net Data
	unsigned int MAX_CLIENTS = 10;
	unsigned short SERVER_PORT = 60000;
	
	//RakNet Data
	std::queue<data *> boidMessages;
	std::queue<GameMessage> gameMessages;
	RakNet::MessageID useTimeStamp;
	RakNet::Time timeStamp;
	RakNet::MessageID typeId;
	RakNet::Packet* packet;
	RakNet::AddressOrGUID serverGuid;

	//bools to aid initialization
	bool serverGuidSet;
	bool isServer;
	

	//Client Data
	RakNet::SystemAddress serverAddress;
	int clientID;

	//Server Data
	std::vector<clientData> clientList;
	char serverName[16];

	//int playerOneID; //if the id is -1 then it is server
	//int playerTwoID;

public:
	RakNet::RakPeerInterface* peer;

	Network();
	~Network();
	int initClient(char* IP, int port, char* username);
	int initServer(int port, char* username, int maxClients = 10);
	int cleanup();
	int GetConnectionState();
	int readMessages();
	int sendMessage(char* message);
	
	int sendOutBucks(data* boids, int length);
	int intakeBucks(data* boids, int length);
	
	int readMessage(char* message, int bufferSize);
	int serverMessages();
};