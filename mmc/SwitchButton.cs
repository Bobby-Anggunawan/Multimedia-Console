using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace mmc
{
    public class SwitchButton
    {

        public SwitchButton()
        {
            T = new Thread(new ThreadStart(RunTime));
            //T.SetApartmentState(ApartmentState.STA);
            T.Start();
        }

        Thread T;
        public List<Button> ButtonList = new List<Button>();    //Menampung semua tombol yang dibuat
        public delegate void AFunction();                       //Untuk menyimpan fungsi(pointer menuju fungsi untuk dieksekusi)
        public int Active = 0;                                  //Menyimpan tombol index berapa di ButtonList yang sedang aktif
        public static int LastActive;                           //Menyimpan tombol terakhir yang ditekan


        public void CreatButton(string Text, AFunction Fungsi, int Pos_X, int Pos_Y)
        {
            ButtonList.Add(new Button(Text, Fungsi, Pos_X, Pos_Y));

            if (ButtonList.Count == 1) ButtonList[ButtonList.Count - 1].DrawSelected();
            else ButtonList[ButtonList.Count - 1].DrawUnselected();
        }
        public void CreatButton(string Text, AFunction Fungsi, int Pos_X, int Pos_Y, int PaddingSumbuX, int PaddingSumbuY)
        {
            ButtonList.Add(new Button(Text, Fungsi, Pos_X, Pos_Y, PaddingSumbuX, PaddingSumbuY));

            if (ButtonList.Count == 1) ButtonList[ButtonList.Count - 1].DrawSelected();
            else ButtonList[ButtonList.Count - 1].DrawUnselected();
        }
        public void CreatButton(string Text, AFunction Fungsi, int Pos_X, int Pos_Y, int PaddingSumbuX, int PaddingSumbuY, ConsoleColor TextSelected, ConsoleColor TextUnselected, ConsoleColor BackgroundSelected, ConsoleColor BackgroundUnselected)
        {
            ButtonList.Add(new Button(Text, Fungsi, Pos_X, Pos_Y, PaddingSumbuX, PaddingSumbuY, TextSelected, TextUnselected, BackgroundSelected, BackgroundUnselected));

            if (ButtonList.Count == 1) ButtonList[ButtonList.Count - 1].DrawSelected();
            else ButtonList[ButtonList.Count - 1].DrawUnselected();
        }

        void DrawButtons()
        {
            /*
            for (int x = 0; x < ButtonList.Count; x++)
            {
                if (x == Active) ButtonList[x].DrawSelected();
                else ButtonList[x].DrawUnselected();
            }*/

            if (Active == 0)
            {
                ButtonList[Active + 1].DrawUnselected();
                ButtonList[ButtonList.Count - 1].DrawUnselected();
                ButtonList[Active].DrawSelected();
            }
            else if (Active == ButtonList.Count - 1)
            {
                ButtonList[Active - 1].DrawUnselected();
                ButtonList[0].DrawUnselected();
                ButtonList[Active].DrawSelected();
            }
            else
            {
                ButtonList[Active - 1].DrawUnselected();
                ButtonList[Active + 1].DrawUnselected();
                ButtonList[Active].DrawSelected();
            }
        }

        void RunTime()
        {
            while (true)
            {
                var a = Console.ReadKey(true);
                if (a.Key == ConsoleKey.UpArrow || a.Key == ConsoleKey.LeftArrow)
                {
                    if (Active == 0) Active = ButtonList.Count - 1;
                    else Active--;
                    DrawButtons();
                }
                else if (a.Key == ConsoleKey.DownArrow || a.Key == ConsoleKey.RightArrow)
                {
                    if (Active == ButtonList.Count - 1) Active = 0;
                    else Active++;
                    DrawButtons();
                }
                else if (a.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    ButtonList[Active].Execute();
                    ButtonList = null;
                    T.Abort();

                }
                LastActive = Active;
            }
        }



        public class Button
        {

            public Button(string text, AFunction Fungsi, int Pos_X, int Pos_Y)
            {
                Text = text;
                Execute = Fungsi;
                posx = Pos_X;
                posy = Pos_Y;
            }

            public Button(string text, AFunction Fungsi, int Pos_X, int Pos_Y, int PaddingSumbuX, int PaddingSumbuY)
            {
                Text = text;
                Execute = Fungsi;
                posx = Pos_X;
                posy = Pos_Y;
                padx = PaddingSumbuX;
                pady = PaddingSumbuY;
            }

            public Button(string text, AFunction Fungsi, int Pos_X, int Pos_Y, int PaddingSumbuX, int PaddingSumbuY, ConsoleColor TextSelected, ConsoleColor TextUnselected, ConsoleColor BackgroundSelected, ConsoleColor BackgroundUnselected)
            {
                Text = text;
                Execute = Fungsi;
                posx = Pos_X;
                posy = Pos_Y;
                padx = PaddingSumbuX;
                pady = PaddingSumbuY;
                Text_Select = TextSelected;
                Text_Unselect = TextUnselected;
                Btn_Select = BackgroundSelected;
                Btn_Unselect = BackgroundUnselected;
            }

            string Text;    //text yang ditampilkan tombol
            int padx = 2, pady = 1; //padx = padding ke samping, pady = padding ke atas
            int posx, posy;
            ConsoleColor Text_Unselect = ConsoleColor.Black;    //warna text button yang gak dipilih
            ConsoleColor Btn_Unselect = ConsoleColor.DarkGray;      //warna background button yang gak dipilih
            ConsoleColor Text_Select = ConsoleColor.Black;      //Warna text tombol yang dipilih
            ConsoleColor Btn_Select = ConsoleColor.White;       //warna background button yang dipilih

            public AFunction Execute;                                  //Fungsi yang dieksekusi saat tombol ini ditekan

            public void DrawSelected()
            {
                Console.BackgroundColor = Btn_Select;
                Console.ForegroundColor = Text_Select;
                Console.SetCursorPosition(posx, posy);
                for (int a = 0; a < pady * 2 + 1; a++)
                {
                    for (int b = 0; b < padx * 2 + 1; b++)
                    {
                        if (a == pady && b == padx)
                        {
                            Console.Write(Text);
                        }
                        else
                        {
                            if (b == padx)
                            {
                                for (int c = 0; c < Text.Length; c++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.SetCursorPosition(posx, Console.CursorTop);
                }
                Console.ResetColor();
            }

            public void DrawUnselected()
            {
                Console.BackgroundColor = Btn_Unselect;
                Console.ForegroundColor = Text_Unselect;
                Console.SetCursorPosition(posx, posy);
                for (int a = 0; a < pady * 2 + 1; a++)
                {
                    for (int b = 0; b < padx * 2 + 1; b++)
                    {
                        if (a == pady && b == padx)
                        {
                            Console.Write(Text);
                        }
                        else
                        {
                            if (b == padx)
                            {
                                for (int c = 0; c < Text.Length; c++)
                                {
                                    Console.Write(" ");
                                }
                            }
                            else
                            {
                                Console.Write(" ");
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.SetCursorPosition(posx, Console.CursorTop);
                }
                Console.ResetColor();
            }

        }
    }
}
