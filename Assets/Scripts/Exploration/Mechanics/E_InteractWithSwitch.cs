using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum e_switchType
{
    STANDART,
    IMPULSION,
    TIMER,
    COLOR
}

public class E_InteractWithSwitch : MonoBehaviour
{
    [SerializeField] private e_switchType e_switch;
    [SerializeField] private float timer;

    [SerializeField] private UnityEvent onEnable;
    [SerializeField] private UnityEvent onDisable;

    private int _isActive;

    private void OnEnableSwitch()
    {
        if (_isActive == 0)
        {
            onEnable.Invoke();
            transform.GetChild(0).transform.localPosition = new Vector3(0, -0.1f, 0);
        }
        _isActive += 1;
    }

    private void OnDisableSwitch()
    {
        _isActive -= 1;
        if (_isActive == 0)
        {
            transform.GetChild(0).transform.localPosition = Vector3.zero;
            onDisable.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnableSwitch();
    }

    private void OnTriggerExit(Collider other)
    {
        if (e_switch == e_switchType.IMPULSION)
            OnDisableSwitch();
        else if (e_switch == e_switchType.TIMER)
            Invoke("OnDisableSwitch", timer);
    }
}
