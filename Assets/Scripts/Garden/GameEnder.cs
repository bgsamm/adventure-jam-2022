using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//an act 5-specific scene that just ends the game when you've watered all the trees
public class GameEnder : MonoBehaviour
{
    public int waterCount = 9;

    // Update is called once per frame
    void Update()
    {
        if (waterCount == 0)
        {
            StartCoroutine(EndGame());
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        ResourceLocator.instance.SceneLoader.LoadMenuScene();
    }
}
