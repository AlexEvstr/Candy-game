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
            NumOfStar = minStar;
            //StarChange?.Invoke();
            _victoryPanel.VictoryState();
            Debug.Log("win");
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
