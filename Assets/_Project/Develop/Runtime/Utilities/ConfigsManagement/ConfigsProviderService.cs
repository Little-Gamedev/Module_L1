using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Utilities.ConfigsManagement
{
    public class ConfigsProviderService
    {
        private readonly Dictionary<Type, object> _configs = new Dictionary<Type, object>();
        private readonly IConfigsLoader[] _loaders;

        public ConfigsProviderService(params IConfigsLoader[] loaders)
        {
            _loaders = loaders;
        }

        public IEnumerator LoadAsync()
        {
            _configs.Clear();

            foreach (IConfigsLoader loader in _loaders)
                yield return loader.LoadAsync(AddConfigs);
        }

        public T GetConfig<T>() where T : class
        {
            if (_configs.ContainsKey(typeof(T)) == false)
                throw new InvalidOperationException($"Конфиг {typeof(T)} не найден");

            return (T)_configs[typeof(T)];
        }

        private void AddConfigs(Dictionary<Type, object> loadedConfigs)
        {
            foreach (KeyValuePair<Type, object> loadedConfig in loadedConfigs)
                _configs.Add(loadedConfig.Key, loadedConfig.Value);
        }
    }
}