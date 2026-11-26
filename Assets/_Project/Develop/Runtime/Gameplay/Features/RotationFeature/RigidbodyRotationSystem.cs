
using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature
{
    public class RigidbodyRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _rotationDirection;
        private ReactiveVariable<float> _rotationSpeed;

        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _rotationDirection = entity.GetComponent<RotationDirection>().Value;
            _rotationSpeed = entity.GetComponent<RotationSpeed>().Value;
            _rigidbody = entity.GetComponent<RigidbodyComponent>().Value;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = _rotationDirection.Value;

            if (direction.magnitude < 0.05f)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

            float step = _rotationSpeed.Value * deltaTime;

            _rigidbody.rotation = Quaternion.RotateTowards(
                _rigidbody.rotation,
                lookRotation,
                step);
        }
    }
}
