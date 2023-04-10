using UnityEngine;

public class Capture : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy")) {
            if(other.GetComponent<EnemyController>().Captured()){
                // ‘•”õî•ñ‚ğæ“¾
                EnemyData enemyData = other.GetComponent<EnemyController>().enemyData;

                // ƒvƒŒƒCƒ„[‚É“`‚¦‚é
                PlayerController p = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
                p.Capture(enemyData);

                Destroy(other.gameObject);
            }
        }
    }
}
