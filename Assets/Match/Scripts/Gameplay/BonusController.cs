using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Assets.Match.Scripts.Audio;
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
        [SerializeField] private GameController _gameController;

        [SerializeField] private GameObject _explosion;

        private Camera _camera;
        private bool isRocketActive = false;
        private bool isBombActive = false;

        private List<BlockController> highlightedBlocks = new List<BlockController>();

        private void Awake()
        {
            _camera = Camera.main;
        }

        public bool IsRocketModeActive() => isRocketActive;
        public bool IsBombModeActive() => isBombActive;

        public void ToggleRocketMode()
        {
            isRocketActive = !isRocketActive;
            isBombActive = false;
            ClearHighlightedBlocks();
        }

        public void ToggleBombMode()
        {
            isBombActive = !isBombActive;
            isRocketActive = false;
            ClearHighlightedBlocks();
        }

        public void HandleBonusSelection(BlockController selectedBlock)
        {
            if (isRocketActive)
            {
                ActivateRocketBonus(selectedBlock);
                _gameController.BonusForMoves();
                isRocketActive = false;
            }
            else if (isBombActive)
            {
                ActivateBombBonus(selectedBlock);
                _gameController.BonusForMoves();
                isBombActive = false;
            }
            ClearHighlightedBlocks();
        }

        public void HighlightBlocks(BlockController selectedBlock)
        {
            ClearHighlightedBlocks();
            if (isRocketActive)
            {
                highlightedBlocks = ActivateRocket(_board.Blocks, selectedBlock.GetX);
            }
            else if (isBombActive)
            {
                highlightedBlocks = ActivateBomb(_board.Blocks, selectedBlock.GetX, selectedBlock.GetY);
            }

            foreach (var block in highlightedBlocks)
            {
                block.SelectTile();
            }
        }

        private void ActivateRocketBonus(BlockController selectedBlock)
        {
            foreach (BlockController block in ActivateRocket(_board.Blocks, selectedBlock.GetX))
            {
                _boardManager.DestroyBlock(block);
                GameObject explosion = Instantiate(_explosion, block.transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
            }

            _audioEffectsGame.PlayRocketSound();
            _camera.transform.DOShakePosition(0.8f, new Vector3(0f, 0.08f, -0.01f), 6, 1, false, true)
                             .OnComplete(() => _boardManager.SetupCamera());
            _boardManager.SearchEmptyTile();
        }

        private void ActivateBombBonus(BlockController selectedBlock)
        {
            foreach (BlockController block in ActivateBomb(_board.Blocks, selectedBlock.GetX, selectedBlock.GetY))
            {
                _boardManager.DestroyBlock(block);
                GameObject explosion = Instantiate(_explosion, block.transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
            }

            _audioEffectsGame.PlayBombSound();
            _camera.transform.DOShakePosition(1f, new Vector3(0f, 0.08f, -0.01f), 6, 1, false, true)
                             .OnComplete(() => _boardManager.SetupCamera());
            _boardManager.SearchEmptyTile();
        }

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

        private List<BlockController> ActivateBomb(Block[,] tiles, int x, int y, int offset = 1)
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

        public bool IsWithinBounds(object[,] array2D, int x, int y)
        {
            return (x >= 0 && x < array2D.GetLength(0) && y >= 0 && y < array2D.GetLength(1));
        }

        private void ClearHighlightedBlocks()
        {
            foreach (var block in highlightedBlocks)
            {
                block.Unselect();
            }
            highlightedBlocks.Clear();
        }
    }
}