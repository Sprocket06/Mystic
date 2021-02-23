﻿using System;
using System.Numerics;
using Chroma.Diagnostics.Logging;
using Chroma;
using Chroma.ContentManagement;
using Mystic.Scenes;
using Chroma.Graphics;
using Chroma.Input;
using Chroma.Diagnostics.Logging;

namespace Mystic
{
    class GameCore : Game
    {
        public static new IContentProvider Content { get; private set; }
        public Log Log = LogManager.GetForCurrentAssembly();
        public GameCore()
        {
            Content = base.Content;
            SceneManager.SetActiveScene<Main>();
            Log.Info("Hello!");
            //Window.TopMost = true;
        }
        
        protected override void Update(float delta)
        {
            SceneManager.ActiveScene.Update(delta);
        }
        protected override void Draw(RenderContext context)
        {
            SceneManager.ActiveScene.Draw(context);
        }
        protected override void KeyPressed(KeyEventArgs e)
        {
            if(e.KeyCode == KeyCode.Escape)
            {
                Quit();
            }
            SceneManager.ActiveScene.KeyPressed(e);
        }
        protected override void KeyReleased(KeyEventArgs e)
        {
            SceneManager.ActiveScene.KeyReleased(e);
        }
        protected override void MousePressed(MouseButtonEventArgs e)
        {
            SceneManager.ActiveScene.MousePressed(e);
        }
        protected override void MouseReleased(MouseButtonEventArgs e)
        {
            SceneManager.ActiveScene.MouseReleased(e);
        }
    }
}
