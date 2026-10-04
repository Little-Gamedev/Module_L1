using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.ModeSelection;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Процесс регистрации сервисов на сцене меню");

            container.RegisterAsSingle(CreateSequenceModeSelector);
        }

        private static SequenceModeSelector CreateSequenceModeSelector(DIContainer c)
            => new SequenceModeSelector(
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>());
    }
}