using System;
using System.ComponentModel.DataAnnotations;

namespace Common.CommConstants
{
    public interface IRequest
    {
        bool ContainsData { get; }
    }
}
