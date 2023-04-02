using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    [SerializeField]
    [Tooltip("��ʏ�ɕ\�����郁�b�Z�[�W�̃p�l��")]
    private GameObject MessagePanel;

    public delegate void PlayerMoveStart();
    public static event PlayerMoveStart OnPlayerMoveStart;

    // Start is called before the first frame update
    void Start() {
        // �v���C���[���S�[��������X�e�[�W�N���A
        PlayerController.OnStageClear += OnStageClear;
        // �|�[�Y��ʂ���X�e�[�W�I��
        Pause.OnStageExit += OnStageExit;

        StartCoroutine(CountDownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStageClear() {
        // �X�e�[�W�I����ʂɖ߂�
        SceneManager.LoadScene("MenuScene");
    }

    public void OnStageExit() {
        // �X�e�[�W�I����ʂɖ߂�
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator CountDownToStart() {
        MessagePanel.SetActive(true);

        Text msg = MessagePanel.GetComponentInChildren<Text>();
        msg.text = "Ready�c";
        yield return new WaitForSeconds(1.0f);

        msg.text = "Go!";
        yield return new WaitForSeconds(1.0f);

        msg.text = "";
        MessagePanel.SetActive(false);

        // �v���C���[�����ړ��J�n
        OnPlayerMoveStart?.Invoke();
    }
}
