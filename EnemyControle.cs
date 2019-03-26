using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControle : MonoBehaviour
{
    EnemyStetasCntrole enemyst;
    CharRendercontroller render;
    GameObject player;
    [SerializeField] GameObject enemy;
    public bool play_targetflag = false;
    public bool moving = true;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("player");
        enemyst = enemy.GetComponent<EnemyStetasCntrole>();
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {//探索範囲に入ったら狙ってくる
        if (collision.gameObject == player)
        {
            play_targetflag = true;
            enemyst.atkflag = true;
            moving = true;
        }

    }



    // Update is called once per frame
    void Update()
    {
        float distans = Mathf.Pow((Mathf.Pow(Mathf.Abs(transform.position.x - player.transform.position.x), 2.0f) + Mathf.Pow(Mathf.Abs(transform.position.y - player.transform.position.y), 2.0f)), 0.5f);
        if (distans <= 1) moving = false;
        else if (play_targetflag) moving = true;


        if (moving == true)
            enemy.transform.position = (Vector3.MoveTowards(enemy.transform.position, player.transform.position, enemyst.enemydt.enemyspeed * Time.deltaTime));
    }
}
