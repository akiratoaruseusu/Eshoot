using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�v���C���[�̍ő�HP")]
    private int hpMax = 100;
    [SerializeField]
    [Tooltip("�v���C���[�̌���HP")]
    private int hp = 100;
    [SerializeField]
    [Tooltip("�v���C���[�̍U����")]
    private int atk = 5;
    [SerializeField]
    [Tooltip("�v���C���[�̈ړ����x")]
    private float speed = 10;

    [SerializeField]
    [Tooltip("�v���C���[��HP�o�[")]
    private Slider hpBar;

    private Rigidbody rb;
    private PlayerAttack playerAttack;

    private float movementX;
    private float movementY;

    public delegate void StageClear();
    public static event StageClear OnStageClear;

    void Start() {
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

    private void OnMove(InputValue movementValue) {
        // Move�A�N�V�����̓��͒l���擾
        Vector2 movementVector = movementValue.Get<Vector2>();

        // x,y�������̓��͒l��ϐ��ɑ��
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate() {
        // ���͒l������3���x�N�g�����쐬
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // rigidbody��AddForce���g�p���ăv���C���[�𓮂���
        rb.AddForce(movement * speed);
    }

    // HP�o�[���X�V
    public void HpUpdate(){
        hpBar.value = (float)hp / (float)hpMax;
    }
}
