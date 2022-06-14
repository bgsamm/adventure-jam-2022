using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Start() { }

    public void LoadMenuScene() {
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadCutsceneScene() {
        SceneManager.LoadScene("CutsceneScene");
    }

    public void LoadLetterScene() {
        SceneManager.LoadScene("LetterScene");
    }

    public void LoadGardenScene() {
        SceneManager.LoadScene("GardenScene");
    }

    public void LoadShopScene() {
        SceneManager.LoadScene("ShopScene");
    }

    public void LoadTreeScene() {
        SceneManager.LoadScene("TreeScene");
    }
}
