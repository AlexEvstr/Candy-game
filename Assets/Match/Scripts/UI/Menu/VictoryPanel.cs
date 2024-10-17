using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Match.Scripts.Audio;
using Assets.Match.Scripts.Gameplay;
using Assets.Match.Scripts.UI.Animations;
using System.Collections;

namespace Assets.Match.Scripts.UI.Menu
{

    public class VictoryPanel : MonoBehaviour
    {

#region Serialized Variables

        [SerializeField] private ButtonAudioEffect _buttonAudioEffect;
        [SerializeField] private AudioEffectsGame _audioEffectsGame;
        [SerializeField] private StarController _starController;
        [SerializeField] private GameMenuAnimation _gameMenuAnimation;
        [SerializeField] private ObjectsAnimation _objectsAnimation;

        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private GameObject _gamePanel;
        [SerializeField] private GameObject[] _starPosition;
        [SerializeField] private GameObject _starPref;

#endregion

        public bool IsVictoryState { get; set; } = false;
        private bool _isOpen = false;

        private void OnEnable()
        {
            _nextLevelButton.onClick.AddListener(ToNextLevel);
            _exitButton.onClick.AddListener(ToStartMenu);
        }

        private void Awake()
        {
            int buildIndex = SceneUtility.GetBuildIndexByScenePath(SceneManager.GetActiveScene().path);

            if (SceneManager.sceneCountInBuildSettings - 1 < buildIndex + 1)         
                _nextLevelButton.interactable = false;           
            else          
                _nextLevelButton.interactable = true;           
        }

        private void ToNextLevel()
        {
            _buttonAudioEffect.PlayClickSound();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("game");
        }

        private async void ToStartMenu()
        {
            try
            {
                _buttonAudioEffect.PlayClickSound();
                await Task.Delay(300);
                SceneManager.LoadScene("menu");
            }
            catch (System.Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        public void VictoryState()
        {
            StartCoroutine(VictoryStateCoroutine());
        }

        private IEnumerator VictoryStateCoroutine()
        {
            yield return new WaitForSeconds(0.5f);
            try
            {
                IsVictoryState = true;

                _gamePanel.SetActive(_isOpen);

                _audioEffectsGame.PlayVictorySound();

                _gameMenuAnimation.ForVictory();

                //CheckNumberOfStars();

                _starController.SaveStarData();
            }
            catch (System.Exception exception)
            {
                Debug.LogError($"Ошибка в VictoryState: {exception.Message}");
            }
        }


        private void CheckNumberOfStars()
        {
            GameObject[] totalStars = new GameObject[3];

            for (int i = 0; i < _starController.NumOfStar; i++)
            {
                totalStars[i] = Instantiate(_starPref, transform.position, Quaternion.identity, _starPosition[i].transform);

                if (i + 1 == 3)
                    _objectsAnimation.Stars(totalStars[i], _starPosition[i].transform.position.x, _starPosition[i].transform.position.y, 3000);
                else if (i + 1 == 2)
                    _objectsAnimation.Stars(totalStars[i], _starPosition[i].transform.position.x, _starPosition[i].transform.position.y, 2000);
                else
                    _objectsAnimation.Stars(totalStars[i], _starPosition[i].transform.position.x, _starPosition[i].transform.position.y, 1000);
            }
        }

        private void OnDisable()
        {
            _nextLevelButton.onClick.RemoveListener(ToNextLevel);
            _exitButton.onClick.RemoveListener(ToStartMenu);
        }

    }
}
