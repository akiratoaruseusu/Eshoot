using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public delegate void StageExit();
    public static event StageExit OnStageExit;

    public void PauseGame() {
        // �|�[�Y��ʕ\��
        pausePanel.SetActive(true);

        // �ꎞ��~
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        // �|�[�Y��ʔ�\��
        pausePanel.SetActive(false);

        // �ꎞ��~����
        Time.timeScale = 1;
    }

    public void BackToMenu() {
        // �|�[�Y��ʔ�\��
        pausePanel.SetActive(false);

        // �ꎞ��~����
        Time.timeScale = 1;

        // �X�e�[�W�I�����j���[�ɖ߂�
        OnStageExit?.Invoke();
    }
}
