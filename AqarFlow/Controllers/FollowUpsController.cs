using AqarFlow.Models;
using AqarFlow.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AqarFlow.Controllers
{
    // Only logged-in users can access Follow-ups
    [Authorize]
    public class FollowUpsController : Controller
    {
        private readonly IFollowUpRepository _followUpRepository;
        private readonly ICustomerRepository _customerRepository;

        public FollowUpsController(
            IFollowUpRepository followUpRepository,
            ICustomerRepository customerRepository)
        {
            _followUpRepository = followUpRepository;
            _customerRepository = customerRepository;
        }


        // =========================
        // INDEX
        // Get only the data needed for the follow-ups list
        // =========================
        public IActionResult Index()
        {
            var followUps = _followUpRepository.GetAll();

            return View(followUps);
        }


        // =========================
        // CREATE - GET
        // Display create follow-up form
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            LoadCustomers();

            return View();
        }


        // =========================
        // CREATE - POST
        // Save new follow-up
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FollowUp followUp)
        {
            if (ModelState.IsValid)
            {
                followUp.CreatedAt = DateTime.Now;

                _followUpRepository.Add(followUp);
                _followUpRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(followUp.CustomerId);

            return View(followUp);
        }


        // =========================
        // EDIT - GET
        // Display edit follow-up form
        // =========================
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp =
                _followUpRepository.GetById(id.Value);

            if (followUp == null)
            {
                return NotFound();
            }

            LoadCustomers(followUp.CustomerId);

            return View(followUp);
        }


        // =========================
        // EDIT - POST
        // Update follow-up
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FollowUp followUp)
        {
            if (id != followUp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingFollowUp =
                    _followUpRepository.GetById(id);

                if (existingFollowUp == null)
                {
                    return NotFound();
                }

                existingFollowUp.CustomerId =
                    followUp.CustomerId;

                existingFollowUp.ContactDate =
                    followUp.ContactDate;

                existingFollowUp.ContactMethod =
                    followUp.ContactMethod;

                existingFollowUp.Result =
                    followUp.Result;

                existingFollowUp.NextFollowUpDate =
                    followUp.NextFollowUpDate;

                existingFollowUp.Status =
                    followUp.Status;

                existingFollowUp.Notes =
                    followUp.Notes;

                _followUpRepository.Update(existingFollowUp);
                _followUpRepository.Save();

                return RedirectToAction(nameof(Index));
            }

            LoadCustomers(followUp.CustomerId);

            return View(followUp);
        }


        // =========================
        // DELETE - GET
        // Display delete confirmation
        // =========================
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var followUp =
                _followUpRepository.GetById(id.Value);

            if (followUp == null)
            {
                return NotFound();
            }

            return View(followUp);
        }


        // =========================
        // DELETE - POST
        // Delete follow-up
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var followUp =
                _followUpRepository.GetById(id);

            if (followUp != null)
            {
                _followUpRepository.Delete(followUp);
                _followUpRepository.Save();
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // LOAD CUSTOMERS
        // Prepare customer dropdown list
        // =========================
        private void LoadCustomers(
            int? selectedCustomerId = null)
        {
            var customers = _customerRepository
                .GetAll()
                .OrderBy(c => c.Name)
                .ToList();

            ViewBag.Customers = new SelectList(
                customers,
                "Id",
                "Name",
                selectedCustomerId
            );
        }
    }
}