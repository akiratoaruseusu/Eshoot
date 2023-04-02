using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    [SerializeField]
    [Tooltip("画面上に表示するメッセージのパネル")]
    private GameObject MessagePanel;

    public delegate void PlayerMoveStart();
    public static event PlayerMoveStart OnPlayerMoveStart;

    // Start is called before the first frame update
    void Start() {
        // プレイヤーがゴールしたらステージクリア
        PlayerController.OnStageClear += OnStageClear;
        // ポーズ画面からステージ終了
        Pause.OnStageExit += OnStageExit;

        StartCoroutine(CountDownToStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStageClear() {
        // ステージ選択画面に戻る
        SceneManager.LoadScene("MenuScene");
    }

    public void OnStageExit() {
        // ステージ選択画面に戻る
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator CountDownToStart() {
        MessagePanel.SetActive(true);

        Text msg = MessagePanel.GetComponentInChildren<Text>();
        msg.text = "Ready…";
        yield return new WaitForSeconds(1.0f);

        msg.text = "Go!";
        yield return new WaitForSeconds(1.0f);

        msg.text = "";
        MessagePanel.SetActive(false);

        // プレイヤー自動移動開始
        OnPlayerMoveStart?.Invoke();
    }
}
