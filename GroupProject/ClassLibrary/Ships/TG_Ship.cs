using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Weapons;
using Microsoft.Xna.Framework.Input;

namespace ClassLibrary.Ships
{
    public class TG_Ship : BaseShip
    {
        private float energy;
        private float energyMax;
        private bool boost;

        public TG_Ship(string id, Texture2D tx) : base(id, tx, 8.0f, 10f)
        {
            energy = 5;
            energyMax = 10;
            boost = false;
        }

        public override Weapon ShipUpdate(KeyboardState newState, KeyboardState oldState)
        {
            if (newState.IsKeyDown(Keys.Space) && !boost && energy > 4)         //check if boost is possible
            {
                CurrentSpeed = (CurrentSpeed * 5);                              //increase movement speed
                boost = true;
            }
            if (newState.IsKeyUp(Keys.Space) && boost)
            {
                CurrentSpeed = (CurrentSpeed / 5);                              //decrase movement speed if no space pressed
                boost = false;
            }

            if (boost)
            {
                if (energy <= 0)
                {
                    boost = false;                                              //decrase movement speed if no energy is left
                    CurrentSpeed = (CurrentSpeed / 5);
                }
                else
                {
                    energy -= 0.5f;                                             //decrase energy if boost is active
                }
            }

            if (!boost && energy < energyMax)
            {
                energy += 0.2f;                                                 //increase energy if boost is not active
            }

            if (energy > energyMax)
                energy = energyMax;                                             //set the energy to max energy if you have more energy than allowed

            return base.ShipUpdate(newState, oldState);
        }
    }
}
