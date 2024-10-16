using UnityEngine;
using TMPro;
using Assets.Match.Scripts.Gameplay;

namespace Assets.Match.Scripts.UI.View
{

    public class MoveView : MonoBehaviour       
    {
        [SerializeField] private TextMeshPro _move;
        [SerializeField] private MoveController _moveController;

        private void OnEnable()
        {
            _moveController.MovesChange += MovementView;
        }

        private void Start()
        {
            MovementView();
        }

        public void MovementView()
        {
            if(_moveController.TotalMove > 0)
            {
                _move.text = $"{_moveController.TotalMove}";  
            }
            else
            {
                _move.text = "0";
            }     
        }

        private void OnDisable()
        {
            _moveController.MovesChange -= MovementView;
        }

    }
}
