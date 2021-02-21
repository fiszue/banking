using System.Collections.Generic;
using Newtonsoft.Json;

namespace Banking.Backend
{
    public class BankingManager
    {
        public List<Client> clients;
        public BankingManager()
        {
            string json = System.IO.File.ReadAllText(@"data.json");
            clients = JsonConvert.DeserializeObject<List<Client>>(json);
        }
        /// <summary>
        /// Szuka użytkownika w liście używając jego imienia i nazwiska
        /// </summary>
        /// <param name="firstname">Imię</param>
        /// <param name="surename">Nazwisko</param>
        /// <returns></returns>
        public Client GetUserByNames(string firstname, string surename)
        {
            return clients.Find(client => client.firstname == firstname && client.surename == surename);
        }

        /// <summary>
        /// Szuka użytkownika w liście używając jego nr klienta
        /// </summary>
        /// <param name="clientId">Nr Klienta</param>
        /// <returns></returns>
        public Client GetUserByClientId(long clientId)
        {
            return clients.Find(client => client.clientId == clientId);
        }
    }
}
