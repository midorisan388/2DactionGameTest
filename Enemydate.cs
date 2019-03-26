using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemydate : ScriptableObject {

    public string name;
    public int HP;
    public int maxHP;
    public int power;
    public int magic;
    public float speed = 1.0f;
    public Sprite enemyglaph;

    public ModelType type;
    public Autlibute autlibute;

    public enum ModelType
    {
        human,
        berst,
        fly,
        fish,
        gast,
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
}
