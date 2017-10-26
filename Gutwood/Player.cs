using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseObjectNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gutwood;

namespace PlayerNamespace
{
    class Player : BaseObject
    {
        public bool CollidingLeft, CollidingRight, CollidingTop, CollidingBottom;
        public bool blockPlayerCollidingLeft, blockPlayerCollidingRight, blockPlayerCollidingUp, blockPlayerCollidingDown;
        public Player()
        {
            CollidingLeft = CollidingRight = CollidingTop = CollidingBottom = false;
            Speed = 5;
        }

        public void UpdateCollision(List<List<Rectangle>> collidableObjectsLists)
        {
            CollidingLeft = CollidingRight = CollidingTop = CollidingBottom = false; foreach (List<Rectangle> collidableObjectsList in collidableObjectsLists)
            {
                foreach (Rectangle collidableObject in collidableObjectsList)
                {
                    float left, right, up, down; blockPlayerCollidingLeft = blockPlayerCollidingRight = blockPlayerCollidingUp = blockPlayerCollidingDown = false; if (Position.X <= collidableObject.Right && Position.X > collidableObject.Center.X && Position.Y + Height >= collidableObject.Top && Position.Y <= collidableObject.Bottom)
                    { blockPlayerCollidingLeft = true; }
                    if (Position.X + Width >= collidableObject.Left && Position.X < collidableObject.Center.X && Position.Y + Height >= collidableObject.Top && Position.Y <= collidableObject.Bottom)
                    { blockPlayerCollidingRight = true; }
                    if (Position.Y <= collidableObject.Bottom && Position.Y > collidableObject.Center.Y && Position.X + Width >= collidableObject.Left && Position.X <= collidableObject.Right)
                    { blockPlayerCollidingUp = true; }
                    if (Position.Y + Height >= collidableObject.Top && Position.Y + Height <= collidableObject.Bottom && Position.X + Width >= collidableObject.Left && Position.X <= collidableObject.Right)
                    { blockPlayerCollidingDown = true; }
                    if (blockPlayerCollidingDown && blockPlayerCollidingRight)
                    {
                        if (Position.Y + Height - collidableObject.Top <= Math.Abs(Position.X + Width - collidableObject.Left))
                        { CollidingBottom = true; continue; }
                    }
                    if (blockPlayerCollidingDown && blockPlayerCollidingRight && blockPlayerCollidingUp)
                    {
                        right = Math.Abs(Position.X + Width - collidableObject.Left); down = Math.Abs(Position.Y + Height - collidableObject.Y); up = Math.Abs(Position.Y - collidableObject.Bottom); if (down <= up)
                        {
                            if (down <= right)
                            { CollidingBottom = true; continue; }
                            else
                            { CollidingRight = true; continue; }
                        }
                        else
                        {
                            if (up <= right)
                            { CollidingTop = true; continue; }
                            else
                            { CollidingRight = true; continue; }
                        }
                    }
                    if (blockPlayerCollidingRight && blockPlayerCollidingDown)
                    {
                        right = Math.Abs(Position.X + Width - collidableObject.Left); down = Math.Abs(Position.Y + Height - collidableObject.Y); if (right <= down)
                        { CollidingRight = true; continue; }
                        else
                        { CollidingBottom = true; continue; }
                    }
                    if (blockPlayerCollidingLeft && blockPlayerCollidingDown)
                    {
                        left = Math.Abs(Position.X - collidableObject.Right); down = Math.Abs(Position.Y + Height - collidableObject.Y); if (left <= down)
                        { CollidingLeft = true; continue; }
                        else
                        { CollidingBottom = true; continue; }
                    }
                    if (blockPlayerCollidingRight && blockPlayerCollidingUp)
                    {
                        right = Math.Abs(Position.X + Width - collidableObject.Left); up = Math.Abs(Position.Y - collidableObject.Bottom); if (up < right)
                        { CollidingTop = true; continue; }
                        else
                        { CollidingRight = true; continue; }
                    }
                    if (blockPlayerCollidingLeft && blockPlayerCollidingUp)
                    {
                        left = Math.Abs(Position.X - collidableObject.Right); up = Math.Abs(Position.Y - collidableObject.Bottom); if (up < left)
                        { CollidingTop = true; continue; }
                        else
                        { CollidingLeft = true; continue; }
                    }
                    if (blockPlayerCollidingUp)
                    { CollidingTop = true; }
                    else if (blockPlayerCollidingDown)
                    { CollidingBottom = true; }
                    else if (blockPlayerCollidingLeft)
                    { CollidingLeft = true; }
                    else if (blockPlayerCollidingRight)
                    { CollidingRight = true; }
                }
            }
        }
    }
}