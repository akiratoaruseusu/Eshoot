using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("プレイヤーの最大HP")]
    private int hpMax = 100;
    [SerializeField]
    [Tooltip("プレイヤーの現在HP")]
    private int hp = 100;
    [SerializeField]
    [Tooltip("プレイヤーの攻撃力")]
    private int atk = 5;
    [SerializeField]
    [Tooltip("プレイヤーの移動速度")]
    private float speed = 10;

    [SerializeField]
    [Tooltip("プレイヤーのHPバー")]
    private Slider hpBar;

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private float movementX;
    private float movementY;

    public delegate void StageClear();
    public static event StageClear OnStageClear;

    void Start() {
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

    private void OnMove(InputValue movementValue) {
        // Moveアクションの入力値を取得
        Vector2 movementVector = movementValue.Get<Vector2>();

        // x,y軸方向の入力値を変数に代入
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() {
        // 入力値を元に3軸ベクトルを作成
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // rigidbodyのAddForceを使用してプレイヤーを動かす
        rb.AddForce(movement * speed);
    }

    // HPバーを更新
    public void HpUpdate(){
        hpBar.value = (float)hp / (float)hpMax;
    }
}
