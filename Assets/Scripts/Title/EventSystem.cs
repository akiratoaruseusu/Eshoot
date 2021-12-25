using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventSystem : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
