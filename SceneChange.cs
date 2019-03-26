using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    public int stagenext;//進行度


    public memberStetus[] battlemember = new memberStetus[3];//戦闘に出すメンバー

    public int[] battlememberID = new int[3];//出撃しているメンバーをID管理

   

    int i;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

   

    
}
