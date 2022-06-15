using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Clock clock => ResourceLocator.instance.Clock;

    public void LoadScene(SceneAsset scene) {
        SceneManager.LoadScene(scene.name);
    }

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
        LoadScene(clock.CurrentAct.scene);
    }

    public void LoadBarterScene() {
        SceneManager.LoadScene("BarterScene");
    }

    public void LoadTreeScene() {
        SceneManager.LoadScene("TreeScene");
    }
}
