using UnityEngine;

namespace Assets.Match.Scripts.Gameplay
{

    public class GameController : MonoBehaviour
    {

#region Serialized Variables

        [SerializeField] private ScoreController _scoreController;
        [SerializeField] private GoalController _goalController;
        [SerializeField] private MoveController _moveController;
        [SerializeField] private StarController _starController;

#endregion

        public void CountingScore(int totalBlock)
        {
            float score;
            if(totalBlock < 3)
            {
                _scoreController.ChangeScore(_scoreController.Counter);
            }
            if (totalBlock >= 3 && totalBlock < 5)
            {
                score = _scoreController.ScorePerBlock * totalBlock;
                _scoreController.ChangeScore(_scoreController.Counter + (int)score);
            }
            if(totalBlock >= 5 && totalBlock <= 7)
            {
                score = _scoreController.ScorePerBlock * totalBlock * _scoreController.ScoreMultiplier;
                _scoreController.ChangeScore(_scoreController.Counter + (int)score);
            }
            if(totalBlock >= 8)
            {                
                score = _scoreController.ScorePerBlock * totalBlock * _scoreController.ScoreMultiplier * _scoreController.ScoreMultiplier;
                _scoreController.ChangeScore(_scoreController.Counter + (int)score);
            }
        }

        public void BonusForMoves()
        {
            if(_moveController.TotalMove > 0)
            {
                _scoreController.ChangeScore(_scoreController.Counter + _moveController.TotalMove * 100);
            }
        }

        public void Restart()
        {
            _scoreController.ChangeScore(0);           
            LevelControl();
        }

        public void LevelControl()
        {
            _goalController.ResetGoals();
            _moveController.ResetMoves();
        }
    }
}