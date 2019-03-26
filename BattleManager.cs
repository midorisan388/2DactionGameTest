using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    PlayerController plycntrl;
  public  Enemygenerater genel;
    public memberStetus plyst;
    [SerializeField] Image HPbar;

    [SerializeField] GameObject Bossend = null;//クリアフラグを持つボスオブジェクト
    public struct Playerstetus
    {
        public int HP;
        public int pow;
        public int magic;
        public int deff;
        public float speed;
    }

    public Playerstetus stetuas = new Playerstetus();

    [SerializeField] GameObject Player;

    public int hukidasicount = 0, hukidasitime = 50;

    public int Wave = 0;//進行度
    public int totalcount = 0;
    public int wavecount = 0;//ウェーブごとの討伐数
    public bool waveclear = false;

    bool clear = false;
    bool lose = false;

    public GameObject[] Wavegenerate;
    [SerializeField] Text total;//総討伐数表示 
    [SerializeField] Text Wave_total;//ウェーブ討伐数表示



    [SerializeField] Text charprott;//吹き出しのセリフ
    [SerializeField] GameObject hukidasi;//吹き出しオブジェクト

    // Use this for initialization
    void Start()
    {
        plycntrl = Player.GetComponent<PlayerController>();

        stetuas.HP = plyst.HP;
        stetuas.pow = plyst.power;
        stetuas.magic = plyst.magic;
        stetuas.deff = plyst.deff;
        stetuas.speed = plyst.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (stetuas.HP <= 0) SceneManager.LoadSceneAsync(0);

        HPbar.fillAmount = (float)stetuas.HP / (float)plyst.MaxHP;
        if (Wave >= 0)
        {
            wavecount = genel.toubatu_;

            Wave_total.text = "討伐数: " + wavecount + "/" + genel.twaveclear_;
            if (wavecount >= genel.twaveclear_) waveclear = true;

            total.text = "TOTAL: " + totalcount;
        }
        if (Bossend == null)
        {
            clear = true;
        }

        if (clear)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadSceneAsync(0);
            }
        }
        Hukidasi();//吹き出し更新
    }

    private void OnGUI()
    {
        if (clear)
        {
            GUI.Label(new Rect(0, 300, 800, 500), "CLEAR!! >>space:Restart");
        }
    }


    public void WaveSet(int wave)
    {
        Wavegenerate[wave].SetActive(true);
        genel = Wavegenerate[wave].GetComponent<Enemygenerater>();
        if (wave > 0)
        {
            totalcount += Wavegenerate[wave - 1].GetComponent<Enemygenerater>().toubatu_;
            Destroy(Wavegenerate[wave - 1]);
        }
    }

    void Hukidasi()
    {
        if (hukidasicount >= hukidasitime)
        {
            hukidasi.SetActive(true);
            if (hukidasicount >= 100)
            {
                hukidasi.SetActive(false);
                hukidasicount = 0;
            }
            else hukidasicount++;
        }
        else
        {
            hukidasi.SetActive(false);
            int randomID = Random.Range(0, 5);
            charprott.text = plyst.hukidasiserihu[randomID];
            hukidasicount++;
        }
    }
}
