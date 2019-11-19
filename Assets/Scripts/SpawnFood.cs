using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    private int VertMax;
    private int HorMax;
    public GameObject[] food;
    // Start is called before the first frame update
    void Start()
    {
        //Spawn food every 4 seconds
        InvokeRepeating("spawn", 3, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawn()
    {
        //Random spawn food
        VertMax = (int)Random.Range(5f,-5f);
        HorMax = (int)Random.Range(2.7f, -2.7f);
        // Random spawn food 
        int rand = Random.Range(0, food.Length);
        Instantiate(food[rand], new Vector2(HorMax,VertMax), Quaternion.identity);
    }
}
