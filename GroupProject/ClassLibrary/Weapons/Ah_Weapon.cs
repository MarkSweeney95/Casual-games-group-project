using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Weapons
{
    public class AH_weapon : Base.Sprite;
    {

        public string createdPlayerID;
        public float damage;
        public float angle;

        public float speed;

        SpriteEffects flip;
        Vector2 moveVector;


        public AH_weapon(string id, Texture2D _tex, float _dam, Vector2 _pos, Vector2 _move, float _ang, float _speed) : base(_tex, _pos)
        {
            createdPlayerID = id;
            damage = _dam;
            angle = _ang;
            moveVector = _move;
            speed = _speed;
        }

        public void WeaponUpdate()
        {
            _position += (moveVector * speed);

           
        }

        public override void Draw(SpriteBatch sp)
        {
            base.Draw(sp, angle, flip);
        }

    }
}




    }
}
