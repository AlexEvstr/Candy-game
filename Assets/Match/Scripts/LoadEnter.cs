using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadEnter : MonoBehaviour
{
    [SerializeField] private Image blueString;
    private float _loadTime = 2.5f;

    private float _currentTime = 0f;

    void Start()
    {
        blueString.fillAmount = 0f;
        StartCoroutine(FillProgressBar());
    }

    IEnumerator FillProgressBar()
    {
        while (_currentTime < _loadTime)
        {
            _currentTime += Time.deltaTime;
            blueString.fillAmount = Mathf.Lerp(0, 1, _currentTime / _loadTime);
            yield return null;
        }
        SceneManager.LoadScene("menu");
    }
}