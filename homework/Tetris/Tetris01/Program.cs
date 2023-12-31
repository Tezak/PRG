﻿using System.IO;
using Tetris01;
using System.Windows.Input;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;


internal class Program
{
    private static void Main(string[] args)
    {
        Game game = new Game();
        Menu menu = new Menu();
        string[] options = { "Play Again", "Exit Game" };
        int option = 0;

        //dokud chce hráč hrát dál vytváří se nová hra
        while (option == 0)
        {
            game.GameOver = false;

            //vykreslí se hera
            game.StartGame();

            game.Update();
            //nechá uživatele si vybrat zda chce hrát znovu nebo ne
            option = menu.Choose(options, Draw.playFieldTop + 26);
        } 
        Console.ReadKey();
    }
}