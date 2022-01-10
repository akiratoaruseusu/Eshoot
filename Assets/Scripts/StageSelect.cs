using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour {
    private const int MIN_STAGE = 1;    // �X�e�[�W���ŏ�
    private const int MAX_STAGE = 10;   // �X�e�[�W���ő�

    public int stageNumber = 1;         // �I�𒆃X�e�[�WNo.
    private int stageClearNumber;       // �N���A�ς݃X�e�[�W��

    public GameObject stageNumberText;  // �X�e�[�WNo.�e�L�X�g
    public GameObject stagePrevButton;  // �X�e�[�W�O�փ{�^��
    public GameObject stageNextButton;  // �X�e�[�W���փ{�^��
    public GameObject stageEnterButton; // �X�e�[�W����{�^��

    public Sprite[] stagePicture = new Sprite[MAX_STAGE]; //�X�e�[�W�摜


    // Start is called before the first frame update
    void Start() {
        // �N���A�X�e�[�WNo.�����[�h
        stageClearNumber = PlayerPrefs.GetInt("CLEAR_STAGE_NO", 0);
        stageNumber = stageClearNumber+1;
        StageDispRefresh();
    }

    // Update is called once per frame
    void Update() {
        
    }

    // �O�X�e�[�W�I��
    public void StageSelectPrev() {
        if (MIN_STAGE >= stageNumber) return;
        stageNumber--;
        StageDispRefresh();
    }

    // ���X�e�[�W�I��
    public void StageSelectNext() {
        if (stageClearNumber+1 <= stageNumber) return;
        stageNumber++;
        StageDispRefresh();
    }

    // �X�e�[�W����
    public void StageSelectEnter() {
        StartCoroutine(StageEnterCoroutine());
    }

    // �I���X�e�[�W�`��X�V
    private void StageDispRefresh() {
        // �O�E���{�^�����s�v�ȂƂ��{�^�����\���ɂ���
        // LayoutGroup�������̂Ŗ������͂��Ȃ�
        // todo:�����Ə�肭�A���t�@�l�����ς�����@������΂���ɕύX����
        var color = stagePrevButton.GetComponent<Image>().color;
        stagePrevButton.GetComponent<Image>().color
            = MIN_STAGE < stageNumber
                ? new Color(color.r, color.g, color.b, 1.0f)
                : new Color(color.r, color.g, color.b, 0.0f);
        stageNextButton.GetComponent<Image>().color
            = stageClearNumber + 1 > stageNumber
                ? new Color(color.r, color.g, color.b, 1.0f)
                : new Color(color.r, color.g, color.b, 0.0f);

        // �X�e�[�WNo.�e�L�X�g���X�V
        stageNumberText.GetComponent<Text>().text = "Stage " + stageNumber.ToString("D2");

        // �X�e�[�W�摜���X�V
        stageEnterButton.GetComponent<Image>().sprite = stagePicture[stageNumber-1];

    }

    // �X�e�[�W����{�^��������0.5�b��ɑJ�ڂ���
    private IEnumerator StageEnterCoroutine() {
        if (PlayerPrefs.GetInt("CLEAR_STAGE_NO", 0) < stageNumber) {
            PlayerPrefs.SetInt("CLEAR_STAGE_NO", stageNumber);
        }

        yield return new WaitForSeconds(0.5f);

        //todo:�Q�[���X�e�[�W�֑J�ڂ���悤�ɕύX����
        SceneManager.LoadScene("Title");

        yield break;
    }
}
