using UnityEngine;

namespace UI.Pause
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _pauseContainer;
        [SerializeField] private Dialogue dialogue;

        private Inputs _inputs;
        private bool _lastPauseState = false;

        private void Awake()
        {
            _inputs = new Inputs();
        }

        private void OnEnable()
        {
            _inputs.Enable();
            _inputs.Player.Pause.performed += cxt => UpdatePauseMenu(!_lastPauseState);
        }

        private void OnDisable()
        {
            _inputs.Disable();
            _inputs.Player.Pause.performed -= cxt => UpdatePauseMenu(!_lastPauseState);
        }

        public void UpdatePauseMenu(bool show)
        {
            _lastPauseState = show;
            Time.timeScale = show ? 0f : 1f;
            if (dialogue != null && dialogue.gameObject.activeInHierarchy)
                Time.timeScale = 0f;
            _pauseContainer.SetActive(show);
        }
    }
}