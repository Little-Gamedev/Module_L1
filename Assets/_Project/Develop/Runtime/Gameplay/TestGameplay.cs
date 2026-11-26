
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private EntitiesFactory _entitiesFactory;

        private Entity _rigidbodyCharacter;
        private Entity _characterControllerCharacter;

        public void Initialize(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            _rigidbodyCharacter = _entitiesFactory.CreateRigidbodyCharacter(
                new Vector3(-2, 0, 0));

            _characterControllerCharacter = _entitiesFactory.CreateCharacterControllerCharacter(
                new Vector3(2, 0, 0));
        }

        public void UpdateInput()
        {
            Vector3 input = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical"));

            SetDirectionFor(_rigidbodyCharacter, input);
            SetDirectionFor(_characterControllerCharacter, input);
        }

        private void SetDirectionFor(Entity entity, Vector3 direction)
        {
            entity.GetComponent<MoveDirection>().Value.Value = direction;
            entity.GetComponent<RotationDirection>().Value.Value = direction;
        }
    }
}
