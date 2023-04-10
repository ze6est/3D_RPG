using UnityEngine;
using CodeBase.Services.Input;
using CodeBase.Infrastructure;

namespace CodeBase.Hero
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        private Camera _camera;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;            
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if(_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }        
    }
}