using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFading : MonoBehaviour
{
    public Image blackImage;
    public AnimationCurve fadeCurve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToAnotherScene(int buildIndex)
    {
        StartCoroutine(FadeOut(buildIndex));
    }
    
    /// <summary>
    /// Coroutine that creates a fading in effect
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        var temp = 1f;

        while (temp > 0f)
        {
            temp -= Time.deltaTime;
            var x = fadeCurve.Evaluate(temp);
            blackImage.color = new Color(0f,0f,0f,x);
            yield return 0;
        }
    }
    
    /// <summary>
    /// Coroutine that creates a fading out effect
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut(int buildIndex)
    {
        var temp = 0f;

        while (temp < 1f)
        {
            temp += Time.deltaTime;
            var x = fadeCurve.Evaluate(temp);
            blackImage.color = new Color(0f,0f,0f,x);
            yield return 0;
        }

        SceneManager.LoadScene(buildIndex);
    }
}
