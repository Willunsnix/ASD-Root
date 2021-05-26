using System;
using System.Drawing;

namespace ASD.Forms.Interfaces
{
    public interface IRenderer : IDisposable
    {
        #region Properties

        object Control { get; set; }

        #endregion Properties

        #region Methods

        bool Update();

        void Draw(Graphics graphics);

        #endregion Methods
    }
}