using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;


namespace MathForGames
{
    class Enemy : Actor
    {
        private float _speed;
        private Vector2 _velocity;
        private Player _player;
        private int _maxViewAngle;
        private int _maxSightDistance;

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

        public Enemy(char icon, float x, float y, float speed, int maxSightDistance, int maxViewAngle, Player player, Color color, string name = "Enemy")
            : base(icon, x, y, color, name)
        {
            _speed = speed;
            _player = player;
            _maxSightDistance = maxSightDistance;
            _maxViewAngle = maxViewAngle;
        }

        public override void Update(float deltaTime)
        {
            Vector2 moveDirection = _player.Position - Position;

            Velocity = moveDirection.Normalized * Speed * deltaTime;

            if (GetTargetInSight())
                Position += Velocity;
            if (TargetCollide())
                //OnCollision(_player);

                base.Update(deltaTime);
        }

        public bool GetTargetInSight()
        {
            Vector2 directionOfTarget = (_player.Position - Position).Normalized;

            float distanceToTarget = Vector2.Distance(_player.Position, Position);

            float dotProduct = Vector2.DotProduct(directionOfTarget, Forward);

            return Math.Acos(dotProduct) * (180 / Math.PI) < _maxViewAngle && distanceToTarget < _maxSightDistance;

        }

        public bool TargetCollide()
        {
            if (_player.Position.X - Position.X > 30 || _player.Position.Y - Position.Y > 30)
                return false;
            else if (_player.Position.X - Position.X < -30 || _player.Position.Y - Position.Y < -40)
                return false;
            return true;
        }

        public override void OnCollision(Actor actor)
        {
            //Position -= Velocity * 25;
        }
    }
}
