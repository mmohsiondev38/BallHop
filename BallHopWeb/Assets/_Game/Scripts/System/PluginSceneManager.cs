using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PluginSceneManager : Singleton<PluginSceneManager>
{
    public float sceneLoadingDelay = 0;
    public Image fillImage;
    #if USE_PLUGIN
    
    public void OnAdControllerStarted()
    {
        StartCoroutine(LoadScene());
    }
    #else
    public void Start()
    {
        StartCoroutine(LoadScene());
    }
#endif
    IEnumerator LoadScene()
    {
        float time = 0f;
        while(time<sceneLoadingDelay)
        {
            fillImage.fillAmount = time/sceneLoadingDelay;
            time+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene("Game");
    }
}
