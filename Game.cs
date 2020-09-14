using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HelloWorld
{
    struct Player
    {
        public string playerName;
        public int playerHealth;
        public int playerDamage;
        public int playerDefense;
    }

    struct weapon
    {
        public string weaponName;
        public int weaponDamage;
        
    }
   
    
    class Game
    {
        bool _gameOver = false;
        int _playerHealth = 120;
        int _playerDamage = 20;
        int _playerDefense = 10;
        int levelScaleMax = 5;
        Player player1;
        Player player2;
        weapon chopsticks;
        weapon scythe;
        
        


        
    
        //Run the game
        public void Run()
        {
            Start();

            while(_gameOver == false)
            {
                Update();
            }
            End();

        }


        
        
        //This function handles the battles for our ladder. roomNum is used to update the our opponent to be the enemy in the current room. 
        //turnCount is used to keep track of how many turns it took the player to beat the enemy
        bool StartBattle(int roomNum, ref int turnCount)
        {
            //initialize default enemy stats
            int player2playerHealth = 80;
            int enemyAttack = 15;
            int enemyDefense = 0;
            string enemyName = "";
            //Changes the enemy's default stats based on our current room number. 
            //This is how we make it seem as if the player is fighting different enemies
           

            //Loops until the player or the enemy is dead
            while(player1.playerHealth >= 0 && player2.playerHealth >= 0)
            {
                //Displays the stats for both charactersa to the screen before the player takes their turn
                PrintStats(player1.playerName, player1.playerHealth, player1.playerDamage, player1.playerDefense);
                PrintStatsP2(player2.playerName, player2.playerHealth, player2.playerDamage, player2.playerDefense);

                //Get input from the player
                char input = ' ';
                GetInput(out input, "Attack", "Defend", "Player 1's turn");
                //If input is 1, the player wants to attack. By default the enemy blocks any incoming attack
                if(input == '1')
                {

                    Console.Clear();
                    BlockAttack(ref player2.playerHealth, ref player1.playerDamage, ref player2.playerDefense);
                    Console.WriteLine(player1.playerName + " dealt "  + chopsticks.weaponDamage  +  (player1.playerDamage - player2.playerDefense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                }
                //If the player decides to defend the enemy just takes their turn. However this time the block attack function is
                //called instead of simply decrementing the health by the enemy's attack value.
                else
                {
                    BlockAttack(ref player1.playerHealth, ref player2.playerDamage, ref player1.playerDefense);
                    Console.WriteLine(player2.playerName + " dealt " + chopsticks.weaponDamage  +  (player2.playerDamage - player1.playerDefense) + " damage.");
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    Console.Clear();
                }
                Console.Clear();
                //After the player attacks, the enemy takes its turn. Since the player decided not to defend, the block attack function is not called.
                GetInput(out input, "Attack", "Defend", "Player 2's turn");
                if (input == '1')
                {
                    BlockAttack(ref player1.playerHealth, ref player2.playerDamage, ref player1.playerDefense);
                    Console.WriteLine(player2.playerName + " dealt " + chopsticks.weaponDamage  +  (player2.playerDamage - player1.playerDefense) + "damage");
                    Console.Write("> ");
                    Console.ReadKey();
                }
                else
                {
                    BlockAttack(ref player2.playerHealth, ref player1.playerDamage, ref player2.playerDefense);
                    Console.WriteLine(player1.playerName + " dealt " + chopsticks.weaponDamage  +  (player1.playerDamage - player2.playerDefense) + "damage");
                    Console.Write("> ");
                    Console.ReadKey();
                    turnCount++;
                    Console.Clear();
                }
                
                
            }
            //Return whether or not our player died
            return _playerHealth != 0;

        }
        //Decrements the health of a character. The attack value is subtracted by that character's defense
        void BlockAttack(ref int opponentHealth, ref int attackVal, ref int opponentDefense)
        {
            int damage = attackVal - opponentDefense;
            if(damage < 0)
            {
                damage = 0;
            }
            opponentHealth -= damage;
        }
        //Scales up the player's stats based on the amount of turns it took in the last battle
        void LevelUp(int turnCount)
        {
            //Subtract the amount of turns from our maximum level scale to get our current level scale
            int scale = levelScaleMax - turnCount;
            if(scale <= 0)
            {
                scale = 1;
                
            }
            Console.WriteLine("You defeated the enemy!!");
            Console.ReadKey();
            Console.Clear();
      
        }
        //Gets input from the player
        //Out's the char variable given. This variables stores the player's input choice.
        //The parameters option1 and option 2 displays the players current chpices to the screen
        void GetInput(out char input, string option1, string option2, string query)
        {
            Console.WriteLine(query);
            //Initialize input
            input = ' ';
            //Loop until the player enters a valid input
            while(input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
                
        }
        void GetInput(out char input, string option1, string option2)
        {

            input = ' ';
            while(input != '1' && input != '2')
            {
                Console.WriteLine("1." + option1);
                Console.WriteLine("2." + option2);
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
            }
        }



        //Prints the stats given in the parameter list to the console
        //This is used for Player 1.
        void PrintStats(string player1playerName, int player1playerHealth, int player1playerDamage, int player1playerDefense )
        {
            Console.WriteLine(player1.playerName);
            Console.WriteLine("Health: " + player1.playerHealth);
            Console.WriteLine("Damage: " + player1.playerDamage);
            Console.WriteLine("Defense: " + player1.playerDefense);
        }
        void PrintStatsP2(string player2playerName, int player2playerHealth, int player2playerDamage, int player2playerDefense )
        {
            Console.WriteLine(player2.playerName);
            Console.WriteLine("Health: " + player2.playerHealth);
            Console.WriteLine("Damage: " + player2.playerDamage);
            Console.WriteLine("Defense: " + player2.playerDefense);
        }

        //This is used to progress through our game. A recursive function meant to switch the rooms and start the battles inside them.
        void ClimbLadder(int roomNum)
        {
            //Displays context based on which room the player is in
            switch (roomNum)
            {
                case 0:
                    {
                        Console.WriteLine("Round 1");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                default:
                    {
                        _gameOver = true;
                        return;
                    }
            }
            int turnCount = 0;
            //Starts a battle. If the player survived the battle, level them up and then proceed to the next room.
            if(StartBattle(roomNum, ref turnCount))
            {
                LevelUp(turnCount);
                ClimbLadder(roomNum + 1);
            }
            _gameOver = true;

        }

        //Displays the character selection menu for player 1
        void SelectPlayer1Character()
        {
            char input = ' ';
            //Loops until a valid option is choosen
            while(input != '1' && input != '2' && input != '3')
            {
                //Prints options
                Console.WriteLine("Welcome Player 1! Please select a character.");
                Console.WriteLine("1.Sora");
                Console.WriteLine("2.Link");
                Console.WriteLine("3.Noctis");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                //Sets player 1's default stats based on which character was picked
                switch (input)
                {
                    case '1':
                        {
                            player1.playerName = "Sora";
                            player1.playerHealth= 200;
                            player1.playerDefense = 10;
                            player1.playerDamage = 45;
                            break;
                        }
                    case '2':
                        {
                            player1.playerName = "Link";
                            player1.playerHealth = 200;
                            player1.playerDefense = 20;
                            player1.playerDamage = 70;
                            break;
                        }
                    case '3':
                        {
                            player1.playerName = "Noctis";
                            player1.playerHealth = 250;
                            player1.playerDefense = 25;
                            player1.playerDamage = 50;
                            break;
                        }
                        //If an invalid input is selected display and input message and input over again.
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;                          
                        }
                }
                Console.Clear();
            }
            //Prints the stats of player 1's character to the screen before the game begins to give the player visual feedback
            PrintStats(player1.playerName ,player1.playerHealth, player1.playerDamage, player1.playerDefense);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }

        void SelectItemP1()
        {
            char input = ' ';
            while (input != '1' && input != '2')
            {
                Console.WriteLine(player1.playerName + ",please pick a weapon ");
                Console.WriteLine("1.Chopsticks");
                Console.WriteLine("2.Scythe");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        {
                            chopsticks.weaponName = "Chopsticks";
                            chopsticks.weaponDamage = 20;
                            break;
                        }
                    case '2':
                        {
                            scythe.weaponName = "scythe";
                            scythe.weaponDamage = 20;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }

        void SelectItemP2()
        {
            char input = ' ';
            while (input != '1' && input != '2')
            {
                Console.WriteLine(player2.playerName + ",please pick a weapon ");
                Console.WriteLine("1.Chopsticks");
                Console.WriteLine("2.Scythe");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        {
                            chopsticks.weaponName = "Chopsticks";
                            chopsticks.weaponDamage = 20;
                            break;
                        }
                    case '2':
                        {
                            scythe.weaponName = "scythe";
                            scythe.weaponDamage = 25;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue.");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }
                }
            }
        }

        void SelectPlayer2Character()
        {
            char input = ' ';
            while (input != '1' && input != '2' && input != '3')
            {
                Console.WriteLine("Welcome Player 2! Please select a character.");
                Console.WriteLine("1.Sora");
                Console.WriteLine("2.Link");
                Console.WriteLine("3.Noctis");
                Console.Write("> ");
                input = Console.ReadKey().KeyChar;
                switch (input)
                {
                    case '1':
                        {
                            player2.playerName = "Sora";
                            player2.playerHealth = 200;
                            player2.playerDefense = 10;
                            player2.playerDamage = 45;
                            break;
                        }
                    case '2':
                        {
                            player2.playerName = "Link";
                            player2.playerHealth = 200;
                            player2.playerDefense = 20;
                            player2.playerDamage = 70;
                            break;
                        }
                    case '3':
                        {
                            player2.playerName = "Noctis";
                            player2.playerHealth = 250;
                            player2.playerDefense = 25;
                            player2.playerDamage = 50;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Press any key to continue");
                            Console.Write("> ");
                            Console.ReadKey();
                            break;
                        }

                }
                Console.Clear();
            }
            PrintStatsP2(player2.playerName, player2.playerHealth, player2.playerDamage, player2.playerDefense);
            Console.WriteLine("Press any key to continue.");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
        }
        
        //Performed once when the game begins
        public void Start()
        {
            SelectPlayer1Character();
            SelectItemP1();
            SelectPlayer2Character();
            SelectItemP2();
            

        }

        //Repeated until the game ends
        public void Update()
        {
            
            ClimbLadder(0);
            
            

        }

        //Performed once when the game ends
        public void End()
        {
            //If the player died print death message
            if(player2.playerHealth <= 0)
            {
                Console.WriteLine("Player 1 wins!");
                return;
            }
            else if(player1.playerHealth <= 0)
            {
                Console.WriteLine("Player 2 wins!");
                return;
            }
            //Print game over message
            Console.WriteLine("Congrats");
        }
    }
}
