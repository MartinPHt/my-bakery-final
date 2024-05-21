using System;

namespace Common.CommConstants
{
    public interface IRequest
    {
        string EndpointName { get; }
        bool HasData { get; }
    }

    public abstract class DataRequest : IRequest
    {
        public abstract string EndpointName { get; }

        public bool HasData 
        { 
            get { return true; } 
        }
    }

    public abstract class NoDataRequest : IRequest
    {
        public abstract string EndpointName { get; }

        public bool HasData
        {
            get { return false; }
        }
    }
}
