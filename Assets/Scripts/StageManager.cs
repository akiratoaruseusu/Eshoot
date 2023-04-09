using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour {
    [SerializeField]
    [Tooltip("画面上に表示するメッセージのパネル")]
    private GameObject MessagePanel;

    [SerializeField]
    [Tooltip("プレイヤー")]
    private GameObject Player;

    private bool startFlg = true;

    static bool eventSetFlg = true;
    // Start is called before the first frame update
    void Start() {
        if (eventSetFlg) {
            eventSetFlg = false;
            // プレイヤーがゴールしたらステージクリア
            PlayerController.OnStageClear += OnStageClear;
            // プレイヤーがHP0になったらステージ失敗
            PlayerController.OnStageFailure += OnStageFailure;
            // ポーズ画面からステージ終了
            Pause.OnStageExit += OnStageExit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(startFlg){
            Player.transform.position = gameObject.transform.Find("StartPoint").transform.position;
            StartCoroutine(CountDownToStart());
            startFlg = false;
        }
    }

    public void OnStageClear() {
        // ステージクリアフラグ
        PlayerPrefs.SetInt("PLAY_STAGE_NO", PlayerPrefs.GetInt("PLAY_STAGE_NO", 0) + MainMenuManager.STAGE_CLEAR_FLG);

        // ステージ選択画面に戻る
        SceneManager.LoadScene("MenuScene");
    }

    public void OnStageFailure() {
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
        Player.GetComponent<PlayerController>().MoveStart();
    }
}
