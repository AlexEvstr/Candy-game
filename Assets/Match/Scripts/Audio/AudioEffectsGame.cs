using System;
using UnityEngine;

namespace Assets.Match.Scripts.Audio
{

    public class AudioEffectsGame : MonoBehaviour
    {

#region Serialized Variables

        [SerializeField] private AudioSource _dropSound;
        [SerializeField] private AudioSource _loseSound;
        [SerializeField] private AudioSource _victorySound;
        [SerializeField] private AudioSource _selectSound;
        [SerializeField] private AudioSource _bombSound;
        [SerializeField] private AudioSource _rocketSound;
        [SerializeField] private AudioSource _bonusSound;
        [SerializeField] private AudioAndVibroUI _audioAndVibroUI;

#endregion

        public void PlayDropSound()
        {
            _dropSound.Play();
            _audioAndVibroUI.PlaySmallVibro();
        }

        public void PlayLoseSound()
        {
            _loseSound.Play();
            _audioAndVibroUI.PlayErrorVibro();
        }

        public void PlayVictorySound()
        {
            _victorySound.Play();
            _audioAndVibroUI.PlayBigVibro();
        }

        public void PlaySelectSound()
        {
            _selectSound.Play();
            _audioAndVibroUI.PlaySmallVibro();
        }

        public void PlayBombSound()
        {
            _bombSound.Play();
            _audioAndVibroUI.PlayBigVibro();
        }

        public void PlayRocketSound()
        {
            _rocketSound.Play();
            _audioAndVibroUI.PlayBigVibro();
        }

        public void PlayBonusSound()
        {
            _bonusSound.Play();
            _audioAndVibroUI.PlayMediumVibro();
        }

        internal void PlayBonusActivationSound()
        {
            throw new NotImplementedException();
        }
    }
}