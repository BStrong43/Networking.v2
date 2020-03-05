using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
public class NetworkManager : MonoBehaviour
{
    public enum netMode
    {
        DATA_PUSH,
        DATA_SHARING,
        DATA_COUPLED
    }
    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get{ return instance; }
    }
    public int numInstances = 5;
    public bool isServer = false;
    public netMode dataMode;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        Network.initNetwork(numInstances);
        DontDestroyOnLoad(this.gameObject);
    }

    public void initServer(int port, string username, netMode netMode = netMode.DATA_PUSH)
    {
        isServer = true;
        dataMode = netMode;
        Network.getNetworkInstance();
        Network.initServer(port, username);
    }
    
    public void initClient(string IP, int port, string username, netMode netMode = netMode.DATA_PUSH)
    {
        isServer = false;
        dataMode = netMode;
        Network.getNetworkInstance();
        Network.initClient(IP, port, username);
    }
    public string readMessage()
    {
        //Message should fit in here hopefully
        StringBuilder sb = new StringBuilder(256);
        
        //read message from packet
        if (Network.readMessage(sb, sb.Capacity) > 0)
            return sb.ToString();//return decoded message as string

        return ""; //Something happened so just return something
    }
    public void sendOutMessage(string msg)
    {
        Network.sendMessage(msg);
        Debug.Log("Seding Message");
    }
    public void sendOutBucks(ref Boid.data[] boids)
    {
        Network.sendBoidMessage(boids, boids.Length);
    }
    public int intakeBucks(ref Boid.data[] boids)
    {
        return Network.readBoidMessage(boids, boids.Length);
    }
}
