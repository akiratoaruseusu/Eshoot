using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("最大HP")]
    private int hpMax = 100;
    [SerializeField]
    [Tooltip("現在HP")]
    private int hpCurrent = 100;
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
    [Tooltip("捕獲ボタンクールダウン(秒)")]
    private float capCoolTime = 1f;

    [SerializeField]
    [Tooltip("UI")]
    private GameObject uiObj;
    [SerializeField]
    [Tooltip("HPバー")]
    private Slider hpBar;
    [SerializeField]
    [Tooltip("装備時間バー")]
    private Slider eqTimeBar;

    private float eqTime = -1f;       // 装備時間
    private float eqTimeRemaining;    // 残り装備時間
    private float capCoolTimeRemaining = -1f; // 残り捕獲クールダウン時間

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private bool isMovingForward;  // 前進フラグ
    private float forwardSpeed;  // 前進速度

    private Gamepad gamepad;
    private bool isPressedA = false;

    private UIController uiCon;

    // イベント
    public delegate void StageClear();
    public static event StageClear OnStageClear;
    public static event StageClear OnStageFailure;


    private void Awake() {
        // PlayerInputのGamepadを取得する
        gamepad = InputSystem.GetDevice<Gamepad>();
    }

    void Start() {
        // プレイヤーにアタッチされているRigidbodyを取得
        rb = gameObject.GetComponent<Rigidbody>();

        // プレイヤーの攻撃処理を取得
        playerAttack = gameObject.GetComponent<PlayerAttack>();

        // UIコントローラ取得
        uiCon = uiObj.GetComponent<UIController>();

        // プレイヤーのHPをHPバーに反映
        HpUpdate();
    }

    private void Update() {
        if (null == gamepad) {
            // PlayerInputのGamepadを取得する
            gamepad = InputSystem.GetDevice<Gamepad>();
        }

        // Aボタン押下中判定
        if (gamepad.buttonEast.isPressed) {
            if(!isPressedA) {
                // ボタンが押された時の処理
                playerAttack.ShootBullet(true, atk);
                isPressedA = true;
            }
        } else {
            if (isPressedA) {
                // ボタンが離された時の処理
                playerAttack.ShootBullet(false);
                isPressedA = false;
            }
        }

        // Bボタン押下判定
        if (gamepad.buttonSouth.wasPressedThisFrame && 0 >= capCoolTimeRemaining) {
            capCoolTimeRemaining = capCoolTime;
            playerAttack.Capture(moveSpeed);
        }

        // 装備時間経過
        EqTimeLapse();

        // 捕獲ボタンクールダウン
        CapBtnTimeLapse();
    }

    private void FixedUpdate() {
        if (null == gamepad) {
            // PlayerInputのGamepadを取得する
            gamepad = InputSystem.GetDevice<Gamepad>();
        }

        // 前進中なら、入力値で位置を補正
        if (isMovingForward){
            Vector3 movement = new Vector3(gamepad.leftStick.ReadValue().x, 0.0f, gamepad.leftStick.ReadValue().y);
            transform.position += movement * 0.1f;

        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        // Updateへ処理移動
    }

    public void OnCapture() {
        // Updateへ処理移動
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Goal")) {
            OnStageClear?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && isMovingForward) {
            // ノックバック
            Vector3 knockbackDirection = Vector3.Reflect(transform.forward, other.contacts[0].normal);
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            StopMoving();
            Invoke("RestartMoving", stopTime);

            // ダメージ
            Damage(other.gameObject.GetComponent<EnemyController>().enemyData.atk);
            HpUpdate();
        }
    }

    // ダメージ
    private void Damage(int damage) {
        // 防御力を引いた分HPを減らす
        hpCurrent -= Math.Max(damage, 0);
        if (0 >= hpCurrent) {
            OnStageFailure?.Invoke();
        }
    }

    public void Capture(EnemyData data) {
        eqTime = eqTimeRemaining = data.eqTime;
        EqTimeUpdateDisp();

        // ボタンを切り替える
        uiCon.SetEequipment(true);
    }

    public void OnMove(InputAction.CallbackContext context) {
        // FixedUpdateへ処理移動
    }

    // プレイヤー自動移動
    public void MoveStart() {
        RestartMoving();
    }

    // プレイヤー前進
    private void MoveForward() {
        if (isMovingForward) {
            // 一定速度で前進する
            rb.velocity = Vector3.zero;
            Vector3 movement = new Vector3(0.0f, 0.0f, forwardSpeed);
            rb.AddForce(movement, ForceMode.VelocityChange);
        }
    }

    // 装備時間更新
    private void EqTimeLapse(){
        // 時間がセットされてないなら更新しない
        if(0 >= eqTime) {
            return;
        }
        eqTimeRemaining -= Time.deltaTime;
        if(eqTimeRemaining < 0f){
            eqTimeRemaining = 0f;
        }

        // 描画更新
        EqTimeUpdateDisp();

        // 時間終了
        if(0 >= eqTimeRemaining){
            eqTime = -1f;
            // ボタンを切り替える
            uiCon.SetEequipment(false);
        }
    }

    // 捕獲ボタン時間更新
    private void CapBtnTimeLapse() {
        // 時間がセットされてないなら更新しない
        if (0 >= capCoolTimeRemaining) {
            return;
        }
        capCoolTimeRemaining -= Time.deltaTime;
    }

    // 前進を停止する
    private void StopMoving() {
        isMovingForward = false;
        forwardSpeed = 0f;
        rb.velocity = Vector3.zero;
    }

    // 前進を再開する
    private void RestartMoving() {
        isMovingForward = true;
        forwardSpeed = moveSpeed;
        MoveForward();
    }

    // HPバーを更新
    public void HpUpdate(){
        hpBar.value = (float)hpCurrent / (float)hpMax;
        hpBar.GetComponentInChildren<Text>().text = "HP " + hpCurrent + "/" + hpMax;
    }

    // 装備時間バーを更新
    public void EqTimeUpdateDisp() {
        eqTimeBar.value = (float)eqTimeRemaining / (float)eqTime;
        eqTimeBar.GetComponentInChildren<Text>().text = "E " + eqTimeRemaining.ToString("F1") + " sec";
    }
}
