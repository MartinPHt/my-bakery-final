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

    public class ResponseOK : Response
    {
        public ResponseOK() :base(true)
        {
        }
    }

    public class ResponseBad : Response
    {
        public ResponseBad() :base(false)
        {
        }
    }
    #endregion

    #region Customers Request
    public class CustomerResponse
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public double AccountBalance { get; set; }

        public bool DeluxeAccount { get; set; }

        public DateTime RegisteredOn { get; set; }
    }
    #endregion
}
