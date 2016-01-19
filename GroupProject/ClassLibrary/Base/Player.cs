using ClassLibrary.Ships;
using ClassLibrary.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary.Base
{
    public class Player : Sprite
    {
        public string UserName; //username which appears in the chat
        public BaseShip Ship;
        public Weapon Weapon;
        public bool isActive;

        public Player(string uName, BaseShip _ship, Weapon _weapon)
        {
            UserName = uName;
            Ship = _ship;
            Weapon = _weapon;
            Ship.weapon = Weapon;
            isActive = true;
        }

    }
}
