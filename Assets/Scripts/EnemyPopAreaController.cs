using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopAreaController : MonoBehaviour {
    [SerializeField]
    [Tooltip("敵")]
    private GameObject enemy;
    List<Vector3> posList = new List<Vector3>();

    private void Start() {
        posList.Clear();

        // PopPointの位置を保持する
        GameObject[] goList = GameObject.FindGameObjectsWithTag("PopPoint");
        foreach(GameObject go in goList) {
            posList.Add(go.transform.position);
            go.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void EnemyPop() {
        foreach (Vector3 v3 in posList) {
            // 敵を生成
            Instantiate(enemy, v3, new Quaternion());
        }
    }
}
