using chinese_checkers.Core.Enums;
using chinese_checkers.Core.Models.Characters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using chinese_checkers.Core.Helpers;
using chinese_checkers;


namespace chinese_checkers.Core.Models
{
    public class Player : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public bool IsAI { get; set; }
        public ICharacter Character { get; set; }
        public NestColor NestColor { get; set; }
        public bool Highlight { get; set; }
        public bool AbilitySelected { get; private set; }
        private int? _placement;
        public int? Placement
        {
            get
            {
                return _placement;
            }
            set
            {
                _placement = value;
                OnPropertyChanged("Placement");
                
            }
        }
        public List<Location> AvailableMoves { get; set; }
        public Piece selectedPiece { get; set; }
        public List<LinkedList<Point>> Paths { get; set; }

        private int _score;
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        // Player
        public Player(int id, ICharacter character, NestColor nestColor)
        {
           // this.Highligh = true;
            this.Id = id;
            this.Character = character;
            this.NestColor = nestColor;
            this.AvailableMoves = new List<Location>();
            this.AbilitySelected = false;
        }

        // AI
        public Player(int id, NestColor nestColor)
        {
           // this.Highligh = true;

            this.Id = id;
            this.IsAI = true;
            this.NestColor = nestColor;
            this.Character = GetRandomCharacter();
            this.AvailableMoves = new List<Location>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void SelectPiece(Location L, Board board)
        {
            DeSelectAbility();
            this.selectedPiece = board.Pieces.Find(piece => piece.Id == L.PieceId.Value);
            this.Paths = board.GetPaths(this.selectedPiece.Point, board.GetAvailableMoves(this.selectedPiece));
            this.AvailableMoves = board.GetAvailableMoves(this.selectedPiece);
        }

        public void DeSelectPiece()
        {
            
            selectedPiece = null;
            Paths = null;
            this.AvailableMoves = new List<Location>();
        }

        public void SelectAbility(Board board)
        {
            
            DeSelectPiece();
            this.AbilitySelected = true;
            this.AvailableMoves = this.Character.UsableLocations(board, this);
            //if (Character.GetType().Name == "Hunter")
            //{
            //    UseCharaterAbility(board);
            //}
        }

        public void UseCharaterAbility(Board board, Location location = null)
        {
            if (location != null)
            {
                //this.selectedPiece = board.Pieces.Find(x => x.Id == location.PieceId);
            }
            Character.UseAbility(board, location);
            DeSelectAbility();
            //ChangeTurn();
        }

        public void DeSelectAbility()
        {
            this.AbilitySelected = false;
            this.AvailableMoves = new List<Location>();
        }

        private ICharacter GetRandomCharacter()
        {
            Random rnd = new Random();
            switch (rnd.Next(5))
            {
                case 0:
                    return new Mage();
                case 1:
                    return new Druid();
                case 2:
                    return new Hunter();
                case 3:
                    return new Priest();
                case 4:
                    return new Warlock();
                case 5:
                    return new Warrior();
                default:
                    return null;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}\t\t   {1}\t{2}", NestColor, Score, Placement);
        }
    }
}
