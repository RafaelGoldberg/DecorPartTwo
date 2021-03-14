using System;
using System.Collections.Generic;
using System.Text;

namespace Tzedakah_Models.ViewModels
{
    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
