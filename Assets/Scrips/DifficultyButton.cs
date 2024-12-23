using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{

    private Button _button;
    private GameManager _gameManager;

    public int difficulty;
    public AudioClip buttonSFX;
    public AudioSource playerAudio;
    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetDifficulty);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void SetDifficulty()
    {
        _gameManager.StartGame(difficulty);
        Debug.Log(gameObject.name + " was clicked");
        playerAudio.PlayOneShot(buttonSFX, 10);
    }
}
