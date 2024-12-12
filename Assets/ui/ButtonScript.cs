using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ButtonScript : MonoBehaviour{
    public void ResumeGame() {
        Time.timeScale = 1;
        transform.parent.parent.GetComponent<Canvas>().enabled = false;
    }
    public void LeaveGame() {
        Time.timeScale = 1;
        /*TEMP*/transform.parent.parent.GetComponent<Canvas>().enabled = false;
        //SceneManager.LoadScene("Main Menu"); < actual thing
    }
    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Hi");
    }
    public void QuitGame() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}