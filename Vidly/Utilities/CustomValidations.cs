using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Utilities
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;

            if (customer.MembershipTypeId == MembershipTypes.NoMembership.GetHashCode() || customer.MembershipTypeId == MembershipTypes.PayAsYouGo.GetHashCode())
                return ValidationResult.Success;

            if (customer.DateOfBirth == null)
                return new ValidationResult("Date of birth is required");

            if ((DateTime.Now.Year - customer.DateOfBirth.Value.Year) < 18)
                return new ValidationResult("Customer should be minimum 18 years old if you go with a membership");


            return ValidationResult.Success;
        }
    }
}