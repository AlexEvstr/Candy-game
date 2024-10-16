using UnityEngine;
using DG.Tweening;

namespace Assets.Match.Scripts.UI.Animations
{

    public class GameMenuAnimation : MonoBehaviour
    {
#region Serialized Variables

        [SerializeField] private RectTransform _victoryPanelTransform;
        [SerializeField] private RectTransform _overPanelTransform;
        [SerializeField] private RectTransform _boardTransform;

#endregion

        private Sequence _sequence;

        private void Awake()
        {
            _sequence = DOTween.Sequence();
        }

        public void ForRestartAndContinue()
        {
            _sequence.Append(_overPanelTransform.DOAnchorPosX(620, 1, true));          
            _sequence.Append(_boardTransform.DOAnchorPosY(0, 2, false));
        }

        public void ForVictory()
        {
            _sequence.Insert(2f,_victoryPanelTransform.DOAnchorPosX(0, 1, true));
            _sequence.Append(_boardTransform.DOAnchorPosY(50, 1, false));
        }

        public void ForGameOver()
        {
            _sequence.Insert(2f,_overPanelTransform.DOAnchorPosX(0, 1, true));
            _sequence.Append(_boardTransform.DOAnchorPosY(50, 1, false));
        }   

    }
}