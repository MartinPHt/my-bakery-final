using Common.Entities;
using System;

namespace Common.CommConstants
{
    #region Customers Request
    public class CreateCustomerRequest : DataRequest
    {
        public CreateCustomerRequest(string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn) 
            : base(Endpoints.CreateCustomerEndPoint)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public double AccountBalance { get; }
        public bool DeluxeAccount { get; }
        public DateTime RegisteredOn { get; }
    }

    public class GetCustomerRequest : DataRequest
    {
        public GetCustomerRequest(int id)
            : base(Endpoints.GetCustomerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetAllCustomerRequest : NoDataRequest
    {
        public GetAllCustomerRequest()
            : base(Endpoints.GetAllCustomerEndPoint)
        { }
    }

    public class UpdateCustomerRequest : DataRequest
    {
        public UpdateCustomerRequest(int id ,string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn)
            : base(Endpoints.UpdateCustomerEndPoint)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            AccountBalance = accountBalance;
            DeluxeAccount = deluxeAccount;
            RegisteredOn = registeredOn;
        }
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Address { get; }
        public double AccountBalance { get; }
        public bool DeluxeAccount { get; }
        public DateTime RegisteredOn { get; }
    }

    public class DeleteCustomerRequest : DataRequest
    {
        public DeleteCustomerRequest(int id)
            : base(Endpoints.DeleteCustomerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class SearchCustomersByFirstNameRequest : DataRequest
    {
        public SearchCustomersByFirstNameRequest(string firstName)
            : base(Endpoints.SearchCustomersByFirstNameEndPoint)
        {
            FirstName = firstName;
        }
        public string FirstName { get; }
    }
    #endregion
}
