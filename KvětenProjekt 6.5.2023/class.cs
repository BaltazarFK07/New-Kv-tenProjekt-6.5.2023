using Spectre.Console;
using System;
using System.Collections.Generic;

class KvětenProjekt1
{



    private class Product
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public double Price { get; set; }

    }



    private static class Inventory
    {
        public static List<Product> Products { get; set; } = new List<Product>();

        public static int Count { get; set; }

        public static void AddProduct(string name, double price)
        {
            Products.Add(new Product(name, price));
        }

        public static void RemoveProduct(string name)
        {
            var selectedProduct = Products.Find(p => p.Name == name);
            int index = Products.IndexOf(selectedProduct);

            Products.RemoveAt(index);
        }
    }



    public static void Main()
    {

        while (true)
        {

            Console.Clear();
            var vyber = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Vyber z možností")
                .PageSize(10)
                .AddChoices(new[] {
                        "Přidat","Smazat","Vypiš",
                }
            ));


            switch (vyber)
            {
                case "Přidat":
                    {
                        Console.WriteLine("Zadej název a cenu produktu");
                        string name = Console.ReadLine();
                        double price = 0;
                        
                        try {
                            price = Double.Parse(Console.ReadLine() ?? "0");

                            if(price < 0) throw new Exception("Pod nulu jít nemůžeš");

                        } catch(Exception e) {
                            Console.Clear();
                            Console.WriteLine($"Někde si napsal nějakou chybu: {e.Message}");
                            Console.ReadKey();
                            break;
                        }

                        Inventory.AddProduct(name, price);
                        Console.Clear();
                    }
                    break;

                case "Vypiš":
                    {
                        foreach (var Item in Inventory.Products) Console.WriteLine($"{Item.Name} = {Item.Price} Kč");
                        Console.ReadKey();
                    }
                    break;

                case "Smazat":
                    {
                        var pole = new List<string>();
                        foreach (var p in Inventory.Products) pole.Add(p.Name);


                        var name = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Vyber, co chceš smazat.")
                            .PageSize(10)
                            .AddChoices(pole)
                        );

                        Inventory.RemoveProduct(name);
                        Console.Clear();
                    }
                    break;


            } // konec switche
        }
    }
}
