using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private int _currentMoney;
    [SerializeField] private Text _text;

    private void Awake()
    {
        _text.text = _currentMoney.ToString();
    }

    public int Get()
        => _currentMoney;

    public void Add(int amount)
    {
        _currentMoney += amount;
        _text.text = _currentMoney.ToString();
    }

    public void Substract(int amount)
    {
        int tempValue = _currentMoney - amount;
        if (tempValue < 0)
        {
            _currentMoney = 0;
            _text.text = _currentMoney.ToString();
            return;
        }

        _currentMoney -= amount;
        _text.text = _currentMoney.ToString();
    }
}
