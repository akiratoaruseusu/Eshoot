using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    public GameObject captureButton;
    public GameObject eqUseButton;
    
    void Start() {
        captureButton.SetActive(true);
        eqUseButton.SetActive(false);
    }

    public void SetEequipment(bool flg){
        if(flg) {
            captureButton.SetActive(false);
            eqUseButton.SetActive(true);
        } else {
            captureButton.SetActive(true);
            eqUseButton.SetActive(false);
        }
    }
}
