using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    class Bullet : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Vector2 basePosition;

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

        public Bullet(char icon, float x, float y, float speed, Color color, float velocityX, float velocityY, string name = "Actor")
            : base(icon, x, y, color, name)
        {
            _velocity.X = velocityX;
            _velocity.Y = velocityY;
            _speed = speed;
            CollisionRadius = 10;
            basePosition = Position;
        }

        public override void Update(float deltaTime, Scene currentScene)
        {
            base.Update(deltaTime, currentScene);

            Vector2 moveDirection = new Vector2(_velocity.X, _velocity.Y);

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            Position += Velocity;

            if (Position.X - basePosition.X > 100 || Position.Y - basePosition.Y > 100 || 
            Position.X - basePosition.X < -100 || Position.Y - basePosition.Y < -100)
                currentScene.RemoveActor(this);
        }

        public override void Draw()
        {
            Raylib.DrawText(Icon.Symbol.ToString(), (int)Position.X - 10, (int)Position.Y - 35, 45, Icon.color);
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        { 

        }
    }
}

