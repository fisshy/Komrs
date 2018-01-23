using Komrs.Product.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Komrs.Product.Application.Requests
{
    public class ListAllSuppliersRequest : IRequest<IEnumerable<SupplierModel>>
    {
    }
}
