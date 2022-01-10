using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManeger : MonoBehaviour {
    public enum MENU_MODE {         // ���j���[��Ԓ�`
        NOEMAL,                         // �ʏ�
        SWIPE,                          // �X���C�v��
        STAGE_SELECTED,                 // �X�e�[�W�I���ς�
    };
    public enum SWIPE {             // �X���C�v������`
        NONE,                           // �Ȃ�
        LEFT,                           // ��
        RIGHT,                          // �E
    };

    private const int MIN_STAGE = 1;    // �X�e�[�W���ŏ�
    private const int MAX_STAGE = 10;   // �X�e�[�W���ő�

    public int stageNumber = 1;         // �I�𒆃X�e�[�WNo.
    private int stageClearNumber;       // �N���A�ς݃X�e�[�W��

    public GameObject stageNumberText;  // �X�e�[�WNo.�e�L�X�g
    public GameObject stagePrevButton;  // �X�e�[�W�O�փ{�^��
    public GameObject stageNextButton;  // �X�e�[�W���փ{�^��
    public GameObject stageEnterButton; // �X�e�[�W����{�^��

    public Sprite[] stagePicture = new Sprite[MAX_STAGE]; //�X�e�[�W�摜

    private MENU_MODE menuMode = MENU_MODE.NOEMAL;       // ���j���[���

    private float pressStartPosX;           // �X���C�v����p:�����J�nX���W
    private float swipeDiffPosX = 10.0f;    // �X���C�v����Ƃ��鋗��


    // Start is called before the first frame update
    void Start() {
        // �N���A�X�e�[�WNo.�����[�h
        stageClearNumber = PlayerPrefs.GetInt("CLEAR_STAGE_NO", 0);
        stageNumber = stageClearNumber+1;
        StageDispRefresh();
    }

    // Update is called once per frame
    void Update() {

        switch (menuMode) {
        case MENU_MODE.NOEMAL:
        case MENU_MODE.SWIPE:
            // �X���C�v����
            switch (SwipeCheck()) {
            case SWIPE.LEFT: StageSelectNext(); break;
            case SWIPE.RIGHT: StageSelectPrev(); break;
            }
            break;

        case MENU_MODE.STAGE_SELECTED:
            // �X�e�[�W�摜�i�{�^���j���g��
            if (stageEnterButton.GetComponent<RectTransform>().localScale.x < 2.5f)
                stageEnterButton.GetComponent<RectTransform>().localScale += new Vector3(0.5f / 30.0f, 0.5f / 30.0f, 0.0f);
            break;
        }
    }

    // �O�X�e�[�W�I��
    public void StageSelectPrev() {
        // �ŏ��̃X�e�[�W���`�F�b�N
        if (MIN_STAGE >= stageNumber) return;
        // �X���C�v�D��
        if (MENU_MODE.SWIPE == menuMode && SWIPE.NONE != SwipeCheck()) {
            menuMode = MENU_MODE.SWIPE;
            return;
        }
        stageNumber--;
        StageDispRefresh();
    }

    // ���X�e�[�W�I��
    public void StageSelectNext() {
        // �Ō�̃X�e�[�W���`�F�b�N
        if (System.Math.Min(stageClearNumber + 1, MAX_STAGE) <= stageNumber) return;
        // �X���C�v�D��
        if (MENU_MODE.SWIPE == menuMode && SWIPE.NONE != SwipeCheck()) {
            menuMode = MENU_MODE.SWIPE;
            return;
        }
        stageNumber++;
        StageDispRefresh();
    }

    // �X�e�[�W����
    public void StageSelectEnter() {
        // �I���ς݂��`�F�b�N
        if (MENU_MODE.STAGE_SELECTED == menuMode) return;
        // �X���C�v�D��
        if (MENU_MODE.SWIPE == menuMode && SWIPE.NONE != SwipeCheck()) {
            menuMode = MENU_MODE.SWIPE;
            return;
        }
        menuMode = MENU_MODE.STAGE_SELECTED;
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
            = System.Math.Min(stageClearNumber + 1, MAX_STAGE) > stageNumber
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

    // �X���C�v����
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
