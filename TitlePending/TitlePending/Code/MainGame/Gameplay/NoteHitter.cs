﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TitlePending.Collisions;

namespace TitlePending.Code.MainGame.Gameplay
{
    public class NoteHitter : GameObject
    {
        public NoteColor hitterType;

        private BoundingRectangle bounds;
        public bool Past { get; set; } = false;

        public delegate bool BoolReturner();
        public BoolReturner onTrigger;

        private Texture2D darkTexture;
        private Texture2D lightTexture;

        private GameScore score => GameManager.Score;

        public NoteHitter(Vector2 position) : base(position)
        {
            scale = 0.5f;
        }
        public BoundingRectangle Bounds => bounds;

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            lightTexture = content.Load<Texture2D>("square");
            darkTexture = content.Load<Texture2D>("darksquare");
            texture = lightTexture;

            this.bounds = new BoundingRectangle(position - new Vector2(texture.Width / 2 * scale, 0), texture.Width * scale, texture.Height * scale);
        }
        public override void Update()
        {
            base.Update();


            texture = lightTexture;
            int hitsNote = 0;
            if (InputManager.SpacePressed)
            {
                foreach (GameObject go in GameManager.currentState.gameObjects)
                {
                    if (go is Note n && n.color == color)
                    {

                        if (onTrigger())
                        {
                            if (score.NotesHitInARow >= 10 && score.NotesHitInARow < 20)
                            {
                                score.Multiplier = 2;
                            }
                            else if (score.NotesHitInARow >= 20 && score.NotesHitInARow < 30)
                            {
                                score.Multiplier = 3;
                            }
                            else if (score.NotesHitInARow >= 30 && score.NotesHitInARow < 40)
                            {
                                score.Multiplier = 4;
                            }
                            if (n.Bounds.CollidesWith(bounds))
                            {
                                //successfully hitting a note
                                n.color = Color.Transparent;
                                score.Score += 50 * score.Multiplier;
                                score.NotesHitInARow++;
                                n.RegisteredPast = true;
                                hitsNote = 1;

                                //FIX THIS LATER -- Destroys the note in a cheaty way
                                GameManager.currentState.RemoveObject(n);
                            }
                            else
                            {
                                hitsNote = 2;
                            }
                        }
                        if(hitsNote == 0)
                        {
                            hitsNote = 3;
                        }
                    }
                }
            }
            if (hitsNote == 2 || hitsNote == 3)
            {
                // missing a note
                score.NotesHitInARow = 0;
                score.Multiplier = 1;
            }
            if (onTrigger())
            {
                texture = darkTexture;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Past) return;
            base.Draw(spriteBatch);

            
        }
    }
}
