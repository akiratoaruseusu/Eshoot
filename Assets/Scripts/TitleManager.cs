using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour {
    public void StartGame() {
        StartCoroutine(StartGameCoroutine());
    }

    // ��ʉ�����0.5�b��ɑJ�ڂ���
    private IEnumerator StartGameCoroutine() {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("MenuScene");

        yield break;
    }
}