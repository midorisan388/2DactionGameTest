using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject[] skillparticles;


    public GameObject popupeffect = null;
 public   Vector3 targetvectpe;//向きによって近距離攻撃の発生位置を変更
    public Vector3 effectpoint;
    public Vector3 baseposition;

    PlayerController plycntrl;
    SkillEffect skillstetus;
    CharRendercontroller render;

    public bool skillpop = false;
    int damage = 100;
    int wait = 10, waitimer;

    private void Start()
    {
        render = GetComponent<CharRendercontroller>();
        plycntrl = GetComponent<PlayerController>();
    }

    private void Update()
    {
        plycntrl.anim_atk -= 0.1f;
        plycntrl.plyanim.SetFloat("Attaking", plycntrl.anim_atk);
        if (wait >= waitimer)
        {
            //通常攻撃 ものによりけり

            if (skillpop == false)
            {
                //スキル1 遠距離攻撃
                if (Input.GetKeyDown(KeyCode.X))
                {
                    damage = 100;
                    ParticleInstance(0);

                }
                //スキル2 近距離攻撃
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    damage = 240;
                    ParticleInstance(1);

                }
            }
        }
        else
        {
          wait++;
        }
    }

    void ParticleInstance(int skillID)
    {
        

        if (render.enemytag != null)//ターゲットしている
        {
            effectpoint = render.enemytag.transform.position;//到達地点
            effectpoint.z = 5;
            if (render.enemytag.transform.position.x > transform.position.x)
            { //前向き               
                targetvectpe = render.enemytag.transform.position;
                if (render.transform.position.y > transform.position.y)
                {
                    baseposition = new Vector3(transform.position.x + 1, transform.position.y + 1, 5);
                }
                else
                {//右向き
                    baseposition = new Vector3(transform.position.x + 1, transform.position.y - 1, 5);
                }
            }
            else
            {
                //後ろ
                if (render.transform.position.y > transform.position.y)
                {
                    baseposition = new Vector3(transform.position.x - 1, transform.position.y - 1, 5);
                }
                else
                { //左向き
                    baseposition = new Vector3(transform.position.x - 1, transform.position.y + 1, 5);
                }
            }
            //生成
            SkillPop(baseposition, effectpoint, render.enemytag, skillID,damage);
            //向き調整 ターゲットのほうを向く
            plycntrl.PlayerVectorSet(render.enemytag.transform.position);

        }
        else
        {


            //近距離攻撃の発射到達地点

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            //ターゲットしていない
          //const使います
            if (plycntrl.vectorID == 3)
            { //前向き               
                targetvectpe = new Vector3(transform.position.x + 3, transform.position.y + 3, 5);
                baseposition = new Vector3(targetvectpe.x - 2, targetvectpe.y - 2, 5);
            }
            if (plycntrl.vectorID == 1)
            {//右向き
                targetvectpe = new Vector3(transform.position.x + 3, transform.position.y - 3, 5);
                baseposition = new Vector3(targetvectpe.x - 2, targetvectpe.y + 2, 5);
            }
            if (plycntrl.vectorID == 0)
            { //後ろ向き               
                targetvectpe = new Vector3(transform.position.x - 3, transform.position.y - 3, 5);
                baseposition = new Vector3(targetvectpe.x + 2, targetvectpe.y + 2, 5);
            }
            if (plycntrl.vectorID == 2)
            {//左向き
                targetvectpe = new Vector3(transform.position.x - 3, transform.position.y + 3, 5);
                baseposition = new Vector3(targetvectpe.x + 2, targetvectpe.y - 2, 5);
            }

            SkillPop(baseposition, targetvectpe, null, skillID,damage);
        }

        plycntrl.plyanim.SetFloat("Attaking",plycntrl.anim_atk);


        wait = 0;
        skillpop = true;
    }

    void SkillPop(Vector3 skillstart,Vector3 skillendpos,GameObject target,int skillID_,int damage_)
    {

        popupeffect = Instantiate(skillparticles[skillID_]);
        popupeffect.transform.position = skillstart;//始点  

        skillstetus = popupeffect.GetComponent<SkillEffect>();

        if (target != null)
            skillstetus.target = target;

        skillstetus.targetpos = skillendpos;//終点


        skillstetus = popupeffect.GetComponent<SkillEffect>();
        skillstetus.damage = damage;
        skillstetus.plyskill = this;

        plycntrl.plyanim.SetFloat("Moving", 0);
        plycntrl.moving = false;
        plycntrl.anim_atk = 1.0f;
    }
}
