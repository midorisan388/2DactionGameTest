using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waveline : MonoBehaviour {

    BattleManager manager;

    GameObject Player;
    [SerializeField] GameObject linewall;
    public int waveID;

	// Use this for initialization
	void Start () {
        manager = GameObject.Find("Master").GetComponent<BattleManager>();
        Player = GameObject.Find("player");
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject == Player)
        {
            if (manager.Wave == -1)
            {
                manager.Wave = waveID;
                manager.WaveSet(waveID);

                manager.waveclear = false;
                Destroy(this.gameObject);
            }
            else if (manager.Wave >= 0 && manager.waveclear)
            {
                manager.Wave = waveID;
                manager.WaveSet(waveID);

                manager.waveclear = false;
                Destroy(this.gameObject);
            }
        }
    }
   
}
