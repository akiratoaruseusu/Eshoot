using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

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
    [Tooltip("�e�̔��ˊԊu(�b)")]
    private float delayTime = 0.5f;
    [SerializeField]
    [Tooltip("�e��������܂ł̎���(�b)")]
    private float lifeTime = 1f;
    [SerializeField]
    [Tooltip("�e�̈ʒu(Z) ���@����ǂꂾ���O�ɏo����")]
    private float posZ = 0.25f;

    private bool isShooting = false;    // ���˒�(�������̊�)
    private bool isFire = false;        // ����
    private Timer timer;

    private void Start() {
        // �^�C�}�[��������
        timer = new Timer(delayTime * 1000);
        timer.Elapsed += OnTimerElapsed;
    }

    private void Update() {
        if (isFire) {
            MakeBullet();
            isFire = false;
        }
    }

    public void ShootBullet(bool _isShooting) {
        isShooting = _isShooting;
        Debug.Log("isShooting:"+isShooting);

        // �{�^�������J�n�����甭�˒���Ԃɂ���
        if (isShooting) {
            // �^�C�}�[��~���Ă���΍ŏ���1���𔭎�
            if(!timer.Enabled){
                isFire = true;
            }
            timer.Enabled = true;
        }
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e) {
        // ���ˎ��ԂɂȂ�����e����
        if (isShooting){
            isFire = true;
            MakeBullet();
        }else{
            // �^�C�}�[�o�߂܂łɂ������������~�߂Ă������~����
            timer.Stop();
        }
    }

    private void MakeBullet() {
        // �e�̈ʒu
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + posZ);
        // �e�̉�]�@�Ȃ�
        Quaternion rot = new Quaternion();
        // �e�I�u�W�F�N�g�𐶐�
        GameObject obj = Instantiate(bullet, pos, rot);
        // �e�̈ړ���
        Vector3 movement = new Vector3(0.0f, 0.0f, speed);

        // �e�̔���
        obj.GetComponent<Rigidbody>().AddForce(movement);
        obj.GetComponent<Bullet>().pow = atk;

        Destroy(obj, lifeTime);
    }
}
