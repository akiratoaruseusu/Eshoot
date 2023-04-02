using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�ő�HP")]
    private int hpMax = 100;
    [SerializeField]
    [Tooltip("����HP")]
    private int hp = 100;
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

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private bool isMovingForward;  // �O�i�t���O
    private float forwardSpeed;  // �O�i���x

    private float movementX;
    private float movementY;

    public delegate void StageClear();
    public static event StageClear OnStageClear;

    void Start() {
        // �X�e�[�W�}�l�[�W���[����ړ��J�n���󂯎��
        StageManager.OnPlayerMoveStart += OnPlayerMoveStart;

        // �v���C���[�ɃA�^�b�`����Ă���Rigidbody���擾
        rb = gameObject.GetComponent<Rigidbody>();

        // �v���C���[�̍U���������擾
        playerAttack = gameObject.GetComponent<PlayerAttack>();

        // �v���C���[��HP��HP�o�[�ɔ��f
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
            Debug.Log("�G�ƂԂ�����");
            // �m�b�N�o�b�N�����̌v�Z
            Vector3 knockbackDirection = Vector3.Reflect(transform.forward, other.contacts[0].normal);

            // �m�b�N�o�b�N�͂̉��Z
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

            StopMoving();
            Invoke("RestartMoving", stopTime);

        }
    }

    private void OnMove(InputValue movementValue) {
        // Move�A�N�V�����̓��͒l���擾
        Vector2 movementVector = movementValue.Get<Vector2>();

        // x,y�������̓��͒l��ϐ��ɑ��
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // �v���C���[�����ړ�
    public void OnPlayerMoveStart() {
        Debug.Log("Start!!");
        RestartMoving();
    }

    // �v���C���[�O�i
    private void MoveForward() {
        if (isMovingForward) {
            // ��葬�x�őO�i����
            Vector3 movement = new Vector3(0.0f, 0.0f, forwardSpeed);
            rb.AddForce(movement, ForceMode.VelocityChange);
        }
    }

    // �O�i���~����
    private void StopMoving() {
        isMovingForward = false;
        forwardSpeed = 0f;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    // �O�i���ĊJ����
    private void RestartMoving() {
        isMovingForward = true;
        forwardSpeed = moveSpeed;
        MoveForward();
    }

    private void FixedUpdate() {
        // ���͒l������3���x�N�g�����쐬
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // rigidbody��AddForce���g�p���ăv���C���[�𓮂���
        rb.AddForce(movement * moveSpeed);
    }

    // HP�o�[���X�V
    public void HpUpdate(){
        hpBar.value = (float)hp / (float)hpMax;
    }
}
