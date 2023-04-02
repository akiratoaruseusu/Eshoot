using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopArea : MonoBehaviour {
    [SerializeField]
    [Tooltip("敵")]
    private GameObject enemy;

    // エリア内にプレイヤーが入ったら敵が出現
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            EnemyPop();
        }
    }

    private void EnemyPop() {
        // 敵の位置
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // 敵の回転　なし
        Quaternion rot = new Quaternion();
        // 敵を生成
        GameObject obj = Instantiate(enemy, pos, rot);

        Destroy(this);
    }
}
