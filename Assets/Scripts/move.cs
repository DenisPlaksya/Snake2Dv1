using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class move : MonoBehaviour
{
    Vector2 vector = Vector2.up;
    Vector2 moveVector;
    public List<Transform> tail = new List<Transform>();
    public GameObject body;
    public float snakespeed = 0.3f;
    public bool notlose;
    public bool ate;
    public bool atebed;
    public bool horizontal=true;
    public bool vertical=false;

    // Start is called before the first frame update
    void Start()
    {
        notlose = false;
        atebed = false;
        ate = false;
        InvokeRepeating("Movement",1f, snakespeed);
    }
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.D) && horizontal)
        {
            vector = Vector2.right;
            vertical = true;
            horizontal = false;
        }
        else if (Input.GetKey(KeyCode.W) && vertical)
        {
            vector = Vector2.up;
            vertical = false;
            horizontal = true;
        }
        else if (Input.GetKey(KeyCode.S) && vertical)
        {
            vector = -Vector2.up;
            vertical = false;
            horizontal = true;
        }
        else if (Input.GetKey(KeyCode.A) && horizontal)
        {
            vector = -Vector2.right;
            vertical = true;
            horizontal = false;
        }
        moveVector = vector / 3f;
          
    }
    //Function for move
    void Movement()
    {
        //Save old position headsnake
        Vector2 pos = transform.position;
        //Save new position headsnake
        transform.Translate(moveVector);

        //Destroy last object in list
        if(atebed == true)
        {
            //Remove object from list
            if (tail.Count > 3)
            {
                Destroy(tail.Last().gameObject);
                tail.RemoveAt(tail.Count - 1);
            }
            else
            { 
                SceneManager.LoadScene("Level1");
            }
            atebed = false;
        }

        //Add new body
        if (ate == true)
        {
            //Instantiate new body
            GameObject g = (GameObject)Instantiate(body, pos, Quaternion.identity);
            // Keep track of it in our tail list
            tail.Insert(0, g.transform);
            ate = false;

        }
        // Move last Tail Element to where the Head was
        if (tail.Count > 0)
        {
            tail.Last().transform.position = pos;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collion with normal food
        if (collision.gameObject.tag == "NormalFood")
        {
            ate = true;
            Destroy(collision.gameObject);
        }
        //Collion with bed food
        if (collision.gameObject.tag == "BedFood")
        {
            atebed = true;
            Destroy(collision.gameObject);
        }
        //Collion with body 
        if (collision.gameObject.tag == "Playerbody")
        {
            if(notlose==false)
            {
                //Reload level 
                SceneManager.LoadScene("Level1");
            }
        }
        //Collion with boost food
        if (collision.gameObject.tag == "Boost")
        {
            StartCoroutine(boost());
            Destroy(collision.gameObject);
        }
    }

    //Coroutine for boost
    IEnumerator boost()
    {
        snakespeed = 0.1f;
        yield return new WaitForSeconds(5f);
        snakespeed = 0.3f;
    }
}
