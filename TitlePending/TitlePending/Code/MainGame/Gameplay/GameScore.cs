﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TitlePending.Code.MainGame;
using TitlePending.Code.MainGame.Gameplay;

namespace TitlePending.Code.MainGame.Gameplay
{
    public class GameScore : GameObject
    {
        public int Score { get; set; }
        public int NotesHitInARow { get; set; }

        public int Multiplier { get; set; }
        public GameScore(Vector2 position) : base(position)
        {
            this.position = position;
            this.Score = 0;
            this.NotesHitInARow = 0;
            this.Multiplier = 1;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
