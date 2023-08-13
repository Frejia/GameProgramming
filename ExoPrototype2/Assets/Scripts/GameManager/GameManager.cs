using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public enum GameState
{
    Shooter,
    Race,
    Win,
    Lose,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private GameState gameState;
    [SerializeField] private bool allowSecondPlayer;
    private bool newGame = false;
    
    private GameObject player1;
    private GameObject player2;
    
    [SerializeField] private Canvas inGameUI;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Canvas pauseCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInputManager.instance.onPlayerJoined += GetSecondPlayer;
        GameMode1.Player1Win += SetWin;
        GameMode1.Player2Win += SetWin;

        newGame = true;
    }

    public void SetPause()
    {
        this.gameState = GameState.Paused;
        SwitchGameState();
    }
    
    // Switch Game State
  private void SwitchGameState()
  {

      switch (gameState)
      {
          case GameState.Shooter:
              if (newGame)
              {
                  GetComponent<GameMode1>().InitShooter();
                  ShowCanvas(inGameUI);
                  newGame = false;
              }
              else
              {
                  DisableCanvas(pauseCanvas);
                  ContinueGame();
              }
              break;
          case GameState.Race:
              if (newGame)
              {
                  GetComponent<GameMode1>().InitRace();
                  ShowCanvas(inGameUI);
                  newGame = false;
              }
              else
              {
                  DisableCanvas(pauseCanvas);
                  ContinueGame();
              }
              break;
          case GameState.Win:
              newGame = true;
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(winCanvas);
              break;
          case GameState.Lose:
              newGame = true;
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(loseCanvas);
              break;
          case GameState.Paused:
              newGame = false;
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(pauseCanvas);
              break;
      }
  }

  // Method for UI Buttons
  private void AllowSecondPlayer(bool allow)
  {
      allowSecondPlayer = allow;
      
      if (!allowSecondPlayer)
      {
          PlayerInputManager.instance.DisableJoining();
      }
      else
      {
          PlayerInputManager.instance.EnableJoining();
      }
  }

  private void GetSecondPlayer(PlayerInput obj)
  {
      player2 = obj.gameObject;
  }

  private void ShowCanvas(Canvas canvas)
  {
      canvas.gameObject.SetActive(true);
  }

  private void DisableCanvas(Canvas canvas)
  {
      canvas.gameObject.SetActive(false);
  }

  private void PauseGame()
  {
      // Pause game
        Time.timeScale = 0;
        PlayerInputManager.instance.StopAllCoroutines();
        player1.GetComponent<PlayerInput>().StopAllCoroutines();
        player1.GetComponent<ShipMovement>().enabled = false;
        player1.GetComponent<PlayerShoot>().enabled = false;
        player1.transform.GetChild(0).gameObject.SetActive(false);
        if (player2 != null)
        {
            player2.GetComponent<PlayerInput>().StopAllCoroutines();
            player2.GetComponent<ShipMovement>().enabled = false;
            player2.GetComponent<PlayerShoot>().enabled = false;
            player2.transform.GetChild(0).gameObject.SetActive(false);
        }
  }

  public void ContinueGame()
  {
        Time.timeScale = 1;
        player1.GetComponent<ShipMovement>().enabled = true;
        player1.GetComponent<PlayerShoot>().enabled = true;
        player1.transform.GetChild(0).gameObject.SetActive(true);
        
        if (player2 != null)
        {
            player2.GetComponent<ShipMovement>().enabled = true;
            player2.GetComponent<PlayerShoot>().enabled = true;
            player2.transform.GetChild(0).gameObject.SetActive(true);
        }
  }

  public void GoToMainMenu()
  {
      SceneManager.LoadScene(0);
  }
  
    public void SetWin()
    {
        this.gameState = GameState.Win;
        SwitchGameState();
    }
  
}
