using System.Net.Http.Headers;
using System.Security.Cryptography;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Target : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public int pointValue = 0;
    public AudioClip deathSFX;
    private AudioSource _fruitAudio;
    private GameManager _gameManager;
    private Rigidbody _targetRB;
    private float _minSpeed = 14;
    private float _maxSpeed = 17;
    private float _maxTorque = 10;
    private float _xRange = 4;
    private float _ySpawnPos = -4;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _fruitAudio = GameObject.Find("Game Manager").GetComponent<AudioSource>();
        _targetRB = GetComponent<Rigidbody>();
        
        _targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRB.AddTorque(RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GarbageCollector"))
        {
            Destroy(gameObject);
            if(!gameObject.CompareTag("Deadly"))
                _gameManager.objectsMissed++;
            if(_gameManager.objectsMissed >= 5)
                _gameManager.GameOver();
        }
    }

    private void OnMouseDown()
    {
        if (_gameManager.getScore() < 0)
        {
            _gameManager.GameOver();
        } 
        else
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            _gameManager.UpdateScore(pointValue);
            _fruitAudio.PlayOneShot(deathSFX, 1);
        }
    }

    private Vector3 RandomForce() 
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    private Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-_maxTorque, _maxTorque), Random.Range(-_maxTorque, _maxTorque), Random.Range(-_maxTorque, _maxTorque));
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos, 0);
    }
}
