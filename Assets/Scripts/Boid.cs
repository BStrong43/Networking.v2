using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;



public class Boid: MonoBehaviour
{
    [StructLayout(LayoutKind.Sequential)]
    public struct data  //Struct acts as wrapper between A3 and Unity
    {
        public Network.vec3 position;
        public Network.vec3 velocity;
    }
    public struct behavior
    {
        public float radius;
        public float separation;
        public float alignment;
        public float cohesion;
        public float forward;
        public float maxForce;
        public behavior(float Radius, float Separation, float Alignment, float Cohesion, float Forward, float MaxForce)
        {
            radius = Radius;
            separation = Separation;
            alignment = Alignment;
            cohesion = Cohesion;
            forward = Forward;
            maxForce = MaxForce;
        }
    }
    
    //Flocking Data
    float radius = .25f;
    float maxSpeed = 3f;
    public GameObject buck;
    int MAX_BUCKS = 4;

    public data[] bucks;
    behavior[] buckBehavior;
    public GameObject[] gameObjects;

    public void updateBoids(float dt)
    {
        checkCollision();
        screenWrap();
        simulate(dt);
    }
    public void updateBoids(float dt, Boid with)
    {
        checkCollision(with);
        screenWrap();
        simulate(dt);
    }


    public void moreBoids(int newLength)
    {
        
        foreach(GameObject go in gameObjects)
        {
            go.SetActive(false);
        }
        MAX_BUCKS = newLength;
        initBoidObjects();
    }

    public void initBoidObjects()
    {
        bucks = new data[MAX_BUCKS];
        buckBehavior = new behavior[MAX_BUCKS];
        gameObjects = new GameObject[MAX_BUCKS];

        Network.vec3 position = new Network.vec3(-2,0,0);
        Network.vec3 velocity = new Network.vec3(5f, 1,1);

        for (int i=0; i < MAX_BUCKS; i++)
        {
            gameObjects[i] = Instantiate(buck);
            bucks[i].position = position;
            position.x+=2;
            bucks[i].velocity = velocity;
        }
    }

    public void checkCollision()
    {
        //Shout out to game physics
        for (int i = 0; i < MAX_BUCKS; i++)
        {
            data currentBoid = bucks[i];
            Vector3 position = bucks[i].position.toVector3();

            // get all other boids data
            for (int j = 0; j < MAX_BUCKS; j++)
            {
                if (i == j) //skip self
                    j++;
                if (j >= MAX_BUCKS)
                    break;
                Vector3 oPosition = bucks[j].position.toVector3();
                Vector3 diff = (position-oPosition);
                if(diff.sqrMagnitude <= 4 * radius*radius)
                {
                    Vector3 newVel =Vector3.Reflect(bucks[i].velocity.toVector3(), diff);
                    bucks[i].velocity = new Network.vec3(newVel.normalized*maxSpeed);
                }
            }
        }
    }
    public void checkCollision(Boid with)
    {
        //Shout out to game physics
        for (int i = 0; i < MAX_BUCKS; i++)
        {
            data currentBoid = bucks[i];
            Vector3 position = bucks[i].position.toVector3();

            for (int j = 0; j < MAX_BUCKS; j++)
            {
                if (i == j)
                    j++;//Dont collide with yourself
                if (j >= MAX_BUCKS)
                    break;
                Vector3 oPosition = bucks[j].position.toVector3();
                Vector3 diff = (position - oPosition);
                if (diff.sqrMagnitude <= 4 * radius * radius)
                {
                    Vector3 newVel = Vector3.Reflect(bucks[i].velocity.toVector3(), diff);
                    bucks[i].velocity = new Network.vec3(newVel.normalized * maxSpeed);
                }
            }
        }
        for (int i = 0; i < MAX_BUCKS; i++)
        {
            data currentBoid = bucks[i];
            Vector3 position = bucks[i].position.toVector3();
            
            for (int c = 0; c < MAX_BUCKS; c++)
            {
                if (c >= with.getNumBoids())
                    break;
                Vector3 oPosition = with.bucks[c].position.toVector3();
                Vector3 diff = (position - oPosition);
                if (diff.sqrMagnitude <= 4 * radius * radius)
                {
                    Vector3 newVel = Vector3.Reflect(bucks[i].velocity.toVector3(), diff);
                    bucks[i].velocity = new Network.vec3(newVel.normalized * maxSpeed);
                }
            }
        }
    }
    public void screenWrap()
    {
        float border = 50f;
        for(int i = 0; i<MAX_BUCKS; i++)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(bucks[i].position.toVector3());
            if(screenPos.x > Screen.width-border)
            {
                bucks[i].velocity.x = -bucks[i].velocity.x;
            }
            else if (screenPos.x < 0f+border) {
                bucks[i].velocity.x = -bucks[i].velocity.x;
            }
            if(screenPos.y > Screen.height-border)
            {
                bucks[i].velocity.y = -bucks[i].velocity.y;
            }
            else if (screenPos.y < 0f+border)
            {
                bucks[i].velocity.y = -bucks[i].velocity.y;
            }
            if(screenPos.z > 25)
            {
                bucks[i].velocity.z = -bucks[i].velocity.z;
            }
            if(screenPos.z < 15)
            {
                bucks[i].velocity.z = -bucks[i].velocity.z;
            }
            bucks[i].position = new Network.vec3(Camera.main.ScreenToWorldPoint(screenPos));
        }
    }
    public void simulate(float dt)
    {
        for(int i =0; i<MAX_BUCKS; i++)
        {
            bucks[i].position += bucks[i].velocity * dt;
            gameObjects[i].transform.position = bucks[i].position.toVector3();
        }
    }
    public void setPosition()
    {
        for (int i = 0; i < MAX_BUCKS; i++)
        {
            gameObjects[i].transform.position = bucks[i].position.toVector3();
        }
    }

    public int getNumBoids()
    {
        return MAX_BUCKS;
    }
}
