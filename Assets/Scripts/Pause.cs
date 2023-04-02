using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    public delegate void StageExit();
    public static event StageExit OnStageExit;

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

    public void BackToMenu() {
        // ポーズ画面非表示
        pausePanel.SetActive(false);

        // 一時停止解除
        Time.timeScale = 1;

        // ステージ選択メニューに戻る
        OnStageExit?.Invoke();
    }
}
