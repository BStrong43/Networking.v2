using UnityEngine;

public class SharingBoidManager : MonoBehaviour
{
    Boid buck;
    Boid remoteBuck;
    public GameObject remoteBuckFile;
    NetworkManager netMan;
    
    void Start()
    {
        netMan = NetworkManager.Instance;
        buck = GetComponent<Boid>();
        remoteBuck = gameObject.AddComponent<Boid>();
        remoteBuck.buck = remoteBuckFile;
        if (!netMan.isServer)
        {
            buck.initBoidObjects();
            remoteBuck.initBoidObjects();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //get networking
        if (netMan.isServer)
        {
            int netResult = Network.serverMessages();
            if (netResult == 0)
            {
                Debug.Log("No networking done");
            }  //Something went wrong so gtfo
            return;
        }

        //Nothing went wrong, carry on
        Network.readMessages();
        string message = netMan.readMessage();
        while (message != "") //Why process empty message?
        {
            Debug.Log("Reading Messages");
            // see MessageParser.messageTypes
            if (message[0] == '0') //there's a boid position update
            {
                Debug.Log("Reading Other Boids");
                int length = int.Parse(message.Substring(1));
                if (length != remoteBuck.bucks.Length)
                {
                   //Heres where I would allow more boids IF I HAD MORE CLIENTS
                }
                netMan.intakeBucks(ref remoteBuck.bucks);
            }
            else if (message[0] == '1')
            {
                // spawn boids message;
                int boidsToSpawn = NetTranslator.getNumSpawnBoids(message);
                remoteBuck.moreBoids(boidsToSpawn);
            }
            message = netMan.readMessage();
        }
        if (!netMan.isServer)
        {
            buck.updateBoids(Time.deltaTime);
            netMan.sendOutBucks(ref buck.bucks);
            remoteBuck.setPosition();
        }
    }
}
