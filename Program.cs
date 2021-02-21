using Banking.Backend;
using System;

namespace Banking
{
    class Program
    {
        static bool working = true;
        static BankingManager bankingManager;
        static Client currentClient;
        static Boolean Login()
        {
            Helpers.Header();
            Console.Write("Podaj numer klienta lub wpisz q by wyjść: ");
            string line = Console.ReadLine();
            if (line.ToLower().StartsWith("q"))
            {
                return true;
            }

            long clientNumber;
            if (!long.TryParse(line, out clientNumber))
            {
                Console.WriteLine("Nieprawidłowa zawartość pola");
                return false;
            }

            currentClient = bankingManager.GetUserByClientId(clientNumber);
            if (currentClient == null)
            {
                Console.WriteLine("Nie znaleziono klienta o podanym numerze");
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            Console.Title = "Banking v1.0";
            bankingManager = new BankingManager();
            currentClient = null;
            while (working)
            {
                Helpers.Header();
                // Jeśli niezalogowany to pojawia się menu dla niezalogowanych
                if (currentClient == null)
                {
                    Console.WriteLine("a - By się zalogować");
                    Console.WriteLine("b - By wyświetlić listę");
                    Console.WriteLine("q - By wyjść");
                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.A:
                            while (true)
                            {
                                bool result = Login();
                                if (result)
                                {
                                    break;
                                }
                                Helpers.Pause();
                            }
                            break;

                        // Lista użytowników
                        case ConsoleKey.B:
                            Console.Clear();
                            Console.WriteLine("Lista klientów:");
                            foreach (Client client in bankingManager.clients)
                            {
                                Console.WriteLine($"{client.firstname} {client.surename} - {client.accountNumber} - {client.amount} PLN");
                            }
                            Helpers.Pause();
                            break;

                        // Wyjście
                        case ConsoleKey.Q:
                            working = false;
                            break;
                    }
                }
                else
                {
                    //Jan Kowalski
                    //Nr Klienta: 25
                    //Nr konta: 12 3232 3232 3232 3232 3232 3232
                    //Saldo: 1340 PLN
                    //Adres: Bukowa 12 / 1
                    //Telefon: 123456789

                    Console.WriteLine($"Witaj {currentClient.firstname} {currentClient.surename}");
                    Console.WriteLine($"Nr klienta: {currentClient.clientId}");
                    Console.WriteLine($"Nr konta:   {currentClient.accountNumber}");
                    Console.WriteLine($"Saldo:      {currentClient.FormattedAmount}");
                    Console.WriteLine($"Adres:      {currentClient.address.FormattedAddress}");
                    Console.WriteLine($"Telefon:    {currentClient.phone}");
                    Helpers.Separator();
                    Console.WriteLine("p - By przelać");
                    Console.WriteLine("w - By się wylogować");
                    Console.WriteLine("q - By wyjść");
                    ConsoleKeyInfo key = Console.ReadKey();
                    switch (key.Key)
                    {
                        // Q - Wyjście
                        case ConsoleKey.Q:
                            working = false;
                            break;

                        // W - Wylogowanie
                        case ConsoleKey.W:
                            currentClient = null;
                            Console.Clear();
                            Console.WriteLine("Pomyślnie wylogowano");
                            Helpers.Pause();
                            break;

                        // P - Przelew
                        case ConsoleKey.P:
                            Client client = null;
                            // Szukamy odpowiedniego klienta
                            while(true)
                            {
                                Helpers.Header();
                                Console.WriteLine("Wpisz imię i nazwisko osoby do której chcesz przelać pieniądze");
                                Console.WriteLine("Lub napisz Q by wyjść");
                                string line = Console.ReadLine();
                                if (line.ToLower() == "q")
                                {
                                    break;
                                }

                                string[] names = line.Split(' ');
                                if(names.Length != 2)
                                {
                                    Console.WriteLine("Podany tekst musi się składać z imienia i nazwiska");
                                    Helpers.Pause();
                                    continue;
                                }
                                client = bankingManager.GetUserByNames(names[0], names[1]);
                                if(client == null)
                                {
                                    Console.WriteLine("Nie znaleziono klienta o takich danych");
                                    Helpers.Pause();
                                    continue;
                                }

                                if(client == currentClient)
                                {
                                    Console.WriteLine("Nie można przelać samemu sobie");
                                    Helpers.Pause();
                                    continue;
                                }
                                break;
                            }
                            if(client != null)
                            {
                                while(true)
                                {
                                    Helpers.Header();
                                    Console.WriteLine($"Przelew do {client.firstname} {client.surename} (ID: {client.clientId})");
                                    Helpers.Separator();
                                    Console.WriteLine("Podaj kwotę przelewu:");
                                    Console.WriteLine("Lub napisz Q by wyjść");
                                    string line = Console.ReadLine();
                                    if (line.ToLower().StartsWith("q"))
                                    {
                                        break;
                                    }

                                    float amount;
                                    if (!float.TryParse(line, out amount))
                                    {
                                        Console.WriteLine("Nieprawidłowa kwota");
                                        Helpers.Pause();
                                        continue;
                                    }
                                    if (amount <= 0)
                                    {
                                        Console.WriteLine("Nieprawidłowa kwota");
                                        Helpers.Pause();
                                        continue;
                                    }
                                    if (amount > currentClient.amount)
                                    {
                                        Console.WriteLine("Nie masz tyle na koncie");
                                        Helpers.Pause();
                                        continue;
                                    }
                                    currentClient.amount -= amount;
                                    client.amount += amount;
                                    Helpers.Header();
                                    Console.WriteLine($"Przelew do {client.firstname} {client.surename} (ID: {client.clientId}) na kwotę {amount.ToString("0.00")} wykonano pomyślnie");
                                    Console.WriteLine($"Aktualny stan konta wynosi {currentClient.FormattedAmount}");
                                    Helpers.Pause();
                                    break;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
