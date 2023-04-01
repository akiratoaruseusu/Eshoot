using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public void PauseGame() {
        // ポーズ画面表示
        pausePanel.SetActive(true);

        // 一時停止
        Time.timeScale = 0;
    }

    public void ResumeGame() {
        // ポーズ画面非表示
        pausePanel.SetActive(false);

        // 一時停止解除
        Time.timeScale = 1;
    }
}
