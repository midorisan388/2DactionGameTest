using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemygenerater : MonoBehaviour
{

    public int enemycount, maxenemycount = 10;
    int r = 8;

    public int twaveclear_;//目標討伐数
    public int toubatu_;//現在討伐数
    [SerializeField] GameObject Enemyprefab;
    GameObject instansenemy;
    [SerializeField] Enemydate[] enemydt;



    void Update()
    {
        if (enemycount < maxenemycount && toubatu_ < twaveclear_)
        {
            instansenemy = Instantiate(Enemyprefab);
            instansenemy.GetComponent<EnemyStetasCntrole>().battle = this;
            int enemystID = Random.Range(0, enemydt.Length);
            instansenemy.GetComponent<EnemyStetasCntrole>().enemystetus = enemydt[enemystID];
            float randomx = Random.Range(transform.position.x - r, transform.position.x + r);
            float randomy = Random.Range(transform.position.y - r, transform.position.y + r);

            instansenemy.transform.position = new Vector3(randomx, randomy, 0);

            enemycount++;
        }
    }
}

