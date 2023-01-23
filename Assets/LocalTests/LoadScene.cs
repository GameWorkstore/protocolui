using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public float LoadAfter = 1;
    public int SceneIndex = 0;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(LoadAfter);
        SceneManager.LoadScene(SceneIndex);
    }
}
