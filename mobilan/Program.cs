using System;
using System.Threading;
using System.Media;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace mobilan
{
	class Program
	{
		static int playerPos = 0;
		static int playerSize = 3;
		
		static int enemySize = 3;
		static int enemyPos = 0;
		
		static int shootPos = playerSize+1;
		static int initialShootPos = playerSize+1;
		
		static Random rand = new Random();
		
		static int ancientPosX;
		static int ancientPosY;
		
		static int scorePlayer = 0;
		static int scoreEnemy = 0;
		
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);
		
		public static void Main(string[] args)
		{
			Console.Title = "Destroy of The Ancient";
			Console.CursorVisible = false;
			Maximize();
			Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
			var player = new System.Media.SoundPlayer();
			player.SoundLocation = Environment.CurrentDirectory + @"\Blazer_Rail_2.wav";
			player.PlayLooping();
			landingScreen();
			
			ancientPosX = rand.Next(1, Console.WindowWidth - 3);
			ancientPosY = rand.Next(1, Console.WindowHeight - 3);
			howToPlay();
			showScoreAwal();
			while(true){
				if(Console.KeyAvailable){
					ConsoleKeyInfo readKey = Console.ReadKey();
					if(readKey.Key == ConsoleKey.LeftArrow){
						playerLeft();
					}
					if(readKey.Key == ConsoleKey.RightArrow){
						playerRight();
					}
					if(readKey.Key == ConsoleKey.UpArrow){
						playerShoot();	
					}
					
					if(readKey.Key == ConsoleKey.A){
						enemyLeft();
					}
					if(readKey.Key == ConsoleKey.D){
						enemyRight();
					}
					if(readKey.Key == ConsoleKey.S){
						enemyShoot();	
					}
					
				}
				Console.Clear();
				PlayerDisplay();
				Thread.Sleep(10);
				enemyDisplay();
				Thread.Sleep(10);
				ancientDisplay();
				Thread.Sleep(10);
			}
			Console.ReadKey(true);
		}
		
		static void removeScrollBar(){
			Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;
		}
		
		static void PlayerDisplay(){
			for(int i = playerPos; i<playerPos+playerSize; i++){
				Console.ForegroundColor = ConsoleColor.Blue;
				printPlayer(i,(Console.WindowWidth)-1,'*');
				printPlayer(i,(Console.WindowWidth)-2,'*');
				printPlayer(i,(Console.WindowWidth)-3,'*');
			}	
		}
		
		static void printPlayer(int x, int y, char playerNum){
			Console.SetCursorPosition(x,y);
			Console.Write(playerNum);
		}
		
		static void playerLeft(){
			if(playerPos > 0){
				playerPos--;
			}
		}
		
		static void playerRight(){
			if(playerPos < Console.WindowWidth - playerSize){
				playerPos++;
			}
		}
		
		static void playerShoot(){
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Beep(1500, 100);
			printPlayer(playerPos+1, 0, '1');
			if(playerPos == ancientPosX){
				Console.Clear();
				Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2);
				Console.WriteLine("Player 1 Won");
				Console.SetCursorPosition((Console.WindowWidth/2)+1, (Console.WindowHeight/2)+1);
				scorePlayer+=100;
				Console.WriteLine("Score: {000000}", scorePlayer);
				Console.ReadKey();
				Thread.Sleep(2000);
				restart();
			}
			Thread.Sleep(100);
		}
		
		
		
		static void enemyDisplay(){
			for(int i = enemyPos; i<enemyPos+enemySize; i++){
				Console.ForegroundColor = ConsoleColor.Red;
				printPlayer(i,0,'@');
				printPlayer(i,1,'@');
				printPlayer(i,2,'@');
			}
		}
		
		static void enemyLeft(){
			if(enemyPos > 0){
				enemyPos--;
			}
		}
		
		static void enemyRight(){
			if(enemyPos < Console.WindowWidth - enemySize){
				enemyPos++;
			}
		}
		
		static void enemyShoot(){
			Console.ForegroundColor = ConsoleColor.Yellow;
			printPlayer(enemyPos+1, Console.WindowWidth/2, '2');
			Console.Beep(3000, 100);
			if(enemyPos == ancientPosX){
				Console.Clear();
				Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2);
				Console.WriteLine("Player 2 Won");
				Console.SetCursorPosition((Console.WindowWidth/2)+1, (Console.WindowHeight/2)+1);
				scoreEnemy+=100;
				Console.WriteLine("Score: {000000}", scoreEnemy);
				Console.ReadKey();
				Thread.Sleep(2000);
				restart();
			}
				
			Thread.Sleep(100);
		}
		
		static void ancientDisplay(){
			Console.ForegroundColor = ConsoleColor.Green;
			printPlayer(ancientPosX, ancientPosY, 'A');
		}
		
		static void restart(){
			playerPos = 0;
			enemyPos = 0;
			ancientPosX = rand.Next(1, Console.WindowWidth - 3);
			ancientPosY = rand.Next(1, Console.WindowHeight - 3);
		}
		
		static void landingScreen(){
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.SetCursorPosition(Console.WindowWidth/2, Console.WindowHeight/2);
			Console.WriteLine("Destroy of The Ancient");
			Console.SetCursorPosition((Console.WindowWidth/2)-1, (Console.WindowHeight/2)+1);
			Console.ForegroundColor = ConsoleColor.Black;
			Console.WriteLine("~Press Any Key to Start~");
			Console.ReadKey();
			Console.Clear();
			Thread.Sleep(200);
		}
		
		static void howToPlay(){
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(Console.WindowWidth/2, 2);
			Console.WriteLine("~How to Play~");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("\n\tPlayer 1: \n\t[LeftArrow] = Move Left \n\t[RightArrow] = Move Right \n\t[UpArrow] = Shot");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\n\tPlayer 2: \n\t[A] = Move Left \n\t[D] = Move Right \n\t[W] = Shot");
			Console.ReadKey();
			Console.BackgroundColor = ConsoleColor.Black;
		}
		
		static void showScoreAwal(){
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition(Console.WindowWidth/4, 2);
			Console.WriteLine("Score Player 1: {0}", scorePlayer);
			Console.SetCursorPosition(3*Console.WindowWidth/4, 2);
			Console.WriteLine("Score Player 2: {0}", scoreEnemy);
			Thread.Sleep(1000);
		}
		
		private static void Maximize(){
    		Process p = Process.GetCurrentProcess();
    		ShowWindow(p.MainWindowHandle, 3);
		}
	}
}