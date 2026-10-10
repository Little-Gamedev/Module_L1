using System;

namespace Assets._Project.Develop.Runtime.Infrastructure.DI
{
    public class Registration : IRegistrationOptions
    {
        public bool IsNonLazy { get; private set; }

        private readonly Func<DIContainer, object> _creator;

        private object _cachedInstance;

        public Registration(Func<DIContainer, object> creator)
        {
            _creator = creator;
        }

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance != null)
                return _cachedInstance;

            if (_creator == null)
                throw new InvalidOperationException("Нет ни готового объекта, ни способа его создать");

            _cachedInstance = _creator.Invoke(container);

            return _cachedInstance;
        }

        public void NonLazy() => IsNonLazy = true;
    }
}