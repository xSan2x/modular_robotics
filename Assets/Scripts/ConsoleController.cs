using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleController : MonoBehaviour
{
    [SerializeField] private GameObject _consoleUI;
    [SerializeField] private GameObject _consoleInputField;
    [SerializeField] private GameObject _consoleTextArea;

    [SerializeField] private GameObject _textPrefab;

    public bool _isInConsole;
    public static ConsoleController _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RTSInputController.OnConsoleInput += ToggleConsole;
    }

    private void ToggleConsole()
    {
        _consoleUI.SetActive(!_consoleUI.activeSelf);
        _isInConsole = _consoleUI.activeSelf;
    }

    public void ActivateCommand()
    {
        string command = _consoleInputField.GetComponent<TMP_InputField>().text;
        string[] strings = command.Split(' ');
        switch(strings[0])
        {
            case "help":
                AddRow("Available commands: help, clear, exit");
                _consoleInputField.GetComponent<TMP_InputField>().text = "";
                break;
            case "clear":
                ClearConsole();
                _consoleInputField.GetComponent<TMP_InputField>().text = "";
                break;
            case "exit":
                Application.Quit();
                break;
            default:
                AddRow("Unknown command: " + strings[0]);
                break;
        }
    }

    private void ClearConsole()
    {
        for(int i=0;i< _consoleTextArea.transform.childCount; i++)
        {
            Destroy(_consoleTextArea.transform.GetChild(i).gameObject);
        }
    }

    private void AddRow(string row)
    {
        GameObject newRow = Instantiate(_textPrefab, _consoleTextArea.transform);
        newRow.GetComponent<TMP_Text>().text = row;
    }
}
