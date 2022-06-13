using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms; // room LR = 0, room LRB = 1, room LRBT = 2, room LRT = 3


    private int direction;
    public float moveAmount = 10f;

    private float timeBtwRoom;
    public float startTimeBtwRoom = 0.25f;

    public float maxX;
    public float minX;
    public float minY;

    public bool stopGeneration;
    public bool playerSpawned = false;

    public LayerMask room;

    public int downCounter;

    void Start()
    {
        int randomStartPositions = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randomStartPositions].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
        stopGeneration = false;
    }

    void Update()
    {
        if(timeBtwRoom <= 0 && stopGeneration == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
        if(direction == 1 || direction == 2)
        {
            if(transform.position.x < maxX)//move right
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1,6);
                if(direction == 3)
                {
                    direction = 2;
                }
                else if(direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }
        
        else if(direction == 3 || direction == 4)//move left
        {
            if(transform.position.x > minX)
            {
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3,6);
            }
            else
            {
                direction = 5;
            }
            
        }
        else if(direction == 5)
        {
            downCounter++;
            if(transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if(roomDetection.GetComponent<RoomType>().type != 0 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if(downCounter >= 2)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[2], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        int randomBottonRoom = Random.Range(1,3);

                        Instantiate(rooms[randomBottonRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);
                
                direction = Random.Range(1,6);
            }
            else
            {
                stopGeneration = true;
            }
            
        }
    }
}
