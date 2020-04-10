using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SharedLibrary {
    public interface IValidateable {

    }

    static class ValidateableExtension {

        public static IEnumerable<string> Validate( this IValidateable obj ) {

            List<string> errors = new List<string>();

            IEnumerable<PropertyInfo> properties = obj.GetType().GetProperties();
            foreach ( PropertyInfo property in properties ) {

                DisplayAttribute nameAttribute = property.GetCustomAttribute<DisplayAttribute>();
                string propName = ( nameAttribute == null ) ? property.Name : nameAttribute.Name;

                object value = property.GetValue( obj );

                IEnumerable<ValidationAttribute> attributes = property.GetCustomAttributes<ValidationAttribute>(true);
                foreach ( ValidationAttribute attribute in attributes ) {

                    if ( !attribute.IsValid( value ) ) {

                        errors.Add( attribute.FormatErrorMessage( propName ) );
                    }
                }
            }

            return errors;
        }
    }
}
