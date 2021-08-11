using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public Transform[] spawnPoints;

    public float maxSpawnDelayAtFront;
    private float curSpawnDelayAtFront;
    private float maxSpawnDelayAtSide;
    private float curSpawnDelayAtSide;

    public GameObject player;
    public Image[] lifeImage;
    public GameObject gameOverSet;

    

    private void Update()
    {
        curSpawnDelayAtFront += Time.deltaTime;
        curSpawnDelayAtSide += Time.deltaTime;


        
            if (curSpawnDelayAtSide > maxSpawnDelayAtSide)
            {
                SpawnEnemyAtSide();
                maxSpawnDelayAtSide = Random.Range(0.5f, 2f);
                curSpawnDelayAtSide = 0;
            }
            if (curSpawnDelayAtFront > maxSpawnDelayAtFront)
            {
                SpawnEnemyAtFront();
                curSpawnDelayAtFront = 0;
            }
        
         
        
    }
    private void SpawnEnemyAtFront()
    {

        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(0, 5);

        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();


        Vector2 playerPos = player.transform.position;
        Vector2 spawnPos = new Vector2(spawnPoints[ranPoint].position.x, spawnPoints[ranPoint].position.y);
        Vector2 temp = (playerPos - spawnPos).normalized;

        rigid.velocity = new Vector2(enemyLogic.speed * temp.x, enemyLogic.speed * temp.y);


    }

    private void SpawnEnemyAtSide()
    {
        
        int ranEnemy = Random.Range(0, 3);
        int ranPoint = Random.Range(5, 13);

        GameObject enemy = Instantiate(enemyObjs[ranEnemy], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
        Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
        Enemy enemyLogic = enemy.GetComponent<Enemy>();
        
        Vector2 playerPos = player.transform.position;
        Vector2 spawnPos = new Vector2(spawnPoints[ranPoint].position.x, spawnPoints[ranPoint].position.y);
        Vector2 temp = (playerPos - spawnPos).normalized;

        rigid.velocity = new Vector2(enemyLogic.speed * temp.x, enemyLogic.speed * temp.y);


    }

    public void UpdateLifeIcon(int life)
    {
        for(int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }

        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void gameOver()
    {
        gameOverSet.SetActive(true);
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 3.5f;
        player.SetActive(true); 
    }
}
