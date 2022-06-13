using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public LayerMask whatIsRoom;
    public LevelGenerator levelGen;

    void Update()
    {
        Collider2D roomCheck = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if(roomCheck == null && levelGen.stopGeneration == true)
        {
            int rand = Random.Range(0, levelGen.rooms.Length);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
