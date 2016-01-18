﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Weapons;

namespace ClassLibrary.Ships
{
    class AH_Ship : ClassLibrary.Base.Sprite
    {
        private string ID;
        private SpriteEffects flip;
        private Vector2 moveVector;
        private Vector2 newFireDirection = new Vector2(0, 1);
        private Vector2 currentFireDirection;

        private float Health;
        private float initSpeed;
        private float currentSpeed;
        private float angle;

        public Weapon weapon;
        private Weapon firedWeapon;

        public BaseShip(string id, Texture2D _tex, float speed) : base(_tex, Vector2.Zero)
        {
            ID = id;
            Health = 100.0f;
            initSpeed = speed;
            currentSpeed = initSpeed;
            currentFireDirection = newFireDirection;
        }

        public virtual void GotShoot(Weapon w)
        {
            if (ID != w.createdPlayerID)                               //test if the weapon is from the player and decrease the Health
            {
                Health -= w.damage;
                //decrease movement (implement later)
            }
        }

        public virtual Weapon ShipUpdate(KeyboardState newState, KeyboardState oldState)
        {


            if (newState.IsKeyDown(Keys.W) && _position.Y > 32)         //handle vertical movement
            {
                moveVector += new Vector2(0, -currentSpeed);
                newFireDirection += new Vector2(0, -1);                    //the direction to fire
            }
            else if (newState.IsKeyDown(Keys.S) && _position.Y < 736)
            {
                moveVector += new Vector2(0, currentSpeed);
                newFireDirection += new Vector2(0, 1);
            }


            if (newState.IsKeyDown(Keys.A) && _position.X > 32)         //handle horizontal movement
            {
                moveVector += new Vector2(-currentSpeed, 0);
                newFireDirection += new Vector2(-1, 0);
            }
            else if (newState.IsKeyDown(Keys.D) && _position.X < 1252)
            {
                moveVector += new Vector2(currentSpeed, 0);
                newFireDirection += new Vector2(1, 0);
            }


            if (oldState != newState && newState.IsKeyDown(Keys.H) && weapon != null)
                firedWeapon = Shoot(weapon.createdPlayerID, weapon.speed);
            else firedWeapon = null;



            if (newState.IsKeyDown(Keys.W) || newState.IsKeyDown(Keys.S) || newState.IsKeyDown(Keys.D) || newState.IsKeyDown(Keys.A))
            {
                angle = GetAngle();
                currentFireDirection = newFireDirection;
                newFireDirection = Vector2.Zero;
                _position += moveVector;
            }

            moveVector = Vector2.Zero;

            return firedWeapon;
        }

        public override void Draw(SpriteBatch sp)
        {
            flip = SpriteEffects.None;

            if (_position.X < 0)
                flip = SpriteEffects.FlipVertically;

            base.Draw(sp, angle, flip);
        }

        public virtual Weapon Shoot(string id, float _speed)
        {
            return new Weapon(id, weapon._texture, weapon.damage, _position, currentFireDirection, angle, _speed);
        }

        private float GetAngle()
        {
            return (float)Math.Atan2(moveVector.Y, moveVector.X);
        }

    }
}




 