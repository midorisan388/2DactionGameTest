using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharRendercontroller : MonoBehaviour
{

    // レイヤーの管理番号を取得
    int layerNo = 1 << 10;
    
    // マスクへの変換（ビットシフト）

    [SerializeField]
    GameObject player;
    [SerializeField] GameObject enemybody;
    PlayerController plycontrl;
    public GameObject target;//接触したスプライト
    public GameObject enemytag;
    public Vector3 pos;
    public float speed;
    public Character craramode;

    public enum Character
    {
        player,
        member,
        enemy,
        boss
    }

    
    void Start()
    {
        plycontrl = GetComponent<PlayerController>();
        pos = this.transform.position;
    }


    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "chara")
        {
            target = collision.gameObject;//接触相手
            if (this.transform.position.y <= target.transform.position.y)
            {
                pos.x = this.transform.position.x;
                pos.y = this.transform.position.y;
                pos.z = target.transform.position.z - 1;
            }
            else if (this.transform.position.y > target.transform.position.y)
            {
                pos.x = this.transform.position.x;
                pos.y = this.transform.position.y;
                pos.z = target.transform.position.z + 1;
            }

            if (pos.z <= -5) pos.z = -5;
            Changepos(pos);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "chara")
        {

            pos.x = this.transform.position.x;
            pos.y = this.transform.position.y;
            pos.z = 0;

            Changepos(pos);
        }
    }

    void Changepos(Vector3 newpos)
    {
        transform.position = newpos;
    }

}

