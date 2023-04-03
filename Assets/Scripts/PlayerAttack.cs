using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class PlayerAttack : MonoBehaviour {
    [SerializeField]
    [Tooltip("弾")]
    private GameObject bullet;
    [SerializeField]
    [Tooltip("弾の攻撃力")]
    private int atk = 20;
    [SerializeField]
    [Tooltip("弾の速さ")]
    private float speed = 30f;
    [SerializeField]
    [Tooltip("弾の発射間隔(秒)")]
    private float delayTime = 0.5f;
    [SerializeField]
    [Tooltip("弾が消えるまでの時間(秒)")]
    private float lifeTime = 1f;
    [SerializeField]
    [Tooltip("弾の位置(Z) 自機からどれだけ前に出すか")]
    private float posZ = 0.25f;

    private bool isShooting = false;    // 発射中(長押しの間)
    private bool isFire = false;        // 発射
    private Timer timer;

    private void Start() {
        // タイマーを初期化
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

        // ボタン押下開始したら発射中状態にする
        if (isShooting) {
            // タイマー停止していれば最初の1発を発射
            if(!timer.Enabled){
                isFire = true;
            }
            timer.Enabled = true;
        }
    }

    private void OnTimerElapsed(object sender, ElapsedEventArgs e) {
        // 発射時間になったら弾生成
        if (isShooting){
            isFire = true;
            MakeBullet();
        }else{
            // タイマー経過までにもう長押しを止めていたら停止する
            timer.Stop();
        }
    }

    private void MakeBullet() {
        // 弾の位置
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + posZ);
        // 弾の回転　なし
        Quaternion rot = new Quaternion();
        // 弾オブジェクトを生成
        GameObject obj = Instantiate(bullet, pos, rot);
        // 弾の移動力
        Vector3 movement = new Vector3(0.0f, 0.0f, speed);

        // 弾の発射
        obj.GetComponent<Rigidbody>().AddForce(movement);
        obj.GetComponent<Bullet>().pow = atk;

        Destroy(obj, lifeTime);
    }
}
