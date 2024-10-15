using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(PressPlayBtn());
    }

    private IEnumerator PressPlayBtn()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("game");
    }
}