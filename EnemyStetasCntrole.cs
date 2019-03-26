using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStetasCntrole : MonoBehaviour {

    GameObject player;
    PlayerController plystetatu;
    SkillEffect skilstetus;
  public  Enemygenerater battle;

    BattleManager manager;

    [SerializeField] Image HPbar;

    public Enemydate enemystetus;
 

    public bool atkflag;
    public int wait, waittimer;//待機時間、行動間隔
    int damagecount, damagetimer = 10;//ダメージ無効時間
    public struct EnemySt
    {
        public int HP;
        public float enemyspeed;
        public int pow;
        public int magic;
    }

    public EnemySt enemydt = new EnemySt();

    void Start()
    {
        player = GameObject.Find("player");
        manager = GameObject.Find("Master").GetComponent<BattleManager>();
        plystetatu = player.GetComponent<PlayerController>();
        //////////////////////////////////////////////////////////////////////////////
        enemydt.HP = enemystetus.HP;
        enemydt.pow = enemystetus.power;
        enemydt.magic = enemystetus.magic;
        enemydt.enemyspeed = enemystetus.speed;
        //////////////////////////////////////////////////////////////////////////////
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damagecount > damagetimer)
        {
            if (collision.tag == "Hitarea")
            {
                skilstetus = collision.gameObject.GetComponent<SkillEffect>();
                enemydt.HP -= skilstetus.damage;
                damagecount = 0;

            }
        }
      
    }


    void Update () {

       
        HPbar.fillAmount = ((float)enemydt.HP / (float)enemystetus.maxHP);
        if (damagecount <= damagetimer) damagecount++;
        if (enemydt.HP <= 0)
        {
            Destroy(this.gameObject);
            battle.enemycount--;
            battle.toubatu_++;
        }
      

        if (atkflag)
        {
            if (wait >= waittimer)
            {
                //攻撃可能

                manager.stetuas.HP -= (enemydt.pow - manager.stetuas.deff);
                wait = 0;
            }
            else
            {
                //行動待機
                wait++;
            }
        }
    }
}
