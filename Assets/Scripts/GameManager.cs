using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int enemiesAlive; //numero de enemigos sin vida 
    public int round; //numero de rondas
    public GameObject[] spawnPoints; //puntos de instanciamiento del zombie
    public GameObject enemyPrefab; //prefab del zombie
    public TextMeshProUGUI roundText; //Texto de las rondas actuales
    public TextMeshProUGUI roundsSurvivedText; //Texto de las rondas totales

    public GameObject gameOverPanel; //canvas de GameOver
    public GameObject pausePanel; //canvas de pausa

    public Animator fadePanelAnimator; //animacion para cabiar de escena

    public bool isPaused; //bandera de pausa
    public bool isGameOver; //bandera de GameOver

    public static GameManager sharedInstance;

    //Paneles aparecen y desaparecen

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        isPaused = false; //Inicializas una bandera de que el menu game over esta inactivo
        isGameOver = false;
        Time.timeScale = 1; //Tiempo de transicion entre una escena y otra 
    }

    void Update()
    {
        if (enemiesAlive == 0) //Si no hay enemigos
        {
            round++; //sumamos ronda 
            roundText.text = $"Round: {round}"; //pasamos el numero de ronda del texto transformado a entero y se lo asignamos a la ronda 
            NextWave(round);
        }

        if (Input.GetKeyDown(KeyCode.Escape) || OVRInput.GetDown(OVRInput.Button.Four)) //Boton de pausa
        {
            Pause();
        }
    }

    public void NextWave(int round)
    {
        for (int i = 0; i < round; i++)
        {
            int randomPos = Random.Range(0, spawnPoints.Length);
            GameObject spawnPoint = spawnPoints[randomPos];

            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemyInstance.GetComponent<EnemyManager>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
    }

    public void GameOver() //Funcion para activar CANVAS de gameover 
    {

        gameOverPanel.SetActive(true);
        roundsSurvivedText.text = round.ToString();
        
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        isGameOver = true;
    }

    public void RestartGame() //Funcion del BOTON para que al perder podamos volver a repetir las rondas del juego
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu() //Funcion del BOTON para regresar al menu principal
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        fadePanelAnimator.SetTrigger("FadeIn");
        Invoke("LoadMainMenuScene", 0.5f);
    }

    public void LoadMainMenuScene() //Funcion que se manda a llamar desde el BOTON para regresar al menu principal con un cierto retardo
    {
        SceneManager.LoadScene(0);
    }

    public void Pause() //Funcion para activar CANVAS de pausa
    {

        pausePanel.SetActive(true);
        AudioListener.volume = 0;
        Time.timeScale = 0;
        //Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }
    
    public void Resume() //Funcion del BOTON para que despues de presionar pausa regresar al mismo punto donde nos quedamos 
    {
        pausePanel.SetActive(false);
        AudioListener.volume = 1;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }
}
