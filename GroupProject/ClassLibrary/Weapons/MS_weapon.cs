﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.MS_weapons
{
    public class MS_weapon : Base.Sprite
    {
        public string createdPlayerID;
        public float damage;
        public float angle;

        public float speed;

        SpriteEffects flip;
        Vector2 moveVector;


        public MS_weapon(string id, Texture2D _tex, float _dam, Vector2 _pos, Vector2 _move, float _ang, float _speed) : base(_tex, _pos)
        {
            createdPlayerID = id;
            damage = _dam;
            angle = _ang;
            moveVector = _move;
            speed = _speed;
        }

        public void MS_weaponUpdate()
        {
            _position += (moveVector * speed);

            //if (_position.X >= 1280 || _position.Y >= 768)            Destroy object
            //    this = null;
        }

        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp, angle, flip);
        }

    }
}
