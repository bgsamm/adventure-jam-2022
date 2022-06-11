using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance { get; private set; }
    private Clock clock;
    private SoundManager sound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        clock = GetComponent<Clock>();
        sound = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    public void MenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void GardenScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ShopScene()
    {
        SceneManager.LoadScene(2);
    }

    public void TreeScene()
    {
        SceneManager.LoadScene(3);
    }
}
