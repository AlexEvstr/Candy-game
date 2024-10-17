using System;
using UnityEngine;
using Assets.Match.Scripts.UI.Menu;
#if (UNITY_EDITOR)
using Assets.Match.Scripts.EditorChanges;
#endif
using Assets.Match.Scripts.ScriptableObjects;


namespace Assets.Match.Scripts.Gameplay
{

    public class StarController : MonoBehaviour
    {
#if (UNITY_EDITOR)
        [RequiredField]
#endif
        [SerializeField] private LevelsConfigurationScriptable _levelConfig;
        [SerializeField] private VictoryPanel _victoryPanel;

        public int NumOfStar { get; set; }

        public event Action StarChange;

        private int _minStar;

        private void Awake()
        {
            NumOfStar = _minStar;
        }

        public void StarIncrease(int minStar)
        {
            int currentCandyIndex = PlayerPrefs.GetInt("Candy1Index", 0);
            currentCandyIndex++;
            PlayerPrefs.SetInt("Candy1Index", currentCandyIndex);

            NumOfStar = minStar;
            _victoryPanel.VictoryState();
        }

        public void ResetStar()
        {
            _minStar = 0;
            NumOfStar = _minStar;
            StarChange?.Invoke();
        }

        public void SaveStarData()
        {
            _levelConfig.level.totalStar = NumOfStar;
        }

    }
}
