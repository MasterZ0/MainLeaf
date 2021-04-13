using UnityEngine;
using UnityEngine.Events;

public class EventEmmiter : MonoBehaviour {
    public UnityEvent[] onIndex;
    public IntEvent onDynamicInt;
    public void OnIndex(int index) {
        onIndex[index].Invoke();
    }
    public void OnDynamicInt(int i) {
        onDynamicInt.Invoke(i);
    }
}
