using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class memberStetus : ScriptableObject {

    public string name;
    [Multiline]
    public string charadiscript;


    public string saportname;
    [Multiline] public string saportskilldiscription;

    //最大5まで
    public int atklv = 0;//攻撃レベル
    public int magiclv = 0;//魔法レベル
    public int skinshiplv = 0;//好感度

    public int MaxHP;
    public int HP;

    public int power;
    public int magic;
    public int deff;

    public float speed;

    public string[] hukidasiserihu = new string[5];

    public string[] skillname = new string[3];
    [Multiline] public string[] skilldiscription = new string[3];
    public Sprite[] skillIcon = new Sprite[3];


    public ModelType type;
    public Autlibute autlibute;

    public enum ModelType
    {
        human,
        berst,
       
    }

    public enum Autlibute
    {
        fire,
        ice,
        wind,
        erth,
        light,
        dark
    }
    //なんかanimation　public SpriteRenderer[] skillAnimation;
}
