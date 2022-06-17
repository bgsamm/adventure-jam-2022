using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private AudioManager audioManager => ResourceLocator.instance.AudioManager;
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
        // TODO: I reeeeeally don't like this, but for now I guess it works
        audioManager.SetGlobalParameter("NPC IO", 0);
        audioManager.SetGlobalParameter("NPC SWITCH", 0);
        LoadScene(clock.CurrentAct.scene);
    }

    public void LoadBarterScene() {
        SceneManager.LoadScene("BarterScene");
    }

    public void LoadTreeScene() {
        SceneManager.LoadScene("TreeScene");
    }
}
