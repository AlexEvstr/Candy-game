using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using Assets.Match.Scripts.Audio;
using Assets.Match.Scripts.Enum;
using Assets.Match.Scripts.Models;
using Assets.Match.Scripts.UI.Animations;
using Assets.Match.Scripts.ScriptableObjects;

namespace Assets.Match.Scripts.Gameplay
{

    public class BonusController : MonoBehaviour
    {
        [SerializeField] private BoardManager _boardManager;
        [SerializeField] private AudioEffectsGame _audioEffectsGame;
        [SerializeField] private ObjectsAnimation _objectsAnimation;
        [SerializeField] private BoardScriptableObject _board;

        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private GameObject _rocketPrefab;  // Ракета = шоколадка

        [SerializeField] private GameObject _explosion;

        private Camera _camera;

        // Флаги для отслеживания активированных бонусов
        private bool isRocketActive = false;
        private bool isBombActive = false;

        private void Awake()
        {
            _camera = Camera.main;
        }

        // Активация режима ракеты (шоколадки)
        public void ActivateRocketMode()
        {
            isRocketActive = true;
            Debug.Log("Режим ракеты активирован. Выберите конфету.");
        }

        // Активация режима бомбы
        public void ActivateBombMode()
        {
            isBombActive = true;
            Debug.Log("Режим бомбы активирован. Выберите конфету.");
        }

        // Метод для обработки выбора конфеты
        public void HandleBonusSelection(BlockController selectedBlock)
        {
            Debug.Log("noneeee");
            if (isRocketActive)
            {
                Debug.Log("rocket");
                ActivateRocketBonus(selectedBlock);
                isRocketActive = false; // Сбрасываем флаг
            }
            else if (isBombActive)
            {
                Debug.Log("bomb");
                ActivateBombBonus(selectedBlock);
                isBombActive = false; // Сбрасываем флаг
            }
        }

        // Метод для активации бонуса "ракета" (шоколадка)
        private void ActivateRocketBonus(BlockController selectedBlock)
        {
            Debug.Log($"Активирован бонус ракета (шоколадка) для столбца {selectedBlock.GetX}");

            foreach (BlockController block in ActivateRocket(_board.Blocks, selectedBlock.GetX))
            {
                _boardManager.DestroyBlock(block);
                GameObject explosion = Instantiate(_explosion, block.transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
            }

            _audioEffectsGame.PlayRocketSound();
            _camera.transform.DOShakePosition(0.8f, new Vector3(0f, 0.08f, -0.01f), 6, 1, false, true)
                             .OnComplete(() => _boardManager.SetupCamera());

            _boardManager.SearchEmptyTile(); // Обновляем доску
        }

        // Метод для активации бонуса "бомба"
        private void ActivateBombBonus(BlockController selectedBlock)
        {
            Debug.Log($"Активирован бонус бомба для области вокруг конфеты ({selectedBlock.GetX}, {selectedBlock.GetY})");

            foreach (BlockController block in ActivateBomb(_board.Blocks, selectedBlock.GetX, selectedBlock.GetY))
            {
                _boardManager.DestroyBlock(block);
                GameObject explosion = Instantiate(_explosion, block.transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
            }

            _audioEffectsGame.PlayBombSound();
            _camera.transform.DOShakePosition(1f, new Vector3(0f, 0.08f, -0.01f), 6, 1, false, true)
                             .OnComplete(() => _boardManager.SetupCamera());

            _boardManager.SearchEmptyTile(); // Обновляем доску
        }

        // Логика для активации ракет (выбор всех блоков в столбце)
        private List<BlockController> ActivateRocket(Block[,] allPieces, int column)
        {
            List<BlockController> gamePieces = new List<BlockController>();

            for (int y = 0; y < allPieces.GetLength(1); y++)
            {
                if (allPieces[column, y] != null)
                {
                    gamePieces.Add(allPieces[column, y].GetComponent<BlockController>());
                }
            }

            return gamePieces;
        }

        // Логика для активации бомбы (взрыв вокруг выбранной конфеты)
        private List<BlockController> ActivateBomb(Block[,] tiles, int x, int y, int offset = 2)
        {
            List<BlockController> gamePieces = new List<BlockController>();

            for (int i = x - offset; i <= x + offset; i++)
            {
                for (int j = y - offset; j <= y + offset; j++)
                {
                    if (IsWithinBounds(tiles, i, j))
                    {
                        gamePieces.Add(tiles[i, j].GetComponent<BlockController>());
                    }
                }
            }

            return gamePieces;
        }

        // Проверка на выход за границы доски
        public bool IsWithinBounds(object[,] array2D, int x, int y)
        {
            return (x >= 0 && x < array2D.GetLength(0) && y >= 0 && y < array2D.GetLength(1));
        }
    }


}
