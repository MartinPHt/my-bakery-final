using System;

namespace Common.CommConstants
{
    #region Base Response
    public interface IResponse
    {
        bool Successfull { get; }
    }

    public abstract class Response : IResponse
    {
        public Response(bool isSuccessfull)
        {
            Successfull = isSuccessfull;
        }
        public bool Successfull { get; }
    }
    #endregion

    #region Customers Request
    public class CustomerResponse
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public double AccountBalance { get; set; }

        public bool DeluxeAccount { get; set; }

        public DateTime RegisteredOn { get; set; }
    }

    public class SearchCustomersResponse
    {
        public List<CustomerResponse> Response { get; set; }
    }
    #endregion
}
