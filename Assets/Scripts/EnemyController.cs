using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour {
    public EnemyData enemyData;
    private int hpCurrent;

    // Start is called before the first frame update
    void Start(){
        hpCurrent = enemyData.maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter(Collider other) {
        // プレイヤーの通常攻撃に当たった
        if (other.CompareTag("PlayerBullet")) {
            Bullet bullet = other.GetComponent<Bullet>();
            Damage(bullet.pow);
        }
        // 捕獲ハンドに当たった
        if (other.CompareTag("Capture")) {
            //
        }
    }

    // ダメージ
    private void Damage(int damage){
        // 防御力を引いた分HPを減らす
        hpCurrent -= Math.Max(damage - enemyData.def, 0);
        Debug.Log("ENEMY HP:" + hpCurrent);
        if(0 >= hpCurrent) {
            Destroy(gameObject);
        }
    }

    // 捕獲判定
    public bool Captured() {
        return true;
    }
}
