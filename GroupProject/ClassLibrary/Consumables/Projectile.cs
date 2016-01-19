using ClassLibrary.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Consumables
{
    public class Projectile : Sprite
    {
        float weight;

        public float Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        public Projectile(float _weight, Texture2D _tex, Vector2 _pos) : base(_tex, _pos)
        {
            Weight = _weight;
        }

    }
}
