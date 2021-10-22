using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Player _player;
        private Scene _scene;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        }

        public Player(char icon, float x, float y, float speed, Color color, string name = "Actor")
            : base(icon, x, y, color, name)
        {
            _speed = speed;
            CollisionRadius = 10;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            //Get the player input direction
            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W))
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            //Create a vector that stores the move input
            Vector2 moveDirection = new Vector2(xDirection, yDirection);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Position += Velocity;

            base.Update(deltaTime, currentScene);

            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                Bullet bullet = new Bullet('.', Position.X + 9, Position.Y - 20, 500, Color.RED, 0, 1);

                currentScene.AddActor(bullet);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                Bullet bullet = new Bullet('.', Position.X + 9, Position.Y - 20, 500, Color.RED, 0, -1);

                currentScene.AddActor(bullet);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                Bullet bullet = new Bullet('.', Position.X + 9, Position.Y - 20, 500, Color.RED, -1, 0);

                currentScene.AddActor(bullet);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                Bullet bullet = new Bullet('.', Position.X + 9, Position.Y - 20, 500, Color.RED, 1, 0);

                currentScene.AddActor(bullet);
            }

            
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {
            if (actor is Enemy)
                Engine.CloseApplication();
        }
    }
}
