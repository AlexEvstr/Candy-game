using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _menu; 
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private Image _candy1Image;
    [SerializeField] private Image _menuBarImage;
    [SerializeField] private Sprite[] _candySprites;
    [SerializeField] private Sprite[] _barSprites;
    [SerializeField] private TMP_Text _currentCandy1IndexText;
    [SerializeField] private GameObject _shine;
    [SerializeField] private GameObject _winner;

    private void Start()
    {
        int currentCandy1Index = PlayerPrefs.GetInt("Candy1Index", 0);
        if (currentCandy1Index >= 10)
        {
            _shine.SetActive(true);
            _winner.SetActive(true);
            currentCandy1Index = 10;
        }
        _candy1Image.sprite = _candySprites[currentCandy1Index];
        _menuBarImage.sprite = _barSprites[currentCandy1Index];
        _currentCandy1IndexText.text = $"{currentCandy1Index}/10";

        
    }

    public void PlayGame()
    {
        StartCoroutine(PressPlayBtn());
    }

    private IEnumerator PressPlayBtn()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("game");
    }

    public void OpenTutorial()
    {
        _menu.SetActive(false);
        _tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        _tutorial.SetActive(false);
        _menu.SetActive(true);
    }
}