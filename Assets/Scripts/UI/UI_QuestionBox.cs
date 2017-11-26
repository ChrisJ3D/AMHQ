using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestionBox : MonoBehaviour
{
    [SerializeField]
    private GameObject _questionButtonPrefab;

    private List<UI_QuestionButton> _questionButtonList = new List<UI_QuestionButton>();
    private Action<int> _callback;

    public void CreateOptions(List<string> options, Action<int> callBack)
    {
        _callback = callBack;
        for(int i = 0; i < options.Count; i++)  {
            UI_QuestionButton option = Instantiate(_questionButtonPrefab).GetComponent<UI_QuestionButton>();
            option.transform.SetParent(transform, false);
            option.SetText(options[i]);
            option.SetValueAndButtonCallBack(i, ButtonCallBack);
            _questionButtonList.Add(option);
        }
    }

    public float CellHeight() {
        return GetComponent<GridLayoutGroup>().cellSize.y;
    }

    private void ButtonCallBack(int value) {
        _callback(value);
        ClearList();
    }

    private void ClearList() {
        foreach(UI_QuestionButton button in _questionButtonList) {
            Destroy(button.gameObject);
        }
        _questionButtonList.Clear();
        this.gameObject.SetActive(false);
    }
}
