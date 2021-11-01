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
        private int _lives;
        private Vector2 _velocity;
        private UI_Text _uiText;

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

        public int Lives
        {
            get { return _lives; }
            set { _lives = value; }
        }

        public Player(float x, float y, float speed, int lives,string name = "Actor", string path = "")
            : base(x, y, name, path)
        {
            _speed = speed;
            CollisionRadius = 10;
            _lives = lives; 
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
                Bullet bullet = new Bullet(Position.X, Position.Y, 500, 0, 1);

                currentScene.AddActor(bullet);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
            {
                Bullet bullet = new Bullet(Position.X, Position.Y, 500,0, -1);

                currentScene.AddActor(bullet);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
            {
                Bullet bullet = new Bullet(Position.X, Position.Y, 500, -1, 0);

                currentScene.AddActor(bullet);
            }
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
            {
                Bullet bullet = new Bullet(Position.X, Position.Y, 500, 1, 0);

                currentScene.AddActor(bullet);
            }

            
        }

        public override void OnCollision(Actor actor, Scene currentScene)
        {

            if (actor is Enemy)
            {
                Lives--;
                
                Position = new Vector2(0, 0);

            }


            if (Lives == 0)
            {
                UI_Text deathMessage = new UI_Text(200, 150, "Death Message", Color.BLACK, 1000, 1000, 100, "You Lose");
                currentScene.AddUIElement(deathMessage);
                currentScene.RemoveActor(this);
            }
               
        }
    }
}
