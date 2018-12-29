﻿using System;

namespace TicTacToo {
    class Program {
        static void Main(string[] args) {
            var controller = new Controller();
            while (true) {
                controller.Run();
                if (!Controller.Confirm("Try Again?"))
                    break;
            }
        }
    }
}
