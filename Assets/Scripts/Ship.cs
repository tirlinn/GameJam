using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour
{
    [SerializeField] 
    private Text text;
    private GameObject player;
    public SpriteRenderer spriteRenderer;
    public Sprite[] Perspectives;
    public float speed;
    private int onShip;
    private Vector3 scaleChange;
    private Vector3 positionChange;
    Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        onShip = 0;
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.GetComponent<SpriteRenderer>().sortingOrder == 0)
        {
            text.gameObject.SetActive(true);
            text.text = "Press M to get out of the ship";
            onShip = 1;
            if(Input.GetKeyDown(KeyCode.M))
            {
                print("Here");
                DismountShip();
                text.gameObject.SetActive(false);
            }
        }
        if (onShip == 1)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputY = Input.GetAxisRaw("Vertical");
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
        }
    }
    void ChangePlayer(Sprite image)
    {
        spriteRenderer.sprite = image; 
    }
    void DismountShip()
    {
        player.transform.parent = null;
        player.GetComponent<SpriteRenderer>().sortingOrder = 5;
        onShip = 0;
    }
}
