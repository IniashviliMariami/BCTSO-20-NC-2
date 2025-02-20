﻿using MiniBank.Models;

namespace MiniBank.Repository
{
    public class CustomerCsvRepository
    {
        private readonly string _filePath;
        private List<Customer> _customers;

        public CustomerCsvRepository(string filePath)
        {
            _filePath = filePath;
            _customers = LoadData();
        }

        public List<Customer> GetCustomers() => _customers;
        public Customer GetCustomer(int id) => _customers.FirstOrDefault(c => c.Id == id);


        public void Create(Customer customer)
        {
            customer.Id = _customers.Any() ? _customers.Max(customer => customer.Id) + 1 : 1;
            _customers.Add(customer);
            SaveData();
        }

        public void Update(Customer customer)
        {
            var index = _customers.FindIndex(c => c.Id == customer.Id);
            if (index >= 0)
            {
                _customers[index] = customer;
                SaveData();
            }
        }

        public void Delete(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer != null)
            {
                _customers.Remove(customer);
                SaveData();
            }
        }

        private void SaveData()
        {
            var lines = new List<string>() { "Id,Name,IdentityNumber,PhoneNumber,Email,Type" };

            lines.AddRange(_customers.Select(customer => $"{customer.Id},{customer.Name},{customer.IdentityNumber},{customer.PhoneNumber},{customer.Email},{customer.Type}"));

            File.WriteAllLines(_filePath, lines);
        }

        private List<Customer> LoadData()
        {
            if (!File.Exists(_filePath))
                return new List<Customer>();

            return File
                .ReadAllLines(_filePath)
                .Skip(1)
                .Select(line => line.Split(','))
                .Select(parts => new Customer()
                {
                    Id = int.Parse(parts[0]),
                    Name = parts[1],
                    IdentityNumber = parts[2],
                    PhoneNumber = parts[3],
                    Email = parts[4],
                    Type = Enum.Parse<Models.Type>(parts[5])
                })
                .ToList();
        }
    }
}
