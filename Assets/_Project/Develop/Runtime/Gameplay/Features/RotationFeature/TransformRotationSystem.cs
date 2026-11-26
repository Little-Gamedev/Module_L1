
using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature
{
    public class TransformRotationSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _rotationDirection;
        private ReactiveVariable<float> _rotationSpeed;

        private Transform _transform;

        public void OnInit(Entity entity)
        {
            _rotationDirection = entity.GetComponent<RotationDirection>().Value;
            _rotationSpeed = entity.GetComponent<RotationSpeed>().Value;
            _transform = entity.GetComponent<TransformComponent>().Value;
        }

        public void OnUpdate(float deltaTime)
        {
            Vector3 direction = _rotationDirection.Value;

            if (direction.magnitude < 0.05f)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

            float step = _rotationSpeed.Value * deltaTime;

            _transform.rotation = Quaternion.RotateTowards(
                _transform.rotation,
                lookRotation,
                step);
        }
    }
}
