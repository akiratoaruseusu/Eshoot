using UnityEngine;

public class Capture : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if(other.GetComponent<EnemyController>().Captured()){
                // 装備時情報を取得
                EnemyData enemyData = other.GetComponent<EnemyController>().enemyData;

                // プレイヤーに伝える
                PlayerController p = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
                p.Capture(enemyData);

                Destroy(other.gameObject);
            }
        }
    }
}
