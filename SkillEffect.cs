using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour {

    public int damage;
    public float speed;
    public GameObject target;
   public  PlayerSkill plyskill;
    EnemyControle enemycntrol;
    PlayerController plycntrl;
    public Vector3 targetpos;
    public int time = 0;//経過タイマー
    public int exitime = 20;//最大継続時間

    private void Start()
    {
        plycntrl = GameObject.Find("player").GetComponent<PlayerController>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "chara")
        {
            if (collision.gameObject.GetComponent<CharRendercontroller>().craramode == CharRendercontroller.Character.enemy)
            {

                enemycntrol = collision.gameObject.transform.FindChild("Enemybody").GetComponent<EnemyControle>();
                enemycntrol.play_targetflag = true;
                enemycntrol.moving = true;


                Destroy(this.gameObject, 0.5f);
                plyskill.skillpop = false;
            }

        }
    }


    private void Update()
    {
        if (time <= exitime)
        {
            transform.position = (Vector3.MoveTowards(transform.position, targetpos, speed * Time.deltaTime));
            time++;
        }
        else
        {
            Destroy(this.gameObject);
            plyskill.skillpop = false;
        }
       
    }

}
