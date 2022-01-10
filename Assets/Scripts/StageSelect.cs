using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {
    private const int MIN_STAGE = 1;    // ステージ数最小
    private const int MAX_STAGE = 10;   // ステージ数最大

    public int stageNumber = 1;         // 選択中ステージNo.
    private int stageClearNumber;       // クリア済みステージ数

    public GameObject stageNumberText;  // ステージNo.テキスト
    public GameObject stagePrevButton;  // ステージ前へボタン
    public GameObject stageNextButton;  // ステージ次へボタン
    public GameObject stageEnterButton; // ステージ決定ボタン

    public Sprite[] stagePicture = new Sprite[MAX_STAGE]; //ステージ画像

    private bool zoomFlg = false;       // ステージ画像拡大フラグ(決定時)


    // Start is called before the first frame update
    void Start() {
        // クリアステージNo.をロード
        stageClearNumber = PlayerPrefs.GetInt("CLEAR_STAGE_NO", 0);
        stageNumber = stageClearNumber+1;
        StageDispRefresh();
    }

    // Update is called once per frame
    void Update() {
        if (zoomFlg && stageEnterButton.GetComponent<RectTransform>().localScale.x < 2.5f)
            stageEnterButton.GetComponent<RectTransform>().localScale += new Vector3(0.5f / 30.0f, 0.5f / 30.0f, 0.0f);
    }

    // 前ステージ選択
    public void StageSelectPrev() {
        if (MIN_STAGE >= stageNumber) return;
        stageNumber--;
        StageDispRefresh();
    }

    // 次ステージ選択
    public void StageSelectNext() {
        if (System.Math.Min(stageClearNumber + 1, MAX_STAGE) <= stageNumber) return;
        stageNumber++;
        StageDispRefresh();
    }

    // ステージ決定
    public void StageSelectEnter() {
        zoomFlg = true;
        StartCoroutine(StageEnterCoroutine());
    }

    // 選択ステージ描画更新
    private void StageDispRefresh() {
        // 前・次ボタンが不要なときボタンを非表示にする
        // LayoutGroupが崩れるので無効化はしない
        // todo:もっと上手くアルファ値だけ変える方法があればそれに変更する
        var color = stagePrevButton.GetComponent<Image>().color;
        stagePrevButton.GetComponent<Image>().color
            = MIN_STAGE < stageNumber
                ? new Color(color.r, color.g, color.b, 1.0f)
                : new Color(color.r, color.g, color.b, 0.0f);
        stageNextButton.GetComponent<Image>().color
            = System.Math.Min(stageClearNumber + 1, MAX_STAGE) > stageNumber
                ? new Color(color.r, color.g, color.b, 1.0f)
                : new Color(color.r, color.g, color.b, 0.0f);

        // ステージNo.テキストを更新
        stageNumberText.GetComponent<Text>().text = "Stage " + stageNumber.ToString("D2");

        // ステージ画像を更新
        stageEnterButton.GetComponent<Image>().sprite = stagePicture[stageNumber-1];

    }

    // ステージ決定ボタン押下後0.5秒後に遷移する
    private IEnumerator StageEnterCoroutine() {
        if (PlayerPrefs.GetInt("CLEAR_STAGE_NO", 0) < stageNumber) {
            PlayerPrefs.SetInt("CLEAR_STAGE_NO", stageNumber);
        }

        yield return new WaitForSeconds(0.5f);

        //todo:ゲームステージへ遷移するように変更する
        SceneManager.LoadScene("Title");

        yield break;
    }
}
