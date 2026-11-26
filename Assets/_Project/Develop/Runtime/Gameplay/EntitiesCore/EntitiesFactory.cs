
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.RotationFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateRigidbodyCharacter(Vector3 position)
        {
            Entity entity = CreateCharacter(position, "Entities/TestEntity");

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotationSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateCharacterControllerCharacter(Vector3 position)
        {
            Entity entity = CreateCharacter(position, "Entities/CharacterControllerTestEntity");

            entity
                .AddSystem(new CharacterControllerMovementSystem())
                .AddSystem(new TransformRotationSystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private Entity CreateCharacter(Vector3 position, string path)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, path);

            entity
                .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.zero) })
                .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10) })
                .AddComponent(new RotationDirection() { Value = new ReactiveVariable<Vector3>(Vector3.zero) })
                .AddComponent(new RotationSpeed() { Value = new ReactiveVariable<float>(360) });

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
