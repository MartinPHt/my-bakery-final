namespace Common.CommConstants
{
    #region Base Requests
    public abstract class DataRequest : IRequest
    {
        public bool ContainsData
        {
            get { return true; }
        }
    }

    public abstract class NoDataRequest : IRequest
    {
        public bool ContainsData
        {
            get { return false; }
        }
    }
    #endregion

    #region Customers Requests
    public class CreateCustomerRequest : DataRequest
    {
        public CreateCustomerRequest(string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn) 
            : base()
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

    public class UpdateCustomerRequest : DataRequest
    {
        public UpdateCustomerRequest(int id ,string firstName, string lastName, string address, double accountBalance, bool deluxeAccount, DateTime registeredOn)
            : base()
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
    #endregion

    #region Baker Requests
    public class CreateBakerRequest : DataRequest
    {
        public CreateBakerRequest(string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
            : base()
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

    public class UpdateBakerRequest : DataRequest
    {
        public UpdateBakerRequest(int id, string firstName, string lastName, string emailAddress, double salary, bool isFullTime, DateTime registeredOn)
            : base()
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
    #endregion

    #region Order Requests
    public class CreateOrderRequest : DataRequest
    {
        public CreateOrderRequest(string details, int quantity, double tip, double total, bool isExpress, DateTime placedOn, int customer_ID, int baker_ID)
            : base()
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

    public class UpdateOrderRequest : DataRequest
    {
        public UpdateOrderRequest(int id, string details, int quantity, double tip, double total, bool isExpress, DateTime placedOn, int customer_ID, int baker_ID)
            : base()
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
    #endregion
}
