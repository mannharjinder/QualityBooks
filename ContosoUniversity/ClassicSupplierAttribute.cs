using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ContosoUniversity.Models;
using System.Reflection;

namespace ContosoUniversity
{
    public class ClassicSupplierAttribute: ValidationAttribute
    {
        private string[] PropertyList { get; set; }

        public ClassicSupplierAttribute(params string[] propertyList)
        {
            this.PropertyList = propertyList;
        }

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var supplier = (Supplier)validationContext.ObjectInstance;

            PropertyInfo propertyInfo;
            foreach (string propertyName in PropertyList)
            {
                propertyInfo = value.GetType().GetProperty(propertyName);

                if (propertyInfo != null && propertyInfo.GetValue(value, null) != null)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }            
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"For Testing";
        }
    }
}
