using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour {
    public EnemyData enemyData;
    private int hp;

    // Start is called before the first frame update
    void Start(){
        hp = enemyData.maxHp;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // �v���C���[�̒ʏ�U���ɓ�������
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("PlayerBullet")) {
            Bullet bullet = other.GetComponent<Bullet>();
            Damage(bullet.pow);
        }
    }

    // �_���[�W
    private�@void Damage(int damage){
        // �h��͂���������HP�����炷
        hp -= Math.Max(damage - enemyData.def, 0);
        Debug.Log("ENEMY HP:" + hp);
        if(0 >= hp){
            Destroy(gameObject);
        }
    }
}
