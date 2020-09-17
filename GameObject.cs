using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommonLib;

namespace _20200613_TankLibrary
{
    public abstract class GameObject
    {
        #region ===--- Dataset ---===

        public Coordinate Position { get; internal set; }
        public ColorSkin Color { get; protected set; }
        protected readonly IField _owner;    // IField

        #endregion

        #region ===--- Constructor ---===

        public GameObject(IField field)
        {
            Position = new Coordinate();
            Color = ColorSkin.NoColor;
            _owner = field;
        }

        public GameObject(Coordinate position, ColorSkin colorObj, IField owner)   
        {
            Position = position;
            Color = colorObj;
            _owner = owner;
        }

        #endregion

        #region ===--- Properties ---===

        #endregion

        #region ===--- Abstracts properties ---===

        public abstract ObjectType KindOfObject { get; }

        #endregion

        #region ===--- Abstracts methods ---===

        #endregion
    }
}
