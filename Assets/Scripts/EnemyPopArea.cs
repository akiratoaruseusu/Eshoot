using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopArea : MonoBehaviour {
    [SerializeField]
    [Tooltip("“G")]
    private GameObject enemy;

    // ƒGƒŠƒA“à‚ÉƒvƒŒƒCƒ„[‚ª“ü‚Á‚½‚ç“G‚ªoŒ»
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            EnemyPop();
        }
    }

    private void EnemyPop() {
        // “G‚ÌˆÊ’u
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        // “G‚Ì‰ñ“]@‚È‚µ
        Quaternion rot = new Quaternion();
        // “G‚ğ¶¬
        GameObject obj = Instantiate(enemy, pos, rot);

        Destroy(this);
    }
}
