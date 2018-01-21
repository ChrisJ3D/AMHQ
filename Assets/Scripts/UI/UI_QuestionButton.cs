using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestionButton : MonoBehaviour
{
    public void SetText(string option)
    {
        GetComponentInChildren<Text>().text = option;
    }

    public void SetValueAndButtonCallBack(int value, Action<int> buttonCallBack)
    {
        GetComponent<Button>().onClick.AddListener(delegate { buttonCallBack(value); });
    }
}
