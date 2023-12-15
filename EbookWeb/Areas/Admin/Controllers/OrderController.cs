using EbookMVC.DataAccess.Repository.IRepository;
using EbookMVC.Models;
using EbookMVC.Models.ViewModels;
using EbookMVC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EbookMVCWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(int orderId)
        {
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties:"Product")

            };
            return View(OrderVM);
        }
        [HttpPost]
        [Authorize(Roles =SD.Role_Admin+","+SD.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = OrderVM.OrderHeader.City;
            orderHeaderFromDb.State = OrderVM.OrderHeader.State;
            orderHeaderFromDb.PostalCode = OrderVM.OrderHeader.PostalCode;
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFromDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFromDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }
            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();
            TempData["Success"] = "Order Details Updated Successfully";
            return RedirectToAction(nameof(Details), new {orderId=orderHeaderFromDb.Id});
        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeader = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            switch (status)
            {
                case "pending":
                    objOrderHeader = objOrderHeader.Where(u => u.PaymentStatus == SD.PaymentStatusPending);
                    break;
                case "completed":
                    objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    objOrderHeader = objOrderHeader.Where(u => u.OrderStatus == SD.StatusApproved);
                    break;
                case "inprocess":
                    objOrderHeader = objOrderHeader.Where(u => u.PaymentStatus == SD.StatusInprocess);
                    break;
                default:
                    break;
            }



            return Json(new { data = objOrderHeader });
        }
        #endregion
    }

}
