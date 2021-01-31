using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private Text text;
    public SpriteRenderer spriteRenderer;
    public Sprite[] Perspectives;
    public Sprite[] Ships;    public float speed;
    private int onWater;
    private Vector3 scaleChange;
    private Vector3 positionChange;
    private bool mountAllowed;
    private GameObject collisionItem;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        mountAllowed = false;
        text.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            transform.position = transform.parent.transform.position;
        }
        
        if (transform.parent == null && mountAllowed)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                MountShip(collisionItem);
            }
        }

    }
    void FixedUpdate()
    {
        if (transform.parent == null)
        {
            Vector3 person = new Vector3(0.25f, 0.25f, 1f);
            Vector3 boat = new Vector3(0.3f, 0.3f, 1f);
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
            if (onWater == 0)
            {
                if (inputX > 0) {
                    if (inputY > 0) {
                        ChangePlayer(Perspectives[4]);
                    } else if (inputY < 0) {
                        ChangePlayer(Perspectives[5]);
                    } else {
                        ChangePlayer(Perspectives[2]);
                    }
                } else if (inputX < 0) {
                    if (inputY > 0) {
                        ChangePlayer(Perspectives[6]);
                    } else if (inputY < 0) {
                        ChangePlayer(Perspectives[7]);
                    } else {
                        ChangePlayer(Perspectives[1]);
                    }
                } else {
                    if (inputY > 0) {
                        ChangePlayer(Perspectives[3]);
                    } else if (inputY < 0) {
                        ChangePlayer(Perspectives[0]);
                    }
                }
                if (inputX != 0 || inputY != 0)
                {
                    transform.localScale = person;
                }
            }
            else if (onWater == 1)
            {
                if (inputX > 0) {
                    if (inputY > 0) {
                        ChangePlayer(Ships[4]);
                    } else if (inputY < 0) {
                        ChangePlayer(Ships[5]);
                    } else {
                        ChangePlayer(Ships[2]);
                    }
                } else if (inputX < 0) {
                    if (inputY > 0) {
                        ChangePlayer(Ships[6]);
                    } else if (inputY < 0) {
                        ChangePlayer(Ships[7]);
                    } else {
                        ChangePlayer(Ships[1]);
                    }
                } else {
                    if (inputY > 0) {
                        ChangePlayer(Ships[3]);
                    } else if (inputY < 0) {
                        ChangePlayer(Ships[0]);
                        boat = new Vector3(0.4f, 0.4f, 1f);
                    }
                }
                if (inputX != 0 || inputY != 0)
                {
                    transform.localScale = boat;
                }
            }
            float velocityX = 0;
            float velocityY = 0;
            if (inputY != 0 && inputX != 0) {
                velocityX = speed * Mathf.Sin(inputX * Mathf.PI / 4);
                velocityY = speed * Mathf.Sin(inputY * Mathf.PI / 4);
            }
            else {
                velocityX = speed * inputX;
                velocityY = speed * inputY;
            }
            body.velocity = new Vector2(velocityX, velocityY);
            
            // if(Input.GetKeyDown(KeyCode.Space) && mountAllowed)
            // {
            //     MountShip(collisionItem);
            // }
        }
    }
    void OnTriggerEnter2D(Collider2D hitObject)
    {
        if (hitObject.gameObject.tag == "Water")
        {
            onWater = 1;
        }
        else if (hitObject.gameObject.name.Equals("Ship"))
        {
            collisionItem = hitObject.gameObject;
            mountAllowed = true;
            text.gameObject.SetActive(true);
            text.text = "Press spacebar to get into Ship";
        }
    }
    void OnTriggerStay2D(Collider2D hitObject)
    {
        if (hitObject.gameObject.tag == "Water")
        {
            onWater = 1;
        }
        else if (hitObject.gameObject.name.Equals("Ship"))
        {
            collisionItem = hitObject.gameObject;
            mountAllowed = true;
        }
    }
    void OnTriggerExit2D(Collider2D hitObject)
    {
        if (hitObject.tag == "Water")
        {
            onWater = 0;
        }
        else if (hitObject.name.Equals("Ship"))
        {
            collisionItem = null;
            mountAllowed = false;
            text.gameObject.SetActive(false);
        }
    }
    void ChangePlayer(Sprite image)
    {
        spriteRenderer.sprite = image; 
    }
    void MountShip(GameObject ship)
    {
        mountAllowed = false;
        transform.position = ship.transform.position;
        transform.parent = ship.transform;
        GetComponent<SpriteRenderer>().sortingOrder = 0;
    }
}
