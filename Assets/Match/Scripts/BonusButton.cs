using Assets.Match.Scripts.Gameplay;
using UnityEngine;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private BonusController _bonusController; // Ссылка на BonusController

    // Кнопка для активации/деактивации режима ракеты (шоколадка)
    public void OnRocketButtonPressed()
    {
        // Проверяем текущее состояние и переключаем его
        if (_bonusController.IsRocketModeActive())
        {
            _bonusController.DeactivateRocketMode();  // Деактивируем режим
        }
        else
        {
            _bonusController.ActivateRocketMode();  // Активируем режим
        }
    }

    // Кнопка для активации/деактивации режима бомбы
    public void OnBombButtonPressed()
    {
        // Проверяем текущее состояние и переключаем его
        if (_bonusController.IsBombModeActive())
        {
            _bonusController.DeactivateBombMode();  // Деактивируем режим
        }
        else
        {
            _bonusController.ActivateBombMode();  // Активируем режим
        }
    }
}
