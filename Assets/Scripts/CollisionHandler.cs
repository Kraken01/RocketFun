using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeToReload = 0f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem succesParticles;
   
    AudioSource audiosource;

    bool isAlive = false;
    bool collisionDisabled = false;
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys(); 
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isAlive || collisionDisabled) { return; }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartLevelCompleteSequence();
                break;
            default: 
                StartCrashSequence();
                break;
        }
    }
    
    void StartCrashSequence()
    {
        isAlive = true;
        audiosource.Stop();
        audiosource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",timeToReload);  
    }

    void StartLevelCompleteSequence()
    {
        isAlive = true;
        audiosource.Stop();
        audiosource.PlayOneShot(successSound);
        succesParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", timeToReload);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//returns current index in the build setting
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)//it counts the total no. of scenes in the build setting
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
