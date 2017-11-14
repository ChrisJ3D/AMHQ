using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestionBox : MonoBehaviour
{
    [SerializeField]
    private GameObject _optionPrefab;

    private List<UI_QuestionButton> _buttonHandlers = new List<UI_QuestionButton>();
    private Action<int> _callback;

    public void CreateOptions(List<string> allOptions, Action<int> callBack)
    {
        _callback = callBack;
        for(int x = 0; x < allOptions.Count; x++)
        {
            UI_QuestionButton buttonHandler = Instantiate(_optionPrefab).GetComponent<UI_QuestionButton>();
            buttonHandler.transform.SetParent(transform, false);
            buttonHandler.SetText(allOptions[x]);
            buttonHandler.SetValueAndButtonCallBack(x, ButtonCallBack);
            _buttonHandlers.Add(buttonHandler);
        }
    }

    void ButtonCallBack(int value)
    {
        _callback(value);
        ClearList();
    }

    public float CellHeight()
    {
        return GetComponent<GridLayoutGroup>().cellSize.y;
    }

    public void ClearList()
    {
        foreach(UI_QuestionButton handler in _buttonHandlers)
        {
            Destroy(handler.gameObject);
        }
        _buttonHandlers.Clear();
        Destroy(gameObject);
    }
}
