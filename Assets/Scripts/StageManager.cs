using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {
        // プレイヤーがゴールしたらステージクリア
        PlayerController.OnStageClear += OnStageClear;
        // ポーズ画面からステージ終了
        Pause.OnStageExit += OnStageExit;
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
}
