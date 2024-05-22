namespace Common.CommConstants
{
    public static class Endpoints
    {
        #region Customer endpoints
        public const string CreateCustomerEndPoint = "createcustomer";
        public const string GetCustomerEndPoint = "getcustomer";
        public const string GetAllCustomersEndPoint = "getallcustomers";
        public const string UpdateCustomerEndPoint = "updatecustomer";
        public const string DeleteCustomerEndPoint = "deletecustomer";
        public const string SearchCustomersByFirstNameEndPoint = "searchcustomer";
        #endregion

        #region Baker endpoints
        public const string CreateBakerEndPoint = "createbaker";
        public const string GetBakerEndPoint = "getbaker";
        public const string GetAllBakersEndPoint = "getallbakers";
        public const string UpdateBakerEndPoint = "updatebaker";
        public const string DeleteBakerEndPoint = "deletebaker";
        public const string SearchBakersByFirstNameEndPoint = "searchbaker";
        #endregion

        #region Order endpoints
        public const string CreateOrderEndPoint = "createorder";
        public const string GetOrderEndPoint = "getorder";
        public const string GetAllOrdersEndPoint = "getallorders";
        public const string UpdateOrderEndPoint = "updateorder";
        public const string DeleteOrderEndPoint = "deleteorder";
        public const string SearchOrdersByFirstNameEndPoint = "searchorder";
        #endregion
    }
}
