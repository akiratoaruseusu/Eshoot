using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField]
    [Tooltip("弾")]
    private GameObject bullet;
    [SerializeField]
    [Tooltip("弾の速さ")]
    private float speed = 30f;
    [SerializeField]
    [Tooltip("弾が消えるまでの時間(秒)")]
    private float time = 1f;
    [SerializeField]
    [Tooltip("弾の位置(Z) 自機からどれだけ前に出すか")]
    private float posZ = 0.25f;

    public void ShootBullet() {
        // 弾の位置
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + posZ);
        // 弾の回転　なし
        Quaternion rot = new Quaternion();
        // 弾を生成
        GameObject obj = Instantiate(bullet, pos, rot);
        // 弾の移動力
        Vector3 movement = new Vector3(0.0f, 0.0f, speed);

        // 弾の発射
        obj.GetComponent<Rigidbody>().AddForce(movement);

        Destroy(obj, time);
    }

}
