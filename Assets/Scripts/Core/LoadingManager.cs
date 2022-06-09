using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [Header("Sound Manager")]
    [SerializeField] private SoundManager sound;

    public static LoadingManager instance { get; private set; }

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
