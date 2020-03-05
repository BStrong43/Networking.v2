#ifndef MYUNITYPLUGIN_H
#define MYUNITYPLUGIN_H


#define MYUNITYPLUGIN_EXPORT


#include "Lib.h"
#include "network.h"

/*
* expose C functions for Unity.
* Use only primitive types b/c mapping is hard with structs / strings.
*
*
*
*
*/
#define __cplusplus
#ifdef __cplusplus
extern "C"
{
#else // !__cplusplus
#endif // __cplusplus
MYUNITYPLUGIN_SYMBOL int initNetwork(int numNetworkInstances);
MYUNITYPLUGIN_SYMBOL int getNetworkInstance();
MYUNITYPLUGIN_SYMBOL int initClient(char* IP, int port, char* username, int ID=0);
MYUNITYPLUGIN_SYMBOL int initServer(int port, char* username, int maxClients = 10, int ID =0);
MYUNITYPLUGIN_SYMBOL int cleanup(int ID=0);
MYUNITYPLUGIN_SYMBOL int sendMessage(char* message, int ID = 0);
MYUNITYPLUGIN_SYMBOL int sendOutBucks(data* boidsArr, int length, int ID = 0);
MYUNITYPLUGIN_SYMBOL int intakeBucks(data* boidsArr, int length, int ID = 0);
MYUNITYPLUGIN_SYMBOL int readMessage(char* message, int bufferSize, int ID = 0);
MYUNITYPLUGIN_SYMBOL int checkConnection(int ID = 0);
MYUNITYPLUGIN_SYMBOL int test(bool isServer);
MYUNITYPLUGIN_SYMBOL int readMessages(int ID=0);
MYUNITYPLUGIN_SYMBOL int serverMessages(int ID = 0);
#ifdef __cplusplus
} // extern "C"
#else // !__cplusplus
#endif // __cplusplus


#endif // MYUNITYPLUGIN_H