using UnityEngine;

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

    // public void for each scene, call sound manager here
}
