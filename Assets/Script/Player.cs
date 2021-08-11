using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float maxShotDelay;
    public float curShotDelay;

    public int life;

    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;

    public float unBeatTime;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameManager manager;

    SpriteRenderer spriteRenderer;
    Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchRight && h == 1) || (isTouchLeft && h == -1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;
        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
    }

    private void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;

        if (curShotDelay < maxShotDelay)
            return;


        GameObject bulletCC = Instantiate(bulletObjB, transform.position + Vector3.forward * 1f, transform.rotation);
        GameObject bulletRR = Instantiate(bulletObjA, transform.position + Vector3.right * 0.5f, transform.rotation);
        GameObject bulletLL = Instantiate(bulletObjA, transform.position + Vector3.left * 0.5f, transform.rotation);
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
        rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
        rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);



        curShotDelay = 0;
    }
    private void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }

        else if (collision.gameObject.tag == "Enemy")
        {
            life--;
            manager.UpdateLifeIcon(life);

            if (life == 0)
                manager.gameOver();
            
            else
                manager.RespawnPlayer();


            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }

    public void UnBeatTime()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        for (int i = 0; i < unBeatTime * 10; ++i)
        {
            if (i % 2 == 0)
                spriteRenderer.color = new Color(1,1,1,0.4f);
            else
                spriteRenderer.color = new Color(1,1,1,1);
        }

        //Alpha Effect End
        spriteRenderer.color = new Color(1, 1, 1, 1);

        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }
}
