using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    public enum MENU_MODE {         // メニュー状態定義
        NOEMAL,                         // 通常
        SWIPE,                          // スワイプ中
        STAGE_SELECTED,                 // ステージ選択済み
    };
    public enum SWIPE {             // スワイプ方向定義
        NONE,                           // なし
        LEFT,                           // 左
        RIGHT,                          // 右
    };

    private const int MIN_STAGE = 1;    // ステージ数最小
    private const int MAX_STAGE = 10;   // ステージ数最大
    public const int STAGE_CLEAR_FLG = 10000;   //ステージクリアフラグ（ステージNoに加算）

    public int stageNumber = 1;         // 選択中ステージNo.
    private int stageClearNumber;       // クリア済みステージ数

    public GameObject stageNumberText;  // ステージNo.テキスト
    public GameObject stagePrevButton;  // ステージ前へボタン
    public GameObject stageNextButton;  // ステージ次へボタン
    public GameObject stageEnterButton; // ステージ決定ボタン

    public Sprite[] stagePicture = new Sprite[MAX_STAGE]; //ステージ画像

    private MENU_MODE menuMode = MENU_MODE.NOEMAL;       // メニュー状態

    private float pressStartPosX;           // スワイプ判定用:押下開始X座標
    private float swipeDiffPosX = 10.0f;    // スワイプ判定とする距離


    // Start is called before the first frame update
    void Start() {
        // クリアステージNo.をロード
        stageClearNumber = PlayerPrefs.GetInt("CLEAR_STAGE_NO", 0);

        // プレイ中ステージNo.をロード
        stageNumber = PlayerPrefs.GetInt("PLAY_STAGE_NO", 0);
        int clearCheck = stageNumber / STAGE_CLEAR_FLG;
        stageNumber %= STAGE_CLEAR_FLG;

        // クリア済みならクリアステージを更新
        if (1 == clearCheck && stageClearNumber < stageNumber) {
            PlayerPrefs.SetInt("CLEAR_STAGE_NO", stageNumber);
            stageClearNumber = stageNumber;
            Debug.Log("クリアステージ更新:"+ stageClearNumber);
        }

        // プレイ中ステージNo.をリセット
        PlayerPrefs.SetInt("PLAY_STAGE_NO", 0);

        // デフォルト選択ステージはクリア済み＋1とする
        stageNumber = System.Math.Min(stageClearNumber + 1, MAX_STAGE);
        StageDispRefresh();
    }

    // Update is called once per frame
    void Update() {

        switch (menuMode) {
        case MENU_MODE.NOEMAL:
        case MENU_MODE.SWIPE:
            // スワイプ判定
            switch (SwipeCheck()) {
            case SWIPE.LEFT: StageSelectNext(); break;
            case SWIPE.RIGHT: StageSelectPrev(); break;
            }
            break;

        case MENU_MODE.STAGE_SELECTED:
            // ステージ画像（ボタン）を拡大
            if (stageEnterButton.GetComponent<RectTransform>().localScale.x < 2.5f)
                stageEnterButton.GetComponent<RectTransform>().localScale += new Vector3(0.5f / 30.0f, 0.5f / 30.0f, 0.0f);
            break;
        }
    }

    // 前ステージ選択
    public void StageSelectPrev() {
        // 最初のステージかチェック
        if (MIN_STAGE >= stageNumber) return;
        // スワイプ優先
        if (MENU_MODE.SWIPE == menuMode && SWIPE.NONE != SwipeCheck()) {
            menuMode = MENU_MODE.SWIPE;
            return;
        }

        // アニメーション
        var button = Instantiate(stageEnterButton, stageEnterButton.transform.parent);
        button.GetComponent<Animator>().SetTrigger("prevOut");
        button.transform.SetAsFirstSibling();
        Destroy(button, 0.4f);
        stageEnterButton.GetComponent<Animator>().SetTrigger("prevIn");


        stageNumber--;
        StageDispRefresh();
    }

    // 次ステージ選択
    public void StageSelectNext() {
        // 最後のステージかチェック
        if (System.Math.Min(stageClearNumber + 1, MAX_STAGE) <= stageNumber) return;
        // スワイプ優先
        if (MENU_MODE.SWIPE == menuMode && SWIPE.NONE != SwipeCheck()) {
            menuMode = MENU_MODE.SWIPE;
            return;
        }

        // アニメーション
        var button = Instantiate(stageEnterButton, stageEnterButton.transform.parent);
        button.GetComponent<Animator>().SetTrigger("nextOut");
        button.transform.SetAsFirstSibling();
        Destroy(button, 0.4f);
        stageEnterButton.GetComponent<Animator>().SetTrigger("nextIn");

        stageNumber++;
        StageDispRefresh();
    }

    // ステージ決定
    public void StageSelectEnter() {
        // 選択済みかチェック
        if (MENU_MODE.STAGE_SELECTED == menuMode) return;
        // スワイプ優先
        if (MENU_MODE.SWIPE == menuMode && SWIPE.NONE != SwipeCheck()) {
            menuMode = MENU_MODE.SWIPE;
            return;
        }
        menuMode = MENU_MODE.STAGE_SELECTED;
        StartCoroutine(StageEnterCoroutine());
    }

    // 選択ステージ描画更新
    private void StageDispRefresh() {
        // 前・次ボタンが不要なときボタンを非表示にする
        stagePrevButton.SetActive(MIN_STAGE < stageNumber ? true : false);
        stageNextButton.SetActive(System.Math.Min(stageClearNumber + 1, MAX_STAGE) > stageNumber ? true : false);

        // ステージ名を表示
        stageNumberText.GetComponent<Text>().text = "Stage " + stageNumber.ToString("D2");

        // ステージ画像を更新
        stageEnterButton.GetComponent<Image>().sprite = stagePicture[stageNumber-1];

    }

    // ステージ決定ボタン押下後0.5秒後に遷移する
    private IEnumerator StageEnterCoroutine() {
        // 選択したステージ番号を保持
        PlayerPrefs.SetInt("PLAY_STAGE_NO", stageNumber);
        Debug.Log("Play Stage:"+stageNumber);

        yield return new WaitForSeconds(0.5f);

        //todo:ゲームステージへ遷移するように変更する
        SceneManager.LoadScene("tmp");

        yield break;
    }

    // スワイプ判定
    private SWIPE SwipeCheck() {
        if (Input.GetMouseButtonDown(0)) {
            menuMode = MENU_MODE.SWIPE;
            pressStartPosX = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0)) {
            menuMode = MENU_MODE.NOEMAL;
            if (swipeDiffPosX <= pressStartPosX - Input.mousePosition.x) {
                return SWIPE.LEFT;
            } else if (swipeDiffPosX <= Input.mousePosition.x - pressStartPosX) {
                return SWIPE.RIGHT;
            }
        }
        return SWIPE.NONE;
    }
}
