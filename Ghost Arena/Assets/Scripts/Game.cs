using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public GameObject Ghost;
    public GameObject Player;

    public GameObject GameOverPanel;

    public Text ScoreText;
    public Text HealthText;
    public Text PauseText;

    private float _timer;

    public AudioSource GhostAudioSource;
    public AudioSource PlayerDeathAudioSource;
    public AudioSource BulletAudioSource;

    private GameObject _player;

    private bool _playSound;

    public LeaderboardsManager Leaderboards;
    public AchievementsManager Achievements;

    // Start is called before the first frame update
    void Start ()
    {
        if (Globals.RandomNum == null)
            Globals.RandomNum = new System.Random();

        Globals.BulletAudioSource = BulletAudioSource;

        _player = (GameObject)GameObject.Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        ResetGame();

        //_player.GetComponent<PlayerBullet>().Accuracy += Leaderboards.AccuracyHandler;
        //_player.gameObject.GetComponent<PlayerController>().PlayerSurvival += Leaderboards.PlayerSurvivalHandler;
        //_player.gameObject.GetComponent<PlayerBullet>().GhostDestroyed += Leaderboards.GhostDestroyedHandler;
    }

    void ResetGame()
    {
        Globals.Health = 100;
        Globals.Score = 0;
        Globals.CurGameState = GameState.PlayingGame;
        _player.transform.position = new Vector3(0, 0, 0);
        _playSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.CurGameState == GameState.PlayingGame)
        {
            _timer += Time.deltaTime;

            if (_timer >= Globals.SpawnTimes[Globals.DifficultyLevel])
            {
                SpawnGhost();

                _timer -= Globals.SpawnTimes[Globals.DifficultyLevel];
            }

            ScoreText.text = "Score: " + Globals.Score.ToString();
            HealthText.text = "Health: " + Globals.Health.ToString();

            if (Globals.Health == 0)
            {
                PlayerDeathAudioSource.Play();
                Globals.CurGameState = GameState.GameOver;
                //kill player, game over
                GameOverPanel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Globals.CurGameState == GameState.PlayingGame)
            {
                Globals.CurGameState = GameState.PauseGame;
                PauseText.gameObject.SetActive(true);
            }
            else if (Globals.CurGameState == GameState.PauseGame)
                SceneManager.LoadScene("Menu");
        }

        if (Input.GetKeyDown(KeyCode.Return) && Globals.CurGameState == GameState.PauseGame)
        {
            PauseText.gameObject.SetActive(false);
            Globals.CurGameState = GameState.PlayingGame;

        }
    }


    private void SpawnGhost()
    {
        int index = Globals.RandomNum.Next(Globals.StartingPoints.Length);

        Vector3 vec = Globals.StartingPoints[index];
        GameObject ghost = (GameObject)Instantiate(Ghost, vec, Quaternion.identity);
        ghost.GetComponent<Ghost>().Player = _player;

        // ghost.GetComponent<Ghost>().GhostSurvival += Leaderboards.GhostSurvivalHandler;


        //rotate as necessary
        switch (index)
        {
            //rotate 180
            case 0:
            case 1:
            case 2:
            {
                ghost.transform.Rotate(0.0f, 0.0f, 180.0f);

                break;
            }
            // rotate 90 counter-clockwise
            case 3:
            case 4:
            {
                    ghost.transform.Rotate(0.0f, 0.0f, 90.0f);

                    break;
            }
            // rotate 90 clockwise
            case 8:
            case 9:
            {
                    ghost.transform.Rotate(0.0f, 0.0f, -90.0f);

                    break;
            }
        }

        //if hard difficulty only play sound every other time
        if (Globals.DifficultyLevel == 2)
            _playSound = !_playSound;

        if(_playSound)
            GhostAudioSource.Play();
    }


    public void ExitButton_Click()
    {
        SceneManager.LoadScene("Menu");
    }

    public void PlayAgainButton_Click()
    {
        GameOverPanel.SetActive(false);
        ResetGame();
    }
}
