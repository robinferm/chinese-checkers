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
        private int _defaultDamage;
        public int Damage { get; set; }
        public Point Point { get; set; }
        public NestColor NestColor { get; set; }
        public Item? Buff { get; set; }
        public CanvasBitmap Image { get; set; }
        public bool Thorns { get; set; }
        public bool Cursed { get; set; }

        public bool Hidden { get; private set; }

        public Piece(int id, Point point, NestColor nestColor)
        {
            _maxHealth = 100;
            _defaultDamage = 20;
            this.Id = id;
            this.Point = point;
            this.NestColor = nestColor;
            this.Health = _maxHealth;
            this.Damage = _defaultDamage;
            this.Hidden = false;
            this.Buff = null;
            this.Thorns = false;
            this.Cursed = false;
        }

        public void PickUpItem(Item item)
        {
            this.Buff = item;
            switch (item)
            {
                case Item.DoubleDamage:
                    Damage = _defaultDamage * 2;
                    break;
                case Item.HalfDamage:
                    Damage = _defaultDamage / 2;
                    break;
                case Item.Heal:
                    Heal(_maxHealth / 2);
                    break;
                case Item.TakeDamage:
                    Health -= 40;
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
                this.Health = _maxHealth;
            }
        }

        public void Reset()
        {
            this.Buff = null;
            this.Health = 100;
            this.Damage = 20;
            this.Thorns = false;
            this.Cursed = false;
            this.Hidden = false;
        }

        public void ToggleHidden()
        {
            this.Hidden = !this.Hidden;
        }

    }
}
