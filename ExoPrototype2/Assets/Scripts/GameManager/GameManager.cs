using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private GameObject player1;
    private GameObject player2;
    
    [SerializeField] private Canvas inGameUI;
    [SerializeField] private Canvas winCanvas;
    [SerializeField] private Canvas loseCanvas;
    [SerializeField] private Canvas pauseCanvas;

    [SerializeField] WorldManager world;
    [SerializeField] PerlinNoiseGen noiseGen;

    public bool debug;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (!debug)
        {
            StartCoroutine(noiseGen.Generate());
            if(noiseGen.isDone)world.InitializeGrid();
        }

        PlayerInputManager.instance.onPlayerJoined += GetSecondPlayer;
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
              DisableCanvas(pauseCanvas);
              ContinueGame();
              ShowCanvas(inGameUI);
              break;
          case GameState.Race:
              DisableCanvas(pauseCanvas);
              ContinueGame();
              ShowCanvas(inGameUI);
              break;
          case GameState.Win:
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(winCanvas);
              break;
          case GameState.Lose:
              DisableCanvas(inGameUI);
              PauseGame();
              ShowCanvas(loseCanvas);
              break;
          case GameState.Paused:
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
  
}
