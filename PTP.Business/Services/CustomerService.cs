using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTP.Business.Abstractions;
using PTP.DataAccess.Repositories;
using PTP.EntityLayer.Models;

namespace PTP.Business.Services
{
    public class CustomerService : IService<Customer>
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void Add(Customer entity)
        {
            _customerRepository.Add(entity);
        }

        public void Delete(int id)
        {
            _customerRepository.Delete(id);
        }

        public IEnumerable<Customer>? GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetByID(int id)
        {
            return _customerRepository.GetById(id);
        }

        public void Update(Customer entity)
        {
            _customerRepository.Update(entity);
        }
    }
}
