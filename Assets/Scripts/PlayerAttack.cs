using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    [SerializeField]
    [Tooltip("�e")]
    private GameObject bullet;
    [SerializeField]
    [Tooltip("�e�̍U����")]
    private int atk = 20;
    [SerializeField]
    [Tooltip("�e�̑���")]
    private float speed = 30f;
    [SerializeField]
    [Tooltip("�e��������܂ł̎���(�b)")]
    private float time = 1f;
    [SerializeField]
    [Tooltip("�e�̈ʒu(Z) ���@����ǂꂾ���O�ɏo����")]
    private float posZ = 0.25f;

    public void ShootBullet() {
        // �e�̈ʒu
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + posZ);
        // �e�̉�]�@�Ȃ�
        Quaternion rot = new Quaternion();
        // �e�𐶐�
        GameObject obj = Instantiate(bullet, pos, rot);
        // �e�̈ړ���
        Vector3 movement = new Vector3(0.0f, 0.0f, speed);

        // �e�̔���
        obj.GetComponent<Rigidbody>().AddForce(movement);
        obj.GetComponent<Bullet>().pow = atk;

        Destroy(obj, time);
    }

}
