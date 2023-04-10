using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopArea : MonoBehaviour {
    EnemyPopAreaController controller;

    private void Start() {
        controller = GetComponentInParent<EnemyPopAreaController>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    // �G���A���Ƀv���C���[����������G���o��
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            controller.EnemyPop();
            Destroy(gameObject);
        }
    }
}
