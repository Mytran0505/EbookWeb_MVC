using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbookMVC.Utility
{
    public static class SD
    {
        public const string Role_Customer = "Customer";
        public const string Role_Company = "Company";
        public const string Role_Admin = "Admin";
        public const string Role_Employee = "Employee";

        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInprocess = "Inprocessing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefuned = "Refuned";

        public const string PaymentStatusPending = "Pending";
        public const string PaymentStatusApproved = "Appoved";
        public const string PaymentStatusDelayedPayment = "Approved For Delayed Payment";
        public const string PaymentStatusRejected = "Rejected";

	}
}
