using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopArea : MonoBehaviour {
    EnemyPopAreaController controller;

    private void Start() {
        controller = GetComponentInParent<EnemyPopAreaController>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    // エリア内にプレイヤーが入ったら敵が出現
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            controller.EnemyPop();
            Destroy(gameObject);
        }
    }
}
