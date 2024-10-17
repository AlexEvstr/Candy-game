using Assets.Match.Scripts.Gameplay;
using UnityEngine;

public class BonusButton : MonoBehaviour
{
    [SerializeField] private BonusController _bonusController; // Изменяем ссылку на BonusController вместо BoardManager

    // Кнопка для активации режима ракеты (шоколадка)
    public void OnRocketButtonPressed()
    {
        _bonusController.ActivateRocketMode(); // Активируем режим ракеты
    }

    // Кнопка для активации режима бомбы
    public void OnBombButtonPressed()
    {
        _bonusController.ActivateBombMode(); // Активируем режим бомбы
    }
}
