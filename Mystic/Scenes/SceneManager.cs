using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mystic.Scenes
{
    public static class SceneManager
    {
        private static readonly List<Scene> _scenes = new();

        public static Scene ActiveScene { get; private set; }

        public static void SetActiveScene<T>() where T : Scene, new()
        {
            if (!_scenes.Exists(x => x is T))
                _scenes.Add(new T());

            ActiveScene = _scenes.First(x => x is T);
        }
    }
}
