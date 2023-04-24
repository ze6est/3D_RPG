using UnityEngine;

namespace CodeBase.Enemy
{
    public class Agro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private AgentMoveToPlayer _follow;

        private void Start()
        {
            _triggerObserver.TriggerEnter += OnTriggerEnter;
            _triggerObserver.TriggerExit += OnTriggerExit;

            SwitchFollowOff();
        }

        private void OnTriggerExit(Collider obj)
        {
            SwitchFollowOff();
        }

        private void OnTriggerEnter(Collider obj)
        {
            SwitchFollowOn();
        }

        private void SwitchFollowOn()
        {
            _follow.enabled = true;
        }

        private void SwitchFollowOff()
        {
            _follow.enabled = false;
        }
    }
}