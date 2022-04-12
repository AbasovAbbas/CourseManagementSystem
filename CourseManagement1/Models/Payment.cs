using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagement.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int TotalFee { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        [BindProperty]
        public DateTime Date { get; set; }
        [DisplayName("Student")]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual ApplicationUser Student { get; set; }

    }
    public enum PaymentMethod
    {
        Cash,
        Card
    }
    public enum PaymentStatus
    {
        Due,
        Paid
    }
}
