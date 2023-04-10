using UnityEngine;

public class Capture : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if(other.GetComponent<EnemyController>().Captured()){
                // �����������擾
                EnemyData enemyData = other.GetComponent<EnemyController>().enemyData;

                // �v���C���[�ɓ`����
                PlayerController p = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
                p.Capture(enemyData);

                Destroy(other.gameObject);
            }
        }
    }
}
