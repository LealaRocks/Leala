using iSOLID.RiskAdvisory.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iSOLID.RiskAdvisory.Domain.Services
{
    public interface IClientService
    {
        void AddClient(Client client);

        void DeleteClient(Guid clientId);

        void Update(Client clientId);

        Client GetClient(Guid clientId);

        IEnumerable<Client> GetAllClients();
    }
}
