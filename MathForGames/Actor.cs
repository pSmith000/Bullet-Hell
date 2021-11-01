using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames
{
    struct Icon
    {
        public char Symbol;
        public Color color;
    }


    class Actor
    {
        private string _name;
        private Vector2 _position;
        private bool _started;
        private Vector2 _forward;
        private Matrix3 _transform = Matrix3.Identity;
        private Matrix3 _translation = Matrix3.Identity;
        private Matrix3 _rotation = Matrix3.Identity;
        private Matrix3 _scale = Matrix3.Identity;
        private float _collisionRadius;
        private Sprite _sprite;

        /// <summary>
        /// True if the start function has been called for this actor
        /// </summary>
        public bool Started
        {
            get { return _started; }
        }

        public Vector2 Position
        {
            get { return new Vector2(_translation.M02, _translation.M12); }
            set
            {
                SetTranslation(value.X, value.Y);
            }
        }

        public Vector2 Forward
        {
            get
            {
                return _forward;
            }
            set
            {
                _position = value;
            }
        }

        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }

        public float CollisionRadius
        {
            get { return _collisionRadius; }
            set { _collisionRadius = value; }
        }

        public Actor( float x, float y, string name = "Actor", string path = "") :
            this( new Vector2 { X = x, Y = y }, name, path)
        { }


        public Actor(Vector2 position, string name = "Actor", string path = "")
        {
            _position = position;
            _name = name;

            if (path != "")
                _sprite = new Sprite(path);
        }

        public Actor() { }

        public virtual void Start()
        {
            _started = true;
        }

        public virtual void Update(float deltaTime, Scene currentScene)
        {
            Console.WriteLine(_name + ": " + Position.X + ", " + Position.Y);

            _transform = _translation * _rotation * _scale;

            Raylib.DrawCircleLines((int)Position.X, (int)Position.Y, CollisionRadius, Color.BLACK);
        }

        public virtual void Draw()
        {
            
            if (_sprite != null)
                _sprite.Draw(_transform);
        }

        public void End()
        {

        }

        public virtual void OnCollision(Actor actor, Scene currentScene)
        {

        }

        /// <summary>
        /// Checks if this actor collided with another actor
        /// </summary>
        /// <param name="other">The actor to check for a collision against</param>
        /// <returns>True if the distance between the actors is less then the radii of the two combined</returns>
        public virtual bool CheckForCollision(Actor other)
        {
            float combinedRadii = other.CollisionRadius + CollisionRadius;
            float distance = Vector2.Distance(Position, other.Position);

            return distance <= combinedRadii;
        }

        public void SetTranslation(float translationX, float translationY)
        {
            _translation = Matrix3.CreateTranslation(translationX, translationY);
        }

        public void Translate(float translationX, float translationY)
        {
            _translation *= Matrix3.CreateTranslation(translationX, translationY);
        }

        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        }

        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        }

        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        }

        public void Scale(float x, float y)
        {
            _scale *= Matrix3.CreateScale(x, y);
        }

        /// <summary>
        /// Rotates the actor to face the given position
        /// </summary>
        /// <param name="position">The position the actor should be looking at</param>
        public void LookAt(Vector2 position)
        {
            //Find the direction that the actor should look in
            Vector2 direction = (position - Position).Normalized;

            //Use the dot product to find the angle the actor needs to rotate
            float dotProd = Vector2.DotProduct(direction, Forward);

            if (dotProd > 1)
                dotProd = 1;

            float angle = (float)Math.Acos(dotProd);

            //Find a perpendicular vedctor to the direction
            Vector2 perpDirection = new Vector2(direction.Y, -direction.X);

            //Find the dot product of the perpendicular vector and the current forward
            float perpDot = Vector2.DotProduct(perpDirection, Forward);

            //If the result is not 0, use it to change the sign of the angle to be either positive or negative
            if (perpDot != 0)
                angle *= -perpDot / Math.Abs(perpDot);

            Rotate(angle);
        }
    }
}