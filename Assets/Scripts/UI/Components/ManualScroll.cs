using AdventureGame.Inputs;
using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame.UI
{
    public class ManualScroll : MonoBehaviour
    {
        [SerializeField] private float ScrollSpeed;
        [SerializeField] private ScrollRect scrollRect;

        private ScrollInput scrollInputs;
        private float direction;

        //private float ScrollSpeed => GameSettings.UI.ManualScrollSpeed;

        private void Awake()
        {
            scrollInputs = new ScrollInput();
            scrollInputs.onMove += OnMove;
        }

        private void OnEnable() => scrollInputs.SetActive(true);
        private void OnDisable() => scrollInputs.SetActive(false);
        private void OnDestroy() => scrollInputs.Dispose();

        private void OnMove(float value)
        {
            direction = value;
        }

        private void Update()
        {
            float displacementSpeed = direction * ScrollSpeed * Time.unscaledDeltaTime;
            scrollRect.content.localPosition += Vector3.down * displacementSpeed;
        }
    }
}