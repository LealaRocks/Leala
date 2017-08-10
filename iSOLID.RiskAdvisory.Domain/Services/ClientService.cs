using iSOLID.RiskAdvisory.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using iSOLID.RiskAdvisory.Domain.Model;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public class ClientService  : IClientService
    {
        private readonly IClientRepository clients;

        public ClientService(IClientRepository clients)
        {
            this.clients = clients;
        }

        public void AddClient(Client client)
        {
            client.Id = Guid.NewGuid();
            this.clients.Add(client);
        }

        public void DeleteClient(Guid clientId)
        {
            var client = this.clients[clientId];

            if(client != null)
            {
                this.clients.Delete(client);
            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            return this.clients.All();
        }

        public Client GetClient(Guid clientId)
        {
            return this.clients[clientId];
        }

        public void Update(Client client)
        {
            this.clients.Update(client);
        }
    }
}
