using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field : MonoBehaviour
{
    public Transform snake;
    public bool portal;
    // Start is called before the first frame update
    void Start()
    {
        portal = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(portal==true)
        {
           snake.transform.position = - new Vector2(snake.position.x,snake.position.y);
           portal = false;
        }
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            portal = true;
        }
    }
}
