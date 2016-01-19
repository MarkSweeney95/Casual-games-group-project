﻿using ClassLibrary.Base;
using ClassLibrary.Consumables;
using ClassLibrary.MS_weapons;
using ClassLibrary.Ships;
using ClassLibrary.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using WebAPIAuthenticationClient;

namespace Client
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string Message;
        SpriteFont font;

        BaseShip testShip;
        Texture2D _textureShip;
        Texture2D _textureWeapon;
        //
        MS_ship ms_ship;
        Texture2D _ms_textureS;
        Texture2D _ms_textureW;
        MS_weapon ms_weapon;
        //
        TC_ship TC_ship;
        Texture2D _tc_textureS;
        Texture2D _tc_textureW;
        MS_weapon tc_weapon;
        //
        AH_Ship AH_ship;
        Texture2D _ah_textureS;
        Texture2D _ah_textureW;
        AH_weapon ah_weapon;


        string[] menuOptions = new string[] { "Fast", "Normal", "Strong" };

        enum currentDisplay { Selection, Game};
        currentDisplay currentState = currentDisplay.Selection;

        Menu menu;



        Player testPlayer;

        Weapon testWeapon;
        Weapon newWeapon;
        List<Weapon> Weapons = new List<Weapon>();
        List<Player> Enemies = new List<Player>();
        List<Weapon> DestroyWeapons = new List<Weapon>();

        List<Projectile> GotCollected = new List<Projectile>();
        List<Projectile> Projectiles = new List<Projectile>();

        KeyboardState newState;
        KeyboardState oldState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            try
            {
                bool valid = PlayerAuthentication.login("powell.paul@itsligo.ie", "itsPaul$1").Result;

                if (valid) Message = "Player Logged in with Token " + PlayerAuthentication.PlayerToken;
                else Message = PlayerAuthentication.PlayerToken;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>(@"SpriteFont\MessgaeFont");

            _textureShip = Content.Load<Texture2D>(@"Assets\Textures\Ships\msShip");
            _textureWeapon = Content.Load<Texture2D>(@"Assets\Textures\Weapons\missle");

            _ms_textureS = Content.Load<Texture2D>(@"Assets\Textures\Ships\msShip");
            _ms_textureW = Content.Load<Texture2D>(@"Assets\Textures\Weapons\missle");

            _tc_textureS = Content.Load<Texture2D>(@"Assets\Textures\Ships\tcShip");
            _tc_textureW = Content.Load<Texture2D>(@"Assets\Textures\Weapons\laser");

            _ah_textureS = Content.Load<Texture2D>(@"Assests\Textures\Ships\AH_Ship");
            _ah_textureW = Content.Load<Texture2D>(@"Assets\Textures\Weapons\bomb");


            testWeapon = new Weapon("0", _textureWeapon, 20f, Vector2.Zero, Vector2.Zero, 0f, 20);
            testShip = new BaseShip("0", _textureShip, 5.0f);

            //ms_weapon = new MS_weapon("0", _textureWeapon, 20f, Vector2.Zero, Vector2.Zero, 0f, 20);
            ms_ship = new MS_ship("0", _textureShip, 7.0f);
            //ms_ship.weapon = ms_weapon;
            testShip.weapon = testWeapon;


            TC_ship = new TC_ship("2", _tc_textureS, 6.0f);

            AH_ship = new AH_Ship("3", _ah_textureS, 5.0f);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            newState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            #region Select Character

            if (currentState == currentDisplay.Selection)
            {
                menu.CheckMouse();

              

                menu.MenuAction = null; //reset the selection
            }

            #endregion


            newWeapon = testShip.ShipUpdate(newState, oldState); //update the ship and if the ship fired return a new projectile 



            if (newWeapon != null)
            {
                Weapons.Add(newWeapon);
            }

            if (Weapons.Count > 0)
            {
                foreach (Weapon item in Weapons)
                {
                    item.WeaponUpdate();
                }
            }

            ms_ship.ShipUpdate(newState, oldState);



            #region Collision

            foreach (var item in Weapons)
            {
                if (item.IsVisible)
                {
                    foreach (var ene in Enemies)
                    {
                        if (item.CollisiionDetection(ene.Ship.Rectangle)) //check if bullet hits any enemy
                        {
                            if (item.createdPlayerID != ene.Ship.Id)    //if the ID if the enemy is the same as the bullet
                            {
                                ene.Ship.GotShoot(item);                //ship got hit
                                item.IsVisible = false;                 //disable bullet
                                DestroyWeapons.Add(item);               //destroy bullet at the end (to clear the ram
                                if (!ene.Ship.IsVisible)                //check if ship get destroied 
                                {
                                    ene.isActive = false;               //deactivate the ship                               !!!!!!!!!!!!!!!! TO DO <-- !!!!!!!!!!!!!!!!!
                                    if (item.createdPlayerID == testPlayer.UserName)
                                        testPlayer.score += 50;
                                }





                                //deactivate the ship                               !!!!!!!!!!!!!!!! TO DO <-- !!!!!!!!!!!!!!!!!
                            }
                        }
                    }
                    if (item.Rectangle.Intersects(testPlayer.Ship.Rectangle))    //check if playership got hit by the Weapon
                    {
                        testPlayer.Ship.GotShoot(item);                         //ship got hit
                        item.IsVisible = false;                                 //disable the weapon
                        DestroyWeapons.Add(item);
                    }
                }
            }

            foreach (var item in Projectiles)
            {
                if (testPlayer.Ship.CollisiionDetection(item.Rectangle))    //check if any consumables hit player ship
                {
                    GotCollected.Add(item);
                    item.IsVisible = false;
                    testPlayer.Ship.CollectPickup(item);
                }

                foreach (var ene in Enemies)
                {
                    if (ene.Ship.CollisiionDetection(item.Rectangle))
                    {
                        ene.Ship.CollectPickup(item);
                        item.IsVisible = false;
                        GotCollected.Add(item);
                    }
                }
            }

            #endregion
            

            oldState = newState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //testShip.Draw(spriteBatch);
            spriteBatch.Begin();
            ms_ship.Draw(spriteBatch);
            TC_ship.Draw(spriteBatch);
            AH_ship.Draw(spriteBatch);
            spriteBatch.DrawString(font, Message, new Vector2(10, 10), Color.White);
            spriteBatch.End();


            if (Weapons.Count > 0)
            {
                foreach (Weapon item in Weapons)
                {
                    item.Draw(spriteBatch);
                }
            }

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
