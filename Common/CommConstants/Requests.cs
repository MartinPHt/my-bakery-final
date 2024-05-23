﻿namespace Common.CommConstants
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
            : base(Endpoints.GetAllCustomersEndPoint)
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

    #region Baker Request
    public class CreateBakerRequest : DataRequest
    {
        public CreateBakerRequest(string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
            : base(Endpoints.CreateBakerEndPoint)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Salary = salary;
            IsFullTime = isFullTime;
            RegisteredOn = registeredOn;
        }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public double Salary { get; }
        public bool IsFullTime { get; }
        public DateTime RegisteredOn { get; }
    }

    public class GetBakerRequest : DataRequest
    {
        public GetBakerRequest(int id)
            : base(Endpoints.GetBakerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetAllBakerRequest : NoDataRequest
    {
        public GetAllBakerRequest()
            : base(Endpoints.GetAllBakersEndPoint)
        { }
    }

    public class UpdateBakerRequest : DataRequest
    {
        public UpdateBakerRequest(int id, string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
            : base(Endpoints.UpdateBakerEndPoint)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            Salary = salary;
            IsFullTime = isFullTime;
            RegisteredOn = registeredOn;
        }
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public double Salary { get; }
        public bool IsFullTime { get; }
        public DateTime RegisteredOn { get; }
    }

    public class DeleteBakerRequest : DataRequest
    {
        public DeleteBakerRequest(int id)
            : base(Endpoints.DeleteBakerEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class SearchBakersByFirstNameRequest : DataRequest
    {
        public SearchBakersByFirstNameRequest(string firstName)
            : base(Endpoints.SearchBakersByFirstNameEndPoint)
        {
            FirstName = firstName;
        }
        public string FirstName { get; }
    }
    #endregion

    #region Order Request
    public class CreateOrderRequest : DataRequest
    {
        public CreateOrderRequest(string details, int quantity, double tip, double total, bool isExpress, DateTime placedOn, int customer_ID, int baker_ID)
            : base(Endpoints.CreateOrderEndPoint)
        {
            Details = details;
            Quantity = quantity;
            Tip = tip;
            Total = total;
            IsExpress = isExpress;
            PlacedOn = placedOn;
            Customer_ID = customer_ID;
            Baker_ID = baker_ID;
        }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public double Tip { get; set; }
        public double Total { get; set; }
        public bool IsExpress { get; set; }
        public DateTime PlacedOn { get; set; }
        public int Customer_ID { get; set; }
        public int Baker_ID { get; set; }
    }

    public class GetOrderRequest : DataRequest
    {
        public GetOrderRequest(int id)
            : base(Endpoints.GetOrderEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class GetAllOrdersRequest : NoDataRequest
    {
        public GetAllOrdersRequest()
            : base(Endpoints.GetAllOrdersEndPoint)
        { }
    }

    public class UpdateOrderRequest : DataRequest
    {
        public UpdateOrderRequest(int id, string details, int quantity, double tip, double total, bool isExpress, DateTime placedOn, int customer_ID, int baker_ID)
            : base(Endpoints.UpdateOrderEndPoint)
        {
            Id = id;
            Details = details;
            Quantity = quantity;
            Tip = tip;
            Total = total;
            IsExpress = isExpress;
            PlacedOn = placedOn;
            Customer_ID = customer_ID;
            Baker_ID = baker_ID;
        }

        public int Id { get; set; }
        public string Details { get; set; }
        public int Quantity { get; set; }
        public double Tip { get; set; }
        public double Total { get; set; }
        public bool IsExpress { get; set; }
        public DateTime PlacedOn { get; set; }
        public int Customer_ID { get; set; }
        public int Baker_ID { get; set; }
    }

    public class DeleteOrderRequest : DataRequest
    {
        public DeleteOrderRequest(int id)
            : base(Endpoints.DeleteOrderEndPoint)
        {
            Id = id;
        }
        public int Id { get; }
    }

    public class SearchOrdersByDetailsRequest : DataRequest
    {
        public SearchOrdersByDetailsRequest(string details)
            : base(Endpoints.SearchOrdersByDetailsEndPoint)
        {
            Details = details;
        }
        public string Details { get; }
    }
    #endregion
}
