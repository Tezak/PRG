using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        Human Human1 = new Human(24, 187, 77);
        Human1.Name = "David";
        Human1.EyeColor = Color.Peru;
        Human1.HairColor = Color.Brown;
        Human1.PrintCharakteristics();
        Human Human2 = new Human();
        Human2.Age = 20;
        Human2.Height = 174;
        Human2.Weight = 60;
        Human2.PrintCharakteristics();
        
        Console.WriteLine(Human1.BMI() + Human1.get);
        Console.ReadLine();
        

    }

    class Human
    {
        public string Name;
        public Color EyeColor;
        public int Age;
        public int Height;
        public int Weight;
        public Color HairColor;

        public float BMI()
        { 
            return (float)(Weight * 10000.0 / (Height * Height));
        }
        public Human()
        {
        }

        public Human(int incomingAge, int Height, int Weight) {
            Age = incomingAge;
            this.Height = Height;
            this.Weight = Weight;
        }

        public Human(int Age, int Height, int Weight, Color EyeColor, string Name, Color HairColor)
        {
            this.Age = Age;
            this.Height = Height;
            this.Weight = Weight;
            this.EyeColor = EyeColor;
            this.Name = Name;
            this.HairColor = HairColor;
        }
        public void PrintCharakteristics()
        {
            Console.WriteLine($"znám nového člověka, jmenuje se {Name} je mu {Age} let");
        }

    }
}