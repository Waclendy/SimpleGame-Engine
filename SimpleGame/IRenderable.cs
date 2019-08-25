using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SimpleGame.GAME;

namespace SimpleGame {
    interface IRenderable {
        sboolean Rendered { get; set; }
        void Update();
    }
}
