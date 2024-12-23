using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public AudioClip gameSound;
    public AudioClip waitingSound;
    public AudioSource playerSound;
    // public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public int objectsMissed;
    public GameObject gameOverUI;
    public GameObject titleScreen;

    private int _score;
    private float _spawnRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        playerSound = GetComponent<AudioSource>();
        playerSound.clip = waitingSound;
        playerSound.loop = true;
        playerSound.Play();
    }

    public void StartGame(int difficulty)
    {
        objectsMissed = 0;
        _spawnRate /= difficulty;
        isGameActive = true;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        playerSound.Stop();
        titleScreen.gameObject.SetActive(false);
        playerSound.clip = gameSound;
        playerSound.loop = true;
        playerSound.Play();
    }

    void Update()
    {
        if(_score < 0)
        {
            GameOver();
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive && objectsMissed < 5)
        {
            yield return new WaitForSeconds(_spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        scoreText.text = "Score: " + _score;
    }

    public void RestartGame()
    {
        Debug.Log("Restart button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        isGameActive = false;
        // gameOverText.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(true);
    }

    public int getScore()
    {
        return _score;
    }


}
