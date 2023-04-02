using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("最大HP")]
    private int hpMax = 100;
    [SerializeField]
    [Tooltip("現在HP")]
    private int hp = 100;
    [SerializeField]
    [Tooltip("攻撃力")]
    private int atk = 5;
    [SerializeField]
    [Tooltip("移動速度")]
    private float moveSpeed = 10;

    [SerializeField]
    [Tooltip("ノックバック力")]
    private float knockbackForce = 1f;
    [SerializeField]
    [Tooltip("衝突時停止時間")]
    private float stopTime = 1f;

    [SerializeField]
    [Tooltip("HPバー")]
    private Slider hpBar;

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private bool isMovingForward;  // 前進フラグ
    private float forwardSpeed;  // 前進速度

    private float movementX;
    private float movementY;

    public delegate void StageClear();
    public static event StageClear OnStageClear;

    void Start() {
        // ステージマネージャーから移動開始を受け取る
        StageManager.OnPlayerMoveStart += OnPlayerMoveStart;

        // プレイヤーにアタッチされているRigidbodyを取得
        rb = gameObject.GetComponent<Rigidbody>();

        // プレイヤーの攻撃処理を取得
        playerAttack = gameObject.GetComponent<PlayerAttack>();

        // プレイヤーのHPをHPバーに反映
        HpUpdate();
    }

    private void OnAttack() {
        Debug.Log("OnAttack");
        playerAttack.ShootBullet();
    }

    private void OnCapture() {
        Debug.Log("OnCapture");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");
        if (other.CompareTag("Goal")) {
            OnStageClear?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("敵とぶつかった");
            // ノックバック方向の計算
            Vector3 knockbackDirection = Vector3.Reflect(transform.forward, other.contacts[0].normal);

            // ノックバック力の加算
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            StopMoving();
            Invoke("RestartMoving", stopTime);

        }
    }

    private void OnMove(InputValue movementValue) {
        // Moveアクションの入力値を取得
        Vector2 movementVector = movementValue.Get<Vector2>();

        // x,y軸方向の入力値を変数に代入
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // プレイヤー自動移動
    public void OnPlayerMoveStart() {
        Debug.Log("Start!!");
        RestartMoving();
    }

    // プレイヤー前進
    private void MoveForward() {
        if (isMovingForward) {
            // 一定速度で前進する
            Vector3 movement = new Vector3(0.0f, 0.0f, forwardSpeed);
            rb.AddForce(movement, ForceMode.VelocityChange);
        }
    }

    // 前進を停止する
    private void StopMoving() {
        isMovingForward = false;
        forwardSpeed = 0f;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // 前進を再開する
    private void RestartMoving() {
        isMovingForward = true;
        forwardSpeed = moveSpeed;
        MoveForward();
    }

    private void FixedUpdate() {
        // 入力値を元に3軸ベクトルを作成
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // rigidbodyのAddForceを使用してプレイヤーを動かす
        rb.AddForce(movement * moveSpeed);
    }

    // HPバーを更新
    public void HpUpdate(){
        hpBar.value = (float)hp / (float)hpMax;
    }
}
