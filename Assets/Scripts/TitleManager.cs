using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    public void StartGame() {
        StartCoroutine(StartGameCoroutine());
    }

    // ‰æ–Ê‰Ÿ‰ºŒã0.5•bŒã‚É‘JˆÚ‚·‚é
    private IEnumerator StartGameCoroutine() {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MenuScene");

        yield break;
    }
}