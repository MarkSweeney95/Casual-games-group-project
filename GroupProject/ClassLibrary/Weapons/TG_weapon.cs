using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Weapons
{
    public class TG_weapon : Weapon
    {
        private static Random r = new Random();

        public TG_weapon(Texture2D _tex, Vector2 _pos) : base(_tex, _pos)
        {
            ProjectileWeight = 3;
        }

        public override void WeaponUpdate()
        {
            moveVector = (moveVector + new Vector2((r.Next(-10, 10) / 100), (r.Next(-10, 10) / 100)));        //should create a possible inaccuracy of 10% into any direcion 
            base.WeaponUpdate();
        }

    }
}
