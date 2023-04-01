using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject {
    // ��{�X�e�[�^�X
    public string enemyName;
    public int maxHp;
    public int atk;
    public int def;
    public int exp;

    // �������X�e�[�^�X
    public float eqTime;
}