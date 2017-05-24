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

public class E_Switch : MonoBehaviour
{
    [SerializeField] private e_switchType e_switch;
    [SerializeField] private float timer;

    [SerializeField] private UnityEvent onEnable;
    [SerializeField] private UnityEvent onDisable;

    private void OnEnableSwitch()
    {
        onEnable.Invoke();
    }

    private void OnDisableSwitch()
    {
        onDisable.Invoke();
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
