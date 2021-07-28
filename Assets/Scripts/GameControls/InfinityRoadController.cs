using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityRoadController : MonoBehaviour
{
    // Definition
    public GameObject[] roads;
    public Transform player;
    private List<GameObject> activeRoads = new List<GameObject>();
    float step = 0;
    public float roadLength = 50;
    public int startRoad = 2;

    // Start function
    private void Start()
    {
        for (int i = 0; i < startRoad; i++)
        {
            if (i==0)
            {
                clone_roads(0);
            }
            else
            {
                clone_roads(Random.Range(0, roads.Length));
            }
        }
    }

    // Update function
    private void Update()
    {
        if (player.position.z -55 > step - (startRoad * roadLength))
        {
            clone_roads(Random.Range(0, roads.Length));
            DeleteRoad();
        }
    }

    // Clone roads function
    void clone_roads(int roadIndex)
    {
        GameObject active = Instantiate(roads[roadIndex], transform.forward * step, Quaternion.identity);
        activeRoads.Add(active);
        step += roadLength;
    }
    void DeleteRoad()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}
