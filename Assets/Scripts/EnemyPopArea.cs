using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopArea : MonoBehaviour {
    [SerializeField]
    [Tooltip("�G")]
    private GameObject enemy;

    // �G���A���Ƀv���C���[����������G���o��
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            EnemyPop();
        }
    }

    private void EnemyPop() {
        // �G�̈ʒu
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // �G�̉�]�@�Ȃ�
        Quaternion rot = new Quaternion();
        // �G�𐶐�
        GameObject obj = Instantiate(enemy, pos, rot);

        Destroy(this);
    }
}
