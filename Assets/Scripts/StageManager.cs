using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {
        // �v���C���[���S�[��������X�e�[�W�N���A
        PlayerController.OnStageClear += OnStageClear;
        // �|�[�Y��ʂ���X�e�[�W�I��
        Pause.OnStageExit += OnStageExit;
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
}
