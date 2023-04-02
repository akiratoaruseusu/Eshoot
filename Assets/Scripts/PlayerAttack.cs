using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField]
    [Tooltip("’e")]
    private GameObject bullet;
    [SerializeField]
    [Tooltip("’e‚ÌUŒ‚—Í")]
    private int atk = 20;
    [SerializeField]
    [Tooltip("’e‚Ì‘¬‚³")]
    private float speed = 30f;
    [SerializeField]
    [Tooltip("’e‚ªÁ‚¦‚é‚Ü‚Å‚ÌŠÔ(•b)")]
    private float time = 1f;
    [SerializeField]
    [Tooltip("’e‚ÌˆÊ’u(Z) ©‹@‚©‚ç‚Ç‚ê‚¾‚¯‘O‚Éo‚·‚©")]
    private float posZ = 0.25f;

    public void ShootBullet() {
        // ’e‚ÌˆÊ’u
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + posZ);
        // ’e‚Ì‰ñ“]@‚È‚µ
        Quaternion rot = new Quaternion();
        // ’e‚ğ¶¬
        GameObject obj = Instantiate(bullet, pos, rot);
        // ’e‚ÌˆÚ“®—Í
        Vector3 movement = new Vector3(0.0f, 0.0f, speed);

        // ’e‚Ì”­Ë
        obj.GetComponent<Rigidbody>().AddForce(movement);
        obj.GetComponent<Bullet>().pow = atk;

        Destroy(obj, time);
    }

}
