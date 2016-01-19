using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Weapons
{
    public class Weapon : Base.Sprite
    {
        public string createdPlayerID;
        public float damage;
        public float angle;
        private float projectileWeight;

        public float speed;

        SpriteEffects flip;
        protected Vector2 moveVector;

        public float ProjectileWeight
        {
            get
            {
                return projectileWeight;
            }

            set
            {
                projectileWeight = value;
            }
        }

        public Weapon(Texture2D _tex, Vector2 _pos) : base(_tex, _pos)
        {        }

        public Weapon(string id, Texture2D _tex, float _dam, Vector2 _pos, Vector2 _move, float _ang, float _speed) : base(_tex, _pos)
        {
            createdPlayerID = id;
            damage = _dam;
            angle = _ang;
            moveVector = _move;
            speed = _speed;
        }

        public virtual void WeaponUpdate()
        {
            _position += (moveVector * speed);

            if (_position.X >= 1280 || _position.Y >= 768)           // Destroy object
                IsVisible = false;
        }

        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp, angle, flip);
        }

    }
}
