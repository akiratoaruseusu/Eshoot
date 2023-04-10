using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�ő�HP")]
    private int hpMax = 100;
    [SerializeField]
    [Tooltip("����HP")]
    private int hpCurrent = 100;
    [SerializeField]
    [Tooltip("�U����")]
    private int atk = 5;
    [SerializeField]
    [Tooltip("�ړ����x")]
    private float moveSpeed = 10;

    [SerializeField]
    [Tooltip("�m�b�N�o�b�N��")]
    private float knockbackForce = 1f;
    [SerializeField]
    [Tooltip("�Փˎ���~����")]
    private float stopTime = 1f;

    [SerializeField]
    [Tooltip("HP�o�[")]
    private Slider hpBar;
    [SerializeField]
    [Tooltip("�������ԃo�[")]
    private Slider eqTimeBar;

    private float eqTime;             // ��������
    private float eqTimeRemaining;    // �c�葕������

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private bool isMovingForward;  // �O�i�t���O
    private float forwardSpeed;  // �O�i���x

    private Gamepad gamepad;
    private bool isPressedA = false;

    // �C�x���g
    public delegate void StageClear();
    public static event StageClear OnStageClear;
    public static event StageClear OnStageFailure;


    private void Awake() {
        // PlayerInput��Gamepad���擾����
        gamepad = InputSystem.GetDevice<Gamepad>();
    }

    void Start() {
        // �v���C���[�ɃA�^�b�`����Ă���Rigidbody���擾
        rb = gameObject.GetComponent<Rigidbody>();

        // �v���C���[�̍U���������擾
        playerAttack = gameObject.GetComponent<PlayerAttack>();

        // �v���C���[��HP��HP�o�[�ɔ��f
        HpUpdate();
    }

    private void Update() {
        if (null == gamepad) {
            // PlayerInput��Gamepad���擾����
            gamepad = InputSystem.GetDevice<Gamepad>();
        }

        // A�{�^������������
        if (gamepad.buttonEast.isPressed) {
            if(!isPressedA) {
                // �{�^���������ꂽ���̏���
                playerAttack.ShootBullet(true, atk);
                isPressedA = true;
            }
        } else {
            if (isPressedA) {
                // �{�^���������ꂽ���̏���
                playerAttack.ShootBullet(false);
                isPressedA = false;
            }
        }

        // B�{�^����������
        if (gamepad.buttonSouth.wasPressedThisFrame) {
            playerAttack.Capture(moveSpeed);
        }
    }

    private void FixedUpdate() {
        if (null == gamepad) {
            // PlayerInput��Gamepad���擾����
            gamepad = InputSystem.GetDevice<Gamepad>();
        }

        // �O�i���Ȃ�A���͒l�ňʒu��␳
        if (isMovingForward){
            Vector3 movement = new Vector3(gamepad.leftStick.ReadValue().x, 0.0f, gamepad.leftStick.ReadValue().y);
            transform.position += movement * 0.1f;

        }
    }

    public void OnAttack(InputAction.CallbackContext context) {
        // Update�֏����ړ�
    }

    public void OnCapture() {
        // Update�֏����ړ�
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Goal")) {
            OnStageClear?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && isMovingForward) {
            // �m�b�N�o�b�N
            Vector3 knockbackDirection = Vector3.Reflect(transform.forward, other.contacts[0].normal);
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            StopMoving();
            Invoke("RestartMoving", stopTime);

            // �_���[�W
            Damage(other.gameObject.GetComponent<EnemyController>().enemyData.atk);
            HpUpdate();
        }
    }

    // �_���[�W
    private void Damage(int damage) {
        // �h��͂���������HP�����炷
        hpCurrent -= Math.Max(damage, 0);
        if (0 >= hpCurrent) {
            OnStageFailure?.Invoke();
        }
    }

    public void Capture(EnemyData data) {
        eqTime = eqTimeRemaining = data.eqTime;
        EqTimeUpdate();
    }

    public void OnMove(InputAction.CallbackContext context) {
        // FixedUpdate�֏����ړ�
    }

    // �v���C���[�����ړ�
    public void MoveStart() {
        RestartMoving();
    }

    // �v���C���[�O�i
    private void MoveForward() {
        if (isMovingForward) {
            // ��葬�x�őO�i����
            rb.velocity = Vector3.zero;
            Vector3 movement = new Vector3(0.0f, 0.0f, forwardSpeed);
            rb.AddForce(movement, ForceMode.VelocityChange);
        }
    }

    // �O�i���~����
    private void StopMoving() {
        isMovingForward = false;
        forwardSpeed = 0f;
        rb.velocity = Vector3.zero;
    }

    // �O�i���ĊJ����
    private void RestartMoving() {
        isMovingForward = true;
        forwardSpeed = moveSpeed;
        MoveForward();
    }

    // HP�o�[���X�V
    public void HpUpdate(){
        hpBar.value = (float)hpCurrent / (float)hpMax;
        hpBar.GetComponentInChildren<Text>().text = "HP " + hpCurrent + "/" + hpMax;
    }

    // �������ԃo�[���X�V
    public void EqTimeUpdate() {
        eqTimeBar.value = (float)eqTimeRemaining / (float)eqTime;
        eqTimeBar.GetComponentInChildren<Text>().text = "E " + eqTimeRemaining.ToString("F1") + "sec";
    }
}
