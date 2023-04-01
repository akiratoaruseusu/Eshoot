using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    public void StartGame() {
        StartCoroutine(StartGameCoroutine());
    }

    // 画面押下後0.5秒後に遷移する
    private IEnumerator StartGameCoroutine() {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MenuScene");

        yield break;
    }
}