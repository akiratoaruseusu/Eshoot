using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject {
    // 基本ステータス
    public string enemyName;
    public int maxHp;
    public int atk;
    public int def;
    public int exp;

    // 装備時ステータス
    public float eqTime;
}