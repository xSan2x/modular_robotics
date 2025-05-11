using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RTSInputController : MonoBehaviour
{
    public static event Action OnChooseInput;
    public static event Action OnChooseCanceled;
    public static event Action OnActInput;
    public static event Action OnPauseInput;
    public static event Action OnControlInput;
    public static event Action OnControlInputCanceled;
    public static event Action OnSpeedInput;
    public static event Action OnConsoleInput;
    public static event Action OnSpeedInputCanceled;
    public static event Action<Vector2> OnMoveInput;

    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private string _mapRTSName;
    [SerializeField] private string _chooseRTSName;
    [SerializeField] private string _actRTSName;
    [SerializeField] private string _pauseName;
    [SerializeField] private string _controlName;
    [SerializeField] private string _speedName;
    [SerializeField] private string _moveName;
    [SerializeField] private string _consoleName;

    InputAction _chooseAction;
    InputAction _actAction;
    InputAction _pauseAction;
    InputAction _controlAction;
    InputAction _speedAction;
    InputAction _moveAction;
    InputAction _consoleAction;

    private InputActionMap _RTSActionMap;

    private void OnEnable()
    {
        _inputActionAsset.Enable();
        _RTSActionMap = _inputActionAsset.FindActionMap(_mapRTSName);
        _chooseAction = _RTSActionMap[_chooseRTSName];
        _actAction = _RTSActionMap[_actRTSName];
        _pauseAction = _RTSActionMap[_pauseName];
        _controlAction = _RTSActionMap[_controlName];
        _speedAction = _RTSActionMap[_speedName];
        _moveAction = _RTSActionMap[_moveName];
        _consoleAction = _RTSActionMap[_consoleName];

        _chooseAction.performed += ChoosePerformedHandler;
        _chooseAction.canceled += ChooseCanceledHandler;

        _actAction.performed += ActPerformedHandler;

        _pauseAction.performed += PausePerformedHandler;

        _controlAction.performed += ControlPerformedHandler;
        _controlAction.canceled += ControlCanceledHandler;

        _speedAction.performed += SpeedPerformedHandler;
        _speedAction.canceled += SpeedCanceledHandler;

        _moveAction.performed += MovePerformedHandler;
        _moveAction.canceled += MoveCanceledHandler;

        _consoleAction.performed += ConsolePerformedHandler;
    }

    private void ConsolePerformedHandler(InputAction.CallbackContext context)
    {
        OnConsoleInput?.Invoke();
    }

    private void MoveCanceledHandler(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }

    private void MovePerformedHandler(InputAction.CallbackContext context)
    {
        OnMoveInput?.Invoke(context.ReadValue<Vector2>());
    }

    private void SpeedCanceledHandler(InputAction.CallbackContext context)
    {
        OnSpeedInputCanceled?.Invoke();
    }

    private void SpeedPerformedHandler(InputAction.CallbackContext context)
    {
        OnSpeedInput?.Invoke();
    }

    private void ChooseCanceledHandler(InputAction.CallbackContext context)
    {
        OnChooseCanceled?.Invoke();
    }

    private void ControlCanceledHandler(InputAction.CallbackContext context)
    {
        OnControlInputCanceled?.Invoke();
    }

    private void ControlPerformedHandler(InputAction.CallbackContext context)
    {
        OnControlInput?.Invoke();
    }

    private void OnDestroy()
    {
        _chooseAction.performed -= ChoosePerformedHandler;
        _actAction.performed -= ActPerformedHandler;
        _pauseAction.performed -= PausePerformedHandler;

        OnChooseInput = null;
        OnActInput = null;
        OnPauseInput = null;
    }

    private void PausePerformedHandler(InputAction.CallbackContext context)
    {
        OnPauseInput?.Invoke();
    }

    private void ChoosePerformedHandler(InputAction.CallbackContext context)
    {
        OnChooseInput?.Invoke();
    }
    private void ActPerformedHandler(InputAction.CallbackContext context)
    {
        OnActInput?.Invoke();
    }
}
