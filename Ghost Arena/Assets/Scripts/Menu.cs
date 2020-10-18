using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject DifficultyPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGameButton_Click()
    {
        DifficultyPanel.SetActive(true);
        MainPanel.SetActive(false);
    }

    public void CancelButton_Click ()
    {
        DifficultyPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void DifficultyButton_Click(int index)
    {
        Globals.DifficultyLevel = index;
        SceneManager.LoadScene("Game");
    }

    public void QuitButton_Click()
    {
        Application.Quit();
    }
}
