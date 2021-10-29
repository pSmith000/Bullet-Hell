using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    class LivesCounter : UI_Text
    {
        private Player _player;

        public LivesCounter(float x, float y, string name, Color color, Player player) : base(x, y, name, color)
        {
            _player = player;
            Text = "Lives: " + player.Lives.ToString();
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            base.Update(deltaTime, currentScene);

            Text = "Lives: " + _player.Lives.ToString();
        }
    }
}
