using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject[] objects;

    void Start()
    {
        int random = Random.Range(0, objects.Length);
        GameObject instance = Instantiate(objects[random], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
