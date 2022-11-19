using I2.Loc;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AdventureGame.UI
{
    public struct PopupRequest
    {
        public LocalizedString title;
        public LocalizedString confirm;
        public LocalizedString deny;
        public Action<bool> callback;
        public bool startConfirming;
    }

    /// <summary>
    /// Note to developers: Please describe what this MonoBehaviour does.
    /// </summary>
    public class Popup : MonoBehaviour {

        #region Variables and Initialization
        [Header("Popup")]
        [SerializeField] private LocalizedString yes;
        [SerializeField] private LocalizedString no;
        [Space]
        [SerializeField] private Localize title;
        [SerializeField] private Localize confirm;
        [SerializeField] private Localize deny;

        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Selectable confirmBtn;
        [SerializeField] private Selectable denyBtn;

        private PopupRequest request;
        private const string FadeIn = "FadeIn";
        private const string FadeOut = "FadeOut";
        private bool result;
        #endregion

        #region Requests

        public void RequestQuestion(PopupRequest newRequest)
        {
            request = newRequest;

            title.SetTerm(request.title.mTerm);
            confirm.SetTerm(request.confirm.mTerm);
            deny.SetTerm(request.deny.mTerm);

            EventSystem.current.SetSelectedGameObject(null);
            gameObject.SetActive(true);
            animator.Play(FadeIn);
        }

        /// <summary>
        /// Basic Request
        /// </summary>
        public void RequestQuestion(LocalizedString title, Action<bool> callback, bool startConfirming = true)
        {
            PopupRequest request = new PopupRequest()
            {
                title = title,
                confirm = yes,
                deny = no,
                callback = callback,
                startConfirming = startConfirming
            };

            RequestQuestion(request);
        }
        #endregion

        #region Button and Animation Events
        public void OnConfirm() => SendCallBack(true);

        public void OnDeny() => SendCallBack(false);

        private void SendCallBack(bool value)
        {
            result = value;
            EventSystem.current.SetSelectedGameObject(null);
            animator.Play(FadeOut);
        }

        public void OnFadeInEnd()
        {
            if (request.startConfirming)
            {
                confirmBtn.Select();
            }
            else
            {
                denyBtn.Select();
            }
        }

        public void OnFadeOutEnd()
        {
            gameObject.SetActive(false);
            request.callback(result);
        }
        #endregion
    }
}