using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
	public GameObject MainPanel;
	public GameObject DifficultyPanel;

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
