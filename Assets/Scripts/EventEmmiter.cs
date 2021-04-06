using UnityEngine;
using UnityEngine.Events;

public class EventEmmiter : MonoBehaviour {
    public UnityEvent basicTriggerEvent;
    public IntEvent intTriggerEvent;
    public void OnBasicTrigger() {
        basicTriggerEvent.Invoke();
    }
    public void OnIntTrigger(int _int) {
        intTriggerEvent.Invoke(_int);
    }
}
