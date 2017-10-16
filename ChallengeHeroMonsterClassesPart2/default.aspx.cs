using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart2
{


    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Dice AttackDie = new Dice();
            Dice DefendDie = new Dice();
            AttackDie.Sides = 20;
            DefendDie.Sides = 10;

            Character Hero = new Character();
            Character Monster = new Character();
            Hero.Name = "Billy Joe McCallister";
            Hero.Health = 45;
            Hero.DamageMaximum = 10;
            Hero.AttackBonus = false;

            Monster.Name = "Grendel";
            Monster.Health = 36;
            Monster.DamageMaximum = 10;
            Monster.AttackBonus = false;


            //FIGHT!
            //First the hero -- I don't have to save the attack values to make this work, but I might need them later

            int HeroAttack;
            int MonsterAttack;


            while(Hero.Health>=0 && Monster.Health >= 0)
            {
                MonsterAttack = Monster.Attack(AttackDie);
                Hero.Defend(MonsterAttack, Monster.AttackBonus, DefendDie);
                HeroAttack = Hero.Attack(AttackDie);
                Monster.Defend(HeroAttack, Hero.AttackBonus, DefendDie);
            }
            displayResult(Hero, Monster);
        }

        private void displayResult(Character opponent1, Character opponent2)
        {
            if(opponent1.Health < 0 && opponent2.Health < 0)
            {
                lblResult.Text = String.Format("{0} and {1} have both died fighting each other. It was a glorious battle.", opponent1.Name, opponent2.Name);
            }
            else if (opponent1.Health < 0)
            {
                lblResult.Text = String.Format("{0} has prevailed over {1} in armed combat to the death!", opponent2.Name, opponent1.Name);
            }
            else
            {
                lblResult.Text = String.Format("{0} has prevailed over {1} in armed combat to the death!", opponent1.Name, opponent2.Name);
            }
        }

    }

    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }



        public int Attack(Dice Attack)
        {   //Only allow the maximum amount (up to 2000) for a given hero (anything less, too, of course)
            return Math.Min(Attack.Roll(), DamageMaximum);
        }

        public void Defend(int damage, bool bonus, Dice defenseDie)
        {
            //to make it more realistic, I'm going to detect a random amount from the incoming damage
            int defenseValue = defenseDie.Roll();
            //to me, a bonus attack would be a straight additive term to the damage, but I didn't architect this.
            int bonusAttack = 1;
            if (bonus) bonusAttack = 2;
            //conceivably, the defense value could be greater than damage, resulting in IMPROVED health. This takes care of that scenario.
            this.Health -= Math.Max(damage*bonusAttack - defenseValue,0); 

        }


    }
    class Dice
    {
        Random roll = new Random();
        public int Sides { get; set; }
        public int Roll()
        {
            return roll.Next(1, this.Sides);
        }
    }

}