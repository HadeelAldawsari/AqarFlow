
using AqarFlow.Application.Services.Base;
using AqarFlow.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using WebFollowUp = AqarFlow.Models.FollowUp;
using WebCustomer = AqarFlow.Models.Customer;
using DomainFollowUp = AqarFlow.Domain.Models.FollowUp;

namespace AqarFlow.Controllers
{
    [Authorize]
    public class FollowUpsController : Controller
    {
        private readonly IFollowUpService _followUpService;
        private readonly ICustomerService _customerService;

        public FollowUpsController(
            IFollowUpService followUpService,
            ICustomerService customerService)
        {
            _followUpService = followUpService;
            _customerService = customerService;
        }

        // =====================================
        // INDEX
        // =====================================
        public IActionResult Index()
        {
            var customers = _customerService.GetAll()
                .ToDictionary(c => c.Id, c => c.Name);

            var followUps = _followUpService.GetAll()
                .Select(f => new FollowUpDto
                {
                    Id = f.Id,

                    CustomerName = customers.TryGetValue(
                        f.CustomerId,
                        out var customerName)
                        ? customerName
                        : "Unknown",

                    ContactDate = f.ContactDate,
                    ContactMethod = f.ContactMethod,
                    Result = f.Result,
                    NextFollowUpDate = f.NextFollowUpDate,
                    Status = f.Status
                })
                .ToList();

            return View(followUps);
        }

        // =====================================
        // CREATE - GET
        // =====================================
        [HttpGet]
        public IActionResult Create()
        {
            LoadCustomers();

            return View();
        }

        // =====================================
        // CREATE - POST
        // =====================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WebFollowUp followUp)
        {
            if (!ModelState.IsValid)
            {
                LoadCustomers(followUp.CustomerId);

                return View(followUp);
            }

            var domainFollowUp = ToDomainFollowUp(followUp);

            domainFollowUp.CreatedAt = DateTime.Now;

            _followUpService.Add(domainFollowUp);

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // EDIT - GET
        // =====================================
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = _followUpService.GetById(id.Value);

            if (followUp == null)
            {
                return NotFound();
            }

            LoadCustomers(followUp.CustomerId);

            return View(ToWebFollowUp(followUp));
        }

        // =====================================
        // EDIT - POST
        // =====================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, WebFollowUp followUp)
        {
            if (id != followUp.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                LoadCustomers(followUp.CustomerId);

                return View(followUp);
            }

            var existingFollowUp = _followUpService.GetById(id);

            if (existingFollowUp == null)
            {
                return NotFound();
            }

            existingFollowUp.CustomerId = followUp.CustomerId;
            existingFollowUp.ContactDate = followUp.ContactDate;
            existingFollowUp.ContactMethod = followUp.ContactMethod;
            existingFollowUp.Result = followUp.Result;
            existingFollowUp.NextFollowUpDate = followUp.NextFollowUpDate;
            existingFollowUp.Status = followUp.Status;
            existingFollowUp.Notes = followUp.Notes;

            _followUpService.Update(existingFollowUp);

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // DELETE - GET
        // =====================================
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp = _followUpService.GetById(id.Value);

            if (followUp == null)
            {
                return NotFound();
            }

            return View(ToWebFollowUp(followUp));
        }

        // =====================================
        // DELETE - POST
        // =====================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var followUp = _followUpService.GetById(id);

            if (followUp != null)
            {
                _followUpService.Delete(followUp);
            }

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // LOAD CUSTOMERS
        // =====================================
        private void LoadCustomers(int? selectedCustomerId = null)
        {
            var customers = _customerService.GetAll()
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Customers = new SelectList(
                customers,
                "Id",
                "Name",
                selectedCustomerId
            );
        }

        // =====================================
        // DOMAIN TO MVC MODEL
        // =====================================
        private WebFollowUp ToWebFollowUp(
            DomainFollowUp followUp)
        {
            var customer = _customerService.GetCustomerById(
                followUp.CustomerId
            );

            return new WebFollowUp
            {
                Id = followUp.Id,
                CustomerId = followUp.CustomerId,
                ContactDate = followUp.ContactDate,
                ContactMethod = followUp.ContactMethod,
                Result = followUp.Result,
                NextFollowUpDate = followUp.NextFollowUpDate,
                Status = followUp.Status,
                Notes = followUp.Notes,
                CreatedAt = followUp.CreatedAt,

                Customer = customer == null
                    ? null
                    : new WebCustomer
                    {
                        Id = customer.Id,
                        Name = customer.Name
                    }
            };
        }

        // =====================================
        // MVC MODEL TO DOMAIN
        // =====================================
        private static DomainFollowUp ToDomainFollowUp(
            WebFollowUp followUp)
        {
            return new DomainFollowUp
            {
                Id = followUp.Id,
                CustomerId = followUp.CustomerId,
                ContactDate = followUp.ContactDate,
                ContactMethod = followUp.ContactMethod,
                Result = followUp.Result,
                NextFollowUpDate = followUp.NextFollowUpDate,
                Status = followUp.Status,
                Notes = followUp.Notes,
                CreatedAt = followUp.CreatedAt
            };
        }
    }
}
