using chinese_checkers.Core.Enums;
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace chinese_checkers.Core.Models
{
    public class Piece
    {
        public int Id { get; set; }
        private int _maxHealth;
        public int Health { get; set; }
        public int Damage { get; set; }
        public Point Point { get; set; }
        public NestColor NestColor { get; set; }
        public List<Item> Items { get; set; }
        public CanvasBitmap Image { get; set; }

        public bool Hidden { get; private set; }

        public Piece(int id, Point point, NestColor nestColor)
        {
            _maxHealth = 10;
            this.Id = id;
            this.Point = point;
            this.NestColor = nestColor;
            this.Health = _maxHealth;
            this.Damage = 2;
            this.Hidden = false;
            this.Items = new List<Item>();
        }

        public void PickUpItem(Item item)
        {
            Items.Add(item);
            switch (item)
            {
                case Item.DoubleDamage:
                    Damage *= 2;
                    break;
                case Item.HalfDamage:
                    Damage /= 2;
                    break;
                case Item.Heal:
                    Heal(_maxHealth / 2);
                    break;
                case Item.TakeDamage:
                    Health -= 4;
                    break;
                case Item.FreezeSelf:
                    break;
                case Item.FreezeOther:
                    break;
                default:
                    break;
            }
        }

        public void Heal(int healAmount)
        {
            this.Health += healAmount;
            if (Health > _maxHealth)
            {
                healAmount = _maxHealth;
            }
        }

        public void Reset()
        {
            this.Items = new List<Item>();
            this.Health = 2;
            this.Damage = 1;
        }

        public void ToggleHidden()
        {
            this.Hidden = !this.Hidden;
        }

    }
}
