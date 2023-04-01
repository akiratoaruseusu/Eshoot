using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

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
}
