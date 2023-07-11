using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoney : MonoBehaviour, IDataPersistence
{
    private TextMeshProUGUI moneyText;
    public int money;

    public PlayerPositionSO startingPosition;

    private void Start()
    {
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        if (startingPosition.transittedScene || startingPosition.playerDead)
        {
            money = GameManager.instance.money;
        }
    }

    private void Update()
    {
        GameManager.instance.money = money;
        moneyText.text = "GPA: " + money;
    }

    public void LoadData(GameData data)
    {
        money = data.money;
    }

    public void SaveData(GameData data)
    {
        data.money = money;
    }
}
