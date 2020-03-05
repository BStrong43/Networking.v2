using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    Boid bucks;
    NetworkManager netMan;
    // Start is called before the first frame update
    void Start()
    {
        netMan = NetworkManager.Instance;
        bucks = GetComponent<Boid>();
        bucks.initBoidObjects();
    }

    // Update is called once per frame
    void Update()
    {

        Network.readMessages();
        string message = netMan.readMessage();
        while (message != "")
        {
            // see MessageParser.messageTypes
            if (message[0] == '0') //there's a boid position update
            {
                int length = int.Parse(message.Substring(1));
                if (length != bucks.bucks.Length)
                {
                    //heres where I would create more boids,  IF I COULD DO SO
                }
                netMan.intakeBucks(ref bucks.bucks);
            }
            else if (message[0] == '1') //this is where i would read moreBoidMessages, IF I RECIEVED THEM
            {
                int boidsToSpawn = NetTranslator.getNumSpawnBoids(message);
                bucks.moreBoids(boidsToSpawn);
            }
            message = netMan.readMessage();
        }
        if (netMan.isServer)
        {
            bucks.updateBoids(Time.deltaTime);
            netMan.sendOutBucks(ref bucks.bucks);
        }
        else
        {
            bucks.setPosition();
        }
    }
}
