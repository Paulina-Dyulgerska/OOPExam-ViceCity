using ViceCity.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.IO.Contracts;
using ViceCity.IO;

namespace ViceCity.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IController controller;

        public Engine()
        {
            this.reader = new Reader();
            this.writer = new Writer();
            this.controller = new Controller();
        }
        public void Run()
        {
            while (true)
            {
                var input = reader.ReadLine().Split();

                var result = string.Empty;

                if (input[0] == "Exit")
                {
                    Environment.Exit(0);
                }
                try
                {
                    if (input[0] == "AddPlayer")
                    {
                        var playerName = input[1];
                        result = controller.AddPlayer(playerName);
                    }
                    else if (input[0] == "AddGun")
                    {
                        var gunType = input[1];
                        var gunName = input[2];
                        result = controller.AddGun(gunType, gunName);
                    }
                    else if (input[0] == "AddGunToPlayer")
                    {
                        var playerName = input[1];
                        result = controller.AddGunToPlayer(playerName);
                    }
                    else if (input[0] == "Fight")
                    {
                        result = controller.Fight();
                    }

                    writer.WriteLine(result);
                }
                catch (Exception ex)
                {
                    writer.WriteLine(ex.Message);
                }
            }
        }
    }
}
