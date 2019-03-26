using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public Animator plyanim;

    BattleManager manager;
    CharRendercontroller render;
    public  bool maekar_ = false;//マーカーが既にあるか

    public int vectorID = 0;//向きのID
     int vector_ = 0;//前後のスプライトID
     int reverse_sprite = 0;//反転させるか
     GameObject PlayerSprite;//表示しているスプライト
    public GameObject[] Sprite_=new GameObject[2];//前と後ろ向きのスプライトオブジェクト

   public float anim_mov = 1.0f;
    public float anim_atk;

    public bool fieldclick = false;
    bool outfield;
    public GameObject maekar = null;

    public Vector3 selectpos;//行先
    Vector3 basePosition;//現在地
    Vector3 markpos = new Vector3(0, 8.0f, 0);

    public GameObject targetenemy_ = null;
    [SerializeField] GameObject enemytag;
    bool target = false;

    [SerializeField] GameObject pointeffect;//移動先ポイントプレハブ

    public bool moving = true;

    void Start()
    {
        manager = GameObject.Find("Master").GetComponent<BattleManager>();
        render = GetComponent<CharRendercontroller>();
        selectpos = transform.position;
    }

   
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag== "Field")
        {
            moving = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        Mousepoint();
        
    }


    void Mousepoint()
    {
        if (maekar_ == true)//マーカー再設定
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(maekar);
                maekar_ = false;
            }
        }
        //左クリックで移動先登録
        if (Input.GetMouseButtonDown(0))
        {
            selectpos.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            selectpos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

            anim_mov = Mathf.Pow((Mathf.Pow(Mathf.Abs(basePosition.x - selectpos.x), 2.0f) + Mathf.Pow(Mathf.Abs(basePosition.y - selectpos.y), 2.0f)), 0.5f) * 2;

            PlayerVectorSet(selectpos);
            ////////////////////////////////////////////////////////////////////////////////////////////////
            if (RayTest())
            {
               

                

                if (maekar_ == false)
                {
                    maekar = Instantiate(pointeffect);
                    maekar.transform.position = selectpos;

                    maekar_ = true;
                }

                basePosition.x = this.transform.position.x;
                basePosition.y = this.transform.position.y;
                plyanim.SetFloat("Moving", anim_mov);

                moving = true;
            }
        }
            if (moving == true)
                Moving(selectpos);
            else plyanim.SetFloat("Moving", 0);



    }

    void Moving(Vector3 point)//移動
    {
        if (maekar_)
        {
            if (maekar.transform.position == transform.position)
            {
                Destroy(maekar, 0.8f);
                fieldclick = false;
                moving = false;
            }
        }
        if (outfield == false)
        {
            transform.position = (Vector3.MoveTowards(transform.position, point, manager.stetuas.speed * Time.deltaTime));
            anim_mov -= 0.1f;
            plyanim.SetFloat("Moving", anim_mov);
        }
    }

   public void PlayerVectorSet(Vector3 tagpos)
    {
        //キャラの向きを設定
        if (tagpos.y > transform.position.y)
        {
            vector_ = 1;//前か右を向いている
            vectorID = 3;//前
        }
        else
        {
            vector_ = 0;//後ろか左を向いている
            vectorID = 1;//➡
        }
        if (tagpos.x < transform.position.x)
        {
            //反転させる
            vectorID--;
            reverse_sprite = 1;
        }
        else
        {
            reverse_sprite = 0;
        }
        PlayerSprite = Sprite_[vector_];
        for (int i = 0; i < 2; i++)
        {
            Sprite_[i].SetActive(false);
        }
        PlayerSprite.SetActive(true);
        plyanim = PlayerSprite.GetComponent<Animator>();
        PlayerSprite.transform.rotation = Quaternion.Euler(0, 180 * reverse_sprite, 0);
    }

   bool  RayTest()
    {
        //Rayの飛ばせる距離
        int distance = 100;

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(Input.mousePosition, new Vector3(0, 0, -1));
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, distance);

         Debug.DrawLine(ray.origin, ray.direction * distance, Color.red);

        //もしRayにオブジェクトが衝突したら
        if (hit.collider)
        {
            if (hit.collider.tag == "chara")//ターゲット選択
            {
                if (hit.collider.gameObject.GetComponent<CharRendercontroller>().craramode == CharRendercontroller.Character.enemy)
                {
                    if (render.enemytag != hit.collider.gameObject)//ターゲットを切り替える
                    {
                        if (target == true)
                        {
                            Destroy(targetenemy_);
                        }

                        targetenemy_ = Instantiate(enemytag);
                        targetenemy_.transform.parent = hit.collider.gameObject.transform;
                        targetenemy_.transform.localPosition = markpos;

                        render.enemytag = hit.collider.gameObject;
                        target = true;


                        selectpos = render.enemytag.transform.position;
                    }
                    fieldclick = true;
                    moving = true;
                }
                else
                {
                    //ターゲットを解除する
                    Destroy(targetenemy_.gameObject);
                    target = true;


                    selectpos = new Vector3(0, 0, 0);

                    fieldclick = true;
                    moving = true;

                }
            }

            //移動
            if (hit.collider.tag == "Field")
            {
                outfield = false;
                fieldclick = true;
            }
            else
            {
                outfield = true;
                fieldclick = false;
            }
        }

        return fieldclick;
    }


}
