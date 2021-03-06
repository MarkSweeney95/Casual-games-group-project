﻿using ClassLibrary.Consumables;
using ClassLibrary.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Ships
{
    public class BaseShip : ClassLibrary.Base.Sprite
    {
        private string ID;
        public string Id { get; set; }

        public float CurrentSpeed
        {
            get
            {
                return currentSpeed;
            }

            set
            {
                currentSpeed = value;
            }
        }

        private SpriteEffects flip;
        private Vector2 moveVector;
        private Vector2 newFireDirection = new Vector2(0, 1);
        private Vector2 currentFireDirection;

        private float Health;
        private float initSpeed;
        private float currentSpeed;
        private float angle;
        private float maxWeight;
        private float currentWeight;

        public Weapon weapon;
        private Weapon firedWeapon;

        public BaseShip(string id, Texture2D _tex, float speed) : base(_tex, Vector2.Zero)
        {
            ID = id;
            Health = 100.0f;
            initSpeed = speed;
            CurrentSpeed = initSpeed;
            currentFireDirection = newFireDirection;
        }


        public BaseShip(string id, Texture2D _tex, float _speed, float _maxW) : base(_tex, Vector2.Zero)
        {
            ID = id;
            Health = 100.0f;
            initSpeed = _speed;
            CurrentSpeed = initSpeed;
            currentFireDirection = newFireDirection;
            maxWeight = _maxW;
        }

        public virtual void GotShoot(Weapon w)
        {
            if (ID != w.createdPlayerID)                               //test if the weapon is from the player and decrease the Health
            {
                Health -= w.damage;
                CurrentSpeed = (CurrentSpeed * 0.9f);
            }
        }

        public virtual Weapon ShipUpdate(KeyboardState newState, KeyboardState oldState)
        {


            #region Movement

            if (newState.IsKeyDown(Keys.W) && _position.Y > 32)         //handle vertical movement
            {
                moveVector += new Vector2(0, -CurrentSpeed);
                newFireDirection += new Vector2(0, -1);                    //the direction to fire
            }
            else if (newState.IsKeyDown(Keys.S) && _position.Y < 736)
            {
                moveVector += new Vector2(0, CurrentSpeed);
                newFireDirection += new Vector2(0, 1);
            }


            if (newState.IsKeyDown(Keys.A) && _position.X > 32)         //handle horizontal movement
            {
                moveVector += new Vector2(-CurrentSpeed, 0);
                newFireDirection += new Vector2(-1, 0);
            }
            else if (newState.IsKeyDown(Keys.D) && _position.X < 1252)
            {
                moveVector += new Vector2(CurrentSpeed, 0);
                newFireDirection += new Vector2(1, 0);
            }

            #endregion

            if (oldState != newState && newState.IsKeyDown(Keys.H) && weapon != null) //test if the player wants to shoot
                firedWeapon = Shoot(weapon.createdPlayerID, weapon.speed);
            else firedWeapon = null;

            if (newState.IsKeyDown(Keys.W) || newState.IsKeyDown(Keys.S) || newState.IsKeyDown(Keys.D) || newState.IsKeyDown(Keys.A))
            {
                //change some vars only if you moved at the same time
                angle = GetAngle();
                currentFireDirection = newFireDirection;
                newFireDirection = Vector2.Zero;
                _position += (moveVector * GetWeight());
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
            if (currentWeight >= weapon.ProjectileWeight)
            {
                currentWeight -= weapon.ProjectileWeight;
                return new Weapon(id, weapon._texture, weapon.damage, _position, currentFireDirection, angle, _speed);
            }
            else
            {
                return null;
            }
        }

        private float GetAngle()
        {
            return (float)Math.Atan2(moveVector.Y, moveVector.X);
        }

        private float GetWeight()
        {
            float temp;

            temp = (maxWeight / currentWeight);

            if (temp < 0.2f)
            {
                temp = 0.2f;
            }
            return temp;
        }

        public virtual void CollectPickup(Projectile p)
        {
            if (p.IsVisible)
            {
                currentWeight += p.Weight;
                if (currentWeight > maxWeight)
                {
                    currentWeight = maxWeight;
                }
                p.IsVisible = false;
            }
        }
    }
}